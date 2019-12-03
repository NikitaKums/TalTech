def middle(s):
    """
    Return the middle part of the string.

    When the string has odd number of elements, the middle part is one letter.
    When the string has even number of elements, the middle part is two letters.
    In both cases, there will be equal number of letters to the left and to the right of the "middle" part.
    In case the string is empty, return empty string.
    :param s: string
    :return: string
    """
    length = len(s)
    if length % 2 == 0:
        return s[int(length / 2) - 1:int(length / 2) + 1]
    else:
        return s[int((length / 2) - 0.5)]

def bigger_sum(numbers):
    """
    Find the first index where sum of elements up to that index (itself not included) is bigger than the element.

    If no such element can be found, return -1.

    :param numbers: list of non-negative ints
    :return: int
    """
    for i in range(len(numbers)-1):
        if sum(numbers[0:i+1]) > numbers[i+1]:
            return i+1
    return -1

def same_ends(s):
    """
    Return True if the first three letters in the string
    are the same as three last letters reversed.
    The first 3 and last 3 cannot overlap.

    :param s: string
    :return: bool
    """
    if len(s) < 6:
        return False
    first_3_letters = s[0:3]
    last_3_letters = s[-3:]
    last_3_reversed = last_3_letters[::-1]
    if first_3_letters == last_3_reversed:
        return True
    return False


def swap_ends(numbers):
    """
    Return list where elements with indices of first element value and last element value are swapped.
    If this cannot be done (both indices must be >= 0), then return original list.

    :param numbers: list of ints
    :return: list of ints
    """
    numbers2 = numbers[:]
    first_element_index = numbers2[0]
    last_element_index = numbers2[-1]
    if first_element_index < 0 or last_element_index < 0 or first_element_index > (len(numbers2)-1) or last_element_index > (len(numbers2)-1):
        return numbers
    numbers.insert(first_element_index, numbers2[last_element_index])
    numbers.pop(first_element_index + 1)
    numbers.pop(last_element_index)
    numbers.insert(last_element_index, numbers2[first_element_index])
    return numbers

if __name__ == '__main__':
    print(swap_ends([0, 1, 2, 0, 0]))  # [2, 1, 0]
    print(swap_ends([1, 2, 1]))  # [1, 2, 1]
    print(swap_ends([-1, 2, 3, 1]))  # [-1, 2, 3, 1]
    print(swap_ends([1, 2, 3, -1]))  # [1, 2, 3, -1]
    print(swap_ends([1, 2, 3, 7]))  # [1, 2, 3, 7]
    print(swap_ends([4, 1, 2, 3, 0]))  # => [0, 1, 2, 3, 4]
    #print(middle("tere"))  # => er
    #print(middle("help"))  # => el
    #print(middle("hi"))    # => hi
    #print(middle("hey"))   # => e
    #print(middle(""))      # => ""

    print(bigger_sum([1, 2, 3, 4]))  # => 3
    print(bigger_sum([1, 2, 3]))     # => -1
    print(bigger_sum([2, 1]))       # => 1
    print(bigger_sum([1, 1]))        # => -1
    print(bigger_sum([]))            # => -1

    print(same_ends("kirik"))    # => False
    print(same_ends("kaakaak"))  # => True
    print(same_ends("tereke"))   # => False
