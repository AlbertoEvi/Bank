using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank2
{
    #region Program
    public class Program
    {
        #region GlobalVars

        private static decimal amount;

        private static Bank ourBank;
        private static BabyBank ourBabyBank;

        private static CustomerAccount Acc1;
        private static BabyAccount BabyAcc1;
        private static CustomerAccount Acc2;

        #endregion

        #region .ctor

        public Program() { }

        #endregion
        
        #region Main

        public static void Main(string[] args)
        {
            ourBank = new Bank();
            ourBabyBank = new BabyBank();
            Acc1 = new CustomerAccount("Rob", 1000000);
            BabyAcc1 = new BabyAccount("David", 100, "Rob");
            Acc2 = new CustomerAccount("Jim", 50);

            ourBank.SaveAccountOn("Test.txt", Acc1);
            ourBabyBank.SaveAccountOn("TestB.txt", BabyAcc1);

            StoreTest(ourBank, Acc1);
            StoreTest(ourBabyBank, BabyAcc1);

            CustomerAccount loadedAcc = Acc1.GenerateAccountFrom("Test.txt");
            CustomerAccount loadedBabyAcc = BabyAcc1.GenerateAccountFrom("TestB.txt");
            BabyAccount loadedBabeAcc = (BabyAccount)loadedBabyAcc;

            FindTest(ourBank);

            Testing(BabyAcc1);
            Testing(Acc2);

            amount = 30;
            Transfer(Acc1,Acc2,amount,ourBank);
            
            DoEdit(Acc1);
            DoEdit(BabyAcc1);
            DoEdit(Acc2);

            Console.ReadLine();
        }

        #endregion

        #region Testing

        public static string Transfer(CustomerAccount acc1, CustomerAccount acc2, decimal transfer, Bank bank)
        {
            string mssg = null;
            bank.Transfer(acc1, acc2, transfer);
            mssg = "Transference done correctly";
            Console.WriteLine(mssg);
            return mssg;
        }

        public static string StoreTest(Bank bank, CustomerAccount acc)
        {
            string mssg = null;

            if (bank.TryStoreAccount(acc))
            {
                mssg = "Account added to bank";
                Console.WriteLine(mssg);
            }

            mssg = "Failed adding the account";
            Console.WriteLine(mssg);
            return mssg;
        }

        public static string FindTest(Bank bank)
        {
            string mssg = null;

            IAccount storedAccount = bank.TryFindAccount("Rob");
            if (storedAccount != null)
            {
                mssg = "Account found in bank";
                Console.WriteLine(mssg);
            }

            IAccount storedBabyAccount = bank.TryFindAccount("David");
            if (storedBabyAccount != null)
            {
                mssg = "BabyAccount found in bank";
                Console.WriteLine(mssg);
            }

            mssg = "Failed finding the account";
            Console.WriteLine(mssg);
            return mssg;
        }

        public static string Testing(CustomerAccount acc)
        {
            int errorCount = 0;
            string mssg = null;
            string reply = (acc.IsValidName()).ToString();

            if (reply == "false")
            {
                Console.WriteLine("Name test failed");
                errorCount++;
            }

            if (acc.Name != "Jim")
            {
                Console.WriteLine("Jim GetName failed");
                errorCount++;
            }

            mssg = "There are " + errorCount + " errors";
            Console.WriteLine(mssg);
            return mssg;
        }
        public static void DoEdit(CustomerAccount acc)
        {
            string command;
            BabyBank bbank = new BabyBank();
            do
            {
                Console.WriteLine("Editing account for {0}", acc.Name);
                Console.WriteLine("    Enter name to edit name");
                Console.WriteLine("    Enter balance to edit balance");
                if (acc.GetType() == typeof(BabyAccount))
                {
                    Console.WriteLine("    Enter parentname to edit parent name");
                }
                Console.WriteLine("    Enter pay to pay in funds");
                Console.WriteLine("    Enter draw to draw out funds");
                Console.WriteLine("    Enter show to see all the account data");
                Console.WriteLine("    Enter exit to exit program");
                Console.WriteLine("    Enter stop to interrupt the program");
                Console.Write("Enter command : ");

                command = Console.ReadLine();
                command = command.Trim();
                command = command.ToLower();
                switch (command)
                {
                    case "name":
                        NameCase(acc);
                        break;
                    case "balance":
                        BalanceCase(acc);
                        break;
                    case "parentname":
                        ParentNameCase(acc,bbank);
                        break;
                    case "pay":
                        PayCase(acc);
                        break;
                    case "draw":
                        DrawCase(acc);
                        break;
                    case "show":
                        ShowCase(acc);
                        break;
                    case "exit":
                        continue;
                    case "stop":
                        Environment.Exit(255);
                        break;
                    default:
                        Console.WriteLine("The command inserted isn't valid, insert one of those: name, balance, parentname, pay, draw, show, exit or stop");
                        break;
                }
            } while (command != "exit");
        }
        public static void NameCase(CustomerAccount acc)
        {
            Console.Write("Enter new name : ");
            acc.Name = Console.ReadLine();
        }
        public static void BalanceCase(CustomerAccount acc)
        {
            Console.Write("Enter new balance : ");
            acc.Balance = decimal.Parse(Console.ReadLine());
        }
        public static void ParentNameCase(CustomerAccount acc, BabyBank bbank)
        {
            Console.Write("Account data : ");
            CustomerAccount cbaby = acc.GenerateAccountFrom("TestB.txt");
            var cbabe = (BabyAccount)cbaby;
            Console.Write("Enter new  parent name : ");
            cbabe.ParentName = Console.ReadLine();
        }
        public static void PayCase(CustomerAccount acc)
        {
            Console.Write("Enter amount : ");
            decimal amount = decimal.Parse(Console.ReadLine());
            acc.TryPayInFunds(amount);
        }
        public static void DrawCase(CustomerAccount acc)
        {
            Console.Write("Enter amount : ");
            decimal amount = decimal.Parse(Console.ReadLine());
            acc.TryWithdrawFunds(amount);
        }
        public static void ShowCase(CustomerAccount acc)
        {
            Console.WriteLine("Datos de la cuenta {0}", acc.Name);
            Console.WriteLine(" - Balance: {0}",acc.Balance);
            if (acc.GetType() == typeof(BabyAccount))
            {
                var acc1 = (BabyAccount)acc;
                Console.WriteLine(" - Parent name: {0}", acc1.ParentName);
            }
        }
        #endregion
    }
    #endregion
}
