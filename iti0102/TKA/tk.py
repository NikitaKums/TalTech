def count_lower(s):
    """
    count the number of lower ascii letters in the string.

    :param s: string
    :return: int, count of lower ascii letters
    """
    result = 0
    for letter in s:
        if letter.islower():
            result += 1
    return result

def ends_with_pair(s):
    """
    if string ends with pair of symbols, return the index of the start of the pair.
    if not, return the index of the last symbol.
    else, return -1

    :param s: string
    :return: int, index of pair or index of single symbol or -1
    """
    if len(s) == 1:
        return 0
    elif len(s) == 0:
        return -1
    elif s[-2] == s[-1]:
        return len(s) - 2
    elif s[-2] != s[-1]:
        return len(s) - 1
    return -1

def swap_pos(numbers, pos):
    """
    takes the value of element in position pos. this value is swapped
    with the element in position of the value.

    if cannot swap, change nothing.

    :param numbers: list of ints
    :param pos: int, position
    :return: list of ints
    """
    try:
        element_at_pos = numbers[pos]
        element_at_index = numbers[element_at_pos]
        element_at_index_pos = numbers.index(element_at_index)
        numbers.insert(element_at_index_pos, element_at_pos)
        numbers.pop(element_at_index_pos + 1)
        numbers.insert(pos, element_at_index)
        numbers.pop(pos + 1)
        return numbers
    except Exception:
        return numbers

def merge_lists(a, b):
    """
    take first element from a, first element of b, second element of a, second element of b etc.
    if one list ends, then don't continue with the second list.

    :param a: list
    :param b: list
    :return: merged list
    """
    result = []
    for i in range(len(max(a, b))):
        if len(a) <= i:
            return result
        elif len(b) <= i:
            return result
        result.append(a[i])
        result.append(b[i])
    return result

if __name__ == '__main__':
    print(count_lower("tere"))     # => 4
    print(count_lower("tere1"))    # => 4
    print(count_lower("123"))      # => 0
    print(count_lower("AaAa"))     # => 2
    print(count_lower(""))         # => 0

    print("----------------------------")
    print(ends_with_pair("tere"))  # => 3
    print(ends_with_pair("aa"))    # => 0
    print(ends_with_pair("ab"))    # => 1
    print(ends_with_pair("a"))     # => 0
    print(ends_with_pair(""))      # => -1

    print(swap_pos([1, 2, 3], 0))  # => [2, 1, 3]
    print(swap_pos([1, 2, 3], 1))  # => [1, 3, 2]
    print(swap_pos([1, 2, 3], 2))  # => [1, 2, 3]
    print(swap_pos([1, 2, 3], 3))  # => [1, 2, 3]
    print(swap_pos([1, 4, 6, 2, 5], 3)) # => [1, 4, 2, 6, 5]
    print(swap_pos([], 4)) # => []
    print(swap_pos([1, 1, 1], 0)) # => [1, 1, 1]


    print(merge_lists([1, 2, 3], [1, 2, 3])) # => [1, 1, 2, 2, 3, 3]
    print(merge_lists([1], [2, 3])) # => [1, 2]
    print(merge_lists([], [1, 2, 3])) # => []
    print(merge_lists([], [])) # => []
    print(merge_lists([4, 5, 6, 7], [1])) # => [4, 1]