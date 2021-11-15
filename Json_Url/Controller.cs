using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json_Url
{
    public class Controller
    {
        public async Task StartProcessAsync()
        {
            List<string> listURL = GetURL();
            Task<string> directionTask = GetDirection();
            URLValidator urlMenager = new URLValidator(listURL);
            List<string> preparedListURL = await urlMenager.GetPrepraeURLList();
            JsonMenager dataMenager = new JsonMenager(preparedListURL, await directionTask);
            await dataMenager.DwonloadJSON();
        }
        private async Task<string> GetDirection()
        {
            string dir = Console.ReadLine();
            await Task.Run(async () =>
            {
                DIRValidator testDIR = new DIRValidator(dir);
                if (!testDIR.GetValidation())
                {
                    dir = await GetDirection();
                }
            });
            return dir;
        }
        private List<string> GetURL()
        {
            return new List<string>(Console.ReadLine().Split(';'));
        }
    }
}

