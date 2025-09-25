using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

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

    [ForeignKey("AssignedTo")]
    [InverseProperty("TaskAssignments")]
    public virtual User AssignedToNavigation { get; set; } = null!;

    [ForeignKey("TaskId")]
    [InverseProperty("TaskAssignments")]
    public virtual Task Task { get; set; } = null!;
}
