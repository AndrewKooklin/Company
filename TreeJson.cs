using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company
{
    public class TreeJson
    {
        public ObservableCollection<Client> ClientsList { get; set; } =
            new ObservableCollection<Client>();
    }
}
