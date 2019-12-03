"""Tests for EX03A/calculator."""


import calculator


def test_positive_number_addition():
    """Test simple positive number addition."""
    assert calculator.addition(6, 6) == "6 + 6 = 12"


def test_negative_number_addition():
    """Test simple negative number addition."""
    assert calculator.addition(-100, -109) == "-100 + -109 = -209"


def test_negative_number_addition_a():
    """Test negative number addition if first number is negative."""
    assert calculator.addition(-100, 50) == "-100 + 50 = -50"


def test_negative_addition_b():
    """Test negative number addition if second number is negative."""
    assert calculator.addition(100, -50) == "100 + -50 = 50"


def test_subtraction():
    """Test simple subtraction."""
    assert calculator.subtraction(6, 3) == "6 - 3 = 3"


def test_subtraction_negative():
    """Test subtraction if both numbers are negative."""
    assert calculator.subtraction(-60, -40) == "-60 - -40 = -20"


def test_subtraction_negative_b():
    """Test subtraction if second number is negative."""
    assert calculator.subtraction(100, -5) == "100 - -5 = 105"


def test_subtraction_negative_a():
    """Test subtraction if first number is negative."""
    assert calculator.subtraction(-100, 5) == "-100 - 5 = -105"


def test_short_name():
    """Try to convert 2 letter name."""
    assert calculator.convert_name("sk") == "ERROR"


def test_convert_name():
    """Try to convert 3 letter name."""
    assert calculator.convert_name("skk") == "SKK-3kk"


def test_convert_no_name():
    """Try to convert an empty string."""
    assert calculator.convert_name("") == "ERROR"


def test_convert_long_name_random_case():
    """Try to convert long random case name."""
    assert calculator.convert_name("empTYEMPtyEmPTy") == "EMP-15ty"


def test_repeat_zero_length():
    """Repeat character 0 times."""
    assert calculator.repeat("a", 0) == ""


def test_repeat_negative_length():
    """Try to repeat character negative amount of times."""
    assert calculator.repeat("a", -1) == ""


def test_normal_repeat():
    """Simple repeat test."""
    assert calculator.repeat("a", 4) == "aaaa"


def test_no_letter_repeat():
    """Test repeat if no character given."""
    assert calculator.repeat("", 4) == ""


def test_short_decorated():
    """Test short width decorated line."""
    assert calculator.line(2, True) == "><"


def test_long_decorated():
    """Test long width decorated line."""
    assert calculator.line(10, True) == ">--------<"


def test_no_decoration_long():
    """Test long width line without decoration."""
    assert calculator.line(10, False) == "----------"


def test_no_decoration_small():
    """Test small line without decoration."""
    assert calculator.line(1, False) == "-"


def test_decorated_empty():
    """Test decorated line if no width is 0."""
    assert calculator.line(0, True) == ""


def test_empty_no_decoration():
    """Test not decorated line if witdth is 0."""
    assert calculator.line(0) == ""


def test_display_addition_decorated():
    """Test display with addition and decoration."""
    assert calculator.display(5, 3, "test", operation="addition", width=5) == "    TES-4st\n>---------<\n|5 + 3 = 8|\n-----------"


def test_display_subtraction_decorated():
    """Test display with subtration and decoration."""
    assert calculator.display(5, 3, "test", operation="subtraction", width=5) == "    TES-4st\n>---------<\n|5 - 3 = 2|\n-----------"


def test_display_no_operation():
    """Test display if no operation given."""
    assert calculator.display(5, 3, "test", operation="none", width=5) == "ERROR"
