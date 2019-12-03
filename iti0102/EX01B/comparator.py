"""Compare input number with 5."""


def comparator(num: float) -> str:  # Fnkts võtab suvalise numbri ning võrdleb viiega. Tulemus väljasatatakse tekstina.
    """
    Compare the given number with 5 and return message depending on the number's value.

    :param num: input number
    :return: message regarding the number value compared to five.
    """
    # Your code here
    if num > 5:
        return "The input number is bigger than 5!"
    elif num < 5:
        return "The input number is smaller than 5!"
    elif num == 5:
        return "The input number is 5!"


print(comparator)
