using DataAPI;
using DataAPI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTestData.Model;

namespace WpfTestData.Command
{
    public class SaveDataToFileCommand : DataCommand<bool>
    {
        DataAPIBase dataApi = null;
        string filePath = String.Empty;
        List<DataRecord> data = new List<DataRecord>();

        public SaveDataToFileCommand(DataAPIBase api, ObservableCollection<DataModel> records, string path)
        {
            dataApi = api;
            filePath = path;
            records.ToList().ForEach(record => 
            {
                data.Add(new DataRecord(record.Name, record.Surname, record.Patronymic, record.Email));
            });
        }
        public override bool Execute()
        {
            if (dataApi == null)
                return false;
            return dataApi.SaveAllToFile(data, filePath);
        }
    }
}
