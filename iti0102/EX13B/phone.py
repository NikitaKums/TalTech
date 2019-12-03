"""News spread counter."""


def how_many_calls(n, cache=None):
    """
    Return the number of calls made during the current minute.

    Arguments:
    n -- the current minute.
    """
    if cache is None:
        cache = {0: 1, 1: 1, 2: 2, 3: 4}
    if n < 0:
        return None
    elif n in cache:
        return cache[n]
    else:
        cache.update({n: how_many_calls(n - 1, cache) + how_many_calls(n - 2, cache) + how_many_calls(n - 3, cache)})
        return cache[n]


def how_many_people(n, cache=None):
    """
    Return the number of people who know after n minutes has passed.

    Arguments:
    n -- how many minutes has passed.
    """
    if cache is None:
        cache = {0: 1, 1: 2, 2: 4, 3: 8}
    if n < 0:
        return None
    elif n in cache:
        return cache[n]
    else:
        cache.update({n: how_many_people(n - 1, cache) + how_many_people(n - 2, cache) + how_many_people(n - 3, cache) + 1})
        return cache[n]
