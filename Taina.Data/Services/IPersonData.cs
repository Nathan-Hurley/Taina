using System.Collections.Generic;
using Taina.Data.Models;

namespace Taina.Data.Services
{
	public interface IPersonData
    {
		IEnumerable<Person> GetAll();
		Person Get(int id);
		void Add(Person person);
		void Update(Person person);
		void Delete(int id);
	}
}
