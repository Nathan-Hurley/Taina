using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Taina.Data.Models;

namespace Taina.Data.Services
{
	public class SqlPersonData : IPersonData
	{
		private readonly TainaDbContext db;

		public SqlPersonData(TainaDbContext db)
		{
			this.db = db;
		}

		public void Add(Person person)
		{
			db.People.Add(person);
			db.SaveChanges();
		}

		public void Delete(int id)
		{
			var person = db.People.Find(id);
			db.People.Remove(person);
			db.SaveChanges();
		}

		public Person Get(int id)
		{
			return db.People.FirstOrDefault(p => p.Id == id);
		}

		public IEnumerable<Person> GetAll()
		{
			return db.People.OrderBy(p => p.FirstName);
		}

		public void Update(Person person)
		{
			var entry = db.Entry(person);
			entry.State = EntityState.Modified;
			db.SaveChanges();
		}
	}
}
