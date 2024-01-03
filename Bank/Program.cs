using System;

namespace version_2_0
{
    public class Program 
    {
        public static void Main()
        {
            Account account = new Account("John", 100);
            account.Print();
            account.Deposit(100);
            account.Print();
            account.Withdraw(50);
            account.Print();
            account.Deposit(100);
            account.Print();
            account.Withdraw(50);
            account.Print();
        }
    }   
}
