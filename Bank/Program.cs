using System;

namespace version_3_0
{
    public class Program 
    {
        public static void Main()
        {
            MenuOption userSelection;
            Account account_0001 = new("John", 100);
            Account account_0002 = new("Mike", 100);

            do
            {
                userSelection = ReadUserOption();

                switch(userSelection)
                {
                    case MenuOption.Deposit:
                        DoDeposit(account_0001);
                        break;
                    case MenuOption.Withdraw:
                        DoWithdraw(account_0001);
                        break;
                    case MenuOption.Transfer:
                        DoTransfer(account_0001, account_0002);
                        break;
                    case MenuOption.Print:
                        DoPrint(account_0001);
                        DoPrint(account_0002);
                        break;
                    case MenuOption.Quit:
                        Console.WriteLine("Quit");
                        break;
                }
            } while ( userSelection != MenuOption.Quit);
        }

        public static MenuOption ReadUserOption()
        {
            int option;
            Console.WriteLine(@"Type one of the following options:
            1. Deposit Funds,
            2. Withdraw Funds,
            3. Transfer Funds,
            4. Show Account Balance,
            5. Quit");

            do
            {
                Console.Write("Choose an option [1-5]:");
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    Console.WriteLine("Please make sure that you select a valid option");
                    option = -1;
                }
                if (option < 1 || option > 5)
                {
                    Console.WriteLine("Please select a valid number between 1 and 5.");
                }
            } while (option < 1 || option > 5);

            return (MenuOption)(option - 1);
        }

        public static void DoDeposit(Account account)
        {
            decimal depositAmount;
            Console.Write("Please enter the amount you wish to deposit: ");
            try
            {
                depositAmount = Convert.ToDecimal(Console.ReadLine());
                DepositTransaction transaction = new(account, depositAmount);
                transaction.Execute();
                transaction.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DoWithdraw(Account account)
        {
            decimal withdrawAmount;
            Console.Write("Please enter the amount you wish to withdraw: ");
            try
            {
                withdrawAmount = Convert.ToDecimal(Console.ReadLine());
                WithdrawTransaction transaction = new(account, withdrawAmount);
                transaction.Execute();
                transaction.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DoTransfer(Account fromAccount, Account toAccount)
        {
            decimal transferAmount;
            Console.Write($"What amount will you be transferring to {toAccount.Name}?: ");
            try
            {
                transferAmount = Convert.ToDecimal(Console.ReadLine());
                TransferTransaction transaction = new(fromAccount, toAccount, transferAmount);
                transaction.Execute();
                transaction.Print();                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DoPrint(Account account)
        {
            account.Print();
        }

        public enum MenuOption
        {
            Deposit,
            Withdraw,
            Transfer,
            Print,
            Quit,
        }
    }   
}
