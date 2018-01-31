using ComicsManager.BackOffice.ViewModels;
using ComicsManager.Common;
using ComicsManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace ComicsManager.BackOffice.Controllers
{
    /// <summary>
    /// Contrôleur de base
    /// </summary>
    public class BaseController : Controller
    {
        protected readonly ComicsManagerContext _context;

        protected readonly IOptions<AppSettings> _config;

        public BaseController(
            ComicsManagerContext context,
            IOptions<AppSettings> config)
        {
            _context = context;
            _config = config;
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
