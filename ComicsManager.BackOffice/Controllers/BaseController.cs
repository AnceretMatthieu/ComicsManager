using ComicsManager.Model;
using Microsoft.AspNetCore.Mvc;

namespace ComicsManager.BackOffice.Controllers
{
    /// <summary>
    /// Contrôleur de base
    /// </summary>
    public class BaseController : Controller
    {
        protected readonly ComicsManagerContext _context;

        public BaseController(ComicsManagerContext context)
        {
            _context = context;
        }
    }
}
