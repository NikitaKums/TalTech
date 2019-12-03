def bad_index(input_string):
    """
    Return the index of the first occurrence of "bad" in the string.

    If the index is higher than 3, return 4.
    If the word "bad" does not exist in the string, return -1.
    :param input_string: input string (always string).
    :return: index of "bad". -1, if "bad" doesn't exist. 4 if index is > 3.
    """
    try:
        index1 = input_string.index("bad")
        if index1 > 3:
            return 4
        return index1
    except ValueError:
        return -1

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
        to_add = numbers[pos]
        numbers.insert(pos, to_add)
    return numbers


def shift_left(numbers, shift):
    """
    Shift elements in array to the left by shift elements.

    len(numbers) > 0
    0 <= shift <= len(numbers)

    Examples:
    shift_left([1, 2, 3], 0) => [1, 2, 3]
    shift_left([1, 2, 3], 1) => [3, 1, 2]
    shift_left([1, 2, 3], 2) => [2, 3, 1]
    shift_left([1, 2, 3], 3) => [1, 2, 3]

    :param numbers: list of numbers, len > 0
    :param shift: how many elements to shift; in range [0, len(numbers)]
    :return: shifted list of numbers
    """
    if shift == 0:
        return numbers
    else:
        a = numbers[-shift:]
        for letter in a:
            numbers.remove(letter)
        return a + numbers


def sum_digits(text):
    """
    Return the sum of digits 0-9 appearing in string.

    All the other characters should be ignored.

    Examples:
    sum_digits("123") => 6
    sum_digits("1") => 1
    sum_digits("hi") => 0
    sum_digits("hello123hello123") => 12

    :param text: input string
    :return: sum of digits in input string
    """
    somelist = []
    for letter in text:
        try:
            somelist.append(int(letter))
        except ValueError:
            continue
    return sum(somelist)

if __name__ == '__main__':

    print(bad_index("bad"))          # 0
    print(bad_index("vbad"))         # 1
    print(bad_index("vebad"))        # 2
    print(bad_index("verbad"))       # 3
    print(bad_index("verybad"))      # 4
    print(bad_index("veryverybad"))  # 4
    print(bad_index("verygood"))     # -1

    print(extend_list([1, 2, 3], 0))  # [1, 1, 2, 3]
    print(extend_list([1, 2, 3], 1))  # [1, 2, 2, 3]
    print(extend_list([1, 2, 3], 2))  # [1, 2, 3, 3]
    print(extend_list([1, 2, 3], 3))  # [1, 2, 3, 0]
    print(extend_list([1, 2, 3], 9))  # [1, 2, 3, 0]
    print("--------------------------------")
    print(shift_left([1, 2, 3], 0))  # [1, 2, 3]
    print(shift_left([1, 2, 3], 1))  # [3, 1, 2]
    print(shift_left([1, 2, 3], 2))  # [2, 3, 1]
    print(shift_left([1, 2, 3], 3))  # [1, 2, 3]

    print(sum_digits("tere1"))    # 1
    print(sum_digits("tere12"))   # 3
    print(sum_digits(""))         # 0
    print(sum_digits("0"))        # 0
    print(sum_digits("0123123"))  # 12