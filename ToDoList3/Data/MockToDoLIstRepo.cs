using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList3.Models;

namespace ToDoList3.Data
{
    public class MockToDoLIstRepo : IToDoListRepo
    {
        public ToDoList GetToDoListById(int id)
        {
            return new ToDoList { Id = 0, Title = "Test title 1", Text = "Test text 1" };

        }

        public IEnumerable<ToDoList> GetAllToDoLists()
        {
            return new List<ToDoList> { new ToDoList { Id = 0, Title = "Test title all", Text = "Test text all" } };
        
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void CreateToDoList(ToDoList list)
        {
            throw new NotImplementedException();
        }

        public void UpdateToDoList(ToDoList list)
        {
            throw new NotImplementedException();
        }

        public void DeleteToDoList(ToDoList list)
        {
            throw new NotImplementedException();
        }

        public void DeleteToDoList(int id)
        {
            throw new NotImplementedException();
        }
    }
}
