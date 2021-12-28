using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class tbCustomer
    {
        public int PersonID { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        public bool? IsDeleted { get; set; }

        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public DateTime Accesstime { get; set; }
       

    }
}
