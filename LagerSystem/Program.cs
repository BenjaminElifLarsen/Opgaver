using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{

    //Krav

    //Det skal være muligt at indsætte objekter
    //    Objekterne skal som minimum have et id, et navn og et antal
    //    Objekterne må gerne indgå i et arvehirarki
    //Det skal være muligt at indsætte, ændre og slette vare i systemet
    //Det skal være muligt at se en liste over varer i databasen
    //Det er ikke nødvendigt at gemme i filer eller databaser

    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    abstract class Ware
    {
        string name;
        string id;
        uint amount;

        public Ware(string name, string id, uint amount)
        {
            this.name = name;
            this.id = id;
            this.amount = amount;
        }

        public uint Amount { get => amount; set => amount = value; }

        public string ID { get => id; }

        protected virtual void Add(uint amount)
        {
            this.amount += amount;
        }

        protected virtual void Remove(uint amount)
        {
            this.amount -= amount;
        }

        protected void AddAmountEventHandler(object sender, ControlEvents.AddEventArgs e)
        {
            if (e.ID == this.ID)
                Add(e.AmountToAdd);
        }

        protected void RemoveAmountEvnetHandler(object sender, ControlEvents.RemoveEventArgs e)
        {
            if (e.ID == this.ID)
                Remove(e.AmountToRemove);
        }

        public void RemoveSubscriptions()
        {

        }


    }

    class Liquids : Ware
    {
        public Liquids(string name, string id, uint amount) : base(name, id, amount) { }


    }

    public class Publisher
    {
        private static WarePublisher warePublisher;

        public Publisher()
        {
            if (warePublisher == null) ;
            warePublisher = new WarePublisher();
        }

        public static WarePublisher PubWare { get => warePublisher; }
    }

    public class WarePublisher
    {
        public delegate void createWareEventHandler(object sender, ControlEvents.CreateWareEventArgs args);
        public event createWareEventHandler RaiseCreateWareEvent;

        public delegate void addEventHandler(object sender, ControlEvents.AddEventArgs args);
        public event addEventHandler RaiseAddEvent;

        public delegate void removeEventHandler(object sender, ControlEvents.RemoveEventArgs args);
        public event removeEventHandler RaiseRemoveEvent;

        public void CreateWare(string name, string id, uint amount)
        {
            OnCreatingWare(new ControlEvents.CreateWareEventArgs(name, id, amount));
        }

        protected virtual void OnCreatingWare(ControlEvents.CreateWareEventArgs e)
        { //calls a function/class that creates a new class 
            createWareEventHandler eventHandler = RaiseCreateWareEvent;
            if (eventHandler != null)
                eventHandler.Invoke(this, e);
        }

        public void AddToWare(string ID, uint amount)
        {
            OnAddingToWare(new ControlEvents.AddEventArgs(ID, amount));
        }

        protected virtual void OnAddingToWare(ControlEvents.AddEventArgs e)
        { //calls a function/class that creates a new class 
            addEventHandler eventHandler = RaiseAddEvent;
            if (eventHandler != null)
                eventHandler.Invoke(this, e);
        }

        public void RemoveFromWare(string ID, uint amount)
        {
            OnRemovingFomWare(new ControlEvents.RemoveEventArgs(ID, amount));
        }

        protected virtual void OnRemovingFomWare(ControlEvents.RemoveEventArgs e)
        { //calls a function/class that creates a new class 
            removeEventHandler eventHandler = RaiseRemoveEvent;
            if (eventHandler != null)
                eventHandler.Invoke(this, e);
        }

    }


    public class ControlEvents : EventArgs
    {
        public class CreateWareEventArgs
        {
            public CreateWareEventArgs(string name, string ID, uint amount)
            {
                Name = name;
                this.ID = ID;
                Amount = amount;
            }
            public string Name { get; set; }
            public string ID { get; set; }
            public uint Amount { get; set; }
        }

        public class AddEventArgs
        {
            public AddEventArgs(string ID,  uint amountToAdd)
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
