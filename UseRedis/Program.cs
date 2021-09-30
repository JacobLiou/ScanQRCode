using System;
using StackExchange.Redis;

namespace UseRedis
{
    class Program
    {
        static ConnectionMultiplexer connectionMultiplexer;

        /// <summary>
        /// Redis Server搭建教程  部署在centOs 或者UbuntuServer  也可以本机安装
        /// https://linuxize.com/post/how-to-install-and-configure-redis-on-centos-7/
        /// Google it Bing it 
        /// 
        /// 
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            connectionMultiplexer = ConnectionMultiplexer.Connect(
            new ConfigurationOptions
            {
                EndPoints = { "172.28.17.109:6379" },
            });
            var db = connectionMultiplexer.GetDatabase();
            var ping = db.PingAsync().Result;
            Console.WriteLine(ping);

            db.StringSet("foo", "Goody");

            var result = db.StringGet("foo");
            Console.WriteLine($"{result}");
        }
    }
}
