using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Bank2
{
    #region BabyBankStuff
    public class BabyBank : Bank
    {
        #region InheritedMethodsForAccounts
        public override void WriteLinesOnFile(StreamWriter textOut, CustomerAccount account)
        {
            if (account.GetType() != typeof(BabyAccount))
                throw new ArgumentException(nameof(account));

            base.WriteLinesOnFile(textOut, account);
            textOut.WriteLine(((BabyAccount)account).ParentName);
        }
        #endregion
    }
    #endregion
}