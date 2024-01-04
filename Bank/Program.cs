using System;

namespace version_2_0
{
    public class Program 
    {
        public static void Main()
        {
            MenuOption userSelection;
            Account account = new Account("John", 100);

            do
            {
                userSelection = ReadUserOption();

                switch(userSelection)
                {
                    case MenuOption.Deposit:
                        DoDeposit(account);
                        break;
                    case MenuOption.Withdraw:
                        DoWithdraw(account);
                        break;
                    case MenuOption.Print:
                        DoPrint(account);
                        break;
                    case MenuOption.Quit:
                        Console.WriteLine("Quit");
                        break;
                }
            } while ( userSelection != MenuOption.Quit);
        }

        public static MenuOption ReadUserOption()
        {
            int option = 0;
            Console.WriteLine(@"Type one of the following options:
            1. Deposit Funds,
            2. Withdraw Funds,
            3. Show Account Balance,
            4. Quit");

            do
            {
                Console.Write("Choose an option [1-4]:");
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Please make sure that you select a valid option");
                }
            } while (option < 1 || option > 4);

            return (MenuOption)(option - 1);
        }

        public static void DoDeposit(Account account)
        {
            decimal depositAmount = 0;
            bool depositSuccess = false;

            do
            {
                Console.Write("Please enter the amount that you wish to deposit: ");
                try
                {
                    depositAmount = Convert.ToDecimal(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Please ensure that you are entering a valid deposit.");
                }
            } while (depositSuccess == false);

            depositSuccess = account.Deposit(depositAmount);

            if (depositSuccess == true)
            {
                Console.WriteLine("Deposit was successful.");
            }

            if (depositSuccess == false)
            {
                Console.WriteLine("Deposit was not successful. Please use non-negative values.");
            }
        }

        public static void DoWithdraw(Account account)
        {
            decimal withdrawAmount = 0;
            bool withdrawSuccess = false;

            do
            {
                Console.Write("Please enter the amount you wish to withdraw: ");
                try
                {
                    withdrawAmount = Convert.ToDecimal(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Please ensure that you are entering a valid withdrawal.");
                }
            } while (withdrawSuccess == false);

            withdrawSuccess = account.Withdraw(withdrawAmount);

            if (withdrawSuccess == true)
            {
                Console.WriteLine("Withdrawal was successful.");
            }

            if (withdrawSuccess == false)
            {
                Console.WriteLine("Withdraw was not successful.");
                Console.WriteLine("Please use non-negative values.");
                Console.WriteLine("Withdrawal cannot exceed account balance.");
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
            Print,
            Quit,
        }
    }   
}
