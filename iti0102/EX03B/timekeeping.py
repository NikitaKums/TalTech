"""Time converter."""


def convert(time_string, from_seconds_to_minute, to_seconds_from_minute):
    """
    Return time_string converted using to_seconds_from_minute value.

    :param time_string: given time
    :param from_seconds_to_minute: time_string value
    :param to_seconds_from_minute: time_string to convert to
    """
    get_minutes = int(time_string[0:2])  # get minutes as string value and convert to integer
    get_seconds = int(time_string[-2:])  # get seconds as string value and convert to integer
    a = possible(get_minutes, get_seconds, from_seconds_to_minute)
    if 0 <= a:
        return formating(a, to_seconds_from_minute)
    else:
        return


def possible(minute, second, time_seconds_in_minute):
    """
    Return -1 if time is impossible, sum of seconds if possible.

    :param minute: if possible, used for sum value
    :param second: check if possible, used for sum value
    :param time_seconds_in_minute: time system indicator
    """
    if time_seconds_in_minute == 0:
        return -1
    elif time_seconds_in_minute <= second:
        return -1
    else:
        return minute * time_seconds_in_minute + second


def formating(seconds_sum, new_seconds_in_minutes):
    """
    Return new time.

    :param seconds_sum: value from function 'possible'
    :param new_seconds_in_minutes: new seconds/minute
    """
    new_minutes = seconds_sum // new_seconds_in_minutes
    new_seconds = seconds_sum % new_seconds_in_minutes
    return "{:02d}:{:02d}".format(new_minutes, new_seconds)
