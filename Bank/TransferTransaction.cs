public class TransferTransaction
{
    private Account _fromAccount;
    private Account _toAccount;
    private decimal _amount;
    private WithdrawTransaction _theWithdraw;
    private DepositTransaction _theDeposit;
    private bool _executed;
    private bool _reversed;

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

    public bool Succeeded
    {
        get 
        {
            if (_theWithdraw.Succeeded && _theDeposit.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount)
    {
        _fromAccount = fromAccount;
        _toAccount = toAccount;
        _amount = amount;

        _theWithdraw = new WithdrawTransaction(_fromAccount, _amount);
        _theDeposit = new DepositTransaction(_toAccount, _amount);
    }

    public void Execute()
    {
        if (_executed)
        {
            throw new Exception("Transaction cannot be executed. It has aleady been executed.");
        }

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

    public void Rollback()
    {
        if (!_executed)
        {
            throw new Exception("Cannot rollback this transaction. It has not been executed.");
        }

        if (_reversed)
        {
            throw new Exception("Cannot rollback this transaction. It has already been reversed.");
        }

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

    public void Print()
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