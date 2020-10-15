using DataAPI.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataAPI
{
    public class DataAPIBinary : DataAPIBase
    {
        private string binaryfileName = "DataRecordsBinary.dat";
        Stream s = new MemoryStream();
        public override bool AddRecord(DataRecord record)
        {
            try
            {
                using (var binWriter = new BinaryWriter(File.Open(binaryfileName, FileMode.Append)))
                {
                    binWriter.Write(record.id.ToString());
                    binWriter.Write(record.surname);
                    binWriter.Write(record.name);
                    binWriter.Write(record.patronymic);
                    binWriter.Write(record.email);
                    s.Seek(0, SeekOrigin.Begin);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override List<DataRecord> GetRecords()
        {
            List<DataRecord> dataRecords = new List<DataRecord>();
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(binaryfileName, FileMode.OpenOrCreate, FileAccess.Read)))
                {
                    while (reader.PeekChar() != -1)
                    {
                        var data = new DataRecord();
                        data.id = Guid.Parse(reader.ReadString());
                        data.surname = reader.ReadString();
                        data.name = reader.ReadString();
                        data.patronymic = reader.ReadString();
                        data.email = reader.ReadString();
                        dataRecords.Add(data);
                    }
                }
                return dataRecords;
            }
            catch (Exception)
            {
                return dataRecords;
            }
        }

        public override bool RemoveRecord(Guid id)
        {
            try
            {
                var dataRecords = GetRecords().Where(item => item.id != id).ToList();
                File.WriteAllText(binaryfileName, String.Empty);
                dataRecords.ForEach(item => AddRecord(item));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool UpdateRecord(DataRecord record)
        {
            try
            {
                var dataRecords = GetRecords();
                var selected = dataRecords.Where(item => item.id == record.id).Single();
                selected.surname = record.surname;
                selected.name = record.name;
                selected.patronymic = record.patronymic;
                selected.email = record.email;

                File.WriteAllText(binaryfileName, String.Empty);
                dataRecords.ForEach(item => AddRecord(item));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool SaveAllToFile(List<DataRecord> records, string path)
        {
            try
            {
                binaryfileName = path + ".dat";
                using (var binWriter = new BinaryWriter(File.Open(binaryfileName, FileMode.Append)))
                {
                    records.ForEach(record =>
                   {
                       binWriter.Write(record.id.ToString());
                       binWriter.Write(record.surname);
                       binWriter.Write(record.name);
                       binWriter.Write(record.patronymic);
                       binWriter.Write(record.email);
                       s.Seek(0, SeekOrigin.Begin);
                   });
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override List<DataRecord> LoadFromFile(string path)
        {
            List<DataRecord> dataRecords = new List<DataRecord>();
            try
            {
                binaryfileName = path;
                return this.GetRecords();
            }
            catch (Exception)
            {
                return dataRecords;
            }
        }
    }
}
