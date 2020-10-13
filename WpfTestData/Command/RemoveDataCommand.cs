using DataAPI;
using DataAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestData.Command
{
    public class RemoveDataCommand : DataCommand<bool>
    {
        DataAPIBase dataApi = null;
        Guid id;

        public RemoveDataCommand(DataAPIBase api, Guid recId)
        {
            dataApi = api;
            id = recId;
        }
        public override bool Execute()
        {
            if (dataApi == null)
                return false;
            return dataApi.RemoveRecord(id);
        }
    }
}
