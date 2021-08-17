using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList3.Models
{
    public class ToDoList
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int State { get; set; }

        [Required]
        public string Date { get; set; }


    }
}
