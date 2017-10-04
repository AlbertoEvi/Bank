using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank2
{
    #region BabyAccountStuff
    public class BabyAccount : CustomerAccount
    {
        private string _parentName;

        #region .CtorInherited

        public BabyAccount(string newName, decimal initialBalance, string inParentName) : base(newName, initialBalance)
        {
            _parentName = inParentName;
        }

        #endregion

        #region Properties

        public string ParentName
        {
            set
            {
                if (!IsValidParentName())
                {
                    Console.WriteLine("Name invalid");
                    return;
                }

                this._parentName = value;
            }
            get
            {
                return _parentName;
            }
        }

        #endregion

        #region Validations

        public bool IsValidParentName()
        {
            if (String.IsNullOrWhiteSpace(ParentName))
                return false;

            return true;
        }

        #endregion

        #region BalanceOperations

        public override bool TryWithdrawFunds(decimal amount)
        {
            if (amount > 10)
            {
                return false;
            }
            return base.TryWithdrawFunds(amount);
        }

        #endregion

        #region AccountsMethods

        public override CustomerAccount GenerateAccountFrom(string filename)
        {
            BabyAccount result = null;
            using (StreamReader textIn = new StreamReader(filename))
            {
                try
                {

                    string nameText = textIn.ReadLine();

                    string balanceText = textIn.ReadLine();
                    decimal balance = decimal.Parse(balanceText);

                    string parent = textIn.ReadLine();

                    Console.WriteLine("Name: {0}", nameText);
                    Console.WriteLine("Balance: {0}", balance);
                    Console.WriteLine("Parent Name: {0}", parent);

                    result = new BabyAccount(nameText, balance, parent);

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
    #endregion
}