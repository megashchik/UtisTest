using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.DTO
{
    internal record Person : IPerson
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        [Column(TypeName = "Date")]
        public DateTime Birthday { get; set; }
        //public DateTime DateOfBirth { get; set; }
        public Sex Sex { get; set; }
        public bool HaveChildren { get; set; }
        /*[NotMapped]
        public DateOnly Birthday {
            get => DateOnly.FromDateTime(DateOfBirth);
            set => DateOfBirth = value.ToDateTime(TimeOnly.MinValue);
                }*/

    }
}
