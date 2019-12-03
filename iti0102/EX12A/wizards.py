"""Wizards School."""


class MismatchError(Exception):
    """
    Class MismatchError inherits its properties from Exception class.

    Should have user-defined message.
    """

    def __init__(self, message):
        """
        Class constructor.

        :param message: user message
        """
        self.message = message


class Wand:
    """Wands used by wizards."""

    def __init__(self, wood_type, core):
        """
        Class constructor. Each wand has wood type and core.

        :param wood_type: Type of wood the wand is made of
        :param core: The core for wand
        """
        self.wood_type = wood_type
        self.core = core

    def set_wood_type(self, wood_type):
        """
        Assign wood type to wand if missing.

        :param wood_type: Wood type for wand
        :return:
        """
        self.wood_type = wood_type

    def set_core(self, core):
        """
        Set core for wand.

        :param core: Core to be set  for wand
        :return:
        """
        self.core = core

    @staticmethod
    def check_wand(wand):
        """
        Check if wand is correct. Must be Wand instance and have wood type and core.

        :param wand: Wand to check
        :return:
        """
        if not isinstance(wand, Wand):
            raise MismatchError("The wand like that does not exist!")
        if wand.wood_type is None or wand.core is None:
            raise MismatchError("The wand like that does not exist!")

    def __str__(self):
        """
        Print Wand details.

        :return: String
        """
        return f"{self.wood_type}, {self.core}"


class Wizard:
    """Wizards in the school."""

    list_of_wands_n_wizards = []

    def __init__(self, name, wand=None):
        """
        Class constructor. Each wizard has a name and a wand.

        :param name: Wizard's name
        :param wand: Wizard's wand
        """
        self.name = name
        self.wand = wand
        Wizard.list_of_wands_n_wizards.append([name, wand])
        if wand:
            Wand.check_wand(self.wand)

    def set_wand(self, wand):
        """
        Give wizard a wand.

        :param wand: Wand for wizard
        :return:
        """
        if not Wand.check_wand(wand):
            self.wand = wand
            for word in Wizard.list_of_wands_n_wizards:
                if word[0] == self.name:
                    word[1] = str(wand)

    def get_wand(self):
        """
        Get the wand details that is used by wizard.

        :return: Wand details
        """
        return self.wand

    def __str__(self):
        """
        Print wizard's name.

        :return: String
        """
        return self.name


class School:
    """Schools that the wizard attend."""

    schools = [
        "Hogwarts School of Witchcraft and Wizardry", "Durmstrang Institute",
        "Ilvermorny School of Witchcraft and Wizardry", "Castelobruxo",
        "Beauxbatons Academy of Magic"
    ]

    def __init__(self, name: str):
        """
        Class constructor. Each school has a specific name.

        :param name: Name of the school
        """
        if name in School.schools:
            self.name = name
            self.studying = []
        else:
            raise MismatchError("There is no such school!")

    def add_wizard(self, wizard):
        """
        Add wizard to the school if he meets the requirements.

        :param wizard: Wizard that wants to attend
        :return:
        """
        if not isinstance(wizard, Wizard) or wizard.wand is None or wizard.name is None:
            raise MismatchError("It's a filthy muggle!")
        if wizard.name in self.studying:
            return f"{wizard.name} is already studying in this school!"
        else:
            self.studying.append(wizard.name)
            return f"{wizard.name} started studying in {self.name}."

    def remove_wizard(self, wizard):
        """
        Remove wizard from school students list.

        :param wizard: Wizard to be removed
        :return:
        """
        try:
            self.studying.remove(str(wizard))
        except ValueError:
            pass

    def get_wizards(self):
        """
        Get all of wizard's names that are studying in the school.

        :return: List of names
        """
        return self.studying

    def get_wizard_by_wand(self, wand):
        """
        Get wizard's name by wand.

        :param wand: Name of the wand
        :return:
        """
        if Wand.check_wand(wand):
            pass
        else:
            for word in Wizard.list_of_wands_n_wizards:
                if word[1] == str(wand) and word[0] in self.studying:
                    return word[0]
        return None

    def __str__(self):
        """
        Print name of the school.

        :return: String
        """
        return self.name


def wand_check(wand):
    """
    Check wand if it meets the requirements.

    :param wand: Wand to check
    :return: bool
    """
    if not isinstance(wand, Wand):
        return True
    if wand.wood_type is None or wand.core is None:
        return True
