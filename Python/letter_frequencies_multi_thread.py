import json
from urllib.request import urlopen
import time
from threading import Thread


def count_letters(url, frequencies):
    print(f"Contando letras de: {url}")
    with urlopen(url) as response:
        body = response.read()
        text = str(body)
        for l in text:
            letter = l.upper()
            if letter in frequencies:
                frequencies[letter] += 1


def main():
    frequencies = {}
    for c in "ABCDEFGHIJKLMNOPQRSTUVWXYZ":
        frequencies[c] = 0

    start = time.time()
    threads = []
    for i in range(1000, 1020):
        t = Thread(target=count_letters, args=(f"https://www.rfc-editor.org/rfc/rfc{i}.txt", frequencies))
        threads.append(t)

    for t in threads:
        t.start()

    for t in threads:
        t.join()

    end = time.time()

    print(json.dumps(frequencies, indent=4))
    print("Tiempo de ejecucion:", end - start)


if __name__ == "__main__":
    main()