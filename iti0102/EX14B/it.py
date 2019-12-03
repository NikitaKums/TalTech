"""Something strange."""
import math


def get_lines(initial_line_length: float) -> tuple:
    """
    Find length of lines by whole length.

    :param initial_line_length: Length of whole line
    :return: Length of smaller and longer lines
    """
    a = initial_line_length / 2
    longer_part = round(-a + math.sqrt(math.pow(a, 2) + math.pow(initial_line_length, 2)))
    return initial_line_length - longer_part, longer_part


def finder(row, col):
    """
    Find the value of table spot by row and column.

    :param row: Row number
    :param col: Columan number
    :return: Value at the spot
    """
    if col == 1:
        return int((row * (row + 1)) / 2)
    result = (row * (row + 1) / 2) + row
    for i in range(1, col - 1):
        result += row + i
    return int(result)


def clocky(hour, minutes):
    """
    Find the angle between clock pointers.

    :param hour: Hour
    :param minutes: Minute
    :return: Angle
    """
    if hour > 12 or hour < 1 or minutes < 0 or minutes > 59:
        return -1
    if 6 <= hour <= 11:
        return abs(float(((5 * hour - minutes) * 6)))
    if hour == 12:
        return abs(float(360 - ((60 - minutes) * 6)))
    if 1 <= hour <= 5:
        return abs(float(360 - (((60 + 5 * hour) - minutes) * 6)))
