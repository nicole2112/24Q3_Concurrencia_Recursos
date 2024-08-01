from multiprocessing import Process
import multiprocessing
import time


def do_work(id):
    print(f"Inicio de trabajo {id}")
    i = 0
    for _ in range(10000000):
        i += 1
    print(f"Final de trabajo {id}")


def main():
    num_proc = 5
    processes = []
    multiprocessing.set_start_method("spawn")
    # iniciar processes
    for i in range(num_proc):
        # crear
        t = Process(target=do_work, args=(i,))
        processes.append(t)
        # iniciar ejecucion
        t.start()

    # unir processes
    for t in processes:
        t.join()
    print("Fin del programa")


if __name__ == "__main__":
    main()
