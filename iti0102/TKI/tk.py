def count_capitalized_strings(lst):
    """
    Return the count of strings which start with uppercase latin letter.

    :param lst: string
    :return: int
    """
    result = 0
    for word in lst:
        if word[0].isupper():
            result += 1
    return result

def list_of_sums(numbers):
    """
    Replace every element with the sum of all *other* elements.

    :param numbers: list if ints
    :return: list of ints
    """
    numbersv2 = []
    for i in range(len(numbers)):
        numbersv2.insert(i, sum(numbers) - numbers[i])
    return numbersv2


def increment_elements_by_pos(numbers, pos):
    """
    Return numbers where the element at pos
    and the element at the index of value of pos
    are replaced with the sum on those two elements.

    If at least one of the indices is out of the list
    or those both point to the same element, return original list.

    :param numbers: list of ints
    :param pos: int
    :return: list of ints
    """
    try:
        element_at_pos = numbers[pos]
        if pos == element_at_pos or pos < 1:
            return numbers
        element_at_element_at_pos = numbers[element_at_pos]
        sumofelements = element_at_pos + element_at_element_at_pos
        numbers.insert(pos, sumofelements)
        numbers.pop(pos+1)
        numbers.insert(element_at_pos, sumofelements)
        numbers.pop(element_at_pos+1)
        return numbers
    except IndexError:
        return numbers

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


if __name__ == '__main__':

    # pos and value at pos point to the same element, return original
    print(increment_elements_by_pos([0, 1], 1))  # => [0, 1]

    print(increment_elements_by_pos([1, 2, 3, 4], 1))  # => [1, 5, 5, 4]
    print(increment_elements_by_pos([1, 2, 3, 4], 2))  # => [1, 2, 7, 7]
    print(increment_elements_by_pos([1, 2, 6, 4], 1))  # => [1, 8, 8, 4]
    print(increment_elements_by_pos([1, 3, 6, 4], 1))  # => [1, 7, 6, 7]
    print(increment_elements_by_pos([1, 3, 6, 4], 2))  # => [1, 3, 6, 4]
    print(increment_elements_by_pos([-1, 3, 6, 4], 0)) # => [-1, 3, 6, 4]
    print(increment_elements_by_pos([-1, 3, 6, 1], -1)) # => [-1, 3, 6, 1]

    print(reverse_two("tere"))  # => reet
    print(reverse_two("ter"))  # => ret
    print(reverse_two("te"))  # => et
    print(reverse_two("t"))  # => t
    print(reverse_two(""))  # => ""