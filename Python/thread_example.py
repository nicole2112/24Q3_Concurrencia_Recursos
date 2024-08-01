from threading import Thread
import time


def do_work(id):
    print(f"Inicio de trabajo {id}")
    i = 0
    for _ in range(10000000):
        i += 1
    print(f"Final de trabajo {id}")


def main():
    num_threads = 5
    threads = []
    # iniciar threads
    for i in range(num_threads):
        # crear
        t = Thread(target=do_work, args=([i]))
        threads.append(t)
        # iniciar ejecucion
        t.start()

    # unir threads
    for t in threads:
        t.join()
    print("Fin del programa")


if __name__ == "__main__":
    main()
