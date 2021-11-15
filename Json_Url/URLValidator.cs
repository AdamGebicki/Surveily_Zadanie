using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Json_Url
{
    class URLValidator : IURLValidator
    {
        private readonly List<string> listURL;
        public URLValidator(List<string> listURL)
        {
            this.listURL = listURL;
        }
        public URLValidator()
        {
            this.listURL = null;
        }
        private List<bool> AgreementValidation(List<string> listURL)
        {
            List<bool> validationChecklist = new();
            foreach (string url in listURL)
            {
                bool res = SingularValidation(url);
                validationChecklist.Add(res);
            }
            return validationChecklist;
        }
        private bool SingularValidation(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult);
        }
        private List<bool> ConnectionValidation(List<string> listURL)
        {
            List<bool> connectionChecklist = new();
            foreach (string url in listURL)
            {
                connectionChecklist.Add(SingularConnectionValidation(url));
            }
            return connectionChecklist;
        }
        private bool SingularConnectionValidation(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 15000;
                request.Method = "HEAD";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<bool>> GetValidationAsync()
        {
            List<bool> validationList = new();
            await Task.Run(() =>
            {
                validationList = AgreementValidation(listURL);
            });
            return validationList;
        }
        public async Task<List<bool>> GetConnectionAsync()
        {
            List<bool> connectionList = new();
            await Task.Run(() =>
            {
                connectionList = ConnectionValidation(listURL);
                
            });
            return connectionList;
        }
        public async Task<List<string>> PrepraeURLList(List<bool> agreementListURL, List<bool> connectionListURL)
        {
            List<string> PreparedURLList = new List<string>();
            await Task.Run(() =>
            {
                for (int index = 0; index < listURL.Count; index++)
                {
                    if (agreementListURL[index] == true && connectionListURL[index] == true)
                    {
                        PreparedURLList.Add(listURL[index]);
                    }
                }
            });
            return PreparedURLList;
        }
        public async Task<List<string>> GetPrepraeURLList()
        {
            return await PrepraeURLList(await GetValidationAsync(), await GetConnectionAsync());
        }
    }
}
