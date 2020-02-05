using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Verity
{
    public class DataVerityResult : DataVerityResult<object> { }
    public class DataVerityResult<T>: VerityResult
    {
        public T Data { get; set; }
    }
}
