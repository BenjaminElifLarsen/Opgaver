using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ChatApp
{
    static class Messages
    {
        static public void AddMessage()
        {
            Reporter.Log($"Entered Add Message");
            Console.Clear();

            string message = "";
            do
            {
                Console.Clear();
                message = Console.ReadLine().Trim();
            } while (message == "");
            string time = DateTime.UtcNow.ToString("G", new CultureInfo("da-DK"));
            if (RegexControl.ContainsSingleQuouteMark(message))
                message = Support.SanitiseSingleQuotes(message);
            SQLControl.SQLAddMessage(message, time);
        }

        static public void RemoveMessage()
        {
            Reporter.Log($"Entered Remove Message");
            Console.Clear();
            string toRemove = "";
            do
            {
                Console.Clear();
                Console.Write("Enter Message_Information entry condition: Where ");
                toRemove = Console.ReadLine().Trim();
            } while (toRemove == "");

            if (!RegexControl.ContainsForbiddenWords(toRemove))
            {
                SQLControl.SQLRemoveMessage(toRemove);
            }
            else
            {
                Reporter.Log($"Where condition {toRemove} contains forbidden words. Written by user {UserDirectory.User.Name}");
                Support.FoundForbiddenWord();
            }
        }

        static public void UpdateMessage()
        {
            Reporter.Log($"Entered Update Message");
            Console.Clear();
            string columnToUpdate = "";
            do
            {
                Console.Clear();
                Console.Write("Enter Message_Information entry condition: Column = ");
                columnToUpdate = Console.ReadLine().Trim();
            } while (columnToUpdate == "");
            if (RegexControl.ContainsSingleQuouteMark(columnToUpdate))
            {
                columnToUpdate = Support.SanitiseSingleQuotes(columnToUpdate);
            }
            string valueToUpdate = "";
            do
            {
                Console.Clear();
                Console.Write("Enter Message_Information entry condition: New Value = ");
                valueToUpdate = Console.ReadLine().Trim();
            } while (valueToUpdate == "");
            if (RegexControl.ContainsSingleQuouteMark(valueToUpdate))
                valueToUpdate = Support.SanitiseSingleQuotes(valueToUpdate);
            string whereToUpdate = "";
            do
            {
                Console.Clear();
                Console.Write("Enter Message_Information entry condition: Where ");
                whereToUpdate = Console.ReadLine().Trim();
            } while (whereToUpdate == "");
            if (RegexControl.ContainsSingleQuouteMark(whereToUpdate))
                whereToUpdate = Support.SanitiseSingleQuotes(whereToUpdate);
            SQLControl.SQLAlterMessage(columnToUpdate, valueToUpdate, whereToUpdate);
        }

        static public void SeeMessage()
        {
            Reporter.Log($"Entered See menu");
            Console.Clear();
            //string columns = "";
            //do
            //{
            //    Console.Clear();
            //    Console.Write("Enter Columns ");
            //    columns = Console.ReadLine().Trim();
            //    Console.Clear();
            //} while (columns == "");
            //SQLControl.SQLGetMessages(columns);
            SQLControl.SQLGetMessagesAndDisplay();
            Console.Write("Press a Key");
            Console.Read();
        }

    }


}
