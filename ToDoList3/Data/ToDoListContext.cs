using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList3.Models;

namespace ToDoList3.Data
{
    public class ToDoListContext: DbContext
    {
        public ToDoListContext(DbContextOptions<ToDoListContext> opt) : base(opt)
        {

        }

        public DbSet<ToDoList> ToDoLists { get; set; }
    }
}
