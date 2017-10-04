using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Bank2
{
    #region Interfaces

    public interface IBank
    {
        IAccount TryFindAccount(string name);
        bool TryStoreAccount(CustomerAccount account);
        bool TryRemoveAccount(CustomerAccount account);

        void WriteLinesOnFile(StreamWriter textOut, CustomerAccount account);
        bool SaveAccountOn(string fileName, CustomerAccount account);

        CustomerAccount GenerateAccountFrom(string filename);

        void Transfer(CustomerAccount acc1, CustomerAccount acc2);
    }

    #endregion
    #region BankStuff
    public class Bank : IBank
    {
        Dictionary<string, IAccount> accountDictionary = new Dictionary<string, IAccount>();

        #region AccountOperations

        public IAccount TryFindAccount(string name)
        {
            if (accountDictionary.ContainsKey(name))
                return accountDictionary[name];
            return null;
        }

        public bool TryStoreAccount(CustomerAccount account)
        {
            lock (this)
            {
                if (accountDictionary.ContainsKey(account.Name))
                {
                    Console.WriteLine("Account it's stored yet");
                    return false;
                }
                accountDictionary.Add(account.Name, account);
                Console.WriteLine("Account added correctly");
                return true;
            }
        }

        public bool TryRemoveAccount(CustomerAccount account)
        {
            if (accountDictionary.ContainsKey(account.Name))
            {
                accountDictionary.Remove(account.Name);
                Console.WriteLine("Account removed correctly");
                return true;
            }
            Console.WriteLine("The account isn't listed");
            return false;
        }

        #region SubFunctions 


        public virtual void WriteLinesOnFile(StreamWriter textOut, CustomerAccount account)
        {
            textOut.WriteLine(account.Name);
            textOut.WriteLine(account.Balance);
        }

        #endregion

        public virtual bool SaveAccountOn(string fileName, CustomerAccount account)
        {
            using (StreamWriter textOut = new StreamWriter(fileName))
            {
                try
                {
                    WriteLinesOnFile(textOut, account);
                    textOut.Close();
                    Console.WriteLine("Save failed");
                }
                catch (ArgumentNullException ex)
                {
                    throw ex;
                }
                return true;
            }
        }

        public virtual CustomerAccount GenerateAccountFrom(string filename)
        {
            CustomerAccount result = null;
            using (StreamReader textIn = new StreamReader(filename))
            {
                try
                {
                    string nameText = textIn.ReadLine();

                    string balanceText = textIn.ReadLine();
                    decimal balance = decimal.Parse(balanceText);

                    Console.WriteLine(nameText);
                    Console.WriteLine(balance);

                    result = new CustomerAccount(nameText, balance);

                    textIn.Close();
                    Console.WriteLine("File loaded correctly");
                }
                catch
                {
                    throw new ArgumentNullException();
                }

                return result;
            }
        }

        public void Transfer(CustomerAccount acc1, CustomerAccount acc2)
        {
            Console.WriteLine("Enter the amount to transfer: ");
            decimal transf = decimal.Parse(Console.ReadLine());

            if (acc1.Balance < transf)
                Console.WriteLine("Not enough funds to make the transference");

            acc1.TryWithdrawFunds(transf);

            if (acc1.TryWithdrawFunds(transf) == true)
            {
                acc2.TryPayInFunds(transf);

                if (acc2.TryPayInFunds(transf) == false)
                    acc1.TryPayInFunds(transf);

                Console.WriteLine("Tranference done correctly");
            }
        }
        #endregion
    }
    #endregion
}
