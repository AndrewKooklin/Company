using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Text.RegularExpressions;

namespace Company
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string fileNameClients = "\\Clients.json";
        string fileNameChanges = "\\Changes.json";
        Clients clientsList;
        Changes changesList;
        Manager newManager = new Manager();
        Consultant newConsultant = new Consultant();
        List<string> textList = new List<string>();
        Client client;
        Dictionary<int, long> phoneChanges = new Dictionary<int, long>();
        string rootClients;
        string rootChanges;
        string name;
        string textFromFileClients;
        string textFromFileChanges;
        public int index;
        public int type;
        public int user;
        public bool position = true;

        public MainWindow()
        {
            InitializeComponent();

            clientsList = new Clients(this);
            changesList = new Changes(this);
        }

        private void OnSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectPosition.SelectedItem != null)
            {
                ComboBoxItem cbi = (ComboBoxItem)SelectPosition.SelectedItem;
                name = cbi.Name;
                switch (name)
                {
                    case "Manager":
                        {
                            clientItems.ItemsSource = null;
                            position = true;
                            textFromFileClients = newManager.ReadFromFile(fileNameClients);
                            if (!String.IsNullOrEmpty(textFromFileClients))
                            {
                                clientsList.ClientsList = newManager.ParseJsonClients(textFromFileClients, position);
                                if (clientsList.ClientsList != null)
                                {
                                    clientItems.ItemsSource = clientsList.ClientsList;
                                }
                                textFromFileChanges = newManager.ReadFromFile(fileNameChanges);
                                if (!String.IsNullOrEmpty(textFromFileChanges))
                                {
                                    changesList.ChangesList = newManager.ParseJsonChanges(textFromFileChanges);
                                    if (changesList.ChangesList != null)
                                    {
                                        recordItems.ItemsSource = changesList.ChangesList;
                                    }
                                }
                            }

                            AddClient.IsEnabled = true;
                            firstName.IsEnabled = true;
                            lastName.IsEnabled = true;
                            fathersName.IsEnabled = true;
                            phone.IsEnabled = true;
                            passportNumber.IsEnabled = true;
                            break;
                        }
                    case "Consultant":
                        {
                            clientItems.ItemsSource = null;
                            recordItems.ItemsSource = null;
                            firstName.Text = "Имя";
                            lastName.Text = "Фамилия";
                            fathersName.Text = "Отчетство";
                            phone.Text = "Телефон";
                            passportNumber.Text = "Паспорт";
                            position = false;
                            textFromFileClients = newConsultant.ReadFromFile(fileNameClients);
                            if (!String.IsNullOrEmpty(textFromFileClients))
                            {
                                clientsList.ClientsList = newConsultant.ParseJsonClients(textFromFileClients, position);
                                if (clientsList.ClientsList != null)
                                {
                                    clientItems.ItemsSource = clientsList.ClientsList;
                                }
                                textFromFileChanges = newConsultant.ReadFromFile(fileNameChanges);
                                if (!String.IsNullOrEmpty(textFromFileChanges))
                                {
                                    changesList.ChangesList = newConsultant.ParseJsonChanges(textFromFileChanges);
                                    if (changesList.ChangesList != null)
                                    {
                                        recordItems.ItemsSource = changesList.ChangesList;
                                    }
                                }
                            }
                            AddClient.IsEnabled = false;
                            firstName.IsEnabled = false;
                            lastName.IsEnabled = false;
                            fathersName.IsEnabled = false;
                            phone.IsEnabled = true;
                            passportNumber.IsEnabled = false;
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("Выберите должность", "Ошибка", MessageBoxButton.OK);
                            break;
                        }
                }
            }
        }

        private void Row_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            client = (Client)clientItems.SelectedItem;

            if (client == null)
            {
                return;
            }

            switch (name)
            {
                case "Manager":
                    {
                        firstName.IsEnabled = true;
                        lastName.IsEnabled = true;
                        fathersName.IsEnabled = true;
                        phone.IsEnabled = true;
                        passportNumber.IsEnabled = true;
                        AddClient.IsEnabled = true;

                        firstName.Text = client.FirstName;
                        lastName.Text = client.LastName;
                        fathersName.Text = client.FathersName;
                        phone.Text = client.Phone.ToString();
                        passportNumber.Text = client.PassportNumber;
                        
                        break;
                    }
                case "Consultant":
                    {
                        firstName.IsEnabled = false;
                        lastName.IsEnabled = false;
                        fathersName.IsEnabled = false;
                        phone.Text = client.Phone.ToString();
                        passportNumber.IsEnabled = false;
                        AddClient.IsEnabled = false;
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButton.OK);
                        break;
                    }
            }
        }

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

        private void OnClickChange(object sender, RoutedEventArgs e)
        {
            type = 1;
            if (SelectPosition.SelectedItem == null)
            {
                MessageBox.Show("Выберите должность", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (SelectPosition.SelectedItem != null)
            {
                if (clientItems.SelectedItem != null)
                {
                    List<Client> clients = new List<Client>();

                    foreach (var item in clientItems.Items)
                    {
                        clients.Add(item as Client);
                    }

                    index = clientItems.SelectedIndex;

                    var client = clients.ElementAt(index);

                    ComboBoxItem cbi = (ComboBoxItem)SelectPosition.SelectedItem;
                    name = cbi.Name;
                    switch (name)
                    {
                        case "Manager":
                            {
                                user = 1;
                                if (newManager.CheckIsNullOrEmpty(firstName.Text, "Имя")) return;
                                if (newManager.CheckIsNullOrEmpty(lastName.Text, "Фамилия")) return;
                                if (newManager.CheckIsNullOrEmpty(phone.Text, "Телефон")) return;
                                if (newManager.CheckIsNullOrEmpty(passportNumber.Text, "Номер паспорта")) return;

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

                                textList = new List<string>() { lastName.Text, firstName.Text, fathersName.Text, phone.Text, passportNumber.Text };

                                var fieldsList = FieldsChanged(textList, user, index);
                                if (!String.IsNullOrEmpty(fieldsList))
                                {
                                    changesList.ChangesList.Add(newManager.NewRecordChange(fieldsList, type, user));
                                    clientsList.ClientsList.RemoveAt(index);
                                    client.FirstName = firstName.Text.Trim();
                                    client.LastName = lastName.Text.Trim();
                                    client.FathersName = fathersName.Text.Trim();
                                    client.Phone = phoneNumber;
                                    client.PassportNumber = passportNumber.Text.Trim();
                                    clientsList.ClientsList.Insert(index, client);
                                }
                                else
                                {
                                    return;
                                }
                                clientItems.ItemsSource = null;
                                recordItems.ItemsSource = null;

                                clientItems.ItemsSource = clientsList.ClientsList;
                                recordItems.ItemsSource = changesList.ChangesList;

                                break;
                            }
                        case "Consultant":
                            {
                                user = 2;
                                bool parse = newConsultant.CheckParsePhone(phone.Text, out long phoneNumber);
                                if (parse && phoneNumber != 0)
                                {
                                    if (!newConsultant.CheckPhoneMatchesPattern(phone.Text)) return;
                                    if (newConsultant.CheckPhoneExistInBase(clientsList.ClientsList, client, phoneNumber)) return;
                                }
                                else
                                {
                                    return;
                                }

                                textList = new List<string>() { lastName.Text, firstName.Text, fathersName.Text, phone.Text, passportNumber.Text };

                                var fieldsList = FieldsChanged(textList, user, index);
                                if (!String.IsNullOrEmpty(fieldsList))
                                {
                                    changesList.ChangesList.Add(newConsultant.NewRecordChange(fieldsList, type, user));
                                }
                                else
                                {
                                    break;
                                }

                                clientItems.ItemsSource = null;
                                recordItems.ItemsSource = null;

                                client.Phone = phoneNumber;
                                
                                if (phoneChanges.ContainsKey(index))
                                {
                                    phoneChanges.Remove(index);
                                    phoneChanges.Add(index, phoneNumber);
                                }
                                else
                                {
                                    phoneChanges.Add(index, phoneNumber);
                                }

                                clientItems.ItemsSource = clients;
                                recordItems.ItemsSource = changesList.ChangesList;

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
                    MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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

                if (newManager.CheckIsNullOrEmpty(firstName.Text, "Имя")) return;
                if (newManager.CheckIsNullOrEmpty(lastName.Text, "Фамилия")) return;
                if (newManager.CheckIsNullOrEmpty(phone.Text, "Телефон")) return;
                if (newManager.CheckIsNullOrEmpty(passportNumber.Text, 
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

        public string FieldsChanged(List<string> textList, int user, int index)
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

            if (user == 1)
            {
                for (int i = 0; i < clientProperty.Count; i++)
                {
                    if (!String.Equals(clientProperty[i], textList[i].Trim()))
                    {
                        totalString = string.Concat(totalString, fieldNames[i]);
                    }
                }
                return totalString;
            }
            else if (user == 2)
            {
                if (!String.Equals(client.Phone.ToString(), textList[3].Trim()))
                {
                    totalString = string.Concat(totalString, fieldNames[3].Trim());
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
