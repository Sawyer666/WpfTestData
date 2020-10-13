using DataAPI;
using DataAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestData.Command
{
    public class UpdateDataCommand : DataCommand<bool>
    {
        DataAPIBase dataApi = null;
        DataRecord record = null;

        public UpdateDataCommand(DataAPIBase api, DataRecord rec)
        {
            dataApi = api;
            record = rec;
        }
        public override bool Execute()
        {
            if (dataApi == null)
                return false;
            return dataApi.UpdateRecord(record);
        }
    }
}
