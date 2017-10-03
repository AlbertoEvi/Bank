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

    public class Bank : IBank
    {
        Dictionary<string, IAccount> accountDictionary = new Dictionary<string, IAccount>();

        #region AccountOperations

        public IAccount TryFindAccount(string name)
        {
            if (accountDictionary.ContainsKey(name))
                return accountDictionary[name];

            throw new ArgumentException("Account not finded on our register");
        }

        public bool TryStoreAccount(CustomerAccount account)
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
                catch
                {
                    throw new ArgumentNullException();
                }
                return true;
            }
        }

        public virtual CustomerAccount GenerateAccountFrom(string filename)
        {
            CustomerAccount result = null;
            TextReader textIn = null;
            try
            {
                textIn = new StreamReader(filename);
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

        public void Transfer(CustomerAccount acc1, CustomerAccount acc2)
        {
            Console.WriteLine("Enter the amount to transfer: ");
            decimal transf = decimal.Parse(Console.ReadLine());

            if (acc1.Balance < transf)
                Console.WriteLine("Not enough funds to make the transference");

            acc1.WithdrawFunds(transf);

            if (acc1.WithdrawFunds(transf) == true)
            {
                acc2.PayInFunds(transf);

                if (acc2.PayInFunds(transf) == false)
                    acc1.PayInFunds(transf);

                Console.WriteLine("Tranference done correctly");
            }
        }
        #endregion
    }
}
