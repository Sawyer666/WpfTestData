using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestData.Command
{
    public abstract class DataCommand<TResult>
    {
        public abstract TResult Execute();
    }
}
