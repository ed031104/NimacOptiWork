using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

[Table("Task")]
public partial class Task
{
    [Key]
    [Column("TaskID")]
    public int TaskId { get; set; }

    [StringLength(60)]
    public string InvoiceNumber { get; set; } = null!;

    [StringLength(255)]
    public string Description { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column("CREATEDBy")]
    [StringLength(50)]
    public string Createdby { get; set; } = null!;

    [InverseProperty("Task")]
    public virtual ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();
}
