using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public bool? Active { get; set; }

    #region Constructors
    public User()
    {
    }
    public User(int id, string email, string username, string password, DateTime? dateCreated = null, DateTime? dateModified = null, bool? active = null)
    {
        Id = id;
        Email = email;
        Username = username;
        Password = password;
        DateCreated = dateCreated;
        DateModified = dateModified;
        Active = active;
    }
    #endregion

    #region Pattern Builder
    public class Builder
    {
        private readonly User _user;
        public Builder()
        {
            _user = new User();
        }
        public Builder WithId(int id)
        {
            _user.Id = id;
            return this;
        }
        public Builder WithEmail(string email)
        {
            _user.Email = email;
            return this;
        }
        public Builder WithUsername(string username)
        {
            _user.Username = username;
            return this;
        }
        public Builder WithPassword(string password)
        {
            _user.Password = password;
            return this;
        }
        public Builder WithDateCreated(DateTime? dateCreated)
        {
            _user.DateCreated = dateCreated;
            return this;
        }
        public Builder WithDateModified(DateTime? dateModified)
        {
            _user.DateModified = dateModified;
            return this;
        }
        public Builder WithActive(bool? active)
        {
            _user.Active = active;
            return this;
        }
        public User Build()
        {
            return _user;
        }
    }
    #endregion
}
