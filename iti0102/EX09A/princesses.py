"""Generate list of princesses."""

import base64


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
            result = result[3:]
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
    fightforlife = []
    injured = []
    inpanic = []
    bored = []
    patterns = ["FIGHTS FOR LIFE", "INJURED", "IN PANIC", "BORED"]
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

    file = open("princesses_to_save.txt", "w")
    for i in range(len(princesses)):
        if i == len(princesses) - 1:
            str1 = "\n".join(princesses[i])
        else:
            princesses[i].append("\n")
            str1 = "\n".join(princesses[i])
        file.write(str1)
