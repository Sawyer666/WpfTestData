﻿using DataAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAPI
{
    public abstract class DataAPIBase
    {
        public abstract List<DataRecord> GetRecords();

        public abstract bool AddRecord(DataRecord record);

        public abstract bool UpdateRecord(DataRecord record);

        public abstract bool RemoveRecord(Guid id);

        public abstract bool SaveAllToFile(List<DataRecord> records,string path);

        public abstract List<DataRecord> LoadFromFile(string fileName);
    }
}
