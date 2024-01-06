public class WithdrawTransaction
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

    public WithdrawTransaction(Account account, decimal amount)
    {
        _account = account;
        _amount = amount;
    }

    public void Execute()
    {
        if (_executed)
        {
            throw new Exception("Cannot execute this transaction as it has already been executed");
        }

        _executed = true;
        _succeeded = _account.Withdraw(_amount);
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

        if (_account.Deposit(_amount))
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
            Console.WriteLine($"You have withdrawn {_amount} from {_account.Name}'s account");
        }
        else
        {
            Console.WriteLine($"Your withdrawral was unsuccessful");
            if (_reversed)
                Console.WriteLine("Withdraw was reversed.");
        }
    }
}