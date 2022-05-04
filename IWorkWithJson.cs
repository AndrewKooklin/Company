using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Company
{
    interface IWorkWithJson
    {
        /// <summary>
        /// Преобразование объектов "Клиент" к json строке
        /// </summary>
        public string ConvertToJsonClients(ObservableCollection<Client> clients);
        /// <summary>
        /// Преобразование объектов "Запись" к json строке
        /// </summary>
        public string ConvertToJsonChanges(ObservableCollection<Change> dataChanges);
        /// <summary>
        /// Приведение json строки к объектам "Клиент"
        /// </summary>
        public ObservableCollection<Client> ParseJsonClients(string root, Employee.Position position);
        /// <summary>
        /// Приведение json строки к объектам "Запись"
        /// </summary>
        public ObservableCollection<Change> ParseJsonChanges(string root);
    }
}
