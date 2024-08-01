from threading import Thread, Lock, Semaphore
from queue import Queue


BUFFER_SIZE = 10
buffer = Queue(BUFFER_SIZE)
mutex = Lock()
empty = Semaphore(BUFFER_SIZE)
full = Semaphore(0)


def producer():
    print("producer")


def consumer():
    print("consumer")


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
