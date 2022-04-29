using System;
using System.Collections.ObjectModel;

namespace Company
{
    class TreeJsonChanges
    {
        public TreeJsonChanges(ObservableCollection<Change> dataChangeList)
        {
            DataChangeList = new ObservableCollection<Change>();
            DataChangeList = dataChangeList;
        }

        public ObservableCollection<Change> DataChangeList { get; set; }
    }
}
