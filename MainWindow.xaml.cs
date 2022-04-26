using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;
using System.Windows.Media;
using System.Drawing.Printing;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Company
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string fileName = "\\Clients.json";
        NewClients newClients;
        List<Client> clients = new List<Client>();
        Manager newManager = new Manager();
        Consultant newConsultant = new Consultant();
        Client emp;
        string root;
        string name;
        string textFromFile;
        public int index;
        public bool position = true;

        public MainWindow()
        {
            InitializeComponent();

            newClients = new NewClients(this);

            root = newManager.ConvertToJson(newClients.Clients);

            newManager.WriteToFile(root, fileName);
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
                            textFromFile = newManager.ReadFromFile(fileName);
                            position = true;
                            clients = newManager.ParseJson(textFromFile, position);
                            clientItems.ItemsSource = clients;
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
                            textFromFile = newConsultant.ReadFromFile(fileName);
                            position = false;
                            clients = newConsultant.ParseJson(textFromFile, position);
                            clientItems.ItemsSource = clients;
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
            emp = (Client)clientItems.SelectedItem;

            if (emp == null)
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

                        firstName.Text = emp.FirstName;
                        lastName.Text = emp.LastName;
                        fathersName.Text = emp.FathersName;
                        phone.Text = emp.Phone.ToString();
                        passportNumber.Text = emp.PassportNumber;
                        AddClient.IsEnabled = true;
                        break;
                    }
                case "Consultant":
                    {
                        firstName.IsEnabled = false;
                        lastName.IsEnabled = false;
                        fathersName.IsEnabled = false;
                        phone.Text = emp.Phone.ToString();
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
            bool parse = long.TryParse(phone.Text, out long phoneNumber);
            if (parse)
            {
                Regex regex = new Regex(@"\d\d\d\d\d\d\d\d\d\d\d$");
                var bc = new BrushConverter();

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
                return;
            }
        }

        private void OnPassportChanged(object sender, TextChangedEventArgs e)
        {
            if (clientItems.SelectedItem != null)
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
                else
                {
                    return;
                }
            }
        }

        private void OnClickChange(object sender, RoutedEventArgs e)
        {
            if (SelectPosition.SelectedItem != null)
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
                                if (newManager.CheckIsNullOrEmpty(firstName.Text, "Имя")) return;
                                if (newManager.CheckIsNullOrEmpty(lastName.Text, "Фамилия")) return;
                                if (newManager.CheckIsNullOrEmpty(phone.Text, "Телефон")) return;
                                if (newManager.CheckIsNullOrEmpty(passportNumber.Text, "Номер паспорта")) return;

                                bool parse = newManager.CheckParsePhone(phone.Text, out long phoneNumber);
                                if(parse && phoneNumber != 0)
                                {
                                    if (!newManager.CheckPhoneMatchesPattern(phone.Text)) return;
                                    if (!newManager.CheckPassportMatchesPattern(passportNumber.Text)) return;

                                    if(newManager.CheckPhoneExistInBase(clients, phoneNumber)) return;
                                    if(newManager.CheckPassportExistInBase(clients, passportNumber.Text)) return;
                                }
                                else
                                {
                                    return;
                                }
                                client.FirstName = firstName.Text.Trim();
                                client.LastName = lastName.Text.Trim();
                                client.FathersName = fathersName.Text.Trim();
                                client.Phone = phoneNumber;
                                client.PassportNumber = passportNumber.Text.Trim();

                                clientItems.ItemsSource = null;
                                clientItems.ItemsSource = clients;

                                break;
                            }
                        case "Consultant":
                            {
                                bool parse = newManager.CheckParsePhone(phone.Text, out long phoneNumber);
                                if (parse && phoneNumber != 0)
                                {
                                    if (!newConsultant.CheckPhoneMatchesPattern(phone.Text)) return;
                                    if (newConsultant.CheckPhoneExistInBase(clients, phoneNumber)) return;
                                }
                                else
                                {
                                    return;
                                }
                                client.Phone = phoneNumber;
                                clientItems.ItemsSource = null;
                                clientItems.ItemsSource = clients;
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("Выберите пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            List<Client> clients = new List<Client>();

            foreach (var item in clientItems.Items)
            {
                clients.Add(item as Client);
            }

            if (newManager.CheckIsNullOrEmpty(firstName.Text, "Имя")) return;
            if (newManager.CheckIsNullOrEmpty(lastName.Text, "Фамилия")) return;
            if (newManager.CheckIsNullOrEmpty(phone.Text, "Телефон")) return;
            if (newManager.CheckIsNullOrEmpty(passportNumber.Text, "Номер паспорта")) return;

            bool parse = newManager.CheckParsePhone(phone.Text, out long phoneNumber);
            if (parse && phoneNumber != 0)
            {
                if (!newManager.CheckPhoneMatchesPattern(phone.Text)) return;
                if (!newManager.CheckPassportMatchesPattern(passportNumber.Text)) return;

                if (newManager.CheckPhoneExistInBase(clients, phoneNumber)) return;
                if (newManager.CheckPassportExistInBase(clients, passportNumber.Text)) return;
            }
            else
            {
                return;
            }
                Client newClient = newManager.AddClient(firstName.Text.Trim(),
                               lastName.Text.Trim(), fathersName.Text.Trim(),
                               phoneNumber, passportNumber.Text.Trim());
                clients.Add(newClient);
            clientItems.ItemsSource = null;
            clientItems.ItemsSource = clients;
        }
    }
}
