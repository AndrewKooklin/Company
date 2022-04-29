using Newtonsoft.Json;
using System;

namespace Company
{
    [Serializable]
    [JsonObject]
    public class Change
    {
        public Change(string totalString, int type, int user)
        {
            DateTime dateTime = DateTime.Now;
            this.dateTime = dateTime;
            this.TotalString = totalString;
            if (type == 1)
            {
                this.Type = "Изменение данных";
            }
            else if (type == 2)
            {
                this.Type = "Добавление клиента";
            }
            if (user == 1)
            {
                this.Position = "Менеджер";
            }
            else if (user == 2)
            {
                this.Position = "Консультант";
            }
        }
        [JsonConstructor]
        public Change(DateTime dateTime, string totalString, string type, string position)
        {
            this.dateTime = dateTime;
            TotalString = totalString;
            Type = type;
            Position = position;
        }
        [JsonProperty]
        public DateTime dateTime { get; set; }
        [JsonProperty]
        public string TotalString { get; set; }
        [JsonProperty]
        public string Type { get; set; }
        [JsonProperty]
        public string Position { get; set; }
    }
}
