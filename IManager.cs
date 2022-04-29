using System;

namespace Company
{
    interface IManager
    {
        public Change NewRecordAddClient(string totalString, int type, int user);
    }
}
