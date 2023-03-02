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
            this.FieldsChanged = totalString;

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
            FieldsChanged = totalString;
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
        public string FieldsChanged { get; set; }
        [JsonProperty]
        public string Type { get; set; }
        [JsonProperty]
        public string Position { get; set; }
    }
}
