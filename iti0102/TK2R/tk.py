def second_symbol_of_longest_str(a, b, c):
    """
    Return the second symbol of the longest string.

    If there are several strings with the same length, any one of those is correct.

    If the longest string doesn't have second symbol, return empty string.

    :param a: str
    :param b: str
    :param c: str
    :return: str
    """
    # find longest one
    longest = max(len(a), len(b), len(c))
    try:
        if len(a) == longest:
            return a[1]
        elif len(b) == longest:
            return b[1]
        return c[1]
    except IndexError:
        return ""


def index_or_value(numbers):
    """
    Replace every element in the list with the index when index is bigger than value.

    :param numbers: list of ints
    :return: list
    """
    list1 = []
    for i, value in enumerate(numbers):
        if i > value:
            list1.append(i)
        else:
            list1.append(value)
    return list1

def merge_strings(a, b):
    """
    Return a new string where first symbol is the first symbol of the first string, the second symbol is the first symbol of
    the second string, third symbol is the second symbol of the first string etc.

    If one string is finished, continue only with the other string.

    :param a: str
    :param b: str
    :return: str
    """
    i = 0
    result = ""
    while True:
        if i >= len(a):
            result += b[i:]
            return result
        elif i >= len(b):
            result += a[i:]
            return result
        result += a[i] + b[i]
        i += 1

def max_digit(nr):
    """
    Return the highest digit in the number.

    :param nr: int
    :return: int
    """
    return max(int(integer) for integer in str(nr))

if __name__ == '__main__':
    print(merge_strings("abc", "acd"))