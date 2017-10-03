using System;
using BankProyect;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProyect
{
    class Program
    {
        public static void Main(string[] args)//testeos
        {
            Console.ForegroundColor = ConsoleColor.White;//cambia el color de la letra en la consola

            Bank ourBank = new Bank();
            BabyBank ourBabyBank = new BabyBank();

            CustomerAccount newAccount = new CustomerAccount("Rob", 1000000);

            if (ourBank.StoreAccount(newAccount))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Account added to bank");
                Console.ForegroundColor = ConsoleColor.White;
            }

            BabyAccount newBabyAccount = new BabyAccount("David", 100,"Rob");

            if (ourBank.StoreAccount(newBabyAccount))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("BabyAccount added to bank");
                Console.ForegroundColor = ConsoleColor.White;
            }

            ourBank.Save("Test.txt", newAccount);
            ourBabyBank.Save("TestB.txt", newBabyAccount);

            CustomerAccount loadAcc = ourBank.Load("Test.txt");
            BabyAccount loadBabyAcc = ourBabyBank.babyLoad("TestB.txt");

            IAccount storedAccount = ourBank.FindAccount("Rob");
            if (storedAccount != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Account found in bank");
                Console.ForegroundColor = ConsoleColor.White;
            }

            IAccount storedBabyAccount = ourBank.FindAccount("David");
            if (storedBabyAccount != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("BabyAccount found in bank");
                Console.ForegroundColor = ConsoleColor.White;
            }


            //----------------------------

            int errorCount = 0;
        
            string reply = loadAcc.ValidateName(null);

            if (reply != "Name parameter null")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Null name test failed");
                Console.ForegroundColor = ConsoleColor.White;
                errorCount++;
            }

            reply = loadAcc.ValidateName("");
            if (reply != "No text in the name")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Empty name test failed");
                Console.ForegroundColor = ConsoleColor.White;
                errorCount++;
            }

            CustomerAccount a = new CustomerAccount("Jim", 50);

            if (a.GetName() != "Jim")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Jim GetName failed");
                Console.ForegroundColor = ConsoleColor.White;
                errorCount++;
            }

            if (errorCount > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There are "+errorCount+" errors");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("There are " + errorCount + " errors");
                Console.ForegroundColor = ConsoleColor.White;
            }

            //----------------------------transferq 

            ourBank.Transfer(loadAcc,loadBabyAcc);

            //----------------------------
            ourBank.DoEdit(a);
            ourBank.DoEdit(loadAcc);
            ourBank.DoEdit(loadBabyAcc);

            Console.ReadLine();//evita desaparicion de la consola
        }
    }
}
