using eWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace eWeb.Controllers
{
    public class BaseController : Controller
    {
        protected readonly string _StudentUrl = "";
        protected readonly string _CoursesURL = "";
        protected readonly string _EnrollmentsURL = "";

        public BaseController() {

            var apiSettings = new ApiSetting();
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            configuration.GetSection("ApiUrls").Bind(apiSettings);

            this._StudentUrl = apiSettings.StudentsURL;
            this._CoursesURL = apiSettings.CoursesURL;
            this._EnrollmentsURL = apiSettings.EnrollmentsURL;
        }
    }
}
