using System;
using System.Collections.ObjectModel;

namespace Company
{
    public class Manager : Consultant, IConcultant, IManager
    {
        Position position = Position.Manager;

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

        //public Change NewRecordAddClient(string totalString, int type, int user)
        //{
        //    Change newDataChange = new Change(totalString, type, user);

        //    return newDataChange;
        //}

    }  
}