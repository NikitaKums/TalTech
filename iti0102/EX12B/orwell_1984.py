"""Orwell 1984.."""


class Citizen:
    """Class which represents a single citizen."""

    statuses = ["citizen", "prole", "nonperson", "under surveillance"]

    def __init__(self, name, party, status="citizen"):
        """
        Class constructor.

        :param name: name of the citizen
        :param party: party which he belongs to
        :param status: status
        """
        self.name = name
        self.party = party
        if status in Citizen.statuses:
            self.status = status
            if self.status == "prole":
                party = None
                self.party = party
            if self.status == "nonperson":
                self.name = None
                self.party = None
        else:
            self.party = party
            self.status = "citizen"
        if self.party is not None:
            Party.add_party_member(party, self)

    def set_party(self, party):
        """
        Set citizen's party. The method does not return anything.

        :param party: new party (Inner or Outer, both are Party class instances)
        """
        if isinstance(party, Party) or party is None:
            if self.party is not None and self in self.party.get_party_members():
                Party.remove_party_member(self.party, self)
            if party is not None:
                Party.add_party_member(party, self)
            self.party = party

    def get_party(self):
        """
        Get the citizen's party.

        :return: party object
        """
        return self.party

    def set_status(self, status):
        """
        Set citizen's status. The method does not return anything.

        :param status: new status
        """
        if status in Citizen.statuses:
            if status == "nonperson" and self.status is not None:
                self.party.vaporize(self)
            elif status == "prole":
                self.party = None
        else:
            pass

    def get_status(self):
        """
        Get the citizen's status.

        :return: status
        """
        return self.status

    def set_name(self, name):
        """
        Set the citizen's name. The method does not return anything.

        :param name: new name
        """
        self.name = name

    def get_name(self):
        """
        Get the citizen's name.

        :return: name
        """
        return self.name

    def __str__(self):
        """
        Compute string representation of this object.

        :return: f"BIG BROTHER IS WATCHING YOU, {self.name}"
        """
        return f"BIG BROTHER IS WATCHING YOU, {self.name}"


class Party:
    """Party class."""

    def __init__(self):
        """Class constructor."""
        self.party_members = []

    def get_party_members(self):
        """
        Get the list of party members.

        :return: list
        """
        return self.party_members

    def add_party_member(self, citizen):
        """
        Add the citizen to the party members' list.

        Citizen must be instance of Citizen class, must have name, must not already be a member and must not have a
        'nonperson' status.
        Does not return anything.
        :param citizen Citizen class instance
        """
        if isinstance(citizen, Citizen) and (citizen.name is not None) and citizen.status != "nonperson" and citizen not in self.party_members:
            self.party_members.append(citizen)
            citizen.party = self
        pass

    def remove_party_member(self, citizen):
        """Remove the citizen from the party members' list."""
        if citizen in self.party_members:
            self.party_members.remove(citizen)

    def vaporize(self, citizen):
        """
        Remove the citizen from the party members, set his name and party to None and status to nonperson.

                    The method does not return anything.
        :param citizen: Citizen class instance

        """
        if citizen in self.party_members:
            Party.remove_party_member(self, citizen)
            citizen.name = None
            citizen.party = None
            citizen.status = "nonperson"
            vaporized_count(1)

    def get_privileges(self):
        """
        Get privileges granted by party.

        :return: None
        """
        return None

    @staticmethod
    def get_slogan():
        r"""
        Get the party slogan.

        :return: "WAR IS PEACE\nFREEDOM IS SLAVERY\nIGNORANCE IS STRENGTH"
        """
        return "WAR IS PEACE\nFREEDOM IS SLAVERY\nIGNORANCE IS STRENGTH"


class InnerParty(Party):
    """Inner party class, which extends the Party class."""

    def get_privileges(self):
        """
        Get privileges granted by party (Override).

        :return: "Everything"
        """
        return "Everything"

    def __str__(self):
        """
        Compute string representation of this object.

        :return: "Inner party"
        """
        return "Inner party"


class OuterParty(Party):
    """Outer party class, which extends the Party class."""

    def __str__(self):
        """
        Compute string representation of this object.

        :return: "Outer party"
        """
        return "Outer party"


class BigBrother:
    """Big brother class."""

    def __init__(self, inner_party, outer_party):
        """
        Class constructor.

        :param inner_party: inner party object
        :param outer_party: outer party object
        """
        global result
        self.vaporized_amount = result
        self.all_citizens = []
        for person in Party.get_party_members(outer_party):
            self.all_citizens.append(person)
        for another_person in Party.get_party_members(inner_party):
            self.all_citizens.append(another_person)

    def get_all_citizens(self):
        """
        Get all citizens who are members in the parties.

        :return: list
        """
        return self.all_citizens

    def massive_vaporize(self, status):
        """
        Vaporize people with a given status.

        :param status: string
        :return: number of vaporized people per session
        """
        duplicate = self.all_citizens[:]
        for person in duplicate:
            if status == person.status:
                self.all_citizens.remove(person)
                person.party.vaporize(person)
                vaporized_count(1)

    def get_number_of_vaporized(self) -> int:
        """
        Get number of vaporized people of all time.

        :return: integer
        """
        global result
        return int(self.vaporized_amount)


result = 0


def vaporized_count(n):
    """Count the amount of vaporized people of all time."""
    global result
    result += n
