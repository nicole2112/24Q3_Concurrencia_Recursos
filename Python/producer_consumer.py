import random
import time
from threading import Thread, Lock, Semaphore
from queue import Queue


BUFFER_SIZE = 10
buffer = Queue(BUFFER_SIZE)
mutex = Lock()  # sincronizacion del buffer
empty = Semaphore(BUFFER_SIZE)  # espacios vacios en el buffer
full = Semaphore(0)     # espacios llenos en el buffer


def producer():
    while True:
        item = random.randint(1, 100)
        empty.acquire()
        with mutex:
            buffer.put(item)
            print(f"Produciendo: ", item)
        full.release()
        time.sleep(random.random())


def consumer():
    while True:
        full.acquire()
        with mutex:
            item = buffer.get()
            print(f"Consumiendo: ", item)
        empty.release()
        time.sleep(random.random())


def main():
    threads = []
    threads.append(Thread(target=producer, args=([])))
    threads.append(Thread(target=consumer, args=([])))

    for t in threads:
        t.start()

    for t in threads:
        t.join()


if __name__ == "__main__":
    main()
