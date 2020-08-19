using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    public class ControlEvents : EventArgs
    {
        public class CreateWareEventArgs
        {
            public CreateWareEventArgs(string name, string ID, uint amount, string type = "None")
            {
                Name = name;
                this.ID = ID;
                Amount = amount;
                Type = type;
            }
            public string Name { get; set; }
            public string ID { get; set; }
            public uint Amount { get; set; }
            public string Type { get; set; }
        }

        public class AddEventArgs
        {
            public AddEventArgs(string ID, uint amountToAdd)
            {
                this.ID = ID;
                this.AmountToAdd = amountToAdd;
            }
            public string ID { get; set; }
            public uint AmountToAdd { get; set; }
        }

        public class RemoveEventArgs
        {
            public RemoveEventArgs(string ID, uint amountToRemove)
            {
                this.ID = ID;
                this.AmountToRemove = amountToRemove;
            }
            public string ID { get; set; }
            public uint AmountToRemove { get; set; }
        }

    }
}
