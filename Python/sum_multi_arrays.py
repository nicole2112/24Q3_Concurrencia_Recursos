import sys
import multiprocessing
import time
from multiprocessing.context import Process


def sum_multiply_array(start_idx, end_idx, arrays_str, sum_results_shared, product_results_shared):
    for i in range(start_idx, end_idx):
        num_array = list(map(int, arrays_str[i].split(',')))
        sum_results_shared[i] = sum(num_array)
        product = 1
        for num in num_array:
            product *= num
        product_results_shared[i] = product


def main_process(arrays_str, num_process):
    n = len(arrays_str)
    process_load = n // num_process
    processes = []

    sum_results_shared = multiprocessing.Array('i', n, lock=False)
    product_results_shared = multiprocessing.Array('L', n, lock=False)

    for i in range(num_process):
        start_idx = i * process_load
        end_idx = (i + 1) * process_load if i != num_process - 1 else n
        p = multiprocessing.Process(target=sum_multiply_array, args=(start_idx, end_idx, arrays_str,
                                                                     sum_results_shared, product_results_shared))
        processes.append(p)
        p.start()

    for p in processes:
        p.join()

    return sum_results_shared, product_results_shared


def main(file_path, num_process):
    multiprocessing.set_start_method('spawn')

    with open(file_path, 'r') as f:
        arrays_str = f.read().splitlines()
        start_time = time.time()

        sum_results, products_results = main_process(arrays_str, num_process)

        end_time = time.time()

        # for i in range(len(arrays_str)):
        #     print(f"Array {i}: {arrays_str[i]}")
        #     print(f"Suma: {sum_results[i]}")
        #     print(f"Producto: {products_results[i]}")
        #     print("------------------------------")

        print("Tiempo: ", end_time - start_time)


if __name__ == "__main__":
    file_path = sys.argv[1]
    num_process = int(sys.argv[2])
    main(file_path, num_process)
