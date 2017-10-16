using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPAWithKnockOutJs.Models
{
    /// <summary>
    /// 任务状态标识
    /// </summary>
    public enum TaskState
    {
        Active = 1,
        Completed = 2
    }

    /// <summary>
    /// 任务状态实体
    /// </summary>
    public class Task
    {
        /// <summary>
        /// 任务标识Id
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 任务描述内容
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime FinishTime { get; set; }

        /// <summary>
        /// 拥有者
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public TaskState State { get; set; }

        public Task()
        {
            CreateTime = DateTime.Parse(DateTime.Now.ToLongDateString());
            State = TaskState.Active;
        }
    }
}