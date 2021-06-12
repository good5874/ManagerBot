using ManagerBot.DAL.DataBase.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerBot.Admin.Web.Controllers
{
    public class TasksController : Controller
    {
        private ITaskRepository taskRepository { get; set; }

        public TasksController(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        public async Task<ActionResult> Index()
        {
            var tasks = await taskRepository.GetTaskWithIncludesAsync();
            return View(tasks.ToList());
        }
    }
}
