
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DocLive2.Models
{
    public class Contact
    {
       public string Id { get; set; }
        public int UniqueId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}