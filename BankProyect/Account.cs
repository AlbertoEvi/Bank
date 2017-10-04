using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank2
{
    #region Interfaces

    public interface IAccount
    {
        bool IsValidName();
        bool IsValidBalance();

        bool TryPayInFunds(decimal amount);
        bool TryWithdrawFunds(decimal amount);

        CustomerAccount GenerateAccountFrom(string filename);
            
    }
    #endregion
    #region AccountsStuff

    public class CustomerAccount : IAccount
    {
        private string _name;
        private decimal _balance;

        #region .Ctors 

        public CustomerAccount(string newName, decimal initialBalance)
        {
            _name = newName;
            _balance = initialBalance;
        }

        #endregion

        #region Properties

        public string Name
        {
            set
            {
                if (!IsValidName())
                {
                    Console.WriteLine("Name invalid");
                    return;
                }

                this._name = value;
            }
            get
            {
                return _name;
            }
        }

        public decimal Balance
        {
            set
            {
                if (!IsValidBalance())
                {
                    Console.WriteLine("Balance invalid");
                    return;
                }

                this._balance = value;
            }
            get
            {
                return _balance;
            }
        }

        #endregion

        #region Validations

        public bool IsValidName()
        {
            if (String.IsNullOrWhiteSpace(Name)&&String.IsNullOrWhiteSpace(Name))
                return false;

            return true;
        }

        public bool IsValidBalance()
        {
            if (String.IsNullOrWhiteSpace(Balance.ToString()))
                return false;

            return true;
        }

        #endregion

        #region BalanceOperations

        public virtual bool TryWithdrawFunds(decimal amount)
        {
            if (Balance < amount)
            {
                return false;
            }
            Balance -= amount;
            Console.WriteLine(Balance);
            return true;
        }

        public bool TryPayInFunds(decimal amount)
        {
            bool IsAmountPositive = amount > 0;
            if (IsAmountPositive)
                Balance += amount;
            Console.WriteLine(Balance);
            return IsAmountPositive;
        }
        #endregion

        #region AccountsMethods

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
                catch (ArgumentNullException ex)
                {
                    throw ex;
                }

                return result;
            }
        }

        #endregion
    }
}
#endregion