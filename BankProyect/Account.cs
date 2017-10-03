using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProyect
{
    public interface IAccount//metodos necesarios en cada cuenta
    {
        void PayInFunds(decimal amount);
        bool WithdrawFunds(decimal amount);
        decimal GetBalance();
        string GetName();

        bool SetBalance(decimal balance);
        bool SetName(string name);

        string ValidateName(string name);
        string ValidateBalance(decimal balance);

        void EditName(CustomerAccount account);
        void EditBalance(CustomerAccount account);

    }
    public class CustomerAccount : IAccount //cuenta cliente normal
    {
        private string name;
        private decimal balance = 0;

        public CustomerAccount(string newName, decimal initialBalance)//constructor de cuenta de cliente
        {
            name = newName;
            balance = initialBalance;
        }

        public virtual bool WithdrawFunds(decimal amount)//sacar fondos
        {
            if (balance < amount)
            {
                return false;
            }
            balance -= amount;
            Console.WriteLine(balance);
            return true;
        }

        public void PayInFunds(decimal amount)//ingresar dinero
        {
            balance += amount;
            Console.WriteLine(balance);
        }

        public decimal GetBalance()//obtener saldo
        {
            return balance;
        }

        public string GetName()//obtener nombre 
        {
            return name;
        }

        public bool SetName(string Iname)//establecer nombre
        {
            string reply = ValidateName(Iname);
            if (reply.Length < 0)
            {
                return false;
            }
            this.name = Iname.Trim();
            return true;
        }

        public bool SetBalance(decimal balance)//establecer saldo
        {
            string reply = ValidateBalance(balance);
            if (reply.Length < 0)
            {
                return false;
            }
            this.balance = balance;
            return true;
        }

        public string ValidateName(string name)//validar nombre
        {
            if (name == null) {
                return "Name parameter null";
            }
            string trimmedName = name.Trim();
            if (trimmedName.Length == 0) {
                return "No text in the name";
            }
            return "";
        }

        public string ValidateBalance(decimal balance)//validar saldo
        {
            if (balance.ToString() == null){
                return "Balance parameter null";
            }
            return "";
        }

        public void EditName(CustomerAccount account)//editar nombre
        {
            string newName;
            Console.WriteLine("Name Edit");
            while (true)
            {
                Console.Write("Enter new name : ");
                newName = Console.ReadLine();
                string reply;
                reply = account.ValidateName(newName);

                if (reply.Length == 0)
                {
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid name : " + reply);
                Console.ForegroundColor = ConsoleColor.White;
            }
            account.SetName(newName);
        }

        public void EditBalance(CustomerAccount account)//editar saldo
        {
            decimal newBalance;
            Console.WriteLine("Balance Edit");
            while (true)
            {
                Console.Write("Enter new balance value : ");
                newBalance = decimal.Parse(Console.ReadLine());
                string reply;
                reply = account.ValidateBalance(newBalance);

                if (reply.Length == 0)
                {
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid balance : " + reply);
                Console.ForegroundColor = ConsoleColor.White;
            }
            account.SetBalance(newBalance);
        }
    }
    public class BabyAccount : CustomerAccount//cuenta 'baby'
    {
        private string parentName;

        public BabyAccount(string newName, decimal initialBalance, string inParentName) : base(newName, initialBalance) {//constructor basado en el de CustomerAccount
            parentName = inParentName;
        }

        public bool SetParentName(string Pname)//establecer nombre de la cuenta padre
        {
            string reply = ValidateName(Pname);
            if (reply.Length < 0)
            {
                return false;
            }
            this.parentName = Pname.Trim();
            return true;
        }

        public string GetParentName()//obtener nombre de la cuenta 'padre'
        {
            return parentName;
        }

        public void EditParentName(BabyAccount account)//editar nombre
        {
            string newName;
            Console.WriteLine("Parent Name Edit");
            while (true)
            {
                Console.Write("Enter new parent name : ");
                newName = Console.ReadLine();
                string reply;
                reply = account.ValidateName(newName);

                if (reply.Length == 0)
                {
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid name : " + reply);
                Console.ForegroundColor = ConsoleColor.White;
            }
            account.SetParentName(newName);
        }

        public override bool WithdrawFunds(decimal amount)//sacar fondos 'baby'
        {
            if (amount > 10)
            {
                return false;
            }
            return base.WithdrawFunds(amount);
        }
    }
}
