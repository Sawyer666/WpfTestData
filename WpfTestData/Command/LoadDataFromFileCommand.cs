using DataAPI;
using DataAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestData.Command
{
    public class LoadDataFromFileCommand : DataCommand<List<DataRecord>>
    {
        DataAPIBase dataApi = null;
        string filename = String.Empty;

        public LoadDataFromFileCommand(DataAPIBase api, string path)
        {
            dataApi = api;
            filename = path;
        }
        public override List<DataRecord> Execute()
        {
            if (dataApi == null)
                return null;
            return dataApi.LoadFromFile(filename);
        }
    }
}
