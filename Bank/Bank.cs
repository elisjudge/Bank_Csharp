using System;

public class Bank
{
    private List<Account> _accounts;
    private List<Transaction> _transactions;

    public Bank()
    {
        _accounts = new List<Account>();
        _transactions = new List<Transaction>();
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

    public void ExecuteTransaction(Transaction transaction)
    {
        transaction.Execute();
        _transactions.Add(transaction);
    }

    public void PrintTransactionHistory()
    {
        foreach (Transaction transaction in _transactions)
        {
            transaction.Print();
        }
    }
}