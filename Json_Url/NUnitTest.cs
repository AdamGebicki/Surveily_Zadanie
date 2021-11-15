using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json_Url
{
    [TestFixture]
    public class NUnitTest
    {
        //JsonMenager
        [Test]
        public async void SaveJson()
        {
            List<string> ListURL = new List<string> { "http://api.plos.org/search?q=title:DNA" };
            JsonMenager jsonMenager = new JsonMenager(ListURL, Directory.GetCurrentDirectory());
            await jsonMenager.GetJSONListAsync();
            Assert.True(await jsonMenager.SaveJSONListAsync());
        }
        [Test]
        public async Task SaveJsonWrongInputAsync()
        {
            List<string> ListURL = new List<string> { "-" };
            JsonMenager jsonMenager = new JsonMenager(ListURL, Directory.GetCurrentDirectory());
            await jsonMenager.GetJSONListAsync();
            Assert.False(await jsonMenager.SaveJSONListAsync());
        }
        [Test]
        public async void SaveJsonNull()
        {
            List<string> ListURL = new List<string> {  };
            JsonMenager jsonMenager = new JsonMenager(ListURL, Directory.GetCurrentDirectory());
            await jsonMenager.GetJSONListAsync();
            Assert.False(await jsonMenager.SaveJSONListAsync());
        }
        [Test]
        public async void GetJson()
        {
            List<string> ListURL = new List<string> { "http://api.plos.org/search?q=title:DNA" };
            JsonMenager jsonMenager = new JsonMenager(ListURL, Directory.GetCurrentDirectory());
            Assert.True(await jsonMenager.GetJSONListAsync());
        }
        [Test]
        public async void GetJsonWrongInput()
        {
            List<string> ListURL = new List<string> { "-"};
            JsonMenager jsonMenager = new JsonMenager(ListURL, Directory.GetCurrentDirectory());
            Assert.False(await jsonMenager.GetJSONListAsync());
        }
        [Test]
        public async void GetJsonNull()
        {
            List<string> ListURL = new List<string> { };
            JsonMenager jsonMenager = new JsonMenager(ListURL, Directory.GetCurrentDirectory());
            Assert.False(await jsonMenager.GetJSONListAsync());
        }
        //DIRValidator
        [Test]
        public void ValidateDirSpec()
        {
            DIRValidator dIRValidator = new DIRValidator(Directory.GetCurrentDirectory());
            Assert.True(dIRValidator.GetValidation());
        }
        [Test]
        public void ValidateDirSys()
        {
            DIRValidator dIRValidator = new DIRValidator("C:\\");
            Assert.True(dIRValidator.GetValidation());
        }
        //URLValidator
        [Test]
        public async void URLValidationHTTP()
        {
            URLValidator uRLValidator = new(new List<string>() { "http://api.plos.org/search?q=title:DNA" });
            CollectionAssert.Equals(new List<bool>() { true }, await uRLValidator.GetValidationAsync());
        }
        [Test]
        public async void URLValidationNull()
        {
            URLValidator uRLValidator = new(new List<string>() { });
            CollectionAssert.Equals(new List<bool>() { false }, await uRLValidator.GetValidationAsync());
        }
        [Test]
        public async void URLValidationRandomString()
        {
            URLValidator uRLValidator = new(new List<string>() { "aezakmi" });
            CollectionAssert.Equals(new List<bool>() { false }, await uRLValidator.GetValidationAsync());
        }
        public async void URLConenctionHttp()
        {
            URLValidator uRLValidator = new(new List<string>() { "http://api.plos.org/search?q=title:DNA" });
            CollectionAssert.Equals(new List<bool>() { true }, await uRLValidator.GetConnectionAsync());
        }
        [Test]
        public async void URLConnectionNull()
        {
            URLValidator uRLValidator = new(new List<string>() { });
            CollectionAssert.Equals(new List<bool>() { false }, await uRLValidator.GetConnectionAsync());
        }
        [Test]
        public async void URLConnectionRandomString()
        {
            URLValidator uRLValidator = new(new List<string>() { "aezakmi" });
            CollectionAssert.Equals(new List<bool>() { false }, await uRLValidator.GetConnectionAsync());
        }
        public async void UrlPreperationCheckMix()
        {
            URLValidator uRLValidator = new(new List<string>() { "aezakmi","", "http://api.plos.org/search?q=title:DNA" });
            CollectionAssert.Equals(new List<string>() { "http://api.plos.org/search?q=title:DNA" }, await uRLValidator.GetPrepraeURLList());
        }
        public async void UrlPreperationCheckHttp()
        {
            URLValidator uRLValidator = new(new List<string>() { "http://api.plos.org/search?q=title:DNA" });
            CollectionAssert.Equals(new List<string>() { "http://api.plos.org/search?q=title:DNA" }, await uRLValidator.GetPrepraeURLList());
        }




        //[Test]
        //public async void Test()
        //{
        //    //DirMenager dirMenager = new();
        //    //UrlMenager uRLMenager = new();
        //    //DataMenager dataMenager = new();
        //    //Assert.True(uRLMenager.SingularValidation("http://api.plos.org/search?q=title:DNA"));
        //    //Assert.False(uRLMenager.SingularValidation("MakaPaka"));
        //    //Assert.True(uRLMenager.SingularConnectionValidation("http://api.plos.org/search?q=title:DNA"));
        //    //Assert.True(uRLMenager.SingularConnectionValidation("https://www.google.com/"));
        //    //Assert.False(uRLMenager.SingularConnectionValidation("MakaPaka"));
        //    //Assert.AreEqual(new List<bool> { true, false }, uRLMenager.AgreementValidation(new List<string> { "http://api.plos.org/search?q=title:DNA", "blablabla" }));
        //    //Assert.AreEqual(new List<bool> { true, true }, uRLMenager.AgreementValidation(new List<string> { "http://api.plos.org/search?q=title:DNA", "https://www.google.com/" }));
        //    //Assert.AreEqual(new List<bool> { false, true  }, uRLMenager.AgreementValidation(new List<string> {"blablabla", "https://api.ploaaas.org/" }));
        //    //Assert.AreEqual(new List<bool> { true, false }, uRLMenager.ConnectionValidation(new List<string> { "http://api.plos.org/search?q=title:DNA", "blablabla" }));
        //    //Assert.AreEqual(new List<bool> { true, true }, uRLMenager.ConnectionValidation(new List<string> { "http://api.plos.org/search?q=title:DNA", "https://www.google.com/" }));
        //    //Assert.AreEqual(new List<bool> { false, false }, uRLMenager.ConnectionValidation(new List<string> { "blablabla", "https://api.ploaaas.org/" }));
        //    //DownloadJSON
        //    //Assert.IsNull(await dataMenager.DownloadJSONAsync("heioysam"));
        //    //Assert.IsNotNull(await dataMenager.DownloadJSONAsync("http://api.plos.org/search?q=title:DNA"));
        //    ////SaaveJSON
        //    //Assert.False(await dataMenager.SaveJSON("heioysam","aezakmi"));
        //    //Assert.True(await dataMenager.SaveJSON("smth_text_ect", Directory.GetCurrentDirectory()));
        //    ////GetValidation
        //    //Assert.True(dirMenager.GetValidation(Directory.GetCurrentDirectory()));
        //    //Assert.False(dirMenager.GetValidation("aezakmi"));
        //}
    }
}
