public class DepositTransaction : Transaction
{
    private Account _account;  
    private bool _succeeded = false;

    public override bool Succeeded
    {
        get {return _succeeded;}
    }
    
    public DepositTransaction(Account account, decimal amount) : base(amount)
    {
        _account = account;
    }

    public override void Execute()
    {
        base.Execute();
        _succeeded = _account.Deposit(_amount);
    }

    public override void Rollback()
    {
        base.Rollback();

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

    public override void Print()
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