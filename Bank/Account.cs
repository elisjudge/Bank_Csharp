using System;

public class Account
{
    private string _name;
    private decimal _balance;

    public Account(string name, decimal startingBalance)
    {
        _name = name;
        _balance = startingBalance;
    }

    public string Name
    {
        get { return _name; }
    }

    public decimal Balance
    {
        get { return _balance; }
    }

    public void Deposit(decimal amountToAdd)
    {
        Console.WriteLine($"Depositing ${amountToAdd}");
        _balance = _balance + amountToAdd;
    }

    public void Withdraw(decimal amountToSubtract)
    {
        Console.WriteLine($"Withdrawing ${amountToSubtract}");
        _balance = _balance - amountToSubtract;
    }

    public void Print()
    {
        Console.WriteLine($"Account Name: {Name}, Account Balance: ${Balance}");
    }
}