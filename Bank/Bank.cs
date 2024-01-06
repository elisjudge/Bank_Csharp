using System;

public class Bank
{
    private List<Account> _accounts;

    public Bank()
    {
        _accounts = new List<Account>();
    }

    public void AddAccount(Account account)
    {
        _accounts.Add(account);
    }

    public Account? GetAccount(string name)
    {
        foreach(Account account in _accounts)
        {
            if (account.Name.ToLower().Trim() == name.ToLower().Trim())
            {
                return account;
            }
        }
        return null;
    }

    public void ExecuteTransaction(WithdrawTransaction transaction)
    {
        transaction.Execute();
    }

    public void ExecuteTransaction(DepositTransaction transaction)
    {
        transaction.Execute();        
    }

    public void ExecuteTransaction(TransferTransaction transaction)
    {
        transaction.Execute();        
    }
}