"""Simple calculator."""


def convert_name(name):
    """
    Return a string following the a naming convention.

    [first three letters in uppercase]-[length of string][last two letters of string in lowercase]
    If length of string is less than 3, return "ERROR".
    :param name: string, original name
    """
    get_length = len(name)  # get length of name
    if get_length < 3:  # if less than 3 letters in name
        return "ERROR"
    else:
        three_letters_beginning = name[0:3]  # first 3 letters uppercase
        two_letters_end = name[-2:]  # last 2 letters lowercase
        letter_count = str(get_length)  # Convert int to string
        return "{}-{}{}".format(three_letters_beginning.upper(), letter_count, two_letters_end.lower())


def addition(a, b):
    """
    Return an expression that sums the numbers a and b.

    Example: a = 3, b = 7 -> "3 + 7 = 10"
    """
    return "{} + {} = {}".format(a, b, a + b)


def subtraction(a, b):
    """
    Return an expression that subtracts b from a.

    Example: a = 3, b = 1 -> "3 - 1 = 2"
    """
    return "{} - {} = {}".format(a, b, a - b)


def repeat(s, n):
    """
    Repeat the input string n times.

    :param s: string
    :param n: int
    """
    return s * n


def line(width, decorated=False):
    """
    Create a line seperator using "-". Width includes the decorators if it has any.

    :param width: width of the line, which includes the decorator, if it has one
    :param decorated: if True, line starts with ">" and ends with "<", if False, consists of only "-"
    If decorated and width is 1, return an empty string ("").
    """
    if decorated:
        if width < 2:
            return ""
        else:
            return '>' + repeat((width - 2), "-") + '<'
    else:
        return repeat(width, "-")


def display(a, b, name="unnamed", operation="addition", width=0):
    """
    Create the string representing the display.

    :param a: int or float
    :param b: int or float
    :param name: full name of calculator company, by default it's "unnamed"
    :param operation: operation ("addition" or "subtraction") used on numbers a and b,
                      by default it's "addition"
    :param width: width of the display, by default it's 0 -> always scales with expression width
    :return: string representing the final format
    """
    if operation == "addition":
        expression = addition(a, b)
    elif operation == "subtraction":
        expression = subtraction(a, b)
    else:
        return "ERROR"

    # width of display is set to the assigned width or expression width, whichever is bigger
    width = max(width, len(expression) + 2)  # + 2 for the bars ('|') at the sides

    # create the long string by calling previous functions with params
    result = (f"{convert_name(name).rjust(width)}\n"
              f"{line(width, decorated=True)}\n"
              f"|{expression.center(width - 2)}|\n"
              f"{line(width)}")

    return result
