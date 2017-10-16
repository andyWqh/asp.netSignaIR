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
    public class TaskController : ApiController
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

        [Route("api/tasks/GetByState")]
        public IEnumerable<Task> GetByState(string taskState)
        {
            IEnumerable<Task> results = new List<Task>();
            switch (taskState)
            {
                case "":
                case "all":
                    {
                        results = _taskRepository.GetAll();
                        break;
                    }
                case"Active":
                    {
                        results = _taskRepository.GetAll().Where(m => m.State == TaskState.Active);
                        break;
                    }
                case "Completed":
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

        [HttpPut]
        public void Put(Task item)
        {
            if(_taskRepository.Update(item))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void  Delete(int taskId)
        {
            if(taskId <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
            }
            _taskRepository.Remove(taskId);
        }
    }
}
