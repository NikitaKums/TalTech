"""Never late again."""


class Main:
    """Main class where magic happens."""

    def __init__(self, file: str):
        """
        Class constructor.

        :param file: File with bus times.
        """
        self.input = input()
        if not check_input(self.input):
            raise Exception
        self.bus_times = {}
        with open(file) as data:
            for lines in data:
                lines = lines.strip()
                list1 = lines.split("\t")[0]
                self.bus_times[list1] = (" ".join(lines.split("\t")[1:])).split(" ")

    def get_departure_time(self):
        """
        Find next departure time and print it.

        :return:
        """
        hour = int(self.input.split(":")[0])
        minutes = int(self.input.split(":")[1])
        keys_list = list([int(key) for key in self.bus_times.keys()])
        if help_method(hour, minutes, keys_list, self.bus_times):
            print(f"Your bus will depart at {keys_list[0]}:{self.bus_times[str(keys_list[0])][0]}")
            return
        elif hour not in keys_list:
            print(help_method2(keys_list, hour, self.bus_times))
            return
        else:
            for key, value in self.bus_times.items():
                if int(key) == hour:
                    for time in value:
                        if minutes < int(time):
                            print(f"Your bus will depart at {key}:{time}")
                            return
                        elif minutes >= int(time) and time == self.bus_times[key][-1]:
                            for some_key in keys_list:
                                if some_key > hour:
                                    print(f"Your bus will depart at {some_key}:{self.bus_times[str(some_key)][0]}")
                                    return
        print(f"Your bus will depart at {keys_list[0]}:{self.bus_times[str(keys_list[0])][0]}")


def help_method(hour, minutes, keys_list, dictionary):
    """
    Check for time if its right.

    :param hour: input hours.
    :param minutes: input minutes.
    :param keys_list: list of keys.
    :param dictionary: dictionary of bus hour.
    :return: bool
    """
    if hour > keys_list[-1] or hour == keys_list[-1] and minutes > int(dictionary[str(keys_list[-1])][-1]):
        return True


def help_method2(keys_list, hour, dictionary):
    """
    Check for time if its right.

    :param keys_list: list of keys
    :param hour: hour from input
    :param dictionary: dictionary of bus hours
    :return: text
    """
    for key in keys_list:
        if key > hour:
            return f"Your bus will depart at {key}:{dictionary[str(key)][0]}"


def check_input(user_input):
    """
    Check user input for invalid.

    :param user_input: Input to check.
    :return: bool
    """
    if user_input[-3] != ":":
        return False
    separated_input = user_input.split(":")
    try:  # letter in time
        int(separated_input[0])
        int(separated_input[-1])
    except ValueError:
        return False
    if int(separated_input[0]) < 0 or 0 > int(separated_input[-1]) > 59 or int(separated_input[0]) > 24 or int(separated_input[-1]) < 0:
        return False
    return True
