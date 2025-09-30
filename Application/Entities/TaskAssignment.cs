using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Entities;

[Table("TaskAssignment")]
public partial class TaskAssignment
{
    [Key]
    [Column("TaskAssignmentID")]
    public int TaskAssignmentId { get; set; }

    [Column("TaskID")]
    public int TaskId { get; set; }

    public int AssignedTo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime AssignedDate { get; set; }

    [Column("dateStarted", TypeName = "datetime")]
    public DateTime? DateStarted { get; set; }

    [Column("dateCompleted", TypeName = "datetime")]
    public DateTime? DateCompleted { get; set; }

    [Column("timeEstimatedStart", TypeName = "datetime")]
    public DateTime? TimeEstimatedStart { get; set; }

    [Column("timeEstimatedEnd", TypeName = "datetime")]
    public DateTime? TimeEstimatedEnd { get; set; }

    [ForeignKey("AssignedTo")]
    [InverseProperty("TaskAssignments")]
    public virtual User AssignedToNavigation { get; set; } = null!;

    [ForeignKey("TaskId")]
    [InverseProperty("TaskAssignments")]
    public virtual Task Task { get; set; } = null!;
}
