﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagementServer.Models;

namespace UserManagementServer.Services.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        //IEnumerable<User> GetAll();
        User GetById(int id);
        //User GetByName(string name);
        User Create(User user, string password);
        //void Update(User user, string password = null);
        //void Delete(int id);
    }
}
