using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Company
{
    /// <summary>
    /// Логика взаимодействия для ConsultantWindow.xaml
    /// </summary>
    public partial class ConsultantWindow : Window 
    {
        
        public ClientsRepository clientsRepository;
        public ChangesRepository changesRepository;
        Manager newManager = new Manager();
        Consultant newConsultant = new Consultant();
        Client client;
        Dictionary<int, long> phoneChanges = new Dictionary<int, long>();
        string rootClients;
        string rootChanges;

        string textFromFileClients;
        string textFromFileChanges;
        public int index;
        public int type;
        public int user;
        public Employee.Position position;

        public ConsultantWindow()
        {
            InitializeComponent();

            clientItems.ItemsSource = null;
            recordItems.ItemsSource = null;
            phone.Text = "Телефон";
            position = Employee.Position.Consultant;
            clientsRepository.ClientsList = newConsultant.GetClietsItemSourse(position);
            if(clientsRepository.ClientsList != null)
            {
                clientItems.ItemsSource = clientsRepository.ClientsList;
            }
            else
            {
                MessageBox.Show("Нет данных о клиентах", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            changesRepository.ChangesList = newConsultant.GetChangesItemSourse();
            if (changesRepository.ChangesList != null)
            {
                clientItems.ItemsSource = changesRepository.ChangesList;
            }
            else
            {
                MessageBox.Show("Нет данных о новых записях", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Row_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            client = (Client)clientItems.SelectedItem;

            if (client == null)
            {
                return;
            }
            phone.Visibility = Visibility.Visible;
            phone.Text = client.Phone.ToString();
        }
    }
}
