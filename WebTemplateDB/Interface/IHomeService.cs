using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Home;

namespace WebTemplateDB.Interface
{
    public interface IHomeService
    {
        /// <summary>
        /// 取得HomeData
        /// </summary>
        /// <returns></returns>
        Task<HomeViewModel> GetHomeData();
    }
}
