using System;

public abstract class Transaction 
{
    protected decimal _amount;
    protected bool _executed = false;
    protected bool _reversed = false;
    private DateTime _dateStamp;

    public bool Executed
    {
        get {return _executed;}
    }

    public bool Reversed
    {
        get {return _reversed;}
    }

    public DateTime DateStamp
    {
        get {return _dateStamp;}
    }

    public abstract bool Succeeded
    {
        get;
    }

    public Transaction(decimal amount)
    {
        _amount = amount;
    }

    public abstract void Print();

    public virtual void Execute()
    {
        if (_executed)
        {
            throw new Exception("Cannot execute this transaction as it has already been executed.");
        }
        _executed = true;
        _dateStamp = DateTime.Now;
    }

    public virtual void Rollback()
    {
        if (!_executed)
        {
            throw new Exception("Cannot reverse a transaction that has not already been executed");
        }

        if (_reversed)
        {
            throw new Exception("Cannot reverse a transaction that has already been reversed");
        }
    }
}