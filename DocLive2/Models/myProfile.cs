using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;


namespace DocLive2.Models
{
    public class myProfile
    {
        public string Id { get; set; }
        public string userId { get; set;}
        public string name { get; set; }
        public int age { get; set; }
        public string sex { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string pin { get; set; }
        public string maritalstatus { get; set; }

    }
}