using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class TaskAssignment
{
    public int TaskAssignmentId { get; set; }

    public Task Task { get; set; }

    public User AssignedTo { get; set; }

    public DateTime AssignedDate { get; set; }

    public DateTime? DateStarted { get; set; }

    public DateTime? DateCompleted { get; set; }

    public DateTime? TimeEstimatedStart { get; set; }

    public DateTime? TimeEstimatedEnd { get; set; }

    #region Constructors
    public TaskAssignment()
    {
    }
    public TaskAssignment(int taskAssignmentId, Task task, User assignedTo, DateTime assignedDate, DateTime? dateStarted, DateTime? dateCompleted, DateTime? timeEstimatedStart, DateTime? timeEstimatedEnd)
    {
        TaskAssignmentId = taskAssignmentId;
        this.Task = task;
        AssignedTo = assignedTo;
        AssignedDate = assignedDate;
        DateStarted = dateStarted;
        DateCompleted = dateCompleted;
        TimeEstimatedStart = timeEstimatedStart;
        TimeEstimatedEnd = timeEstimatedEnd;
    }
    #endregion

    #region Pattern Builder
    public class Builder
    {
        private readonly TaskAssignment _taskAssignment;
        public Builder()
        {
            _taskAssignment = new TaskAssignment();
        }
        public Builder WithTaskAssignmentId(int taskAssignmentId)
        {
            _taskAssignment.TaskAssignmentId = taskAssignmentId;
            return this;
        }
        public Builder WithTaskId(Task task)
        {
            _taskAssignment.Task = task;
            return this;
        }
        public Builder WithAssignedTo(User assignedTo)
        {
            _taskAssignment.AssignedTo = assignedTo;
            return this;
        }
        public Builder WithAssignedDate(DateTime assignedDate)
        {
            _taskAssignment.AssignedDate = assignedDate;
            return this;
        }
        public Builder WithDateStarted(DateTime? dateStarted)
        {
            _taskAssignment.DateStarted = dateStarted;
            return this;
        }
        public Builder WithDateCompleted(DateTime? dateCompleted)
        {
            _taskAssignment.DateCompleted = dateCompleted;
            return this;
        }
        public Builder WithTimeEstimatedStart(DateTime? timeEstimatedStart)
        {
            _taskAssignment.TimeEstimatedStart = timeEstimatedStart;
            return this;
        }
        public Builder WithTimeEstimatedEnd(DateTime? timeEstimatedEnd)
        {
            _taskAssignment.TimeEstimatedEnd = timeEstimatedEnd;
            return this;
        }
        public TaskAssignment Build()
        {
            return _taskAssignment;
        }
    }
    #endregion
}
