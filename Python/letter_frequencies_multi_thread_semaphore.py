import json
from urllib.request import urlopen
import time
from threading import Thread, Lock, Semaphore

global_count = 0
letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"


def count_letters(url, global_frequencies, mutex: Lock, frequency_semaphores: dict[str, Semaphore]):
    print(f"Contando letras de: {url}")
    local_count = 0
    local_frequencies = {l: 0 for l in letters}

    with urlopen(url) as response:
        body = response.read()
        text = str(body)
        for l in text:
            letter = l.upper()
            if letter in local_frequencies:
                local_frequencies[letter] += 1
                local_count += 1

    for letter, frequency in local_frequencies.items():
        frequency_semaphores[letter].acquire()
        global_frequencies[letter] += frequency
        frequency_semaphores[letter].release()

    global global_count
    with mutex:
        global_count += local_count


def main():
    global_frequencies = {}
    for c in letters:
        global_frequencies[c] = 0

    mutex = Lock()
    frequency_semaphores = {}
    for c in letters:
        frequency_semaphores[c] = Semaphore(1)

    start = time.time()
    threads = []
    for i in range(1000, 1020):
        t = Thread(target=count_letters, args=(f"https://www.rfc-editor.org/rfc/rfc{i}.txt", global_frequencies, mutex,
                                               frequency_semaphores))
        threads.append(t)
        t.start()

    for t in threads:
        t.join()

    end = time.time()

    print(json.dumps(global_frequencies, indent=4))
    print(f"Frecuencia global: {global_count}")
    print("Tiempo de ejecucion:", end - start)


if __name__ == "__main__":
    main()
