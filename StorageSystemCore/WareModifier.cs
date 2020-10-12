﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystemCore
{
    /// <summary>
    /// Contains functions to modify ware amount, add wares and remove ware
    /// </summary>
    public class WareModifier
    {
        /// <summary>
        /// Adds <paramref name="amount"/> to the ware with the id <paramref name="ID"/>.
        /// </summary>
        /// <param name="ID">The id of the ware.</param>
        /// <param name="amount">The amount to add.</param>
        public static void AddToWare(string ID, int amount)
        {
            if (!SQLCode.SQLControl.DatabaseInUse)
                Publisher.PubWare.AddToWare(ID, amount);
            else
                SQLCode.StoredProcedures.AddToWareAmountSP($"'{ID}'",amount);
        }

        /// <summary>
        /// Removes <paramref name="amount"/> from the ware with the id <paramref name="ID"/>.
        /// </summary>
        /// <param name="ID">The id of the ware.</param>
        /// <param name="amount">The amount to remove.</param>
        public static void RemoveFromWare(string ID, int amount)
        {
            if (!SQLCode.SQLControl.DatabaseInUse)
                Publisher.PubWare.RemoveFromWare(ID, amount);
            else
                SQLCode.StoredProcedures.RemoveFromWareAmountSP($"'{ID}'", amount);
        }

        /// <summary>
        /// Removes a ware. Returns true if the item was found and removed else false
        /// </summary>
        /// <param name="ID">The ID of the ware to remove</param>
        /// <returns>Returns true if the item was found and removed else false</returns>
        public static bool RemoveWare(string ID)
        {
            if (Support.Confirmation()) {
                if (!SQLCode.SQLControl.DatabaseInUse)
                    return WareInformation.RemoveWare(ID);
                SQLCode.StoredProcedures.RunDeleteWareSP($"'{ID}'");
                return true;
            }
            return false;
        }

        public static void ModifyWare(string ID) //move a lot of this function into smaller functions to make it easier to read
        {
            Type type; 
            if (!SQLCode.SQLControl.DatabaseInUse)
            {
                type = Publisher.PubWare.GetTypeFromWare(ID); //part of this can be moved into a function
                List<string[]> attributes = WareInformation.FindSearchableAttributes(type); // +
                string[] options = new string[attributes.Count + 1]; // +
                for (int i = 0; i < options.Length - 1; i++) // +
                    options[i] = attributes[i][0]; // +
                options[options.Length - 1] = "Exit"; // +
                Dictionary<string, object> informations = WareInformation.GetWareInformation(ID, out List<Type> valueTypes);
                byte? answer;
                do
                {
                    answer = Visual.MenuRun(options, "Select entry to modify"); 
                    if (answer != options.Length - 1) { 
                        object oldValue = informations[options[(byte)answer]];
                        if (valueTypes[(byte)answer].IsValueType)
                        {
                            Type wareCreatorType = typeof(WareCreator); //basically the same as the one in WareCreator
                            MethodInfo foundMethod = wareCreatorType.GetMethod("EnterExtraInformation", BindingFlags.NonPublic | BindingFlags.Static);
                            MethodInfo genericVersion = foundMethod.MakeGenericMethod(valueTypes[(byte)answer]);
                            try
                            {
                                object newValue = genericVersion.Invoke(null, new object[] {$"Old Value was {oldValue ?? "Null"}. Enter new Value: " });
                                Publisher.PubWare.AlterWare(ID, newValue, options[(byte)answer]);
                            }
                            catch (Exception e)
                            {
                                Reporter.Report(e);
                                Console.Clear();
                                Console.WriteLine("Could not convert: " + e.InnerException.Message);
                                Support.WaitOnKeyInput();
                            }
                        }
                        else
                        { //string and arrays goes here. Most likely also lists and such
                            if(valueTypes[(byte)answer].FullName == "System.String") { 
                                Console.Clear();
                                Console.WriteLine($"Old Value was {oldValue ?? "Null"}. Enter new Value: ");
                                string newValue = Console.ReadLine();
                                Publisher.PubWare.AlterWare(ID, newValue, options[(byte)answer]);
                            }
                            else if (valueTypes[(byte)answer].BaseType.Name == "Array")
                            {
                                List<object> objectList = new List<object>();
                                string[] addValueOptions = new string[] {"Enter Value","Done" };
                                byte? valueAnswer;
                                do
                                {
                                    valueAnswer = Visual.MenuRun(addValueOptions, "Array Data Entry");
                                    if(valueAnswer == 0)
                                    {
                                        if (!valueTypes[(byte)answer].Name.Contains("String") && !valueTypes[(byte)answer].Name.Contains("Char"))
                                        { //non-string
                                            Type wareCreatorType = typeof(WareCreator); //basically the same as the one in WareCreator
                                            MethodInfo foundMethod = wareCreatorType.GetMethod("EnterExtraInformation", BindingFlags.NonPublic | BindingFlags.Static);
                                            MethodInfo genericVersion = foundMethod.MakeGenericMethod(Type.GetType(valueTypes[(byte)answer].FullName.Remove(valueTypes[(byte)answer].FullName.Length-2,2))); //code inside of Type.GetType(...) converts an array type to a non-array type
                                            try
                                            {
                                                object newValue = genericVersion.Invoke(null, new object[] { $"Old Value was {oldValue ?? "Null"}. Enter new Value: " });
                                                objectList.Add(newValue);
                                            }
                                            catch (Exception e)
                                            {
                                                Reporter.Report(e);
                                                Console.Clear();
                                                Console.WriteLine("Could not convert: " + e.InnerException.Message);
                                                Support.WaitOnKeyInput();
                                            }
                                        }
                                        else if (valueTypes[(byte)answer].Name.Contains("Char"))
                                        { //char

                                        }
                                        else
                                        { //string

                                        }
                                    }
                                } while (valueAnswer != addValueOptions.Length - 1);
                                object[] objectArray = objectList.ToArray();
                                Publisher.PubWare.AlterWare(ID, objectArray, options[(byte)answer]);
                            }
                        }
                    }
                } while (answer != options.Length - 1);

            }
            else
            { //get the type of the ID, find the attributes of that ID

            }
        }

        /// <summary>
        /// Used for unit testing 
        /// </summary>
        /// <param name="ID"></param>
        public static void RemoveWareTesting(string ID)
        {
            WareInformation.RemoveWare(ID);
        }
    }
}
