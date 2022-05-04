using System;
using System.Collections.ObjectModel;

namespace Company
{
    public class Manager : Consultant,  IManager, 
                           IGetRecordsFromBase, IWorkWithJson,
                           IWorkWithFiles, ICheckMethods
    {
        public Manager()
        {

        }
        /// <summary>
        /// Создание нового клиента
        /// </summary>
        /// <returns></returns>
        public Client AddClient(string firstName, string lastName,
                        string fathersName, long phone,
                        string passportNumber)
        {
            Client client = new Client(firstName, lastName,
                                    fathersName, phone,
                                    passportNumber);

            return client;
        }
    }  
}