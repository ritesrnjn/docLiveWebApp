using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DocLive2.Models
{
    public class QA
    {
        public string Id { get; set; }
        public string question { get; set; }
        public string ans { get; set; }
        public bool flag { get; set; }

    }
}