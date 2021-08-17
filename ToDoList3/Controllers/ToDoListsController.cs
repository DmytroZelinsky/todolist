using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList3.Data;
using ToDoList3.Dtos;
using ToDoList3.Models;

namespace ToDoList3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListsController : ControllerBase
    {
        private readonly IToDoListRepo _repository;
        private readonly IMapper _mapper;
        public ToDoListsController(IToDoListRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ToDoList>> GetAllToDoLists()
        {
            var toDoListItems = _repository.GetAllToDoLists();

            return Ok(_mapper.Map<IEnumerable<ToDoListReadDto>>(toDoListItems));
        }
        [HttpGet("{id}", Name = "GetToDoListById")]
        public ActionResult<ToDoListReadDto> GetToDoListById(int id)
        {
            var toDoListItem = _repository.GetToDoListById(id);
            if (toDoListItem != null)
            {
                return Ok(_mapper.Map<ToDoListReadDto>(toDoListItem));
            }
            return NotFound();
        }

        [HttpPost]

        public ActionResult<ToDoListReadDto> CreateToDoList(ToDoListCreateDto listCreateDto)
        {
            var listModel = _mapper.Map<ToDoList>(listCreateDto);
            _repository.CreateToDoList(listModel);
            _repository.SaveChanges();

            var listReadDto = _mapper.Map<ToDoListReadDto>(listModel);

            //return CreatedAtRoute(nameof(GetToDoListById), new { Id = listReadDto.Id }, listReadDto);
            return Ok(_repository.GetAllToDoLists());
        }
        [HttpPut("{id}")]
        public ActionResult UpdateToDoList(int id, ToDoListUpdateDto listUpdateDto)
        {
            var listModelFromRepo = _repository.GetToDoListById(id);
            if (listModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(listUpdateDto, listModelFromRepo);
            _repository.UpdateToDoList(listModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteToDoList(int id)
        {
            var listModelFromRepo = _repository.GetToDoListById(id);
            if(listModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteToDoList(listModelFromRepo);
            _repository.SaveChanges();
            return Ok(_repository.GetAllToDoLists());
        }

    }
}
