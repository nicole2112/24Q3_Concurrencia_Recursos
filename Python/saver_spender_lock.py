from threading import Thread, Lock


def saver(global_money, mutex: Lock):
    for i in range(1000000):
        mutex.acquire()
        global_money += 50
        mutex.release()
    print("Thread que ahorra ha acabado.")


def spender(global_money, mutex):
    for i in range(1000000):
        mutex.acquire()
        global_money -= 50
        mutex.release()
    print("Thread que gasta ha acabado.")


def main():
    global_money = 100
    mutex = Lock()

    threads = []
    threads.append(Thread(target=saver, args=([global_money, mutex])))
    threads.append(Thread(target=spender, args=([global_money, mutex])))

    for t in threads:
        t.start()

    for t in threads:
        t.join()

    print("Dinero final: ", global_money)


if __name__ == "__main__":
    main()
