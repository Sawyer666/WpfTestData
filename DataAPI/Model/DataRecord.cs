using System;
using System.Collections.Generic;
using System.Text;

namespace DataAPI.Model
{
    public class DataRecord
    {
        public DataRecord() { }
        public DataRecord(string recName, string recSurnameName, string patronymicName, string recEmail)
        {
            id = Guid.NewGuid();
            name = recName;
            surname = recSurnameName;
            patronymic = patronymicName;
            email = recEmail;
        }

        public DataRecord(Guid recId, string recName, string recSurnameName, string patronymicName, string recEmail)
        {
            id = recId;
            name = recName;
            surname = recSurnameName;
            patronymic = patronymicName;
            email = recEmail;
        }

        public Guid id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }
        public string email { get; set; }

    }
}
