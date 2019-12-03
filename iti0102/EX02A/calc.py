"""Calculate the value of z."""
import math


def value_of_z(ex, x, y) -> float:
    """
    Calculate the value of z with given x and y.

    :param ex: exercise number
    :param x: number
    :param y: number
    :return: float
    """
    if ex == 1:
        return x ** y + y ** x
    elif ex == 2:
        return x / 5.6 - y / 6.5
    elif ex == 3:
        return (x / 5 * (y ** 4) * math.log(5)) / (7 * math.sqrt(x ** 2 + y ** 2) + 1)
    else:
        print("Sellist ülesannet ei ole!")


print(value_of_z)
