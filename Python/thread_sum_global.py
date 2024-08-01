import threading


def partial_sum(partial_arr, results, core):
    results[core] = sum(partial_arr)


def global_sum(arr, cores):
    size = len(arr)
    work_size = size // cores
    results = [0] * cores
    threads = []
    for i in range(cores):
        # 1. balance de carga
        start = i * work_size
        end = (i + 1) * work_size if i != cores - 1 else size
        # creacion de threads
        t = threading.Thread(target=partial_sum, args=(arr[start:end], results, i))
        threads.append(t)
        # iniciar
        t.start()
    # sync
    for t in threads:
        t.join()
    return sum(results)


def main():
    arr = [1,4,3,9,2,8,5,1,1,6,2,7,2,5,0,4,1,8,6,5,1,2,3,9]
    cores = 8

    res_sum = global_sum(arr, cores)
    print(f"Resultado de la suma: {res_sum}")


if __name__ == "__main__":
    main()