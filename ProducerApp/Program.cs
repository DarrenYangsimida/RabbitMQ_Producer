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

            //����ע��
            var services = new ServiceCollection();
            using ServiceProvider serviceProvider = services.BuildServiceProvider();

            var redisHelper = new RedisHelper();

            Application.Run(new Producer(redisHelper));
        }
    }
}
