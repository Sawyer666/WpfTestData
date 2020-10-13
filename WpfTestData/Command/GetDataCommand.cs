using DataAPI;
using DataAPI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestData.Command
{
    public class GetDataCommand : DataCommand<List<DataRecord>>
    {
        DataAPIBase dataApi;
        public GetDataCommand(DataAPIBase api)
        {
            dataApi = api;
        }

        public override List<DataRecord> Execute()
        {
            if (dataApi == null)
                return null;
            return dataApi.GetRecords();
        }
    }
}
