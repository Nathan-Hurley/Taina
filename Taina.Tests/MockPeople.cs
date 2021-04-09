using System;
using System.Collections.Generic;
using Taina.Data.Models;

namespace Taina.Tests
{
	public static class MockPeople
	{
		public static Person Person_1()
		{
			var person = new Person()
			{
				Id = 1,
				FirstName = "John",
				Surname = "Smith",
				Gender = GenderType.Male,
				EmailAddress = "john.doe@hotmail.co.uk",
				PhoneNumber = "07700900223",
				DateOfBirth = new DateTime(1989, 8, 3)
			};

			return person;
		}

		public static Person Person_2()
		{
			var person = new Person()
			{
				Id = 2,
				FirstName = "Jane",
				Surname = "Doe",
				Gender = GenderType.Female,
				EmailAddress = "jane.doe@hotmail.co.uk",
				PhoneNumber = "07700900509",
				DateOfBirth = new DateTime(1988, 4, 13)
			};

			return person;
		}

		public static List<Person> People()
		{
			var people = new List<Person>()
			{
				Person_1(),
				Person_2()
			};

			return people;
		}
	}
}
