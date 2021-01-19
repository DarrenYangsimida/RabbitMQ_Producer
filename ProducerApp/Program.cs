using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProducerApp.AppUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProducerApp
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var redisHelper = new RedisHelper(dbCount: 10);
            Application.Run(new Producer(redisHelper));
        }
    }
}
