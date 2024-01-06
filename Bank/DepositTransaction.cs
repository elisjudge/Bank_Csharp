public class DepositTransaction
{
    private Account _account;
    private decimal _amount;
    private bool _executed = false;
    private bool _succeeded = false;
    private bool _reversed = false;

    public bool Succeeded
    {
        get
        {
            return _succeeded;
        }
    }
    
    public bool Executed 
    {
        get
        {
            return _executed;
        }
    }
    
    public bool Reversed
    {
        get
        {
            return _reversed;
        }
    }

    public DepositTransaction(Account account, decimal amount) 
    {
        _account = account;
        _amount = amount;
    }

    public void Execute()
    {
        if (_executed)
        {
            throw new Exception("Cannot execute this transaction as it has already been executed.");
        }
        _executed = true;
        _succeeded = _account.Deposit(_amount);
    }

    public void Rollback()
    {
        if (!_executed)
        {
            throw new Exception("Cannot reverse a transaction that has not already been executed");
        }

        if (_reversed)
        {
            throw new Exception("Cannot reverse a transaction that has already been reversed");
        }

        if (_account.Withdraw(_amount))
        {
            _reversed = true;
            _executed = false;
            _succeeded = false;
        }
        else
        {
            _reversed = false;
            _executed = true;
            _succeeded = true;
        }
    }

    public void Print()
    {
        if (_succeeded)
        {
            Console.WriteLine($"You have deposited {_amount} into {_account.Name}'s Account");
        }
        else
        {
            Console.WriteLine($"Your deposit was unsuccessful");
            if (_reversed)
                Console.WriteLine("Deposit was reversed.");
        }
    }
}