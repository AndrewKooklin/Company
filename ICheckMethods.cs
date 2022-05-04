using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Company
{
    interface ICheckMethods
    {
        /// <summary>
        /// Проверка на пустоту поля TextBox
        /// </summary>
        public bool CheckTextBoxIsNullOrEmpty(string text, string name);
        /// <summary>
        /// Проверка приведения введеного телефона к типу long
        /// </summary>
        public bool CheckParsePhone(string phoneText, out long phoneNumber);
        /// <summary>
        /// Проверка на соответствие формату введенного телефона
        /// </summary>
        public bool CheckPhoneMatchesPattern(string phoneText);
        /// <summary>
        /// Проверка на соответствие формату введенного паспорта
        /// </summary>
        public bool CheckPassportMatchesPattern(string passportNumberText);
        /// <summary>
        /// Проверка на наличие измененного телефона выбранного клиента в базе
        /// </summary>
        public bool CheckPhoneExistInBase(ObservableCollection<Client> clients, Client client, long phoneNumber);
        /// <summary>
        /// Проверка на наличие введенного телефона в базе
        /// </summary>
        public bool CheckPhoneExistInBase(ObservableCollection<Client> clients, long phoneNumber);
        /// <summary>
        /// Проверка на наличие измененного паспорта выбранного клиента в базе
        /// </summary>
        public bool CheckPassportExistInBase(ObservableCollection<Client> clients, Client client, string passportNumberText);
        /// <summary>
        /// Проверка на наличие введенного паспорта в базе
        /// </summary>
        public bool CheckPassportExistInBase(ObservableCollection<Client> clients, string passportNumberText);
    }
}
