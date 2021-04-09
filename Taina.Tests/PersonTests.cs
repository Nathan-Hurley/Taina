using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Taina.Data.Models;
using Taina.Data.Services;
using Taina.Web.Controllers;

namespace Taina.Tests
{
	[TestClass]
	public class PersonTests
	{
		#region Get - Unit Tests
		[TestMethod]
		public void GetListOfPeople_FromMockDatabase_TestValid()
		{
			//-- Arrange
			var mockRepo = new Mock<IPersonData>();
			mockRepo.Setup(repo => repo.GetAll()).Returns(MockPeople.People());
			var controller = new HomeController(mockRepo.Object);
			
			//-- Act
			var result = controller.Index();

			//-- Assert
			var viewResult = result as ViewResult;
			var actual = viewResult.Model as List<Person>;
			Assert.AreEqual(2, actual.Count);
			Assert.AreEqual("Index", viewResult.ViewName);
		}
		#endregion

		#region Details - Unit Tests
		[TestMethod]
		public void GetSpecificPerson_FromMockDatabase_TestValid()
		{
			//-- Arrange
			var mockRepo = new Mock<IPersonData>();
			mockRepo.Setup(repo => repo.Get(1)).Returns(MockPeople.Person_1());
			var controller = new HomeController(mockRepo.Object);
			var expectedFirstName = "John";
			var expectedPhoneNumber = "07700900223";

			//-- Act
			var result = controller.Details(1);

			//-- Assert
			var viewResult = result as ViewResult;
			var actual = viewResult.Model as Person;
			Assert.AreEqual(expectedFirstName, actual.FirstName);
			Assert.AreEqual(expectedPhoneNumber, actual.PhoneNumber);
			Assert.AreEqual("Details", viewResult.ViewName);
		}

		[TestMethod]
		public void GetSpecificPerson_FromMockDatabase_TestInvalid()
		{
			//-- Arrange
			var mockRepo = new Mock<IPersonData>();
			var controller = new HomeController(mockRepo.Object);

			//-- Act
			var result = controller.Details(2);

			//-- Assert
			var viewResult = result as ViewResult;
			var actual = viewResult.Model as Person;
			Assert.IsNull(actual);
			Assert.AreEqual("NotFound", viewResult.ViewName);
		}
		#endregion

		#region Create - Unit Tests
		[TestMethod]
		public void DisplayCreateView_AndAddToMockDatabase_TestValid()
		{
			//-- Arrange
			var mockRepo = new Mock<IPersonData>();
			var controller = new HomeController(mockRepo.Object);

			//-- Act
			var resultView = controller.Create();

			//-- Assert
			var view = resultView as ViewResult;
			Assert.AreEqual("Create", view.ViewName);
		}

		[TestMethod]
		public void CreateNewPerson_AndAddToMockDatabase_TestValid()
		{
			//-- Arrange
			var mockRepo = new Mock<IPersonData>();
			var controller = new HomeController(mockRepo.Object);

			//-- Act
			var resultModel = controller.Create(MockPeople.Person_1());

			//-- Assert
			var viewResult = resultModel as RedirectToRouteResult;
			Assert.AreEqual("Details", viewResult.RouteValues["action"]);
		}

		[TestMethod]
		public void CreateNewPerson_AndAddToMockDatabase_TestInvalid()
		{
			//-- Arrange
			var mockRepo = new Mock<IPersonData>();
			var controller = new HomeController(mockRepo.Object);

			//-- Act
			var result = controller.Create(null);

			//-- Assert
			var viewResult = result as ViewResult;
			Assert.AreEqual("NotFound", viewResult.ViewName);
		}
		#endregion

		#region Edit - Unit Tests
		[TestMethod]
		public void DisplayEditView_FromMockDatabase_TestValid()
		{
			//-- Arrange
			var mockRepo = new Mock<IPersonData>();
			mockRepo.Setup(repo => repo.Get(1)).Returns(MockPeople.Person_1());
			var controller = new HomeController(mockRepo.Object);

			//-- Act
			var resultView = controller.Edit(1);

			//-- Assert
			var view = resultView as ViewResult;
			Assert.AreEqual("Edit", view.ViewName);
		}

		[TestMethod]
		public void EditSpecificPerson_FromMockDatabase_TestValid()
		{
			//-- Arrange
			var newPerson = MockPeople.Person_1();
			newPerson.FirstName = "Jane";
			newPerson.Surname = "Doe";
			newPerson.Gender = GenderType.Female;
			newPerson.EmailAddress = "jane.doe@hotmail.co.uk";

			var mockRepo = new Mock<IPersonData>();
			mockRepo.Setup(repo => repo.Get(1)).Returns(MockPeople.Person_1());
			mockRepo.Setup(r => r.Update(newPerson)).Verifiable();
			var controller = new HomeController(mockRepo.Object);
			
			//-- Act
			var result = controller.Edit(newPerson);

			//-- Assert
			var viewResult = result as RedirectToRouteResult;
			Assert.AreEqual("Details", viewResult.RouteValues["action"]);
		}

		[TestMethod]
		public void EditSpecificPerson_FromMockDatabase_TestInvalid()
		{
			//-- Arrange
			var mockRepo = new Mock<IPersonData>();
			mockRepo.Setup(repo => repo.Update(null)).Throws(new InvalidOperationException());
			var controller = new HomeController(mockRepo.Object);

			//-- Act
			var result = controller.Edit(null);

			//-- Assert
			var viewResult = result as ViewResult;
			Assert.AreEqual("NotFound", viewResult.ViewName);
		}
		#endregion

		#region Delete - Unit Tests
		[TestMethod]
		public void DisplayDeleteView_FromMockDatabase_TestValid()
		{
			//-- Arrange
			var mockRepo = new Mock<IPersonData>();
			mockRepo.Setup(repo => repo.Get(1)).Returns(MockPeople.Person_1());
			var controller = new HomeController(mockRepo.Object);

			//-- Act
			var resultView = controller.Delete(1);

			//-- Assert
			var view = resultView as ViewResult;
			Assert.AreEqual("Delete", view.ViewName);
		}

		[TestMethod]
		public void DeleteSpecificPerson_FromMockDatabase_TestValid()
		{
			//-- Arrange
			var mockRepo = new Mock<IPersonData>();
			mockRepo.Setup(repo => repo.Delete(1));
			var controller = new HomeController(mockRepo.Object);

			//-- Act
			var result = controller.Delete(1, null);

			//-- Assert
			var viewResult = result as RedirectToRouteResult;
			Assert.AreEqual("Index", viewResult.RouteValues["action"]);
		}

		[TestMethod]
		public void DeleteSpecificPerson_FromMockDatabase_TestInvalid()
		{
			//-- Arrange
			var mockRepo = new Mock<IPersonData>();
			mockRepo.Setup(repo => repo.Delete(1)).Throws(new InvalidOperationException());
			var controller = new HomeController(mockRepo.Object);

			//-- Act
			var result = controller.Delete(0);

			//-- Assert
			var viewResult = result as ViewResult;
			Assert.AreEqual("NotFound", viewResult.ViewName);
		}
		#endregion
	}
}
