using System.Collections.ObjectModel;
using System.Windows;

namespace Company
{
    public class ClientsRepository
    {
        private Window w;
        public ObservableCollection<Client> ClientsList { get; set; }

        public ClientsRepository(Window W)
        {
            this.w = W;
            this.ClientsList = new ObservableCollection<Client>();
        }
    }
}
