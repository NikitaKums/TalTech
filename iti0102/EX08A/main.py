"""Main module."""


import acronymator


def get_reversed_acronym(message: str) -> str:
    """
    The main method of the program.

    :param message: initial text
    :return: reversed acronym of the given message
    """
    return acronymator.acronymize(message)


if __name__ == '__main__':
    print(get_reversed_acronym("That was quite easy, huh?"))  # --> EQT
    print(get_reversed_acronym("Hello, my name is Karen"))  # --> KNH
    print(get_reversed_acronym(""))  # --> ""
    print(get_reversed_acronym(".,212 A 13 he,,.llo to me"))  # --> ""
    print(get_reversed_acronym("Light amplification by the stimulated emission of radiation."))  # --> RESAL
    print(get_reversed_acronym("Self-contained underwater breathing apparatus."))  # --> ABUS
    print(get_reversed_acronym("   Spaces     are    irrelevant   "))  # --> IS

    mes = \
        """
            As soon as the light in the bedroom went out there was a stirring and a
            fluttering all through the farm buildings. Word had gone round during the
            day that old Major, the prize Middle White boar, had had a strange dream
            on the previous night and wished to communicate it to the other animals.
            It had been agreed that they should all meet in the big barn as soon as
            Mr. Jones was safely out of the way.
            """

    print(get_reversed_acronym(mes))
