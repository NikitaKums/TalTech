"""Print halloween."""


a = 0


def first(m):
    """
    Return certain number.

    :param m: Base number to work with
    :return:
    """
    global a
    a += 1
    if a == 10:
        return 11
    else:
        return a


def last(n):
    """
    Return certain number.

    :param n: Number to work with
    :return:
    """
    return (n - 1) / 2
