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
            var users = await userRepository.GetUsersWithRolesAsync();
            return View(users.ToList());
        }

        public async Task<ActionResult> Refresh()
        {
            var users = await userRepository.GetUsersWithRolesAsync();
            foreach(var user in users)
            {
                user.Salary = 0;
            }
            await userRepository.UpdateAll(users);
            return View(nameof(Index), users.ToList());
        }
    }
}
