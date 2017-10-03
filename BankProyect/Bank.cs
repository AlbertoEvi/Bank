using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProyect
{
    public interface IBank//metodos necesarios en cada cuenta
    {
        IAccount FindAccount(string name);
        bool StoreAccount(IAccount account);
        bool RemoveAccount(IAccount account);

        void WriteL(StreamWriter textOut, CustomerAccount account);
        bool Save(string fileName, CustomerAccount account);

        CustomerAccount Load(string filename);

        void DoEdit(CustomerAccount account);

        void Transfer(CustomerAccount acc1, CustomerAccount acc2);
    }
    public class Bank:IBank
    {
        Dictionary<string, IAccount> accountDictionary = new Dictionary<string, IAccount>();//creamos diccionario de las cuentas

        public IAccount FindAccount(string name)//encontrar una cuenta en la lista dictionary
        {
            if (accountDictionary.ContainsKey(name))
                return accountDictionary[name];
            else
                return null;
        }

        public bool StoreAccount(IAccount account)//almacenar una cuenta en la lista dictionary
        {
            if (accountDictionary.ContainsKey(account.GetName()))
                return false;

            accountDictionary.Add(account.GetName(), account);
            return true;
        }

        public bool RemoveAccount(IAccount account)//eliminar una cuenta de la lista dictionary
        {
            if (accountDictionary.ContainsKey(account.GetName()))
            {
                accountDictionary.Remove(account.GetName());
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Account removed correctly");
                Console.ForegroundColor = ConsoleColor.White;
                return true;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The account isn't listed");
            Console.ForegroundColor = ConsoleColor.White;
            return false;
        }

        public virtual void WriteL(StreamWriter textOut, CustomerAccount account)//recibe el stream y escribe el nombre y el balance dentro
        {
            textOut.WriteLine(account.GetName());
            textOut.WriteLine(account.GetBalance());
        }

        public virtual bool Save(string fileName, CustomerAccount account)//recibe el nombre del archivo donde la cuenta va a ser almacenada y crea un stream
        {
            StreamWriter textOut = null;
            try
            {
                textOut = new StreamWriter(fileName);
                WriteL(textOut, account);
            }
            catch
            {
                return false;
            }
            finally
            {
                if (textOut != null)
                {
                    textOut.Close();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Save failed");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            return true;
        }

        public CustomerAccount Load(string filename)//Lee el valor del balance y el name desde el stream y crea un nuevo CustomerAccount con los datos leidos
        {
            CustomerAccount result = null;
            TextReader textIn = null;

            try
            {
                textIn = new System.IO.StreamReader(filename);
                string nameText = textIn.ReadLine();
                Console.WriteLine(nameText);
                string balanceText = textIn.ReadLine();
                decimal balance = decimal.Parse(balanceText);
                Console.WriteLine(balance);
                result = new CustomerAccount(nameText, balance);
            }
            catch
            {
                return null;
            }
            finally
            {
                if (textIn != null)
                    textIn.Close();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("File loaded correctly");
                Console.ForegroundColor = ConsoleColor.White;
            }
            return result;
        }

        public void DoEdit(CustomerAccount account)//permite realizar un cambio de cualquier campo de una cuenta
        {
            string command;
            decimal amount;
            do
            {
                Console.WriteLine("Editing account for {0}", account.GetName());
                Console.WriteLine("    Enter name to edit name");
                Console.WriteLine("    Enter balance to edit balance");
                if (account.GetType() == typeof(BabyAccount))
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
                        account.EditName(account);
                        break;
                    case "balance":
                        account.EditBalance(account);
                        break;
                    case "parentname":
                        ((BabyAccount)account).EditParentName((BabyAccount)account);
                        break;
                    case "pay":
                        Console.Write("Enter amount : ");
                        amount = decimal.Parse(Console.ReadLine());
                        account.PayInFunds(amount);
                        break;
                    case "draw":
                        Console.Write("Enter amount : ");
                        amount = decimal.Parse(Console.ReadLine());
                        account.WithdrawFunds(amount);
                        break;
                    case "show":
                        Console.WriteLine("Datos de la cuenta");
                        Console.WriteLine(" - Name: "+account.GetName());
                        Console.WriteLine(" - Balance: "+account.GetBalance());
                        if (account.GetType() == typeof(BabyAccount))
                        {
                            Console.WriteLine(" - Parent name: "+((BabyAccount)account).GetParentName());
                            break;
                        }
                        break;

                }
            } while (command != "exit");
        }

        public void Transfer(CustomerAccount acc1, CustomerAccount acc2)//realizar tranferencia
        {
            Console.WriteLine("Enter the amount to transfer: ");
            decimal transf = decimal.Parse(Console.ReadLine());

            if (acc1.GetBalance() < transf)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Not enough funds to make the transference");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                acc1.WithdrawFunds(transf);
                acc2.PayInFunds(transf);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Tranference done correctly");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    } 
    public class BabyBank : Bank
    {
        public BabyAccount babyLoad(string filename)//cargar cuenta 'baby'
        {
            BabyAccount result = null;
            TextReader textIn = null;

            try {
                textIn = new System.IO.StreamReader(filename);
                string nameText = textIn.ReadLine();
                string balanceText = textIn.ReadLine();
                string parent = textIn.ReadLine();
                decimal balance = decimal.Parse(balanceText);
                Console.WriteLine(nameText);
                Console.WriteLine(balance);
                Console.WriteLine(parent);
                result = new BabyAccount(nameText, balance, parent);
            }
            catch
            {
                return null;
            }
            finally
            {
                if (textIn != null)
                    textIn.Close();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("File loaded correctly");
                Console.ForegroundColor = ConsoleColor.White;
            }
            return result;
        }
        
        public override void WriteL(StreamWriter textOut, CustomerAccount account)
        {
           if (account.GetType() != typeof(BabyAccount))
               throw new ArgumentException(nameof(account));

           base.WriteL(textOut, account);
           textOut.WriteLine(((BabyAccount)account).GetParentName());
        }
    }
}
