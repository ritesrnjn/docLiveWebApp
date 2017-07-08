using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DocLive2.Models
{
    public class DoctorInfo
    {
        public string Id { get; set; }
        public string medicalid { get; set; }
        public string doctornames { get; set; }
        public string specialization { get; set; }
        public string aboutdoctor { get; set; }
    }
}