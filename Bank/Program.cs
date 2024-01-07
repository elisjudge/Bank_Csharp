using System;

namespace version_4_0
{
    public class Program 
    {
        public static void Main()
        {
            Bank bank = new Bank();
            MenuOption userSelection;

            do
            {
                userSelection = ReadUserOption();

                switch(userSelection)
                {
                    case MenuOption.Add_Account:
                        DoAddAccount(bank);
                        break;
                    case MenuOption.Deposit:
                        DoDeposit(bank);
                        break;
                    case MenuOption.Withdraw:
                        DoWithdraw(bank);
                        break;
                    case MenuOption.Transfer:
                        DoTransfer(bank);
                        break;
                    case MenuOption.Print:
                        DoPrint(bank);
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
            1. Add Account,
            2. Deposit Funds,
            3. Withdraw Funds,
            4. Transfer Funds,
            5. Show Account Balance,
            6. Quit");

            do
            {
                Console.Write("Choose an option [1-6]:");
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
                if (option < 1 || option > 6)
                {
                    Console.WriteLine("Please select a valid number between 1 and 6.");
                }
            } while (option < 1 || option > 6);

            return (MenuOption)(option - 1);
        }

        private static Account? FindAccount(Bank fromBank)
        {
            string? _name;
            do
            {
                Console.Write("Enter account name: ");
                _name = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(_name));

            Account? result = fromBank.GetAccount(_name);
            if (result == null)
            {
                Console.WriteLine($"No account found with {_name}.");
            }
            return result;
        }

        private static void DoAddAccount(Bank toBank)
        {
            string? _accountName;
            do
            {
                Console.Write("Please enter the account name: ");
                _accountName = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(_accountName));

            while (true)
            {
                try
                {
                    Console.Write("Please enter the opening balance: ");
                    decimal openingBalance;
                    openingBalance = Convert.ToDecimal(Console.ReadLine());

                    if (openingBalance < 0)
                    {
                        throw new Exception($"Account balance cannot be negative number.");
                    }
                    toBank.AddAccount(new Account(_accountName, openingBalance));
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }
        }

        private static void DoDeposit(Bank bank)
        {
            Account? account = FindAccount(bank);
            if (account == null)
            {
                return;
            }

            decimal _depositAmount;
            Console.Write($"Please enter the amount you wish to deposit into {account.Name}: ");
            try
            {
                _depositAmount = Convert.ToDecimal(Console.ReadLine());
                DepositTransaction depositTransaction = new(account, _depositAmount);
                bank.ExecuteTransaction(depositTransaction);
                if (!depositTransaction.Succeeded)
                {
                    throw new Exception("Deposit was not successful.");
                }
                depositTransaction.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void DoWithdraw(Bank bank)
        {
            Account? account = FindAccount(bank);
            if (account == null)
            {
                return;
            }
            
            decimal _withdrawAmount;
            Console.Write($"Please enter the amount you wish to withdraw from {account.Name}: ");
            try
            {
                _withdrawAmount = Convert.ToDecimal(Console.ReadLine());
                WithdrawTransaction withdrawTransaction = new(account, _withdrawAmount);
                bank.ExecuteTransaction(withdrawTransaction);
                if (!withdrawTransaction.Succeeded)
                {
                    throw new Exception("Withdraw was not successful.");
                }
                withdrawTransaction.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void DoTransfer(Bank bank)
        {
            try
            {
                Account? fromAccount = FindAccount(bank);
                if (fromAccount == null)
                {
                    return;
                }

                Account? toAccount = FindAccount(bank);
                if (toAccount == null)
                {
                    return;
                }

                if (fromAccount == toAccount)
                {
                    throw new Exception("You cannot transfer between the same account.");
                }
                decimal _transferAmount;
                Console.Write($"What amount will be transferred from {fromAccount.Name} to {toAccount.Name}?: ");
                try
                {
                    _transferAmount = Convert.ToDecimal(Console.ReadLine());
                    TransferTransaction transferTransaction = new(fromAccount, toAccount, _transferAmount);
                    bank.ExecuteTransaction(transferTransaction);
                    if (!transferTransaction.Succeeded)
                    {
                        throw new Exception("Transfer was not successful.");
                    }
                    transferTransaction.Print();              
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void DoPrint(Bank bank)
        {
            Account? account = FindAccount(bank);
            if (account == null)
            {
                return;
            }

            account.Print();
        }

        public enum MenuOption
        {
            Add_Account,
            Deposit,
            Withdraw,
            Transfer,
            Print,
            Quit,
        }
    }   
}
