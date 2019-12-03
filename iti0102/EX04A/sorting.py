"""Sort the strings in ascending order."""


def get_min_len_word(string_list):
    """
    Function to find and return the minimum length word in string_list.

    If two Strings are the same length, the String appearing first must also
    be returned first.

    :param string_list: List of Strings to look through.
    :return: Smallest length String from string_list.
    """
    if not string_list:
        return
    else:
        string_list.sort(key=len)  # key - function that serves as a key for the sort comparison
        return string_list[0]  # len - get length of element in list


def sort_list(string_list):
    """
    Function to sort string_list by the length of it's elements.

    This function must utilize get_min_len_word().

    :param string_list: List of Strings to be sorted.
    :return: Sorted list of Strings.
    """
    if not string_list:
        return []
    else:
        sorted_list = []
        i = 0
        while i != len(string_list):
            a = get_min_len_word(string_list)
            sorted_list.insert(i, a)
            string_list.remove(a)
        sorted_list.reverse()
        return sorted_list
