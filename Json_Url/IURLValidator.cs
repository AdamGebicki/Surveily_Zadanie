using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json_Url
{
    interface IURLValidator
    {
        Task<List<bool>> GetValidationAsync();
        Task<List<bool>> GetConnectionAsync();
        Task<List<string>> GetPrepraeURLList();
    }
}
