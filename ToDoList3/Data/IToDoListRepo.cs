using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList3.Models;

namespace ToDoList3.Data
{
    public interface IToDoListRepo
    {

        bool SaveChanges();
        IEnumerable<ToDoList> GetAllToDoLists();
        ToDoList GetToDoListById(int id);
        void CreateToDoList(ToDoList list);
        void UpdateToDoList(ToDoList list);
        void DeleteToDoList(int id);
    }
}
