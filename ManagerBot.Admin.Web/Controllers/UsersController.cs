using ManagerBot.DAL.DataBase.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerBot.Admin.Web.Controllers
{
    public class UsersController : Controller
    {
        private IUserRepository userRepository { get; set; }

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ActionResult> Index()
        {
            var tasks = await userRepository.GetUsersWithRolesAsync();
            return View(tasks.ToList());
        }
    }
}
