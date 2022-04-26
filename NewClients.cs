using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Company
{
    public class NewClients
    {
        private MainWindow w;
        public ObservableCollection<Client> Clients { get; set; }

        public NewClients(MainWindow W)
        {
            this.w = W;
            this.Clients = new ObservableCollection<Client>();
            
            for (int i = 1; i < 11; i++)
            {
                Client client = new Client(i);
                this.Clients.Add(client);
            }
        }
    }
}
