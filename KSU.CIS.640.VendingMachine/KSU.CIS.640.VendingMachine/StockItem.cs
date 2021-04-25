using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class StockItem
    {
        public double cost;
        public int count = 0;
        public string name = "default";
        public StockItem(string name, double cost, int count)
        {
            this.cost = cost;
            this.count = count;
            this.name = name;
        }
    }

}
