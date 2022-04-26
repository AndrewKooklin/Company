using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company
{
    class DataListChanges
    {
        private MainWindow w;
        public ObservableCollection<DataChange> DataChanges { get; set; }

        public DataListChanges(MainWindow W)
        {
            this.w = W;
            this.DataChanges = new ObservableCollection<DataChange>();

        }
    }
}
