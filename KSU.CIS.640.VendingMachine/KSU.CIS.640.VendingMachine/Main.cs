using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public static class MainClass
    {
        public static void Main(string[] args)
        {
            Run();
        }
        private static void Run()
        {
            VendingMachine vend = new VendingMachine();
            while (vend.Status != VendingMachine.State.OFF)
            {
                if (vend.Status == VendingMachine.State.START)
                {
                    vend.Start();
                }
                if (vend.Status == VendingMachine.State.INSERT)
                {
                    vend.InsertMoney();
                }
                if (vend.Status == VendingMachine.State.SELECT)
                {
                    vend.Select();
                }
                if (vend.Status == VendingMachine.State.DISPENSE)
                {
                    vend.DispenseSelection();
                }
                if (vend.Status == VendingMachine.State.CHANGE)
                {
                    vend.DispenseChange();
                }
                if (vend.Status == VendingMachine.State.STOCK)
                {
                    vend.Restock();
                }
            }
        }
    }
}
