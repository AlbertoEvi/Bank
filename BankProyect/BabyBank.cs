using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Bank2
{
    public class BabyBank : Bank
    {
        #region InheritedMethodsForAccounts
        public override CustomerAccount GenerateAccountFrom(string filename)
        {
            BabyAccount result = null;
            TextReader textIn = null;

            try
            {
                textIn = new StreamReader(filename);

                string nameText = textIn.ReadLine();

                string balanceText = textIn.ReadLine();
                decimal balance = decimal.Parse(balanceText);

                string parent = textIn.ReadLine();

                Console.WriteLine("Name: {0}",nameText);
                Console.WriteLine("Balance: {0}", balance);
                Console.WriteLine("Parent Name: {0}", parent);

                result = new BabyAccount(nameText, balance, parent);

                textIn.Close();
                Console.WriteLine("File loaded correctly");
            }
            catch
            {
                throw new ArgumentNullException();
            }
            return result;
        }

        public override void WriteLinesOnFile(StreamWriter textOut, CustomerAccount account)
        {
            if (account.GetType() != typeof(BabyAccount))
                throw new ArgumentException(nameof(account));

            base.WriteLinesOnFile(textOut, account);
            textOut.WriteLine(((BabyAccount)account).ParentName);
        }
        #endregion
    }
}