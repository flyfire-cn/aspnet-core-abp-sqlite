using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace flyfire.IMS.Web.Host.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            InitBuildWebHost(args).Run();
        }

        public static IWebHost InitBuildWebHost(string[] args)
        {
            var config = new ConfigurationBuilder()
            //.AddInMemoryCollection()                //将配置文件的数据加载到内存中
            //.SetBasePath(AppContext.BaseDirectory)
            .SetBasePath(Directory.GetCurrentDirectory())       //指定配置文件所在的目录
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)              //指定加载的配置文件
            .Build();               //编译成对象

            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                //.UseUrls("http://*:21021")
                .UseUrls(config["App:ServerRootAddress"])
                .Build();
        }
    }
}
