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
using ToDoList3.Services;

namespace ToDoList3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListsController : ControllerBase
    {
        //private readonly IToDoListRepo _repository;
        private readonly ToDoListService _service;
        private readonly IMapper _mapper;
        public ToDoListsController(ToDoListService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ToDoList>> GetAllToDoLists()
        {
            var toDoListItems = _service.GetAllToDoLists();

            return Ok(_mapper.Map<IEnumerable<ToDoListReadDto>>(toDoListItems));
        }
        [HttpGet("{id}", Name = "GetToDoListById")]
        public ActionResult<ToDoListReadDto> GetToDoListById(int id)
        {
            var toDoListItem = _service.GetToDoListById(id);
            return Ok(_mapper.Map<ToDoListReadDto>(toDoListItem));
            
            
        }

        [HttpPost]

        public ActionResult<ToDoListReadDto> CreateToDoList(ToDoListCreateDto listCreateDto)
        {
            var listModel = _mapper.Map<ToDoList>(listCreateDto);
            _service.CreateToDoList(listModel);
            var listReadDto = _mapper.Map<ToDoListReadDto>(listModel);

            //return CreatedAtRoute(nameof(GetToDoListById), new { Id = listReadDto.Id }, listReadDto);
            return Ok(_service.GetAllToDoLists());
        }
        [HttpPut("{id}")]
        public ActionResult UpdateToDoList(int id, ToDoListUpdateDto listUpdateDto)
        {
            var listModelFromRepo = _service.GetToDoListById(id);

            _mapper.Map(listUpdateDto, listModelFromRepo);
            _service.UpdateToDoList(listModelFromRepo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteToDoList(int id)
        {
            var listModelFromRepo = _service.GetToDoListById(id);
            _service.DeleteToDoList(id);

            return Ok(_service.GetAllToDoLists());
        }

    }
}
