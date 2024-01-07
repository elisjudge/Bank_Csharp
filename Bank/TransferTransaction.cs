public class TransferTransaction : Transaction
{
    private Account _fromAccount;
    private Account _toAccount;
    private WithdrawTransaction _theWithdraw;
    private DepositTransaction _theDeposit;

    public override bool Succeeded
    {
        get 
        {
            if (_theWithdraw.Succeeded && _theDeposit.Succeeded)
                return true;      
            else
                return false;
        }
    }

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
    {
        _fromAccount = fromAccount;
        _toAccount = toAccount;
        
        _theWithdraw = new WithdrawTransaction(_fromAccount, _amount);
        _theDeposit = new DepositTransaction(_toAccount, _amount);
    }

    public override void Execute()
    {
        base.Execute();

        _theWithdraw.Execute();
        if (_theWithdraw.Succeeded)
        {
            _theDeposit.Execute();
            if (_theDeposit.Succeeded)
            {
                _executed = true;
            }
            else
            {
                _theWithdraw.Rollback();
            }
        }
        else
        {
            throw new Exception("Cannot execute the deposit as the withdrawal was not successful");
        }
    }

    public override void Rollback()
    {
        base.Rollback();

        if (_theWithdraw.Succeeded)
        {
            _theWithdraw.Rollback();
        }

        if (_theDeposit.Succeeded)
        {
            _theDeposit.Rollback();
        }

        if (_theWithdraw.Reversed && _theDeposit.Reversed)
        {
            _reversed = true;
        }
    }

    public override void Print()
    {
        if (_theWithdraw.Succeeded && _theDeposit.Succeeded)
        {
            Console.WriteLine($"Transfer of {_amount} from {_fromAccount.Name}'s account to {_toAccount.Name}'s account was successful.");
            _theWithdraw.Print();
            _theDeposit.Print();
        }
        else
        {
            Console.WriteLine("Transfer was not successful.");
            if (_reversed)
                Console.WriteLine("Transfer was reversed.");
        }
    }
}