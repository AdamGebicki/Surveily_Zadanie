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
        [Test]
        public async void Test()
        {
            DirMenager dirMenager = new();
            UrlMenager uRLMenager = new();
            DataMenager dataMenager = new();
            Assert.True(uRLMenager.SingularValidation("http://api.plos.org/search?q=title:DNA"));
            Assert.False(uRLMenager.SingularValidation("MakaPaka"));
            Assert.True(uRLMenager.SingularConnectionValidation("http://api.plos.org/search?q=title:DNA"));
            Assert.True(uRLMenager.SingularConnectionValidation("https://www.google.com/"));
            Assert.False(uRLMenager.SingularConnectionValidation("MakaPaka"));
            Assert.AreEqual(new List<bool> { true, false }, uRLMenager.AgreementValidation(new List<string> { "http://api.plos.org/search?q=title:DNA", "blablabla" }));
            Assert.AreEqual(new List<bool> { true, true }, uRLMenager.AgreementValidation(new List<string> { "http://api.plos.org/search?q=title:DNA", "https://www.google.com/" }));
            Assert.AreEqual(new List<bool> { false, true  }, uRLMenager.AgreementValidation(new List<string> {"blablabla", "https://api.ploaaas.org/" }));
            Assert.AreEqual(new List<bool> { true, false }, uRLMenager.ConnectionValidation(new List<string> { "http://api.plos.org/search?q=title:DNA", "blablabla" }));
            Assert.AreEqual(new List<bool> { true, true }, uRLMenager.ConnectionValidation(new List<string> { "http://api.plos.org/search?q=title:DNA", "https://www.google.com/" }));
            Assert.AreEqual(new List<bool> { false, false }, uRLMenager.ConnectionValidation(new List<string> { "blablabla", "https://api.ploaaas.org/" }));
            //DownloadJSON
            Assert.IsNull(await dataMenager.DownloadJSONAsync("heioysam"));
            Assert.IsNotNull(await dataMenager.DownloadJSONAsync("http://api.plos.org/search?q=title:DNA"));
            //SaaveJSON
            Assert.False(await dataMenager.SaveJSON("heioysam","aezakmi"));
            Assert.True(await dataMenager.SaveJSON("smth_text_ect", Directory.GetCurrentDirectory()));
            //GetValidation
            Assert.True(dirMenager.GetValidation(Directory.GetCurrentDirectory()));
            Assert.False(dirMenager.GetValidation("aezakmi"));
        }
    }
}
