using System;
using System.Collections.ObjectModel;

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
