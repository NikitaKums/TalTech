"""Dictionary."""


from collections import defaultdict


class Dictionary:
    """Create a dictionary."""

    def __init__(self, initial_data):
        """
        Class constructor. Provided data handling.

        :param initial_data: Data to format and turn into dictionary.
        """
        skip = False
        noun_list = []
        adj_list = []
        verb_list = []

        banned_symbols = list("0123456789!\"#$%&'()*+,./:;<=>?@[\\]^_`{|}~ ")
        initial_data_list = format_data(initial_data)

        for word in initial_data_list:
            the_word = word[3:word.rfind("-") - 1].lower()
            the_word = the_word.split(" ")[0]
            description = " ".join(word.split(" ")[2:])
            if the_word.startswith("-") or the_word.endswith("-") or the_word.count("-") > 1 or len(description) < 1:
                continue
            for char in description:
                if char.isalpha():
                    break
                continue
            for letter in the_word:
                if letter in banned_symbols:
                    skip = True
                    break
            if "(n)" in word and not skip:
                noun_list.append(tuple((the_word, description)))
            if "(a)" in word and not skip:
                adj_list.append(tuple((the_word, description)))
            if "(v)" in word and not skip:
                verb_list.append(tuple((the_word, description)))
            skip = False
        self.nouns = make_dict(noun_list)
        self.verbs = make_dict(verb_list)
        self.adjectives = make_dict(adj_list)
        self.dict = make_dict(noun_list + verb_list + adj_list)

    def get_definitions(self, word):
        """
        Find all definitions of provided word.

        :param word: Word to find definitions of
        :return: List containing all definitions of the word
        """
        if word.lower() in self.dict:
            return self.dict.get(word.lower())
        return []

    def is_word_noun(self, word):
        """
        Decide if given word is a noun.

        :param word: Word to check
        :return: bool
        """
        if word.lower() in self.nouns:
            return True
        return False

    def is_word_adjective(self, word):
        """
        Decide if given word is an adjective.

        :param word: Word to check
        :return: bool
        """
        if word.lower() in self.adjectives:
            return True
        return False

    def is_word_verb(self, word):
        """
        Decide if given word is a verb.

        :param word: Word to check
        :return: bool
        """
        if word.lower() in self.verbs:
            return True
        return False

    def get_all_nouns(self):
        """
        Find all nouns in dictionary.

        :return: Nouns in list form
        """
        result = []
        if len(self.nouns) == 0:
            return []
        for key, value in self.nouns.items():
            result.append(key)
        return result

    def get_all_verbs(self):
        """
        Find all verbs in dictionary.

        :return: Verbs in list form
        """
        result = []
        if len(self.verbs) == 0:
            return []
        for key, value in self.verbs.items():
            result.append(key)
        return result

    def get_all_adjectives(self):
        """
        Find all adjectives in dictionary.

        :return: Adjectives in list form
        """
        result = []
        if len(self.adjectives) == 0:
            return []
        for key, value in self.adjectives.items():
            result.append(key)
        return result

    def search(self, subword, min_len=None, max_len=None):
        """
        Search dictionary for given subword.

        :param subword: Subword that must be in word
        :param min_len: Optional. Minimum length of word to search for
        :param max_len: Optional. Maximum length of word to search for
        :return: List with words containing subword
        """
        result = []
        subword = subword.lower()
        for key in self.dict.keys():
            if subword in key:
                if min_len and max_len:
                    if min_len <= len(key) <= max_len:
                        result.append(key)
                    continue
                elif min_len:
                    if len(key) >= min_len:
                        result.append(key)
                    continue
                elif max_len:
                    if len(key) <= max_len:
                        result.append(key)
                    continue
                else:
                    result.append(key)
        return result


def make_dict(some_list):
    """
    Make dictionary out of provided list.

    :param some_list: List with values
    :return: Values in dictionary
    """
    result = defaultdict(list)
    for key, value in some_list:
        result[key].append(value)
    return dict(result)


def format_data(initial_data):
    """
    Format given data.

    :param initial_data: Data to format
    :return: Formatted data list
    """
    result = initial_data.split("\n")
    result = [word.rstrip() for word in result if word]
    return result
