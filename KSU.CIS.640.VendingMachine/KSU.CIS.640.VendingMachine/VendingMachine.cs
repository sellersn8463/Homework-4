using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class VendingMachine
    {

        public enum State
        {
            START, INSERT, SELECT, DISPENSE, CHANGE, OFF, STOCK
        }


        public State Status { get; set; }
        public Dictionary<int, StockItem> Stock { get; private set; }
        public double Balance { get; private set; }
        public double MaxCost { get; private set; }
        public int Selection { get; private set; }

        public VendingMachine()
        {
            Stock = new Dictionary<int, StockItem>();
            LoadStock();
            Status = State.START;
        }

        public void Restock()
        {
            Console.WriteLine("ReStocking...");
            List<string> items = ReadFile();
            for (int i = 0; i < items.Count; i++)
            {
                string[] item = items[i].Split(',');
                Stock.Add(i, new StockItem(item[0],
                        Convert.ToDouble(item[1]),
                        Convert.ToInt32(item[2])));
                if (Stock[i].cost > MaxCost)
                {
                    MaxCost = Stock[i].cost;
                }
            }
            Status = State.START;
        }

        public void LoadStock()
        {
            Console.WriteLine("Loading initial Stock...");
            List<string> items = ReadFile();
            for (int i = 0; i < items.Count; i++)
            {
                string[] item = items[i].Split(',');
                Stock.Add(i, new StockItem(item[0],
                        Convert.ToDouble(item[1]),
                        Convert.ToInt32(item[2])));
                if (Stock[i].cost > MaxCost)
                {
                    MaxCost = Stock[i].cost;
                }
            }
        }

        public List<string> ReadFile()
        {            
            try
            {
                List<string> result = new List<string>();
                FileStream file = new FileStream(GetFileName(), FileMode.Open, FileAccess.Read);
                using (StreamReader sr = new StreamReader(file))
                {
                    string st;
                    while ((st = sr.ReadLine()) != null)
                    {
                        result.Add(st);
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }            
        }

        public string GetFileName()
        {
            Console.WriteLine("Enter the Stock's full file path: ");
            return Console.ReadLine(); 
        }

        public void InsertMoney()
        {
            Console.WriteLine("Enter money (.05, .10, .25, 1): ");
            Balance +=  Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Total: $" + Balance);
            if (Balance >= MaxCost)
            {
                Status = State.SELECT;
            }
        }

        public void Select()
        {
            Console.WriteLine("\nPlease select a drink, or (r)efund or (d)isplay choices: ");
            string input = Console.ReadLine();
            int choice = 0;
            try
            {
                choice = Convert.ToInt32(input);
                if (Stock[choice].count > 0)
                {
                    Status = State.DISPENSE;
                    Selection = choice;
                    return;
                }
                Console.WriteLine("Invalid Selection");
            }
            catch (Exception e)
            {
                if (input[0] == 'r')
                {
                    Status = State.CHANGE;
                }
                else if (input[0] == 'd')
                {
                    Display();
                }
                else
                {
                    Console.WriteLine("Invalid Selection");
                }
            }
            Selection = -1;
        }

        private void Display()
        {
            for (int i = 0; i < Stock.Count; i++)
            {
                Console.WriteLine(i + ": " + Stock[i].name + "(" + Stock[i].count + ") @$" + Stock[i].cost);
            }
            Console.WriteLine();
        }

        public void Start()
        {
            Console.WriteLine("\n\nWelcome!\n");
            Display();
            Console.Write("Press (e)xit, (r)eStock, or anything else to continue: ");
            string input = Console.ReadLine();
            if (input[0] == 'r')
            {
                Status = State.STOCK;
            }
            else if (input[0] == 'e')
            {
                Status = State.OFF;
            }
            else
            {
                Status = State.INSERT;
            }
        }

        public void DispenseSelection()
        {
            Console.WriteLine("Dispensing your " + Stock[Selection].name);
            Balance -= Stock[Selection].cost;
            Stock[Selection].count--;
            if (Balance == 0)
            {
                Status = State.CHANGE;
            }
            else
            {
                Status = State.START;
            }
        }

        public void DispenseChange()
        {
            Console.WriteLine("Here is your change back!");
            Console.WriteLine("Dispensed: $" + Balance);
            Balance = 0;
            Status = State.START;
        }
    }

}
