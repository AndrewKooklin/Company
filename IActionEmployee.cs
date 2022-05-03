using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Company
{
    interface IActionEmployee
    {
        ObservableCollection<Client> GetClietsItemSourse(Employee.Position position);
        ObservableCollection<Change> GetChangesItemSourse();
    }
}
