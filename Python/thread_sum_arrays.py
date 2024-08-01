import threading


def partial_sum(arr1, arr2, result, start, end):
    for i in range(start, end):
        result[i] = arr1[i] + arr2[i]


def sum(arr1, arr2, n):
    size = len(arr1)
    work_size = size // n
    result = [0] * size
    threads = []
    for i in range(n):
        # 1. load balancing
        start = i * work_size
        end = (i + 1) * work_size if i != n - 1 else size

        # 2. crear threads
        t = threading.Thread(target=partial_sum, args=(arr1, arr2, result, start, end))
        threads.append(t)

        # 3. iniciar threads
        t.start()

    # 4. sincronizar threads
    for t in threads:
        t.join()

    return result


def main():
    arr1 = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
    arr2 = [1, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 12]
    n = 4  # cantidad de threads

    result = sum(arr1, arr2, n)
    print(f"Resultado de la suma: {result}")


if __name__ == "__main__":
    main()