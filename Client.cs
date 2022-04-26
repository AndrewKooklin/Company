using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company
{
    public class Client
    {
        private string passportNumber;

        [NonSerialized]
        public bool positionSwitch = true;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        public long Phone { get; set; }
        public string PassportNumber
        {
            get
            {
                if (positionSwitch)
                {
                    return passportNumber;
                }
                else
                {
                    return "**** - ******";
                }
            }
            set 
            {
                passportNumber = value;
            }
        }

        protected static Random rand = new Random();

        public Client(string firstName, string lastName,
                        string fathersName, long phone,
                        string passportNumber)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.FathersName = fathersName;
            this.Phone = phone;
            this.PassportNumber = passportNumber;
        }

        public Client(int number)
        {
            this.FirstName = $"firstName_{number}";
            this.LastName = $"lastName_{number}";
            this.FathersName = $"fathersName_{number}";
            this.Phone = 89010000000 + rand.Next(1000000, 9999999);
            this.PassportNumber = $"{rand.Next(1000, 9999)}-{rand.Next(100000, 999999)}";
        }

        public Client()
        {

        }
    }
}
