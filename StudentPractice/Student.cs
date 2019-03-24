using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StudentPractice
{
    public class RootObject
    {
        public Student[] Student { get; set; }
    }
    public class Student
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "totalMinutes")]
        public int TotalMinutes { get; set; }

        public void Print()
        {
            Console.WriteLine("Id: " + Id);

        }
    }
}
