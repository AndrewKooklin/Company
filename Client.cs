using Newtonsoft.Json;
using System;

namespace Company
{
    [Serializable]
    [JsonObject]
    public class Client
    {
        private string passportNumber;

        [NonSerialized]
        public bool positionSwitch = true;
        [JsonProperty]
        public string LastName { get; set; }
        [JsonProperty]
        public string FirstName { get; set; }
        [JsonProperty]
        public string FathersName { get; set; }
        [JsonProperty]
        public long Phone { get; set; }
        [JsonProperty]
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

        [JsonConstructor]
        public Client(string firstName, string lastName,
                        string fathersName, long phone,
                        string passportNumber)
        {
            this.LastName = lastName;
            this.FirstName = firstName;
            this.FathersName = fathersName;
            this.Phone = phone;
            this.PassportNumber = passportNumber;
        }

        public Client()
        {

        }
    }
}
