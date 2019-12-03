"""Message decryption or encryption."""


import string


def caesar_cipher(message, shift, encrypt=False):
    """
    Decrypt or Encrypt a message.

    :param message: Message to work with
    :param shift: Shift for encryption or decryption
    :param encrypt: True or False
    :return: Encrypted or decrypted message
    """
    if shift > 26:
        shift %= 26
    result = []
    clone = list(message)
    for i in message.lower():
        if i not in string.ascii_letters:
            result.append(i)
            continue
        if encrypt:
            new = ord(i) + shift
        else:
            new = ord(i) - shift
        if new < 97:
            new += 26
        elif new > 122:
            new -= 26
        result.append(chr(new))
    for i in range(len(clone)):
        if clone[i].isupper():
            result[i] = result[i].upper()
    return "".join(result)
