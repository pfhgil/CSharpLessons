using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice11
{
    public class BudgetRecord
    {
        public string Name = "";
        public string TypeName = "";
        private int _money;
        public int Money
        {
            get 
            {
                return _money;
            }
            set
            {
                _isIncome = value > 0;

                _money = Math.Abs(value);
            }
        }

        private bool _isIncome = false;
        public bool IsIncome 
        {
            get
            {
                return _isIncome;
            }
        }

        public DateTime DateTime;

        public BudgetRecord() { }

        public BudgetRecord(string name, string typeName, int money, DateTime dateTime)
        {
            this.Name = name;
            this.TypeName = typeName;
            this.Money = money;
            this.DateTime = dateTime;
        }
    }
}
