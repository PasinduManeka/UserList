using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserList.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        //can't be null
        [Required]
        public String Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public String City { get; set; }
    }
}
