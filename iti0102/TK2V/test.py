from TK2V import tk

def test_1():


    assert tk.max_digit(1) == 1
    assert tk.max_digit(10) == 1
    assert tk.max_digit(109) == 9
    assert tk.max_digit(0) == 0
    assert tk.reverse_two("tere") == "reet"
    assert tk.reverse_two("ter") == "ret"
    assert tk.reverse_two("te") == "et"
    assert tk.reverse_two("t") == "t"
    assert tk.reverse_two("") == ""
    assert tk.strpad_a("hi", 5, 10) == "aaaaahiaaa"
    assert tk.strpad_a("hi", 5, 7) == "aaaaahi"
    assert tk.strpad_a("hi", 0, 7) == "hiaaaaa"
    assert tk.strpad_a("hi", 0, 2) == "hi"
    assert tk.extend_list([1, 2, 3], 0) == [1, 1, 2, 3]
    assert tk.extend_list([1, 2, 3], 1) == [1, 2, 2, 3]
    assert tk.extend_list([1, 2, 3], 2) == [1, 2, 3, 3]
    assert tk.extend_list([1, 2, 3], 3) == [1, 2, 3, 0]
    assert tk.extend_list([1, 2, 3], 9) == [1, 2, 3, 0]