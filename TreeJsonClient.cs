using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company
{
    public class TreeJsonClient
    {
        public TreeJsonClient(ObservableCollection<Client> clientsList)
        {
            ClientsList = new ObservableCollection<Client>();
            ClientsList = clientsList;
        }

        public ObservableCollection<Client> ClientsList { get; set; }

    }
}
