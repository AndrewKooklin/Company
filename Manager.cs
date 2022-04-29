using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Company
{
    public class Manager : Consultant, IConcultant, IManager
    {
        public Manager() : base()
        {

        }

        public Client AddClient(string firstName, string lastName,
                        string fathersName, long phone,
                        string passportNumber)
        {
            Client client = new Client(firstName, lastName,
                                    fathersName, phone,
                                    passportNumber);

            return client;
        }

        public Change NewRecordAddClient(string totalString, int type, int user)
        {
            Change newDataChange = new Change(totalString, type, user);

            return newDataChange;
        }
    }  
}
