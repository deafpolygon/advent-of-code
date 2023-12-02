import os
print(os.listdir())

f = open("./2023/day02/input.txt")
r, g, b = 12, 13, 14
possible = 0

for line in f:
    line = line.strip()

    sets = line.split(";")
    split = sets[0].split(":")
    id = int(split[0].replace("Game", ""))
    sets[0] = split[1]

    p = True
    for set in sets:
        cubes = set.split(",")
        for cube in cubes:
            cube = cube.strip()
            n, c = cube.split()
            n = int(n)

            if c == "red" and n > r or c == "green" and n > g or c == "blue" and n > b:
                p = False
                print(f"id {id} - set {set} fails")
                break
        
    if p is True:
        print(id, "ID", set)
        possible += id
        
print("POSSIBILITES =", possible)