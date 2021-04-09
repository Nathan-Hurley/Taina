using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Taina.Data.Models;
using Taina.Data.Services;
using Taina.Web.Utilities;

namespace Taina.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly IPersonData db;

		public HomeController(IPersonData db)
		{
			this.db = db;
		}

		/// <summary>
		/// Get Home view page and display all People in the list
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Index()
		{
			IEnumerable<Person> model = new List<Person>();

			try
			{
				model = db.GetAll();
			}
			catch (Exception e)
			{
				Log.Fatal(Helper.FATAL_ERROR, e);
				return View("Error");
			}

			return View("Index", model);
		}

		/// <summary>
		/// Get Details view of the Person by ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Details(int id)
		{
			Person model = new Person();

			try
			{
				model = db.Get(id);
				CheckForNullPerson(model);
			}
			catch (ArgumentException e)
			{
				Log.Error(Helper.NOT_FOUND, e);
				return View("NotFound");
			}
			catch (Exception e)
			{
				Log.Fatal(Helper.FATAL_ERROR, e);
				return View("Error");
			}

			return View("Details", model);
		}

		/// <summary>
		/// Get Create view to add a new Person
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Create()
		{
			return View("Create");
		}

		/// <summary>
		/// Recieves the Person information back from the user
		/// </summary>
		/// <param name="person"></param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Person person)
		{
			try
			{
				if (ModelState.IsValid)
				{
					db.Add(person);
					Log.Info(Helper.ADD_NEW_PERSON_SUCCESS);
					return RedirectToAction("Details", new { id = person.Id });
				}
			}
			catch (NullReferenceException e)
			{
				Log.Error(Helper.UPDATE_ERROR, e);
				return View("NotFound");
			}
			catch (Exception e)
			{
				Log.Fatal(Helper.FATAL_ERROR, e);
				return View("Error");
			}

			return View("Create");
		}

		/// <summary>
		/// Get Edit page view for Person else return not found
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Edit(int id)
		{
			Person model = new Person();

			try
			{
				model = db.Get(id);
				CheckForNullPerson(model);
			}
			catch (ArgumentException e)
			{
				Log.Error(Helper.NOT_FOUND, e);
				return View("NotFound");
			}
			catch (Exception e)
			{
				Log.Fatal(Helper.FATAL_ERROR, e);
				return View("Error");
			}

			return View("Edit", model);
		}

		/// <summary>
		/// Update changes to Person after clicking the Save button
		/// </summary>
		/// <param name="person"></param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Person person)
		{
			try
			{
				if (ModelState.IsValid)
				{
					db.Update(person);
					Log.Info(Helper.UPDATE_SUCCESS);
					return RedirectToAction("Details", new { id = person.Id });
				}
			}
			catch (InvalidOperationException e)
			{
				Log.Error(Helper.UPDATE_ERROR, e);
				return View("NotFound");
			}
			catch (Exception e)
			{
				Log.Fatal(Helper.FATAL_ERROR, e);
				return View("Error");
			}

			return View("Edit");
		}

		/// <summary>
		/// Get Delete view to remove a Person
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Delete(int id)
		{
			Person model = new Person();

			try
			{
				model = db.Get(id);
				CheckForNullPerson(model);
			}
			catch (ArgumentException e)
			{
				Log.Error(Helper.NOT_FOUND, e);
				return View("NotFound");
			}
			catch (Exception e)
			{
				Log.Fatal(Helper.FATAL_ERROR, e);
				return View("Error");
			}

			return View("Delete", model);
		}

		/// <summary>
		/// Change the database by removing the Person from it
		/// </summary>
		/// <param name="id"></param>
		/// <param name="form">Unused</param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, FormCollection form)
		{
			try
			{
				db.Delete(id);
				Log.Info(Helper.DELETED_PERSON_SUCCESS);
				return RedirectToAction("Index");
			}
			catch (InvalidOperationException e)
			{
				Log.Fatal(Helper.NOT_FOUND, e);
				return View("NotFound");
			}
			catch (Exception e)
			{
				Log.Fatal(Helper.FATAL_ERROR, e);
				return View("Error");
			}
		}

		private void CheckForNullPerson(Person model)
		{
			if (model == null)
				throw new ArgumentException();
		}
	}
}