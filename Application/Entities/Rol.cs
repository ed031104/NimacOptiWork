using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Entities;

[Table("Rol", Schema = "auth")]
public partial class Rol
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateCreated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateModified { get; set; }

    [InverseProperty("Rol")]
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
