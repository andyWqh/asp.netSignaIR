using SPAWithKnockOutJs.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SPAWithKnockOutJs.Respositories
{
    /// <summary>
    /// 这里仓储直接使用示例数据作为演示，真实项目中需要从数据库中动态查询加载
    /// </summary>
    public class TaskRepository
    {
        #region Static Filed
        private static Lazy<TaskRepository> _taskRepositorory = new Lazy<TaskRepository>(() => new TaskRepository());
        public static TaskRepository Current
        {
            get
            {
                //获取当前Lazy<T>实例初始化的值
                return _taskRepositorory.Value;
            }
        }

        #endregion

        #region Fields
        private readonly List<Task> _tasks = new List<Task>() 
        {
            new Task
            {
                TaskId = 1,
                TaskName = "创建一个SPA程序",
                Description = "SPA(Single Page Web Application)的优势就是少量宽带，平滑体验",
                Owner = "andyWqh",
                FinishTime = DateTime.Parse(DateTime.Now.AddDays(2).ToString(CultureInfo.InstalledUICulture))
            },
            new Task
            {
                TaskId = 2,
                TaskName = "KnockoutJs 学习",
                Description = "KnockoutJs是一个MVVM类库,支持双向绑定",
                Owner = "AndySun",
                FinishTime = DateTime.Parse(DateTime.Now.AddDays(1).ToString(CultureInfo.InstalledUICulture))
            },
            new Task
            {
                TaskId =3,
                TaskName = "学习AngularJS",
                Description = "AngularJs是MVVM框架，集MVVM和MVC与一体。",
                Owner = "李志",
                FinishTime = DateTime.Parse(DateTime.Now.AddDays(3).ToString(CultureInfo.InvariantCulture))
            },
            new Task
            {
                TaskId =4,
                TaskName = "学习ASP.NET MVC网站",
                Description = "Glimpse是一款.NET下的性能测试工具，支持asp.net 、asp.net mvc, EF等等，优势在于，不需要修改原项目任何代码，且能输出代码执行各个环节的执行时间",
                Owner = "Tonny Li",
                FinishTime = DateTime.Parse(DateTime.Now.AddDays(4).ToString(CultureInfo.InvariantCulture))
            }
        };
        #endregion

        #region Public Methods
        public IEnumerable<Task> GetAll()
        {
            return this._tasks;
        }

        public Task Get(int taskId)
        {
            if(taskId <= 0)
            {
                return null;
            }
           return  _tasks.Find(m => m.TaskId == taskId);
        }

        public Task Add(Task item)
        {
            if(item == null)
            {
                throw new ArgumentNullException("item");
            }
            item.TaskId = item.TaskId + 1;
            _tasks.Add(item);
            return item;
        }

        public void Remove(int taskId)
        {
            if(taskId <= 0)
            {
                throw new ArgumentNullException("taskId");
            }
            _tasks.RemoveAll(m => m.TaskId == taskId);
        }

        public bool Update(Task item)
        {
            if(item == null)
            {
                throw new ArgumentNullException("item");
            }
            var taskItem = Get(item.TaskId);
            if(taskItem == null)
            {
                return false;
            }
            _tasks.Remove(taskItem);
            _tasks.Add(item);
            return true;
        }
        #endregion
    }
}