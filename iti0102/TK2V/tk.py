def max_digit(nr):
    """
    Return the highest digit in the number.

    :param nr: int
    :return: int
    """
    return max(list(int(integer) for integer in str(nr)))

def reverse_two(s):
    """
    Return string where first two letters are put into end in reverse order.
    If there are no 2 letters, return original string.

    String consists of only latin letters.
    :param s: string
    :return: string
    """
    if len(s) < 2:
        return s
    a = s[:2]
    return s[2:] + a[::-1]

def strpad_a(word: str, left: int, length: int) -> str:
    """
    Return "a" padded word where word starts at position left and total length of result is length.

    length is always enough to fit the word. left >= 0.

    :param word: str to be padded
    :param left: start position of word
    :param length: total length of result
    :return: "a" padded word
    """
    new_word = "a" * left + word
    new_word += "a" * (length - len(word) - left)
    return new_word

def extend_list(numbers, pos):
    """
    Extend list by adding one element.

    The pos indicates where the new element should be inserted.
    Pos also indicates which value should be inserted -
    the value of the element in the pos in the original list.
    If the pos is too high, a new 0 element is added to the end.

    extend_list([1, 2, 3], 0) => [1, 1, 2, 3]
    extend_list([1, 2, 3], 3) => [1, 2, 3, 0]
    extend_list([1, 2, 3], 312123123) => [1, 2, 3, 0]
    extend_list([1, 2], 1) => [1, 2, 2]
    :param pos: list of containing integers.
    :return: extended list
    """
    if pos >= len(numbers):
        numbers.append(0)
    else:
        toadd = numbers[pos]
        numbers.insert(pos, toadd)
    return numbers

print(extend_list([1, 2, 3], 0))
