using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Company
{
    public class Consultant : IConcultant
    {
        string root;
        public Consultant()
        {

        }

        public string ConvertToJson(ObservableCollection<Client> clients)
        {
            TreeJson treeJson;
            treeJson = new TreeJson()
            {
                ClientsList = clients
            };

            root = JsonConvert.SerializeObject(treeJson, Formatting.Indented);
            Debug.WriteLine(root);
            return root;
        }

        public List<Client> ParseJson(string root, bool position)
        {
            List<Client> clients = new List<Client>();

            JObject json = JObject.Parse(root);

            var jToken = json["ClientsList"];

            var clientsList = jToken.ToObject<List<Client>>();

            foreach (var client in clientsList)
            {
                client.positionSwitch = position;
                clients.Add(client);
            }
            return clients;
        }

        public void WriteToFile(string root, string fileName)
        {
            string path = Directory.GetCurrentDirectory() + fileName;
            File.WriteAllText(path, root);
        }

        public string ReadFromFile(string fileName)
        {
            string path = Directory.GetCurrentDirectory() + fileName;
            string jsonString = File.ReadAllText(path);
            return jsonString;
        }

        public bool CheckIsNullOrEmpty(string text, string name)
        {
            bool check;
            if (String.IsNullOrEmpty(text))
            {
                MessageBox.Show($"Заполните поле \"{name}\"", "Ошибка", MessageBoxButton.OK);
                check = true;
                return check;
            }
            else
            {
                check = false;
                return check;
            }
        }

        public bool CheckParsePhone(string phoneText, out long phoneNumber)
        {
            bool parsePhone = long.TryParse(phoneText.Trim(), out long phoneNumberTemp);
            if (parsePhone && phoneNumberTemp > 10000000000 && phoneNumberTemp < 99999999999)
            {
                phoneNumber = phoneNumberTemp;
                parsePhone = true;
                return parsePhone;
            }
            else
            {
                MessageBox.Show("Поле \"Телефон\" должно состоять из 11 цифр", "Ошибка", MessageBoxButton.OK);
                parsePhone = false;
                phoneNumber = 0;
                return parsePhone;
            }
        }

        public bool CheckPhoneMatchesPattern(string phoneText)
        {
            bool check;
            Regex regex = new Regex(@"\d\d\d\d\d\d\d\d\d\d\d$");
            if (regex.IsMatch(phoneText.Trim()))
            {
                check = true;
                return check;
            }
            else
            {
                MessageBox.Show("Поле \"Телефон\" должно состоять из 11 цифр", "Ошибка", MessageBoxButton.OK);
                check = false;
                return check;
            }
        }

        public bool CheckPassportMatchesPattern(string passportNumberText)
        {
            bool check;
            Regex regex = new Regex(@"\d\d\d\d-\d\d\d\d\d\d$");
            if (regex.IsMatch(passportNumberText.Trim()))
            {
                check = true;
                return check;
            }
            else
            {
                MessageBox.Show("Поле \"Номер паспорта\" должно быть в формате 1234-567890", "Ошибка", MessageBoxButton.OK);
                check = false;
                return check;
            }
        }

        public bool CheckPhoneExistInBase(List<Client> clients, long phoneNumber)
        {
            bool exist = false;

            foreach (var item in clients)
            {
                if (item.Phone != phoneNumber)
                {
                    continue;
                }
                else
                {
                    MessageBox.Show("Такой номер телефона уже есть в базе", "Ошибка", MessageBoxButton.OK);
                    exist = true;
                    return exist;
                }
            }
            return exist;
        }

        public bool CheckPassportExistInBase(List<Client> clients, string passportNumberText)
        {
            bool exist = false;
            foreach (var item in clients)
            {
                bool compare = String.Equals(item.PassportNumber, passportNumberText.Trim());
                if (!compare)
                {
                    continue;
                }
                else
                {
                    MessageBox.Show("Такой номер паспорта уже есть в базе", "Ошибка", MessageBoxButton.OK);
                    exist = true;
                    return exist;
                }
            }
            return exist;
        }
    }
}
