using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank2
{
    public class Program
    {
        public Program() { }

        #region Main

        static void Main(string[] args)
        {
            Bank ourBank = new Bank();
            BabyBank ourBabyBank = new BabyBank();

            CustomerAccount Acc1 = new CustomerAccount("Rob", 1000000);
            BabyAccount BabyAcc1 = new BabyAccount("David", 100, "Rob");
            CustomerAccount Acc2 = new CustomerAccount("Jim", 50);

            ourBank.SaveAccountOn("Test.txt", Acc1);
            ourBabyBank.SaveAccountOn("TestB.txt", BabyAcc1);

            StoreTest(ourBank, Acc1);
            StoreTest(ourBabyBank, BabyAcc1);

            CustomerAccount loadedAcc = ourBank.GenerateAccountFrom("Test.txt");
            CustomerAccount loadedBabyAcc = ourBabyBank.GenerateAccountFrom("TestB.txt");
            BabyAccount loadedBabeAcc = (BabyAccount)loadedBabyAcc;

            FindTest(ourBank);

            Testing(BabyAcc1);
            Testing(Acc2);

            Acc1.DoEdit();
            BabyAcc1.DoEdit();
            Acc2.DoEdit();

            Console.ReadLine();
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

        #endregion
        #region Testing

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
        #endregion
    }
}
