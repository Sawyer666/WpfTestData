using DataAPI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace DataAPI
{
    public class DataAPIXML : DataAPIBase
    {
        private XDocument xDocument = null;
        private string fileName = "DataRecords.xml";

        public DataAPIXML()
        {
            FileInfo file = new FileInfo(fileName);
            if (!file.Exists)
            {
                xDocument = new XDocument();
                xDocument.Add(new XElement("records"));
                xDocument.Save(fileName);
            }
            else
                xDocument = XDocument.Load(fileName);
        }

        public override List<DataRecord> GetRecords()
        {
            List<DataRecord> dataRecords = new List<DataRecord>();
            if (xDocument.Root != null)
            {
                var query = from xml in xDocument.Descendants("record")
                            select new DataRecord()
                            {
                                id = (Guid)xml.Element("id"),
                                name = (string)xml.Element("name"),
                                surname = (string)xml.Element("surname"),
                                patronymic = (string)xml.Element("patronymic"),
                                email = (string)xml.Element("email")
                            };
                dataRecords = query.ToList();
            }
            return dataRecords;
        }

        public override bool AddRecord(DataRecord record)
        {
            if (xDocument == null)
                return false;
            try
            {
                var xElement = new XElement("record");
                Type t = record.GetType();
                PropertyInfo[] pi = t.GetProperties();
                foreach (PropertyInfo p in pi)
                    xElement.Add(new XElement(p.Name, p.GetValue(record, null).ToString()));
                xDocument.Root.Add(xElement);
                xDocument.Save(fileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool RemoveRecord(Guid id)
        {
            if (xDocument == null)
                return false;
            try
            {
                xDocument.Descendants("record")
                            .Where(item => Guid.Parse(item.Element("id").Value) == id)
                            .Single()
                            .Remove();
                xDocument.Save(fileName);
                return true;
            }
            catch (Exception) { return false; }
        }

        public override bool UpdateRecord(DataRecord record)
        {
            if (xDocument == null)
                return false;
            try
            {
                var updateItem = xDocument.Descendants("record")
                            .Where(item => Guid.Parse(item.Element("id").Value) == record.id)
                            .Single();
                updateItem.Element("name").Value = record.name;
                updateItem.Element("surname").Value = record.surname;
                updateItem.Element("patronymic").Value = record.patronymic;
                updateItem.Element("email").Value = record.email;

                xDocument.Save(fileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
