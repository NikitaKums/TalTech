from TK2R import tk

def test_1():
    assert tk.second_symbol_of_longest_str("a", "", "") == ""
    assert tk.second_symbol_of_longest_str("a", "b", "c") == ""
    assert tk.second_symbol_of_longest_str("ab", "bt", "ca") in ('b', 't', 'a')
    assert tk.index_or_value([1, 2, 3]) == [1, 2, 3]
    assert tk.index_or_value([1, 1, 1]) == [1, 1, 2]
    assert tk.index_or_value([1, 0, 0]) == [1, 1, 2]
    assert tk.merge_strings("a", "b") == "ab"
    assert tk.merge_strings("a", "bcd") == "abcd"
    assert tk.merge_strings("acd", "b") == "abcd"
    assert tk.merge_strings("acd", "") == "acd"
    assert tk.max_digit(1) == 1
    assert tk.max_digit(10) == 1
    assert tk.max_digit(109) == 9
    assert tk.max_digit(0) == 0