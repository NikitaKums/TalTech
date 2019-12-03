"""Converting sentences."""


def scramble_sentence(sentence: str) -> str:
    """
    Function to change all words in sentence using scramble_word() function.

    :param sentence: sentence to scramble
    :return: scrambled sentence
    """
    scrambled_sentence = []
    sentence_list = sentence.split()
    i = 0
    while i != len(sentence_list):
        a = scramble_word(sentence_list[i])
        scrambled_sentence.insert(i, a)
        i += 1
    return ' '.join(scrambled_sentence)


def scramble_word(word: str) -> str:
    """
    Sort a word alphabetically, keeping only the astrophe, first and last letter as they were.

    If the last letter of a word is a symbol (.,;?!") the second to last letter must remain the same.
    If the length of the word without symbols is greater than 7 or the word can't be changed from the
    original, the initial word must be returned. When sorting, treat every letter as lowercase.

    :param word: input word
    :return: alphabetically scrambled word
    """
    if len(word) > 7 or len(word) < 3:
        return word

    first_letter = word[:1]
    no_astrophe = word.replace("'", "")

    if no_astrophe.isalpha():
        last_letter = word[-1:]
        new_word = word[1:-1]
    else:
        last_letter = word[-2:]
        new_word = word[1:-2]

    only_letters = new_word.replace("'", "")
    alphabetical = ''.join(sorted(only_letters, key=str.lower))  # when sorting all letters are lower case
    scrambled_word = f'{first_letter}{alphabetical}{last_letter}'

    if "'" in word:
        position = word.index("'")
        scrambled_word = scrambled_word[:position] + "'" + scrambled_word[position:]

    return scrambled_word
