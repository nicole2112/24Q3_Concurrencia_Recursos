import os
import sys
from os.path import isdir, join
from threading import Lock, Thread

# directorio: C:\src
# archivo: .gitignore

mutex = Lock()
global_matches = []


def file_search(current_dir, filename):
    print("Searching in:", current_dir)
    child_threads = []
    for file in os.listdir(current_dir):
        full_path = join(current_dir, file)
        if filename in file:     # caso 1: si es el archivo que buscamos, agregarlo
            mutex.acquire()
            global_matches.append(full_path)
            mutex.release()
        if isdir(full_path):    # caso 2: si es un directorio, seguir la busqueda
            t = Thread(target=file_search, args=([full_path, filename]))
            t.start()
            child_threads.append(t)
    for t in child_threads:     # 3: regresar ejecucion al hilo padre cuando termine
        t.join()


def main(root_dir, filename):
    t = Thread(target=file_search, args=([root_dir, filename]))
    t.start()
    t.join()
    print("-----------------------------")
    for m in global_matches:
        print("Matched:", m)
    print("Total matches: ", len(global_matches))


if __name__ == '__main__':
    if len(sys.argv) != 3:
        print("Uso correcto: python file_search.py <directorio raiz> <archivo a buscar>")
    else:
        main(sys.argv[1], sys.argv[2])
