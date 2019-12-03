"""Calculate square root with Newton method."""


def square_root_with_newton_method(number, iterations):
    """
    Calculate the given number's approximate square root.

    :param number: number from which the square root will be calculated.
    :param iterations: number of formula cycles, highest integer not bigger than the value (1.9 => 1).
    :return: approximate square root. In the case of non-positive number or negative iterations, return None.
    """
    # Inital value of g.
    # Cycle based on the iterations number.
    # Formula in the cycle.
    # Return the rounded final result.
    # supposed to find g not x
    g = number / 2
    iterations_new = iterations // 1
    i = 1
    if 0 > iterations or number <= 0:
        return None
    else:
        while iterations_new >= i:
            if i > number:
                i += 1
                break
            else:
                g = (g + (number / g)) / 2
                i += 1
        return round(g, 3)
