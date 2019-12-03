"""Generate list of princesses."""


import base64
header = []


def read(read_file) -> list:
    """
    Read, decrypt and save information from the given file.

    :param read_file: the file we read from
    :exception: Exception
    :return: lines
    """
    result = []
    try:
        with open(read_file) as file:
            for lines in file:
                line = decode(lines.strip(""))
                result.append(extract_information(line))
            global header
            header = result[0:2]
            result = result[3:]
            for word in result:
                if "None" in word[0:3]:
                    raise InvalidPrincessException("Invalid princess!")
                continue
            return result
    except FileNotFoundError:
        raise Exception("File not found!")


def decode(line: str) -> str:
    """
    Decode each line.

    Hint: base64.
    :param line: line from the encoded file.
    :return: same decoded line. String.
    """
    return base64.b64decode(line).decode('utf-8')


def extract_information(line: str) -> list:
    """
    Extract information from each line (without spaces or extra tabulation symbols).

    Example output: ['Helen-elizabeth', 'IN PANIC', 'Ancient Ruins', None]
    Example output: ['Julianne', 'EATEN', 'Heaven', 'Will rule the kingdom'].
    Obviously, she won't rule anything, however. How sad.
    :param line: decrypted line from the file.
    :return: information about single princess
    """
    result = line.split("  ")
    result = [word for word in result if word != ""]  # remove spaces
    for i in range(len(result)):
        result[i] = result[i].lstrip()
        result[i] = result[i].rstrip()
    return result


def filter_by_status(lines) -> list:
    """
    Filter out non-relevant statuses.

    Statuses to filter: "EATEN", "SAVED", "SLAYED THE DRAGON HERSELF". There is no point to save those.

    :param lines: lines
    :return: list
    """
    statuses_to_filter = ["EATEN", "SAVED", "SLAYED THE DRAGON HERSELF"]
    result = lines[:]
    for word in lines:
        if [status for status in statuses_to_filter if status in word]:
            result.remove(word)
    return result


def sort_by_status(filtered_lines) -> list:
    """
    Sort lines by pattern FIGHTS FOR LIFE > INJURED > IN PANIC > BORED.

    FIGHTS FOR LIFE comes before INJURED etc.

    :param filtered_lines:
    :return: sorted lines.
    """
    patterns = ["FIGHTS FOR LIFE", "INJURED", "IN PANIC", "BORED"]
    fightforlife = []
    injured = []
    inpanic = []
    bored = []
    for word in filtered_lines:
        if patterns[0] in word:
            fightforlife.append(word)
            continue
        if patterns[1] in word:
            injured.append(word)
            continue
        if patterns[2] in word:
            inpanic.append(word)
            continue
        if patterns[3] in word:
            bored.append(word)
            continue
    result = fightforlife + injured + inpanic + bored
    return result


def sort_by_place(sorted_lines):
    """
    Sort given line by place.

    :param sorted_lines: Lines to sort
    :return: Sorted lines
    """
    list1 = []
    list2 = []
    list3 = []
    statusestofilter = ["SAVED", "EATEN", "SLAYED THE DRAGON HERSELF"]
    if len(sorted_lines) == 0:
        return sorted_lines
    for i in range(len(sorted_lines)):
        if "None" in sorted_lines[i]:
            raise InvalidPrincessException("Invalid princess!")
        for words in statusestofilter:
            if words in sorted_lines[i]:
                raise InvalidPrincessException(f"The princess is already {words}!")
            continue
    order = makeorder(sorted_lines)
    for word in sorted_lines:
        if order[0] in word:
            list1.append(word)
            continue
        if order[1] in word:
            list2.append(word)
            continue
        if order[2] in word:
            list3.append(word)
            continue
    result = list1 + list2 + list3
    return result


def makeorder(sorted_lines):
    """
    Make order of places from list as they appear.

    :param sorted_lines: List
    :return: Order of places
    """
    order = []
    for i in range(len(sorted_lines)):
        if sorted_lines[i][2] not in order:
            order.append(sorted_lines[i][2])
    return order


def write(read_file):
    """
    Write the sorted lines to the new file 'princesses_to_save.txt'.

    The last princess is NOT followed by a blank line.

    Format:
            Name
            Status
            Place
            Details
            <NEW LINE>
    Example:
            Kathi
            FIGHTS FOR LIFE
            Old Shack
            Sassy
    :param read_file: the file we read from
    :return: None
    """
    princesses = filter_by_status(read(read_file))
    princesses = sort_by_status(princesses)
    princesses = sort_by_place(princesses)

    file = open("princesses_to_save.txt", "w")
    for word in header:
        for something in word:
            file.write("{:20}".format(something))
        file.write("\n")
    for i in range(len(princesses)):
        str1 = princesses[i]
        for word in str1:
            file.write("{:20}".format(word))
        if i != len(princesses) - 1:
            file.write("\n")


class InvalidPrincessException(Exception):
    """Raise exception if princess location is wrong."""

    pass
