import multiprocessing
import time
from multiprocessing.context import Process


def print_array(array):
    while True:
        print(*array, sep=", ")
        # print(1, 2, 3, sep=", ")
        time.sleep(1)


def main():
    multiprocessing.set_start_method("spawn")
    arr = multiprocessing.Array('i', [-1] * 10, lock=True)
    p = Process(target=print_array, args=([arr]))
    p.start()

    for c in range(10):
        time.sleep(2)
        for i in range(10):
            arr[i] = c

    p.join()


if __name__ == "__main__":
    main()
