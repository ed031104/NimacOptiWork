using System;
using System.Collections.Generic;

namespace Domain.Models;

public class Rol
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    #region Constructors
    public Rol()
    {
    }
    public Rol(int id, string name, string? description = null, DateTime? dateCreated = null, DateTime? dateModified = null)
    {
        Id = id;
        Name = name;
        Description = description;
        DateCreated = dateCreated;
        DateModified = dateModified;
    }
    #endregion

    #region Pattern Builder
    public class Builder
    {
        private readonly Rol _rol;
        public Builder()
        {
            _rol = new Rol();
        }
        public Builder WithId(int id)
        {
            _rol.Id = id;
            return this;
        }
        public Builder WithName(string name)
        {
            _rol.Name = name;
            return this;
        }
        public Builder WithDescription(string? description)
        {
            _rol.Description = description;
            return this;
        }
        public Builder WithDateCreated(DateTime? dateCreated)
        {
            _rol.DateCreated = dateCreated;
            return this;
        }
        public Builder WithDateModified(DateTime? dateModified)
        {
            _rol.DateModified = dateModified;
            return this;
        }
        public Rol Build()
        {
            return _rol;
        }
    }
    #endregion
}
