using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Share;

namespace ViewModels.Verity
{
    public class PageDataVerityResult: DataVerityResult
    {
        public PaginationViewModel Pagination { get; set; }
    }
}
