"""Bank Account with transactions."""


class BankAccount:
    """Represent a bank account."""

    def __init__(self, name: str, balance: float):
        """
        Class constructor. Each account has owner's name and balance.

        :param name: Account owner's name
        :param balance: Accounts balance. Must be positive.
        """
        self.name = name
        if balance < 0:
            self.balance = 0
        else:
            self.balance = balance

    def withdraw(self, amount: float):
        """
        Withdraw money from bank account.

        :param amount: Amount of money to withdraw.
        :return: bool
        """
        if amount > self.balance:
            return False
        if amount <= 0:
            return False
        else:
            self.balance = self.balance - amount
            return True

    def deposit(self, amount: float):
        """
        Deposit money to bank account.

        :param amount: Amount of money to deposit.
        :return:
        """
        if amount < 0:
            pass
        else:
            self.balance = self.balance + amount

    def get_balance(self):
        """
        Get balance of account by owner's name.

        :return: Account balance.
        """
        return self.balance

    def get_name(self):
        """
        Get account owner's name.

        :return: Name of the owner.
        """
        return self.name

    def transfer(self, target, amount: float, fee=0.01):
        """
        Transfer money from one account to another.

        :param target: Money receiver.
        :param amount: Amount of money to transfer.
        :param fee: Fee for transfer.
        :return: bool
        """
        try:
            if self.get_name() == target.get_name():
                fee = fee / 2
            if self == target:
                fee = 0
            if self.withdraw(amount + fee * amount) or amount == 0:
                target.deposit(amount)
                return True
            return False
        except AttributeError:
            return False
