"""Retrieve stops and departures info from REST service."""
import json
import urllib.request

API_BASE = "https://public-transport-api.herokuapp.com"
REGION = "tallinn"


def get_nearby_stops(api_base, lat, lng):
    """
    Get nearby stops.

    :param api_base: Base URL that the endpoint gets appended to
    :param lat: Latitude
    :param lng: Longitude
    :return: List of all nearby stops
    """
    data = urllib.request.urlopen(api_base + f"/stops/{lat}/{lng}")
    data = json.load(data)
    return sorted(data, key=lambda x: int(x["distance"].split(" ")[0]))


def get_nearest_stop(api_base, lat, lng):
    """
    Get nearest stop.

    :param api_base: Base URL that the endpoint gets appended to
    :param lat: Latitude
    :param lng: Longitude
    :return: Nearest stop
    """
    if len(get_nearby_stops(api_base, lat, lng)) == 0:
        return None
    return get_nearby_stops(api_base, lat, lng)[0]


def get_next_departures(api_base, region, stop_id):
    """
    Get next departures from stop.

    :param api_base: Base URL that the endpoint gets appended to
    :param region: Region
    :param stop_id: Stop ID
    :return: List of next departures from stop
    """
    data = urllib.request.urlopen(api_base + f"/departures/{region}/{stop_id}")
    data = json.load(data)
    return data["departures"]


def get_next_departure(api_base, region, stop_id):
    """
    Get next departure from stop.

    :param api_base: Base URL that the endpoint gets appended to
    :param region: Region
    :param stop_id: Stop ID
    :return: Next departure from stop
    """
    data = get_next_departures(api_base, region, stop_id)
    if len(data) == 0:
        return None
    return data[0]
