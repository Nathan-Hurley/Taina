using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taina.Data.Services;

namespace Taina.Web
{
	public class ContainerConfig
	{
		internal static void RegisterContainer()
		{
			var builder = new ContainerBuilder();

			builder.RegisterControllers(typeof(MvcApplication).Assembly);
			builder.RegisterType<SqlPersonData>()
				   .As<IPersonData>()
				   .InstancePerRequest();
			builder.RegisterType<TainaDbContext>().InstancePerRequest();

			var container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}