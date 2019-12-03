"""Turn a phrase into acronym."""


def acronymize(message: str) -> str:
    """
    Turn the input text into the acronym and reverse it, if the text is not too long.

    :param message: initial text
    :return: reversed acronym
    """
    if len(message) == 0:
        return " "
    result = ""
    message_list = message.split(" ")
    if not check_message_length(message_list):
        return "Sorry, the input's just too long!"
    for i in range(len(message_list)):
        if check_word(message_list[i]):
            word = message_list[i]
            result = result + word[0]
    return reverse(result).upper()


def check_word(word: str) -> bool:
    """
    Check if the word is long enough and does not contain special symbols.

    The word should be more than 3 chars long (without symbols).
    :param word: word
    :return: bool
    """
    symbol = ['?', ',', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '!', '(', '_', '@', '#', '$', '%', '^', '&', '*', '.', ')', "'"]
    if word.isalpha():
        return word_length(word)
    while True:
        if check_end(word, symbol):
            if word.isalpha():
                return word_length(word)
            else:
                word = word[:-1]
        else:
            if word.isalpha():
                if word_length(word):
                    return True
            else:
                return False


def check_message_length(words: list) -> bool:
    """
    Check if the initial text length is OK (does not contain more than 50 words).

    :param words: list of words
    :return: bool
    """
    return len(words) <= 50


def reverse(message: str) -> str:
    """
    Reverse the given message.

    :param message: acronym
    :return: reversed message
    """
    return message[::-1]


def word_length(word: str) -> bool:
    """
    Check if word is bigger than length of 3.

    :param word: word to check length of
    :return: bool
    """
    if len(word) > 3:
        return True
    else:
        return False


def check_end(word, ending_list):
    """
    Check if word contains symbols or no.

    :param word: word to check
    :param ending_list: list on symbols
    :return: bool
    """
    if word.isalpha() or "-" in word:
        return True
    for ending in ending_list:
        if word.endswith(ending):
            return True
    else:
        return False
