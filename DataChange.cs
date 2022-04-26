using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company
{
    public class DataChange
    {
        public DateTime dateTime { get; set; }

        public List<string> DataChangeList { get; set; }

        public string Type { get; set; }

        public string Position { get; set; }

        public DataChange(DateTime dateTime, List<string> dataChangeList, string type, string position)
        {
            this.dateTime = dateTime;
            DataChangeList = dataChangeList;
            Type = type;
            Position = position;
        }

        public DataChange()
        {

        }

    }
}
