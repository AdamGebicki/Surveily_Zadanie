using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Json_Url
{
    public class ControllerUI
    {
        public async Task StartProcess()
        {
            List<string> listURL = GetURL();
            URLValidator urlMenager = new URLValidator(listURL);
            Task<List<bool>> validationTask = urlMenager.GetValidationAsync();
            Task<List<bool>> connectionTask = urlMenager.GetConnectionAsync();
            Task<string> directionTask = GetDirection();
            List<bool>agreementListURL = await validationTask;
            List<bool>connectionListURL = await connectionTask;
            Task<List<string>> prepareURLTask =  urlMenager.PrepraeURLList(agreementListURL, connectionListURL);
            List<string> preparedListURL = await prepareURLTask;
            string filePatch = await directionTask;
            Summary(ref listURL, agreementListURL, connectionListURL, preparedListURL, filePatch);
            JsonMenager dataMenager = new JsonMenager(preparedListURL, filePatch);
            await dataMenager.DwonloadJSON();
        }
        private void Summary(
            ref List<string> listUrl,
            List<bool> agreementListURL,
            List<bool> connectionListURL,
            List<string> preparedListURL,
            string filePatch)
        {
            Console.Clear();
            Console.WriteLine("URL Summary:");
            Console.WriteLine("");
            Console.WriteLine("Validation");
            for(int index = 0; index < listUrl.Count; index++)
            {
                if (agreementListURL[index] == false)
                {
                    Console.WriteLine("false - " + listUrl[index]);
                }
                if (agreementListURL[index] == true)
                {
                    Console.WriteLine("true - " + listUrl[index]);
                }
            }
            Console.WriteLine("");
            Console.WriteLine("Connection:");
            for(int index = 0; index < listUrl.Count; index++)
            {
                if (connectionListURL[index] == false)
                {
                    Console.WriteLine("false - " + listUrl[index]);
                }
                if (connectionListURL[index] == true)
                {
                    Console.WriteLine("true - " + listUrl[index]);
                }
            }
            Console.WriteLine("");
            Console.WriteLine("Correct links transferred to process:");
            foreach(string link in preparedListURL)
            {
                Console.WriteLine(link);
            }
            if (preparedListURL.Count == 0)
            {
                Console.WriteLine("null");
            }
            Console.WriteLine("");
            Console.WriteLine("File patch summary:");
            Console.WriteLine("Direction:" + filePatch);
            Console.ReadKey();
            Console.Clear();
        }
        private async Task<string> GetDirection()
        {
            Console.WriteLine("Insert Direction:");
            string dir = Console.ReadLine();
            await Task.Run(async () =>
            {
                DIRValidator testDIR = new DIRValidator(dir);
                if (!testDIR.GetValidation())
                {
                    Console.WriteLine("Error: Patch doesn't exist.");
                    dir = await GetDirection();
                }
            });
            return dir;
        }
        private List<string> GetURL()
        {
            Console.WriteLine("Insert URL list:");
            return new List<string>(Console.ReadLine().Split(';'));
        }
    }
}
