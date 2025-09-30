using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class UserRole
{
    public int Id { get; set; }

    public virtual Rol Rol { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string? CreateBy { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    #region Constructors
    public UserRole()
    {
    }
    public UserRole(int id, Rol rol, User user, string? createBy = null, string? modifiedBy = null, DateTime? createDate = null, DateTime? modifiedDate = null)
    {
        Id = id;
        this.Rol = rol;
        this.User = user;
        CreateBy = createBy;
        ModifiedBy = modifiedBy;
        CreateDate = createDate;
        ModifiedDate = modifiedDate;
    }
    #endregion

    #region Pattern Builder
    public class Builder
    {
        private readonly UserRole _userRole;
        public Builder()
        {
            _userRole = new UserRole();
        }
        public Builder WithId(int id)
        {
            _userRole.Id = id;
            return this;
        }
        public Builder WithRol(Rol rol)
        {
            _userRole.Rol = rol;
            return this;
        }
        public Builder WithUser(User user)
        {
            _userRole.User = user;
            return this;
        }
        public Builder WithCreateBy(string? createBy)
        {
            _userRole.CreateBy = createBy;
            return this;
        }
        public Builder WithModifiedBy(string? modifiedBy)
        {
            _userRole.ModifiedBy = modifiedBy;
            return this;
        }
        public Builder WithCreateDate(DateTime? createDate)
        {
            _userRole.CreateDate = createDate;
            return this;
        }
        public Builder WithModifiedDate(DateTime? modifiedDate)
        {
            _userRole.ModifiedDate = modifiedDate;
            return this;
        }
        public UserRole Build()
        {
            return _userRole;
        }
    }
    #endregion
}
