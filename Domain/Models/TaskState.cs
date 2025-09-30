using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class TaskState
{
    public int TaskStateId { get; set; }

    public string TaskStateName { get; set; } = null!;

    
    public TaskState()
    {
    }
    public TaskState(int taskStateId, string taskStateName)
    {
        TaskStateId = taskStateId;
        TaskStateName = taskStateName;
    }
}
