using System;
using System.Collections.Generic;
using System.Linq;
namespace OOP2.Infrastructure.External
{
    public class JsonPlaceholderUser
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public JsonPlaceholderAddress address { get; set; }
        public string website { get; set; }
        public JsonPlaceholderCompany company { get; set; }
    }

    public class JsonPlaceholderAddress
    {
        public string street { get; set; }
        public string city { get; set; }
        public JsonPlaceholderGeo geo { get; set; }
    }

    public class JsonPlaceholderGeo
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }

    public class JsonPlaceholderCompany
    {
        public string name { get; set; }
    }
}