using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Json_Url
{
    class DataMenager
    {
        private readonly List<string> listURL;
        readonly string filePatch;
        public DataMenager(List<string> listURL, string filePatch)
        {
            this.listURL = listURL;
            this.filePatch = filePatch;
        }
        public DataMenager()
        {
            this.listURL = null;
            this.filePatch = null;
        }
        public async Task<List<string>> DownloadJSONListAsync(List<string> listURL)
        {
            List<Task<string>> listOfTasks = new();
            List<string> jsonList = new();
            foreach (string url in listURL)
            {
                listOfTasks.Add(DownloadJSONAsync(url));
            }
            foreach(var item in await Task.WhenAll(listOfTasks))
            {
                jsonList.Add(item);
            }
            return jsonList;
        }
        public async Task<string> DownloadJSONAsync(string url)
        {
            string json = new("");
            await Task.Run(() =>
            {
                try
                {
                    json = new WebClient().DownloadString(url);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
            return json;
        }
        public async Task SaveJSONListAsync(List<string> JsonList, string filePatch)
        {
            List<Task<bool>> listOfTasks = new();
            foreach (string json in JsonList)
            {
                listOfTasks.Add(SaveJSON(json, filePatch));
            }
            await Task.WhenAll(listOfTasks);
        }
        public async Task<bool> SaveJSON(string json, string filePatch)
        {
            bool res = new();
            Random rnd = new();
            //string fileName = json.GetHashCode().ToString();
            string fileName = (json.GetHashCode() % rnd.Next(1, 5000) * rnd.Next(1, 500)).ToString();

            await Task.Run(() =>
            {
                try
                {
                    //Not workign...
                    //int index = 0;
                    //while (File.Exists(filePatch + "\\" + fileName))
                    //{
                    //    index++;
                    //    fileName = json.GetHashCode().ToString() + "(" + index + ")";
                    //}

                    File.WriteAllText(filePatch + "\\" + fileName, json);
                    res = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    res = false;
                }
            });
            return res;

        }
        public async Task<List<string>> DownloadJSONListAsync()
        {
            return await DownloadJSONListAsync(listURL);
        }
        public async Task SaveJSONListAsync()
        {
            await SaveJSONListAsync(listURL, filePatch);
        }
    }
}
