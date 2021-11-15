using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json_Url
{
    interface IJsonMenager
    {
        Task<bool> DwonloadJSON();
        Task<bool> SaveJSONListAsync();
        Task<bool> GetJSONListAsync();
    }
}
