"""Simple bank."""


class Account:
    """Represent a bank account."""

    def __init__(self, name, balance):
        """
        Class constructor. Each account has owner's name and starting balance.

        :param name: account owner name. String.
        :param balance: starting balance of account. Integer.
        """
        self.name = name
        self.balance = balance

    def withdraw(self, amount):
        """
        Withdraw money from account.

        :param amount: amount to withdraw from account, has to be positive
        and the balance can't go below 0.
        """
        if amount > 0:
            if amount > self.balance:
                self.balance = 0
            else:
                self.balance = self.balance - amount

    def deposit(self, amount):
        """
        Deposit money to account.

        :param amount: amount to deposit to account, has to be positive
        """
        if amount > 0:
            self.balance = self.balance + amount

    def get_balance(self):
        """
        Get account balance.

        :return: balance in double form
        """
        return self.balance

    def get_name(self):
        """
        Get account owner name.

        :return: owner name in string form
        """
        return self.name
