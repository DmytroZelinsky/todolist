using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ToDoList3.Dtos;
using ToDoList3.Models;

namespace ToDoList3.Profiles
{
    public class ToDoListProfile: Profile
    {
        public ToDoListProfile()
        {

            //Source => Target
            CreateMap<ToDoList, ToDoListReadDto>();
            CreateMap<ToDoListCreateDto, ToDoList>();
            CreateMap<ToDoListUpdateDto, ToDoList>();
             
        }
    }
}
