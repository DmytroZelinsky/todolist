using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList3.Models;

namespace ToDoList3.Data
{
    public class SqlToDoListRepo : IToDoListRepo
    {
        private readonly ToDoListContext _context;

        public SqlToDoListRepo(ToDoListContext context)
        {
            _context = context;
        }
        public ToDoList GetToDoListById(int id)
        {
            return _context.ToDoLists.FirstOrDefault(p => p.Id == id);

        }

        public IEnumerable<ToDoList> GetAllToDoLists()
        {
            return _context.ToDoLists.ToList();

        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void CreateToDoList(ToDoList list)
        {
            if(list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            _context.ToDoLists.Add(list);
        }

        public void UpdateToDoList(ToDoList list)
        {

        }

        public void DeleteToDoList(ToDoList list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            _context.ToDoLists.Remove(list);
        }
    }
}
