"""Decrypt message that was encrypted previously."""


import encoder
import utils


def _decrypt_message(encrypted_message, shift):
    """
    Decrypt given message.

    :param encrypted_message: message to decrypt
    :param shift: decrypt by how much
    :return: decrypted message
    """
    return utils.caesar_cipher(encrypted_message, shift)


def get_message(initial_message, shift, decrypt=False):
    """
    Correct and decrypt given message.

    :param initial_message: message to work with
    :param shift: if decrypting then by how much
    :param decrypt: If true then decrypt the message
    :return: decrypted or encrypted message
    """
    message = encoder._correct_message(initial_message)
    message = utils.caesar_cipher(message, shift, True)
    if decrypt:
        return utils.caesar_cipher(message, shift)
    else:
        return message
