using System;
using System.Collections.Generic;
using StackExchange.Redis;
using System.Text.Json;

namespace UseRedis
{
    class Program
    {
        static ConnectionMultiplexer connectionMultiplexer;

        class Person
        {
            public string Id { get; set; }

            public int Age { get; set; }

            public string Name { get; set; }

            public decimal Salary { get; set; }

            public List<Person> Relatives { get; set; }
        }

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

            var person = new Person { Id = Guid.NewGuid().ToString(), Age = 23, Name = "Ghsdhg", Salary = 10000, Relatives = null };
            db.StringSet("person1", JsonSerializer.Serialize(person));
            var result = db.StringGet("foo");


            var jsonPerson = db.StringGet("person1");
            var realPerson = JsonSerializer.Deserialize<Person>(jsonPerson);
            Console.WriteLine($"{jsonPerson}");

            Console.WriteLine($"{result}");
        }
    }
}
