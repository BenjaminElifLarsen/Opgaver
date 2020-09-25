using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystemCore
{
    public class ControlEvents : EventArgs
    {
        public class CreateWareEventArgs
        {
            public CreateWareEventArgs()
            {
            }
        }

        public class AddEventArgs
        {
            public AddEventArgs(string ID, int amountToAdd)
            {
                this.ID = ID;
                this.AmountToAdd = amountToAdd;
            }
            public string ID { get; set; }
            public int AmountToAdd { get; set; }
        }

        public class RemoveEventArgs
        {
            public RemoveEventArgs(string ID, int amountToRemove)
            {
                this.ID = ID;
                this.AmountToRemove = amountToRemove;
            }
            public string ID { get; set; }
            public int AmountToRemove { get; set; }
        }

        public class KeyEventArgs
        {
            public KeyEventArgs(ConsoleKey key)
            {
                Key = key;
            }
            public ConsoleKey Key { get; set; }
        }


    }
}
