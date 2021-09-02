using NUnit.Framework;
using Moq;

using System.Collections.Generic;
using System.Linq;
using System;
using ToDoList3.Data;
using ToDoList3.Models;
using ToDoList3.Services;

namespace ToDoList3.Tests
{
    public class ServiceTests
    {
        private Mock<IToDoListRepo> _repositoryMock;
        private List<ToDoList> _testItems;
        private ToDoListService _service;
        private ToDoList _testItem;
        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IToDoListRepo>();
            _testItems = new List<ToDoList>() {
                new ToDoList() { Id = 0, Title="Test number 1", Text = "This is not a test" , State = 0, Date = "перше вересня"},
                new ToDoList(){ Id = 1, Title="Test number 2", Text = "This is a test(no)" , State = 1, Date = "друге вересня"}
            };
            _testItem = new ToDoList() {Id = 3, Title = "Edited", Text = "Wow", State = 3, Date = "третє вересня"};
            _service = new ToDoListService(_repositoryMock.Object);
        }

        [Test]
        public void GetAllToDoListsTest_ReturnsItemsIEnumerable()
        {
            //Arrange
            _repositoryMock
                .Setup(p => p.GetAllToDoLists())
                .Returns(_testItems.AsEnumerable());

            //Act
            var resItems = _service.GetAllToDoLists();

            //Assert
            Assert.AreEqual(_testItems, resItems);
        }

        [Test]
        public void GetToDoListByIdTest_ReturnsItem()
        {
            //Arrange
            _repositoryMock
                .Setup(p => p.GetToDoListById(It.IsAny<int>()))
                .Returns(_testItems[1]);

            //Act
            ToDoList resItem = _service.GetToDoListById(1);

            //Assert
            Assert.AreEqual(_testItems[1], resItem);
        }

        [Test]
        public void GetToDoListByIdTest_ThrowsException()
        {
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _service.GetToDoListById(-1));
        }

        [Test]
        public void CreateToDoListTest_AddsCorrect()
        {
            //Arrange
            ToDoList testItem = _testItem;
            _repositoryMock
                .Setup(p => p.CreateToDoList(It.IsAny<ToDoList>()))
                .Callback<ToDoList>(item => _testItems.Add(item));

            //Act
            _service.CreateToDoList(testItem);

            //Assert
            Assert.AreEqual(_testItems[_testItems.Count - 1], testItem);
        }

        [Test]
        public void CreateToDoListTest_ThrowsException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => _service.CreateToDoList(null));
        }

        [Test]
        public void UpdateToDoListTest_ReturnsCorrect()
        {
            //Arrange
            ToDoList testItem = _testItem;
            _repositoryMock
                .Setup(p => p.UpdateToDoList(It.IsAny<ToDoList>()))
                .Callback<ToDoList>(item => _testItems[_testItems.Count - 1] = item);

            //Act
            _service.UpdateToDoList(testItem);

            //Assert
            Assert.AreEqual(testItem, _testItems[_testItems.Count - 1]);
        }

        [Test]
        public void UpdateToDoListTest_ThrowsException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => _service.UpdateToDoList(null));
        }

        [Test]
        public void DeleteToDoListTest_DeletesItem()
        {
            var testitem = _testItems[0];
            //Arrange
            _repositoryMock
                .Setup(p => p.DeleteToDoList(It.IsAny<int>()))
                .Callback<int>(id => _testItems.RemoveAt(id));

            //Act
            _service.DeleteToDoList(1);

            //Assert
            Assert.IsTrue(_testItems.Count == 1);
            Assert.IsTrue(_testItems[0] == testitem);
            
        }


        [Test]
        public void DeleteToDoList_ThrowsException()
        {
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _service.DeleteToDoList(-1));
        }


    }



}