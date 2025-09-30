using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Entities;

[Table("UserRoles", Schema = "auth")]
public partial class UserRole
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    [Column("RolID")]
    public int RolId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CreateBy { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [ForeignKey("RolId")]
    [InverseProperty("UserRoles")]
    public virtual Rol Rol { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserRoles")]
    public virtual User User { get; set; } = null!;
}
