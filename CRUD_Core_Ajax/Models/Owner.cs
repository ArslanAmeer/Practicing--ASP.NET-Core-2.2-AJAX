using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Core_Ajax.Models
{
    public class Owner
    {
        public Owner()
        {
            Cars = new List<Car>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Phone { get; set; }

        public List<Car> Cars { get; set; }
    }
}
