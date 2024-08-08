import time
import random
import multiprocessing
from multiprocessing.context import Process


matrix_size = 500
process_number = 10
matrix_a = [[0] * matrix_size for r in range(matrix_size)]  # matriz nxn con 0s
matrix_b = [[0] * matrix_size for r in range(matrix_size)]  # matriz nxn con 0s


def generate_random_matrix(matrix):
    for row in range(matrix_size):
        for col in range(matrix_size):
            matrix[row][col] = random.randint(-5, 5)


def row_multiplication(start_row, end_row, result):
    for row in range(start_row, end_row):
        for col in range(matrix_size):
            index_sum = 0
            for i in range(matrix_size):
                index_sum += matrix_a[row][i] * matrix_b[i][col]
            result[row * matrix_size + col] = index_sum     # [row][col]


def main():
    multiprocessing.set_start_method("spawn")
    result = multiprocessing.Array('i', [0] * (matrix_size * matrix_size), lock=False)

    generate_random_matrix(matrix_a)
    generate_random_matrix(matrix_b)

    # balance de carga
    process_load = matrix_size // process_number
    processes = []

    start_time = time.time()
    for p in range(process_number):
        start_row = p * process_load
        end_row = (p + 1) * process_load if p != process_number - 1 else matrix_size
        process = Process(target=row_multiplication, args=(start_row, end_row, result))
        processes.append(process)
        process.start()

    for p in processes:
        p.join()

    end_time = time.time()
    print("Tiempo de ejecucion: ", end_time - start_time)


if __name__ == "__main__":
    main()
