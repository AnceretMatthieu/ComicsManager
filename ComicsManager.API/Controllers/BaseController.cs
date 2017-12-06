using ComicsManager.Model;
using Microsoft.AspNetCore.Mvc;

namespace ComicsManager.API.Controllers
{
    /// <summary>
    /// Contrôleur de base
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// DbContext
        /// </summary>
        protected readonly ComicsManagerContext _context;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="context">DbContext</param>
        public BaseController(ComicsManagerContext context)
        {
            _context = context;
        }
    }
}
