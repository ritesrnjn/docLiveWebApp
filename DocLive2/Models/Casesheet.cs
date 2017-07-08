using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DocLive2.Models
{
    public class Casesheet
    {
        public string Id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public string sex { get; set; }
        public DateTime admission_date { get; set; }
        public string complaints_history { get; set; }
        public string vitals_bp { get; set; }
        public string vitals_sugar { get; set; }
        public int vitals_temp { get; set; }
        public string presonal_history { get; set; }
        public string examination_cardio { get; set; }
        public string examination_respiratory { get; set; }
        public string examination_abdomen { get; set; }
        public string diagnosis { get; set; }
        public string treatment_advised { get; set; }

    }
}