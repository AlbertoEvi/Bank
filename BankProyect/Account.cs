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

        bool PayInFunds(decimal amount);
        bool WithdrawFunds(decimal amount);

        void DoEdit();
    }
    #endregion
    #region Accounts

    public class CustomerAccount : IAccount
    {
        private string _name;
        private decimal _balance;

        #region Constructors 

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
                if (IsValidName())
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
                if (IsValidBalance())
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
            if (String.IsNullOrWhiteSpace(Name))
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

        public virtual bool WithdrawFunds(decimal amount)
        {
            if (Balance < amount)
            {
                return false;
            }
            Balance -= amount;
            Console.WriteLine(Balance);
            return true;
        }

        public bool PayInFunds(decimal amount)
        {
            if (amount <= 0)
            {
                return false;
            }
            Balance += amount;
            Console.WriteLine(Balance);
            return true;
        }

        public void DoEdit()
        {
            string command;
            decimal amount;
            BabyBank bbank = new BabyBank();
            do
            {
                Console.WriteLine("Editing account for {0}", Name);
                Console.WriteLine("    Enter name to edit name");
                Console.WriteLine("    Enter balance to edit balance");
                if (GetType() == typeof(BabyAccount))
                {
                    Console.WriteLine("    Enter parentname to edit parent name");
                }
                Console.WriteLine("    Enter pay to pay in funds");
                Console.WriteLine("    Enter draw to draw out funds");
                Console.WriteLine("    Enter show to see all the account data");
                Console.WriteLine("    Enter exit to exit program");
                Console.Write("Enter command : ");

                command = Console.ReadLine();
                command = command.Trim();
                command = command.ToLower();
                switch (command)
                {
                    case "name":
                        Console.Write("Enter new name : ");
                        Name = Console.ReadLine();
                        break;
                    case "balance":
                        Console.Write("Enter new balance : ");
                        Balance = decimal.Parse(Console.ReadLine());
                        break;
                    case "parentname":
                        Console.Write("Enter new  parent name : ");
                        CustomerAccount cbaby = bbank.GenerateAccountFrom("TestB.txt");
                        var cbabe = (BabyAccount)cbaby;
                        cbabe.ParentName = Console.ReadLine();
                        break;
                    case "pay":
                        Console.Write("Enter amount : ");
                        amount = decimal.Parse(Console.ReadLine());
                        PayInFunds(amount);
                        break;
                    case "draw":
                        Console.Write("Enter amount : ");
                        amount = decimal.Parse(Console.ReadLine());
                        WithdrawFunds(amount);
                        break;
                    case "show":
                        Console.WriteLine("Datos de la cuenta {0}", Name);
                        Console.WriteLine("$ - Balance: {Balance}");
                        if (GetType() == typeof(BabyAccount))
                        {
                            Console.WriteLine("$ - Parent name: {ParentName}");
                            break;
                        }
                        break;
                    default:
                        Console.WriteLine("The command inserted isn't valid");
                        break;
                }
            } while (command != "exit");
        }
        #endregion
    }
}
#endregion