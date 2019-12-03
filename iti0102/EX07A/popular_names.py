"""Order names by popularity."""


def read_from_file() -> list:
    """
    Create the list of all the names.

    :return: list
    """
    names = []
    with open("popular_names.txt", encoding='utf-8') as file:
        for line in file:
            names.append(line.strip())
    return names


def to_dictionary(names: list) -> dict:
    """
    Make a dictionary from a list of names.

    :param names: list of all the names
    :return: dictionary {"name:sex": number}
    """
    names_dict = {}
    for name in names:
        if name not in names_dict:
            names_dict[name] = 1
        else:
            names_dict[name] = names_dict[name] + 1
    return names_dict


def to_sex_dicts(names_dict: dict) -> tuple:
    """
    Divide the names by sex to 2 different dictionaries.

    :param names_dict: dictionary of names
    :return: two dictionaries {"name": number}, {"name": number}
    first one is male names, seconds is female names.
    """
    male_names = {}
    female_names = {}
    for value in names_dict:
        name, gender = value.split(":")
        if gender == "M":
            male_names[name] = names_dict[value]
        if gender == "F":
            female_names[name] = names_dict[value]
    return male_names, female_names


def most_popular(names_dict: dict) -> str:
    """
    Find the most popular name in the dictionary.

    If the dictionary is empty, return "Empty dictionary."
    :param names_dict: dictionary of names
    :return: string
    """
    if len(names_dict) == 0:
        return "Empty dictionary."
    else:
        popular = max(names_dict, key=names_dict.get)  # Finds the key(name) that has the highest value to it
        return popular


def number_of_people(names_dict: dict) -> int:
    """
    Calculate the number of people in the dictionary.

    :param names_dict: dictionary of names
    :return: int
    """
    get_dict_length = sum(names_dict.values())
    return get_dict_length


def names_by_popularity(names_dict: dict) -> str:
    r"""
    Create a string used to print the names by popularity.

    Format:
        1. name: number of people + "\n"
        ...

    Example:
        1. Kati: 100
        2. Mati: 90
        3. Nati: 80
        ...

    :param names_dict: dictionary of the names
    :return: string
    """
    dict_copy = dict(names_dict)
    i = 0
    popularity_list = []
    while i < len(names_dict):
        a = max(dict_copy, key=dict_copy.get)  # get key(name) that is most popular in list
        b = dict_copy[a]  # get the value of most popular name
        del dict_copy[a]  # remove most popular name
        popularity_list.append(str(1 + i) + ". " + a + ": ")
        popularity_list.append(str(b))
        popularity_list.append("\n")
        i += 1
    return "".join(popularity_list)


if __name__ == '__main__':
    example_names = ("Kati:F\n" * 1000 + "Mati:M\n" * 800 + "Mari:F\n" * 600 + "Tõnu:M\n" * 400).rstrip("\n").split(
        "\n")
    people = to_dictionary(example_names)
    print(people)  # -> {'Kati:F': 1000, 'Mati:M': 800, 'Mari:F': 600, 'Tõnu:M': 400}
    male_names, female_names = to_sex_dicts(people)
    print(male_names)  # -> {'Mati': 800, 'Tõnu': 400}
    print(female_names)  # -> {'Kati': 1000, 'Mari': 600}
    print(most_popular(male_names))  # -> "Mati"
    print(number_of_people(people))  # -> 2800
    print(names_by_popularity(male_names))  # ->   1. Mati: 800
#                                                  2. Tõnu: 400
#                                                  (empty line)
