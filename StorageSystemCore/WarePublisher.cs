using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystemCore
{
    /// <summary>
    /// Contains the events, handlers and event class related to ware manipulation. 
    /// </summary>
    public class WarePublisher
    {
        public delegate void createWareEventHandler(object sender, ControlEvents.CreateWareEventArgs args);
        public event createWareEventHandler RaiseCreateWareEvent;

        public delegate void addEventHandler(object sender, ControlEvents.AddEventArgs args);
        public event addEventHandler RaiseAddEvent;

        public delegate void removeEventHandler(object sender, ControlEvents.RemoveEventArgs args);
        public event removeEventHandler RaiseRemoveEvent;

        /// <summary>
        /// Creates an event that all classes that are subscriben to RaiseCreateWareEvent will trigger on.
        /// </summary>
        public void CreateWare()
        {
            OnCreatingWare(new ControlEvents.CreateWareEventArgs());
        }

        /// <summary>
        /// Sends the event out to all subscribers. 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCreatingWare(ControlEvents.CreateWareEventArgs e)
        { //calls a function/class that creates a new class 
            createWareEventHandler eventHandler = RaiseCreateWareEvent;
            if (eventHandler != null)
                eventHandler.Invoke(this, e);
        }

        /// <summary>
        /// Adds unit <paramref name="amount"/> to the ware with the specific <paramref name="ID"/>.
        /// </summary>
        /// <param name="ID">The ID of the ware</param>
        /// <param name="amount">The amount of units to add.</param>
        public void AddToWare(string ID, int amount)
        {
            OnAddingToWare(new ControlEvents.AddEventArgs(ID, amount));
        }

        /// <summary>
        /// Sends out the event to all objects that are subscribed to RaiseAddEvent. 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnAddingToWare(ControlEvents.AddEventArgs e)
        { 
            addEventHandler eventHandler = RaiseAddEvent;
            if (eventHandler != null)
                eventHandler.Invoke(this, e);
        }

        /// <summary>
        /// Remove unit <paramref name="amount"/> from the ware with the specific <paramref name="ID"/>.
        /// </summary>
        /// <param name="ID">The ID of the ware</param>
        /// <param name="amount">The amount of units to add.</param>
        public void RemoveFromWare(string ID, int amount)
        {
            OnRemovingFomWare(new ControlEvents.RemoveEventArgs(ID, amount));
        }

        /// <summary>
        /// Sends out the event to all objects that are subscribed to RaiseRemoveEvent
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnRemovingFomWare(ControlEvents.RemoveEventArgs e)
        { 
            removeEventHandler eventHandler = RaiseRemoveEvent;
            if (eventHandler != null)
                eventHandler.Invoke(this, e);
        }

    }

}
