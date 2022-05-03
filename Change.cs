using Newtonsoft.Json;
using System;

namespace Company
{
    [Serializable]
    [JsonObject]
    public class Change
    {
        public Change(string totalString, DataChange dataChange, Employee.Position position)
        {
            DateTime dateTime = DateTime.Now;
            this.dateTime = dateTime;
            this.TotalString = totalString;

            switch (dataChange)
            {
                case DataChange.ChangingRecord:
                    {
                        this.Type = "Изменение данных";
                        break;
                    }
                case DataChange.AddNewClient:
                    {
                        this.Type = "Добавление клиента";
                        break;
                    }
                default:
                    {
                        this.Type = "Действие нераспознано";
                        break;
                    }
            }

            switch (position)
            {
                case Employee.Position.Consultant:
                    {
                        this.Position = "Консультант";
                        break;
                    }
                case Employee.Position.Manager:
                    {
                        this.Position = "Менеджер";
                        break;
                    }
                default:
                    {
                        this.Position = "Неизвестный";
                        break;
                    }
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
        
        public enum DataChange
        {
            ChangingRecord,
            AddNewClient
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
