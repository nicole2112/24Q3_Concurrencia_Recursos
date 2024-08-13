import re
import sys
import multiprocessing
from multiprocessing.context import Process


POLYGON_REGEX = "\((\d*),(\d*)\)"


def process_polygons(polygons, start_idx, area_array: multiprocessing.Array):
    for idx, polygon in enumerate(polygons):
        area = calculate_area(polygon)
        area_array[start_idx + idx] = area


def calculate_area(polygon):
    vertices = []
    area = 0.0

    # obtener coordenadas del poligono
    for match in re.finditer(POLYGON_REGEX, polygon):
        x = int(match.group(1))
        y = int(match.group(2))
        vertices.append((x,y))

    # shoelace algorithm
    n = len(vertices)
    for i in range(n):
        x1, y1 = vertices[i]
        x2, y2 = vertices[(i + 1) % n]
        area += x1 * y2 - x2 * y1
    area = abs(area) / 2.0
    return area


def main(file_path, num_process):
    multiprocessing.set_start_method('spawn')
    with open(file_path, "r") as file:
        lines = file.read().splitlines()

        n = len(lines)

        # balance de carga
        process_load = n // num_process
        processes = []
        area_array = multiprocessing.Array('d', [0] * n, lock=False)
        for p in range(num_process):
            start_idx = p * process_load
            end_idx = (p + 1) * process_load if p != num_process - 1 else n
            process = Process(target=process_polygons, args=(lines[start_idx:end_idx], start_idx, area_array))
            processes.append(process)
            process.start()

        for p in processes:
            p.join()

        for a in area_array:
            print("Area: ", a)


if __name__ == "__main__":
    file_path = sys.argv[1]
    num_process = int(sys.argv[2])
    main(file_path, num_process)
