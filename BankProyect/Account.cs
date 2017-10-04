using System;
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
        
    }
    #endregion
    #region Accounts

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
                var name = this._name;
                this._name = value;
                if (!IsValidName())
                {
                    this._name = name;
                    Console.WriteLine("Name not valid");
                }
                else
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
                var balance = this._balance;
                this._balance = value;
                if (!IsValidName())
                {
                    this._balance = balance;
                    Console.WriteLine("Balance not valid");
                }
                else
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
            if (amount <= 0)
            {
                return false;
            }
            Balance += amount;
            Console.WriteLine(Balance);
            return true;
        }
        #endregion
    }
}
#endregion