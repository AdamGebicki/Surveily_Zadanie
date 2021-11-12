using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json_Url
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //TEST
            //NUnitTest nUnitTest = new();
            //nUnitTest.Test();

            //v.UI
            //ControllerUI uIController = new();
            //await uIController.StartProcess();

            //v.mashine
            Controller controller = new();
            await controller.StartProcessAsync();
        }
    }
}
