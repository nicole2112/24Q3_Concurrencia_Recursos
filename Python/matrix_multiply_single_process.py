import time
import random

matrix_size = 500
# matrix_a = [[3, 1, -4],
#             [2, -3, 1],
#             [5, -2, 0]]
# matrix_b = [[1, -2, -1],
#             [0, 5, 4],
#             [-1, -2, 3]]
matrix_a = [[0] * matrix_size for r in range(matrix_size)]  # matriz nxn con 0s
matrix_b = [[0] * matrix_size for r in range(matrix_size)]  # matriz nxn con 0s
result = [[0] * matrix_size for r in range(matrix_size)]  # matriz nxn con 0s


def generate_random_matrix(matrix):
    for row in range(matrix_size):
        for col in range(matrix_size):
            matrix[row][col] = random.randint(-5, 5)


def matrix_multiplication():
    for row in range(matrix_size):
        for col in range(matrix_size):
            index_sum = 0
            for i in range(matrix_size):
                index_sum += matrix_a[row][i] * matrix_b[i][col]
            result[row][col] = index_sum


def main():
    generate_random_matrix(matrix_a)
    generate_random_matrix(matrix_b)
    start_time = time.time()
    matrix_multiplication()
    for row in range(matrix_size):
        print(result[row])
    end_time = time.time()
    print("Tiempo de ejecucion: ", end_time - start_time)


if __name__ == "__main__":
    main()
