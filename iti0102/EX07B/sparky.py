"""Simulation."""
def length(some_list: list):
    return int(len(some_list)/2)


def simulate(wmap: list, moves: list): # -> list
    """
    Simulate a robotic lawn mower.

    :param wmap: A list of strings indicating rows that make up the map.
                 The map is always rectangular and the minimum given size is 1x1.
                 Cut grass is indicated by the symbol ('-'), low grass by ('w') and high grass by ('W').
                 The robot position is indicated by the symbol ('X'). There is always one robot on the map.
                 Obstacles are indicated by the symbol ('#').

    :param moves: A list of moves.
                  The moves are abbreviated N - north, E - east, S - south, W - west.
                  Ignore moves that would put the robot out of bounds or crash it into an obstacle.

    :return: A list of strings indicating rows that make up the map. Same format as the given wmap.

    Grass under Sparky's starting position is always cut grass ('-').
    If Sparky mows high grass, it first turns into low grass ('w') and then from low grass into cut grass ('-').
    """
    # maybe work idk try to make E S W aswell then check
    #  return "".join(str1[:int(len(str1)/2)]) + "\n" + "".join(str1[int(len(str1)/2):])
    str1 = list("".join(wmap))
    x_location = str1.index("X")
    str1[x_location] = "-"

    for i in range(len(moves)):
        if moves[i] == "N":
            x_location = x_location - length(str1)
            if str1[x_location] == "#" and x_location > 0:
                x_location = x_location + length(str1)
                continue
            elif str1[x_location] == "W":
                str1[x_location] = "w"
            else:
                str1[x_location] = "-"

        if moves[i] == "E":
            prev = x_location
            x_location = x_location + 1
            if prev == 4 and x_location == 5 or x_location > length(str1) or str1[x_location] == "#":
                x_location = prev
                continue
            elif str1[x_location] == "W":
                str1[x_location] = "w"
            else:
                str1[x_location] = "-"

        if moves[i] == "S":
            prev = x_location - length(str1)

            x_location = x_location + length(str1)
            if x_location > length(str1):
                x_location = prev
                continue
            if str1[x_location] == "#":
                x_location = prev
                continue
            elif str1[x_location] == "W":
                str1[x_location] = "w"
            else:
                str1[x_location] = "-"

        if moves[i] == "W":
            prev = x_location
            x_location = x_location - 1
            if prev == length(str1) and x_location == (length(str1) - 1) or prev == 0 and x_location == -1 or str1[x_location] == "#":
                x_location = prev
                continue
            elif str1[x_location] == "W":
                str1[x_location] = "w"
            else:
                str1[x_location] = "-"

        #if i == len(moves):
         #   str1[x_location] = "X"
    if len(wmap) == 3:
        str1.insert(-4, "\n")
    return "".join(str1[:int(len(str1)/len(wmap))]) + "\n" + "".join(str1[int(len(str1)/len(wmap)):])
    #  return x_location
#wmap1 = [
        #"#www-",
        #"wXw#-",
    #]

#moves1 = ["N", "E", "E", "S", "E"]
#print((simulate(wmap1, moves1)))
    # #---X
    # w-w#-
wmap2 = [
        "WWWW",
        "-wwW",
        "X-#W",
    ]

moves2 = ["N", "N", "E", "E", "S", "W", "W", "S", "E", "E"]
print((simulate(wmap2, moves2)))

    # wwwW
    # ---W
    # -X#W
