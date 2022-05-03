using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Company
{
    /// <summary>
    /// Логика взаимодействия для ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ClientsRepository clientsRepository;
        public ChangesRepository changesRepository;

        Manager newManager = new Manager();
        List<string> textList = new List<string>();
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

        public ManagerWindow()
        {
            InitializeComponent();

            clientItems.ItemsSource = null;
            recordItems.ItemsSource = null;

            position = Employee.Position.Manager;

            clientsRepository.ClientsList = newManager.GetClietsItemSourse(position);
            if (clientsRepository.ClientsList != null)
            {
                clientItems.ItemsSource = clientsRepository.ClientsList;

                changesRepository.ChangesList = newManager.GetChangesItemSourse();
                if (changesRepository.ChangesList != null)
                {
                    clientItems.ItemsSource = changesRepository.ChangesList;
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Файл пока пустой. Добавьте клиента и сохраните.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        /// <summary>
        /// Выбор строки из списка клиентов
        /// </summary>
        private void Row_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            client = (Client)clientItems.SelectedItem;

            if (client == null)
            {
                return;
            }

            firstName.Visibility = Visibility.Visible;
            lastName.Visibility = Visibility.Visible;
            fathersName.Visibility = Visibility.Visible;
            phone.Visibility = Visibility.Visible;
            passportNumber.Visibility = Visibility.Visible;

            firstName.Text = client.FirstName;
            lastName.Text = client.LastName;
            fathersName.Text = client.FathersName;
            phone.Text = client.Phone.ToString();
            passportNumber.Text = client.PassportNumber;
        }
        /// <summary>
        /// Проверка корректности введенного номера телефона
        /// </summary>
        private void OnPhoneChanged(object sender, TextChangedEventArgs e)
        {
            var bc = new BrushConverter();
            bool parse = long.TryParse(phone.Text, out long phoneNumber);
            if (parse)
            {
                Regex regex = new Regex(@"\d\d\d\d\d\d\d\d\d\d\d$");

                if (regex.IsMatch(phone.Text) && phoneNumber > 10000000000 && phoneNumber < 99999999999)
                {
                    phone.Background = (Brush)bc.ConvertFrom("#abffbe");
                }
                else
                {
                    phone.Background = (Brush)bc.ConvertFrom("#ffa4a4");
                }
            }
            else
            {
                phone.Background = (Brush)bc.ConvertFrom("#ffa4a4");
                return;
            }
        }
        /// <summary>
        /// Проверка корректности введенного номера паспорта
        /// </summary>
        private void OnPassportChanged(object sender, TextChangedEventArgs e)
        {
            string pNumber = passportNumber.Text.ToString();

            if (!String.IsNullOrEmpty(pNumber))
            {
                Regex regex = new Regex(@"\d\d\d\d-\d\d\d\d\d\d$");
                var bc = new BrushConverter();
                if (regex.IsMatch(passportNumber.Text))
                {
                    passportNumber.Background = (Brush)bc.ConvertFrom("#abffbe");
                }
                else
                {
                    passportNumber.Background = (Brush)bc.ConvertFrom("#ffa4a4");
                }
            }
        }
        /// <summary>
        /// Действия при нажатии кнопки "Изменить"
        /// </summary>
        private void OnClickChange(object sender, RoutedEventArgs e)
        {
            type = 1;

            if (clientItems.SelectedItem != null)
            {
                List<Client> clients = new List<Client>();

                foreach (var item in clientItems.Items)
                {
                    clients.Add(item as Client);
                }

                index = clientItems.SelectedIndex;

                var client = clients.ElementAt(index);

                if (newManager.CheckTextBoxIsNullOrEmpty(firstName.Text, "Имя")) return;
                if (newManager.CheckTextBoxIsNullOrEmpty(lastName.Text, "Фамилия")) return;
                if (newManager.CheckTextBoxIsNullOrEmpty(phone.Text, "Телефон")) return;
                if (newManager.CheckTextBoxIsNullOrEmpty(passportNumber.Text, "Номер паспорта")) return;

                bool parse = newManager.CheckParsePhone(phone.Text, out long phoneNumber);
                if (parse && phoneNumber != 0)
                {
                    if (!newManager.CheckPhoneMatchesPattern(phone.Text)) return;
                    if (!newManager.CheckPassportMatchesPattern(passportNumber.Text)) return;

                    if (newManager.CheckPhoneExistInBase(clientsRepository.ClientsList, client, phoneNumber)) return;
                    if (newManager.CheckPassportExistInBase(clientsRepository.ClientsList, client, passportNumber.Text)) return;
                }
                else
                {
                    return;
                }

                textList = new List<string>() { lastName.Text, firstName.Text,
                                                fathersName.Text, phone.Text, passportNumber.Text };

                var fieldsList = FieldsChanged(textList, index);
                if (!String.IsNullOrEmpty(fieldsList))
                {
                    var newRecordChange = newManager.NewRecordChange(fieldsList, Change.DataChange.ChangingRecord, position);
                    changesRepository.ChangesList.Add(newRecordChange);
                    clientsRepository.ClientsList.RemoveAt(index);
                    client.FirstName = firstName.Text.Trim();
                    client.LastName = lastName.Text.Trim();
                    client.FathersName = fathersName.Text.Trim();
                    client.Phone = phoneNumber;
                    client.PassportNumber = passportNumber.Text.Trim();
                    clientsRepository.ClientsList.Insert(index, client);
                }
                else
                {
                    return;
                }
                clientItems.ItemsSource = null;
                recordItems.ItemsSource = null;

                clientItems.ItemsSource = clientsRepository.ClientsList;
                recordItems.ItemsSource = changesRepository.ChangesList;
            }
            else
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OnClickAddClient(object sender, RoutedEventArgs e)
        {
            if (SelectPosition.SelectedItem == null)
            {
                MessageBox.Show("Выберите должность", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (SelectPosition.SelectedItem != null)
            {
                user = 1;
                type = 2;

                if (newManager.CheckTextBoxIsNullOrEmpty(firstName.Text, "Имя")) return;
                if (newManager.CheckTextBoxIsNullOrEmpty(lastName.Text, "Фамилия")) return;
                if (newManager.CheckTextBoxIsNullOrEmpty(phone.Text, "Телефон")) return;
                if (newManager.CheckTextBoxIsNullOrEmpty(passportNumber.Text,
                    "Паспорт\" в формате \"1234-567890")) return;

                bool parse = newManager.CheckParsePhone(phone.Text, out long phoneNumber);
                if (parse && phoneNumber != 0)
                {
                    if (!newManager.CheckPhoneMatchesPattern(phone.Text)) return;
                    if (!newManager.CheckPassportMatchesPattern(passportNumber.Text)) return;

                    if (newManager.CheckPhoneExistInBase(clientsList.ClientsList, client, phoneNumber)) return;
                    if (newManager.CheckPassportExistInBase(clientsList.ClientsList, client, passportNumber.Text)) return;
                }
                else
                {
                    return;
                }
                Client newClient = newManager.AddClient(firstName.Text.Trim(),
                               lastName.Text.Trim(), fathersName.Text.Trim(),
                               phoneNumber, passportNumber.Text.Trim());
                clientsList.ClientsList.Add(newClient);

                clientItems.ItemsSource = null;
                recordItems.ItemsSource = null;

                var fieldsAdded = CheckFieldsAdded();
                var newRecordAddClient = newManager.NewRecordAddClient(fieldsAdded, type, user);
                changesList.ChangesList.Add(newRecordAddClient);

                clientItems.ItemsSource = clientsList.ClientsList;
                recordItems.ItemsSource = changesList.ChangesList;
            }
        }

        public string FieldsChanged(List<string> textList, int index)
        {
            string totalString = "";

            List<string> fieldNames = new List<string>() { "Фамилия ", "Имя ", "Отчество ", "Телефон ", "Паспорт" };

            List<Client> clients = new List<Client>();

            foreach (var item in clientItems.Items)
            {
                clients.Add(item as Client);
            }

            var client = clients.ElementAt(index);

            List<string> clientProperty = new List<string>()
            {
                client.LastName.Trim(), client.FirstName.Trim(),
                client.FathersName.Trim(), client.Phone.ToString().Trim(),
                client.PassportNumber.Trim()
            };

            for (int i = 0; i < clientProperty.Count; i++)
            {
                if (!String.Equals(clientProperty[i], textList[i].Trim()))
                {
                    totalString = string.Concat(totalString, fieldNames[i]);
                }
            }
            return totalString;
        }

        public string CheckFieldsAdded()
        {
            string totalString = "Фамилия Имя Телефон Паспорт";

            var fathers = (TextBox)TextBoxList.Children[2];

            if (!String.IsNullOrEmpty(fathers.Text.Trim()))
            {
                totalString = "Фамилия Имя Отчество Телефон Паспорт";
            }
            return totalString;
        }

        private void OnClickSaveToFiles(object sender, RoutedEventArgs e)
        {
            if (SelectPosition.SelectedItem != null)
            {
                ComboBoxItem cbi = (ComboBoxItem)SelectPosition.SelectedItem;
                name = cbi.Name;
                switch (name)
                {
                    case "Manager":
                        {
                            if (clientsList.ClientsList.Count <= 0)
                            {
                                MessageBox.Show("Список клиентов пока пустой", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            else
                            {
                                rootClients = newManager.ConvertToJsonClients(clientsList.ClientsList);
                            }
                            if (changesList.ChangesList.Count > 0)
                            {
                                rootChanges = newManager.ConvertToJsonChanges(changesList.ChangesList);
                            }

                            newManager.WriteToFile(rootClients, fileNameClients);
                            newManager.WriteToFile(rootChanges, fileNameChanges);

                            MessageBox.Show("Файл сохранен", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        }
                    case "Consultant":
                        {
                            position = true;
                            textFromFileClients = newManager.ReadFromFile(fileNameClients);
                            if (!String.IsNullOrEmpty(textFromFileClients))
                            {
                                clientsList.ClientsList = newManager.ParseJsonClients(textFromFileClients, position);
                                if (clientsList.ClientsList == null || clientsList.ClientsList.Count <= 0)
                                {
                                    MessageBox.Show("Список клиентов пока пустой", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }
                                else
                                {
                                    var keys = phoneChanges.Keys.ToList();

                                    for (int i = 0; i < keys.Count; i++)
                                    {
                                        clientsList.ClientsList.ElementAt(keys[i]).Phone = phoneChanges[keys[i]];
                                    }
                                    rootClients = newManager.ConvertToJsonClients(clientsList.ClientsList);
                                }
                            }
                            if (changesList.ChangesList.Count > 0)
                            {
                                rootChanges = newManager.ConvertToJsonChanges(changesList.ChangesList);
                            }

                            newManager.WriteToFile(rootClients, fileNameClients);
                            newManager.WriteToFile(rootChanges, fileNameChanges);

                            MessageBox.Show("Файл сохранен", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("Должность не определена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        }
                }
            }
            else
            {
                MessageBox.Show("Выберите должность", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnFocusLastName(object sender, RoutedEventArgs e)
        {
            if (lastName.Text == "Фамилия")
            {
                lastName.Text = "";
            }
        }

        private void OnFocusFirstName(object sender, RoutedEventArgs e)
        {
            if (firstName.Text == "Имя")
            {
                firstName.Text = "";
            }
        }

        private void OnFocusFathersName(object sender, RoutedEventArgs e)
        {
            if (fathersName.Text == "Отчество")
            {
                fathersName.Text = "";
            }
        }

        private void OnFocusPhone(object sender, RoutedEventArgs e)
        {
            if (phone.Text == "Телефон")
            {
                phone.Text = "";
            }
        }

        private void OnFocusPassportNumber(object sender, RoutedEventArgs e)
        {
            if (passportNumber.Text == "Паспорт")
            {
                passportNumber.Text = "";
            }
        }
    }
}
