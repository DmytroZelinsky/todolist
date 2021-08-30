using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList3.Data;
using ToDoList3.Models;

namespace ToDoList3.Services
{
    public class ToDoListService
    {

        private readonly IToDoListRepo  _repository;

        public ToDoListService(IToDoListRepo repository)
        {
            _repository = repository ?? throw new ArgumentNullException();
        }

        public IEnumerable<ToDoList> GetAllToDoLists()
        {
            return _repository.GetAllToDoLists();
        }

        public ToDoList GetToDoListById(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _repository.GetToDoListById(id);
        }

        public void CreateToDoList(ToDoList item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }

            _repository.CreateToDoList(item);
        }

        public void UpdateToDoList(ToDoList item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }

            _repository.UpdateToDoList(item);
        }

        public void DeleteToDoList(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            _repository.DeleteToDoList(id);

        }
    }
}
