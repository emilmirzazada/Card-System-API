using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSystem.Domain.Entities
{
    public class VendorAddress
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public string Address { get; set; }
    }
}
