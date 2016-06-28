using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TestApi1.Models;
using System.Web.Script.Serialization;
namespace TestApi1.Controllers
{
    public class VehicleController : ApiController
    {
        static void TaskCancel()
        {
            string lines = "Task cancelled timeout";
            TextWriter tw = new StreamWriter("D:\\Sample/taskcancel.txt", true);

            //Write to file
            tw.WriteLine(lines);
            tw.Close();
        }

        List<Vehicle> emp = new List<Vehicle>()
        {
            new Vehicle { Vid = 1, Name = "Benz"},
            new Vehicle { Vid = 2, Name = "BMW"},
        };

        public List<Vehicle> GetVehicle(string param)
        {
            var CancelTokensrc = new CancellationTokenSource();
            var cancelToken = CancelTokensrc.Token;

            cancelToken.Register(() => TaskCancel());
            string filename = "process " + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
            //var t = Task.Factory.StartNew(() => TestMethod(cancelToken, filename), CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            //Task.Run(() => TestMethod(cancelToken, filename), cancelToken);
            //uncomment above line ,up to above code will do mutitask simultaneously
            //brlow code is newly added for callback

            Vehicleparam objVehicle = new JavaScriptSerializer().Deserialize<Vehicleparam>(param);
            CancelTokensrc.CancelAfter(10000); // To cancel task after some specific time
            Task a = Task.Run(() => TestMethod(cancelToken, filename), cancelToken);

            Task b = a.ContinueWith((Taskcallback) =>
           {
               testcall(filename, objVehicle);

           });

            return emp;

        }
        private void Taskcallback(string filename,Vehicleparam v)
        {
            string lines = DateTime.Now.ToString();
            TextWriter tw = new StreamWriter("D:\\Sample/" + filename + " callbk.txt", true);
            tw.WriteLine(lines);
            var json = new JavaScriptSerializer().Serialize(v);
            tw.WriteLine(json);

            //Write to file
            
            tw.Close();
        }
        static async void TestMethod(CancellationToken cancel, string process)
        {
            for (int i = 0; i < 100000; i++)
            {

                if (cancel.IsCancellationRequested)
                {
                    break;
                }
                string lines = i.ToString() + " - " + DateTime.Now.ToString("HH:mm:ss.ffffff");
                string FileName = process;
                TextWriter tw = new StreamWriter("D:\\Sample/" + FileName + ".txt", true);

                //Write to file
                tw.WriteLine(lines);
                tw.Close();
                //System.IO.StreamWriter file = new System.IO.StreamWriter("D:\\Sample/Process.txt");
                //file.WriteLine(lines);
                //file.Close();
            }
        }

        //private Action<Task> TestCallback()
        //{
        //    string lines = DateTime.Now.ToString();
        //    TextWriter tw = new StreamWriter("D:\\Sample/call.txt", true);

        //    //Write to file
        //    tw.WriteLine(lines);
        //    tw.Close();
        //}



        //public string GetVehicle()
        //{
        //    var CancelTokensrc = new CancellationTokenSource();
        //    var cancelToken = CancelTokensrc.Token;
        //    cancelToken.Register(() => TaskCancel());

        //    var t = Task.Factory.StartNew(() => TestMethod(cancelToken), CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        //    return "Your request was under process please wait...";

        //}





    }
}
