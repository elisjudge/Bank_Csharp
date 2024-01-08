public class WithdrawTransaction : Transaction
{
    private Account _account;
    private bool _succeeded = false;

    public override bool Succeeded
    {
        get {return _succeeded;}
    }

    public WithdrawTransaction(Account account, decimal amount) : base(amount)
    {
        _account = account;
    }

    public override void Execute()
    {
        base.Execute();
        _succeeded = _account.Withdraw(_amount);
    }

    public override void Rollback()
    {
        base.Rollback();

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

    public override void Print()
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