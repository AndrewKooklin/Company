using System;

namespace Company
{
    public class Manager : Consultant,  IManager
    {
        public Manager()
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
    }  
}