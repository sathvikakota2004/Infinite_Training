using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using CC9CODEFIRSTAPPROCH1.Models;  // Make sure to include your DbContext namespace

namespace CC9CODEFIRSTAPPROCH1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // EF Code First DB initializer (without migrations)
            Database.SetInitializer<MoviesDbContext>(new CreateDatabaseIfNotExists<MoviesDbContext>());
        }
    }
}
