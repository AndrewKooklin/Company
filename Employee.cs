using System;

namespace Company
{
    public abstract class Employee
    {
        public readonly string fileNameClients = "\\Clients.json";
        public readonly string fileNameChanges = "\\Changes.json";

        protected Employee()
        {
        }

        public enum Position
        {
            Consultant,
            Manager
        }

        /// <summary>
        /// Создание новой записи
        /// </summary>
        /// <returns>new Change</returns>
        public Change NewRecord(string totalString, Change.DataChange dataChange, Employee.Position position)
        {
            Change newDataChange = new Change(totalString, dataChange, position);

            return newDataChange;
        }
    }
}
