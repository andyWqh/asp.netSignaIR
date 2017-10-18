using SPAWithKnockOutJs.Models;
using SPAWithKnockOutJs.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SPAWithKnockOutJs.Controllers
{
    /// <summary>
    /// Task WebAPI提供数据服务
    /// </summary>
    public class TasksController : ApiController
    {
        private readonly TaskRepository _taskRepository = TaskRepository.Current;

        
        public IEnumerable<Task> GetAll()
        {
            return _taskRepository.GetAll().OrderBy(m => m.TaskId);
        }

        public Task Get(int taskId)
        {
            if (taskId <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.ExpectationFailed);
            }
            var item = _taskRepository.Get(taskId);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        //[HttpGet]
        //[Route("api/tasks/GetByState")]
        public IEnumerable<Task> GetByState(string state)
        {
            IEnumerable<Task> results = new List<Task>();
            switch (state)
            {
                case "":
                case "all":
                    {
                        results = _taskRepository.GetAll();
                        break;
                    }
                case"actived":
                    {
                        results = _taskRepository.GetAll().Where(m => m.State == TaskState.Active);
                        break;
                    }
                case "completed":
                    {
                        results = _taskRepository.GetAll().Where(m => m.State == TaskState.Completed);
                        break;
                    }
                default: break;
            }
            results = results.OrderBy(m => m.TaskId);
            return results;
        }

        [HttpPost]
        public Task Create(Task item)
        {
            return _taskRepository.Add(item);
        }

        public void PutUpdate(Task item)
        {
            if(!_taskRepository.Update(item))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void  Delete(int id)
        {
            if (id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
            }
            _taskRepository.Remove(id);
        }
    }
}
