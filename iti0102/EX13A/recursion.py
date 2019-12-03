"""Recursion vs loops."""


def loop_reverse(s: str) -> str:
    """
    Reverse a string using a loop.

    :param s: input string
    :return: reversed input string
    """
    result = ""
    for letter in s:
        result = letter + result
    return result


def recursive_reverse(s: str) -> str:
    """
    Reverse a string using recursion.

    :param s: input string
    :return: reversed input string
    """
    if len(s) == 0:
        return s
    if len(s) == 1:
        return s
    else:
        return s[-1] + recursive_reverse(s[0:-1])


def loop_sum(n: int) -> int:
    """
    Calculate the sum of all numbers up to n (including n) using a loop.

    :param n: the last number to add to the sum
    :return: sum
    """
    result = 0
    for i in range(n + 1):
        result += i
    return result


def recursive_sum(n: int) -> int:
    """
    Calculate the sum of all numbers up to n (including n) using recursion.

    :param n: the last number to add to the sum
    :return: sum
    """
    if n == 0:
        return 0
    else:
        return n + recursive_sum(n - 1)
