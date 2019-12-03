"""Tests for newton square root method."""


import newton


def test_regular():
    """Test square root with regular numbers."""
    assert newton.square_root_with_newton_method(25, 1) == 7.25


def test_negative_iteration():
    """Try to get square root with negative iterations."""
    assert newton.square_root_with_newton_method(25, -3) is None


def test_iteration_zero():
    """Try to get square root if iterations is zero."""
    assert newton.square_root_with_newton_method(25, 0) == 12.5


def test_number_negative():
    """Try to get square root if number is negative."""
    assert newton.square_root_with_newton_method(-25, 1) is None


def test_round_twice():
    """Test if taking square root twice makes a difference."""
    assert newton.square_root_with_newton_method(25, 2) == 5.349


def test_high_iterations():
    """Test if iterations if higher than the number."""
    assert newton.square_root_with_newton_method(156, 157) == 12.49


def test_high_numbers():
    """Test square root with high number."""
    assert newton.square_root_with_newton_method(12335435, 20) == 3512.184


def test_non_int():
    """Test square root with not integer value."""
    assert newton.square_root_with_newton_method(10.5, 1) == 3.625


def test_small_iteration():
    """Test square root if iteration is small, not integer."""
    assert newton.square_root_with_newton_method(10, 0.5) == 5.0


def test_number_iteration_zero():
    """Test square root if both values are zero."""
    assert newton.square_root_with_newton_method(0, 0) is None


def test_number_zero():
    """Test square root if number is zero."""
    assert newton.square_root_with_newton_method(0, 20) is None


def test_both_negative():
    """Test square root if both values are negative."""
    assert newton.square_root_with_newton_method(-1, -3) is None
def test_1():
    assert newton.square_root_with_newton_method(0.0123, 20) == 0.006