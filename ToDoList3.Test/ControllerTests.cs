using NUnit.Framework;
using Moq;

using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ToDoList3.Controllers;
using ToDoList3.Data;
using ToDoList3.Dtos;
using ToDoList3.Models;
using ToDoList3.Services;
using ToDoList3.Profiles;


namespace ToDoList3.Tests
{
    public class ControllerTests
    {
        private Mock<IToDoListService> _serviceMock;
        private List<ToDoList> _testToDoLists;
        private List<ToDoListReadDto> _testToDoListsReadDto;
        private ToDoListsController _controller;
        private ToDoListCreateDto _testDoToListCreateDto;
        private ToDoListUpdateDto _testDoToListUpdateDto;


        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IToDoListService>();
            _testToDoLists = new List<ToDoList>() {
                new ToDoList() { Id = 0, Title = "1", Text = "11" , State = 1, Date = "Mon Aug 30 2021"},
                new ToDoList() { Id = 0, Title = "2", Text = "22", State = 2, Date = "Mon Aug 30 2021"}
            };

            _testToDoListsReadDto = new List<ToDoListReadDto>() {
                new ToDoListReadDto() { Id = 0, Title = "1", Text = "11" , State = 1, Date = "Mon Aug 30 2021"},
                new ToDoListReadDto() { Id = 0, Title = "2", Text = "22", State = 2, Date = "Mon Aug 30 2021"}
            };

            _testDoToListCreateDto = new ToDoListCreateDto()
                {Title = "1", Text = "11", State = 1, Date = "Mon Aug 30 2021"};
            _testDoToListUpdateDto = new ToDoListUpdateDto()
                { Title = "1", Text = "11", State = 1, Date = "Mon Aug 30 2021" };


            _controller = new ToDoListsController(_serviceMock.Object, new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ToDoListProfile>();

            })));

        }

        [Test]
        public void GetAllToDoListsTest_ReturnsItemsIEnumerable()
        {
            //Arrange
            _serviceMock
                .Setup(p => p.GetAllToDoLists())
                .Returns(_testToDoLists.AsEnumerable());
            //Act
            ActionResult<IEnumerable<ToDoListReadDto>> resItems = _controller.GetAllToDoLists();
            var res = (IEnumerable<ToDoListReadDto>)((ObjectResult)resItems.Result).Value;
            //Assert
            Assert.IsTrue(res.AsEnumerable().SequenceEqual(_testToDoListsReadDto.AsEnumerable()));
        }

        [Test]
        public void GetToDoListByIdTest_ReturnsItem()
        {
            //Arrange
            _serviceMock
                .Setup(p => p.GetToDoListById(It.IsAny<int>()))
                .Returns(_testToDoLists[1]);

            //Act
            var resItem = _controller.GetToDoListById(1);
            //Assert
            Assert.AreEqual(_testToDoListsReadDto[1], (ToDoListReadDto)((ObjectResult)resItem.Result).Value);
        }

        [Test]
        public void CreateToDoListTest_AddsCorrect()
        {
            //Arrange
            _serviceMock
                .Setup(p => p.CreateToDoList(It.IsAny<ToDoList>()))
                .Callback<ToDoList>(item => _testToDoLists.Add(item));

            //Act
            _controller.CreateToDoList(_testDoToListCreateDto);

            //Assert
            Assert.AreEqual(_testToDoLists[_testToDoLists.Count - 1], _testToDoLists[0]);
        }


        [Test]
        public void UpdateToDoListTest_ReturnsCorrect()
        {
            //Arrange
            _serviceMock
                .Setup(p => p.UpdateToDoList(It.IsAny<ToDoList>()))
                .Callback<ToDoList>((item) => _testToDoLists[_testToDoLists.Count - 1] = item);

            _serviceMock
                .Setup(p => p.GetToDoListById(It.IsAny<int>()))
                .Returns<int>(id => _testToDoLists[id]);
            //Act
            _controller.UpdateToDoList(1,_testDoToListUpdateDto);

            //Assert
            Assert.AreEqual(_testToDoLists[0], _testToDoLists[_testToDoLists.Count - 1]);
        }


        [Test]
        public void DeleteToDoListTest_DeletesItem()
        {
            //Arrange
            _serviceMock
                .Setup(p => p.DeleteToDoList(It.IsAny<int>()))
                .Callback<int>(id => _testToDoLists.RemoveAt(id));

            //Act
            _controller.DeleteToDoList(1);

            //Assert
            Assert.IsTrue(_testToDoLists.Count == 1);

        }


    }
}
