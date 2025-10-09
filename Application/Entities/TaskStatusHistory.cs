using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Entities;

[Table("TaskStatusHistory")]
public partial class TaskStatusHistory
{
    [Key]
    [Column("TaskStatusHistoryID")]
    public int TaskStatusHistoryId { get; set; }

    [Column("TaskStateID")]
    public int TaskStateId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ChangedDate { get; set; }

    [StringLength(50)]
    public string ChangedBy { get; set; } = null!;

    public int TaskAssignmentId { get; set; }

    [ForeignKey("TaskAssignmentId")]
    [InverseProperty("TaskStatusHistories")]
    public virtual TaskAssignment TaskAssignment { get; set; } = null!;

    [ForeignKey("TaskStateId")]
    [InverseProperty("TaskStatusHistories")]
    public virtual TaskState TaskState { get; set; } = null!;
}
