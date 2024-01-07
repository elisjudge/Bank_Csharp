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

    public bool Deposit(decimal amountToAdd)
    {
        if (amountToAdd > 0)
        {
            _balance += amountToAdd; 
            return true; 
        }
        return false;
    }

    public bool Withdraw(decimal amountToSubtract)
    {
        if (amountToSubtract <= _balance && amountToSubtract > 0)
        {
            _balance -= amountToSubtract;
            return true;
        }
        return false;
    }

    public void Print()
    {
        Console.WriteLine($"Account Name: {Name}, Account Balance: ${Balance}");
    }
}