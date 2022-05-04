using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Company
{
    interface IGetRecordsFromBase
    {
        /// <summary>
        /// Получение списка клиентов из файла
        /// </summary>
        ObservableCollection<Client> GetClietsItemSourse(Employee.Position position);
        /// <summary>
        /// Получение списка записей из файла
        /// </summary>
        ObservableCollection<Change> GetChangesItemSourse();
    }
}
