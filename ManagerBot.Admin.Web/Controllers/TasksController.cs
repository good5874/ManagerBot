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

        public async Task<ActionResult> CheckingTasks()
        {
            var tasks = await taskRepository.GetNotCompletedTasksWithIncludes();
            return View(tasks.ToList());
        }

        public async Task<ActionResult> CompleteTask(int TaskId)
        {
            var task = await taskRepository.GetTaskWithIncludesByTaskId(TaskId);

            task.User.Salary += task.Operation.Cost * task.AmountOperations;
            task.IsFinish = true;

            await taskRepository.UpdateAsync(task);

            var tasks = await taskRepository.GetNotCompletedTasksWithIncludes();
            return View(nameof(CheckingTasks), tasks.ToList());
        }
    }
}
