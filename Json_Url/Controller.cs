using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json_Url
{
    class Controller
    {
        public async Task StartProcessAsync()
        {
            List<string> listURL = GetURL();
            Task<string> directionTask = GetDirection();
            UrlMenager urlMenager = new UrlMenager(listURL);
            List<string> preparedListURL = await urlMenager.GetPrepraeURLList();
            DataMenager dataMenager = new DataMenager(preparedListURL, await directionTask);
            await dataMenager.DownloadJSONListAsync();
            await dataMenager.SaveJSONListAsync();
        }
        private async Task<string> GetDirection()
        {
            string dir = Console.ReadLine();
            await Task.Run(async () =>
            {
                DirMenager testDIR = new DirMenager(dir);
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

