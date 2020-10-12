using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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

        public delegate void getTypeEventHandler(object sender, ControlEvents.GetTypeEventArgs args);
        public event getTypeEventHandler RaiseGetTypeEvent;

        public delegate void alterWareEventHandler(object sender, ControlEvents.AlterValueEventArgs args);
        public event alterWareEventHandler RaiseAlterWareEvent;

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

        public Type GetTypeFromWare(string ID)
        {
            try 
            { 
                return OnGetTypeFromWare(new ControlEvents.GetTypeEventArgs(ID));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected Type OnGetTypeFromWare(ControlEvents.GetTypeEventArgs e)
        {
            getTypeEventHandler eventHandler = RaiseGetTypeEvent;
            if(eventHandler != null) {
                eventHandler.Invoke(this,e);
                try 
                { 
                    return e.GetType(e.ID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return null;

        }

        public void AlterWare(string ID, object newValue, string propertyName)
        {
            OnAlterWare(new ControlEvents.AlterValueEventArgs(ID, newValue, propertyName));
        }

        public void AlterWare(string ID, object[] newValues, string propertyName)
        {
            OnAlterWare(new ControlEvents.AlterValueEventArgs(ID, newValues, propertyName));
        }

        protected void OnAlterWare(ControlEvents.AlterValueEventArgs e)
        {
            alterWareEventHandler eventHandler = RaiseAlterWareEvent;
            if (eventHandler != null)
                eventHandler.Invoke(this, e);
        }

    }

}
