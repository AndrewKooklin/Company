using System;

namespace Company
{
    interface IManager
    {
        public Client AddClient(string firstName, string lastName,
                        string fathersName, long phone,
                        string passportNumber);
    }
}
