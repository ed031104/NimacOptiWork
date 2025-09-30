using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Entities;

[Table("TaskState")]
[Index("TaskStateName", Name = "UQ__TaskStat__A3600986089B19C7", IsUnique = true)]
public partial class TaskState
{
    [Key]
    [Column("TaskStateID")]
    public int TaskStateId { get; set; }

    [StringLength(50)]
    public string TaskStateName { get; set; } = null!;

    [InverseProperty("TaskState")]
    public virtual ICollection<TaskStatusHistory> TaskStatusHistories { get; set; } = new List<TaskStatusHistory>();
}
