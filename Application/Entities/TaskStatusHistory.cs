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

    [Column("TaskID")]
    public int TaskId { get; set; }

    [Column("TaskStateID")]
    public int TaskStateId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ChangedDate { get; set; }

    [StringLength(50)]
    public string ChangedBy { get; set; } = null!;

    [ForeignKey("TaskId")]
    [InverseProperty("TaskStatusHistories")]
    public virtual Task Task { get; set; } = null!;

    [ForeignKey("TaskStateId")]
    [InverseProperty("TaskStatusHistories")]
    public virtual TaskState TaskState { get; set; } = null!;
}
