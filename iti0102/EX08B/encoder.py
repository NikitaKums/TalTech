"""Makes message readable and decrypts it again."""


import string
import utils


def _correct_message(word):
    """
    Correct message so it is readable.

    :param word: Message to correct
    :return: Corrected message
    """
    alphabet = list(string.ascii_letters)
    symbols = ['/', '#', '$', '%', '^', '&', '*', '@', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9']
    word_list = list(word)
    new_string = []
    if not check(word_list):  # if no letters in word
        return word
    for i in range(len(word_list) - 1):
        if word_list[i] == " ":
            new_string.append("")
        if word_list[i] not in alphabet and word_list[i] in symbols:
            if not check(word_list[0:i]):
                new_string.append(word_list[i])
            if not check(word_list[i:]) and word_list[i] in symbols:
                new_string.append(word_list[i])
            else:
                continue
        if word_list[i] in alphabet or word_list[i] not in symbols:
            letter = word_list[i]
            new_string.append(letter)
        if word_list[i] in symbols and word_list[i + 1] == "":
            new_string.append(word_list[i])
        else:
            continue
    new_string.append(word_list[-1])
    return "".join(new_string)


def _encrypt_message(message, shift):
    """
    Encrypt given message.

    :param message: Message to encrypt
    :param shift: Encrypt by how much
    :return: Encrypted message
    """
    return utils.caesar_cipher(message, shift, True)


def get_corrected_encrypted_message(initial_message, shift):
    """
    Correct and encrypt given message.

    :param initial_message: message to correct
    :param shift: shift to encrypt by
    :return: corrected and encrypted message
    """
    message = _encrypt_message(_correct_message(initial_message), shift)
    return message


def check(list1)-> bool:
    """
    Check if list contains any characters.

    :param list1: List to check for characters
    :return: bool
    """
    if any(letter.isalpha() for letter in list1):
        return True
    return False
