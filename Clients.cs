using System.Collections.ObjectModel;

namespace Company
{
    public class Clients
    {
        private MainWindow w;
        public ObservableCollection<Client> ClientsList { get; set; }

        public Clients(MainWindow W)
        {
            this.w = W;
            this.ClientsList = new ObservableCollection<Client>();
        }
    }
}
