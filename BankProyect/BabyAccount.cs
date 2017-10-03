using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank2
{
    public class BabyAccount : CustomerAccount
    {
        private string _parentName;

        #region ConstructorInherited

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
                if (IsValidParentName())
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

        public override bool WithdrawFunds(decimal amount)
        {
            if (amount > 10)
            {
                return false;
            }
            return base.WithdrawFunds(amount);
        }

        #endregion
    }
}