using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class TaskStatusHistory
{
    public int TaskStatusHistoryId { get; set; }

    public virtual Task Task { get; set; } = null!;

    public virtual TaskState TaskState { get; set; } = null!;
    
    public DateTime ChangedDate { get; set; }

    public string ChangedBy { get; set; } = null!;

    #region Constructors
    public TaskStatusHistory()
    {
    }
    public TaskStatusHistory(int taskStatusHistoryId, Task task, TaskState taskState, DateTime changedDate, string changedBy)
    {
        TaskStatusHistoryId = taskStatusHistoryId;
        this.Task = task;
        this.TaskState = taskState;
        ChangedDate = changedDate;
        ChangedBy = changedBy;
    }
    #endregion

    #region Pattern Builder
    public class Builder
    {
        private readonly TaskStatusHistory _taskStatusHistory;
        public Builder()
        {
            _taskStatusHistory = new TaskStatusHistory();
        }
        public Builder WithTaskStatusHistoryId(int taskStatusHistoryId)
        {
            _taskStatusHistory.TaskStatusHistoryId = taskStatusHistoryId;
            return this;
        }
        public Builder WithTask(Task task)
        {
            _taskStatusHistory.Task = task;
            return this;
        }
        public Builder WithTaskState(TaskState taskState)
        {
            _taskStatusHistory.TaskState = taskState;
            return this;
        }
        public Builder WithChangedDate(DateTime changedDate)
        {
            _taskStatusHistory.ChangedDate = changedDate;
            return this;
        }
        public Builder WithChangedBy(string changedBy)
        {
            _taskStatusHistory.ChangedBy = changedBy;
            return this;
        }
        public TaskStatusHistory Build()
        {
            return _taskStatusHistory;
        }
    }
    #endregion

}
