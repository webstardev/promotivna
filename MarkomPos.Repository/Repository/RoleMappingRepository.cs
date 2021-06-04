using MarkomPos.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Mapster;
using System.Web.Mvc;
using MarkomPos.Model.Model;

namespace MarkomPos.Repository.Repository
{
    public class RoleMappingRepository : IDisposable
    {
        public List<UserRoleMappingVm> GetAll()
        {
            using (var context = new markomPosDbContext())
            {
                return context.UserRoleMappings
                    .Include(p => p.User)
                    .Include(i => i.Roles)
                    .Where(w => w.RolesId != 1)
                    .Adapt<List<UserRoleMappingVm>>().ToList();
            }
        }
        public UserRoleMappingVm GetById(int id)
        {
            using (var context = new markomPosDbContext())
            {
                var userRoleMappingVm = new UserRoleMappingVm();
                var userRoleMappingData = context.UserRoleMappings.Include(p => p.User).Include(i => i.Roles).FirstOrDefault(f => f.ID == id).Adapt<UserRoleMappingVm>();
                if (userRoleMappingData != null)
                {
                    userRoleMappingVm = userRoleMappingData;
                    using (var roleRepository = new RoleRepository())
                    {
                        userRoleMappingVm.UserRoles = new SelectList(roleRepository.getAllRole(), "ID", "Name", userRoleMappingData.RolesId).ToList();
                    }
                    using (var userRepository = new UserRepository())
                    {
                        userRoleMappingVm.Users = new SelectList(userRepository.getAllUser(), "ID", "Name", userRoleMappingData.UserId).ToList();
                    }
                }
                else
                {
                    using (var roleRepository = new RoleRepository())
                    {
                        userRoleMappingVm.UserRoles = new SelectList(roleRepository.getAllRole(), "ID", "Name").ToList();
                    }
                    using (var userRepository = new UserRepository())
                    {
                        userRoleMappingVm.Users = new SelectList(userRepository.getAllUser(), "ID", "Name").ToList();
                    }
                }
                return userRoleMappingVm;
            }
        }
        public bool AddUpdateUserRoleMapping(UserRoleMappingVm userRoleMapping)
        {
            using (var context = new markomPosDbContext())
            {
                try
                {
                    if (userRoleMapping.ID > 0)
                    {
                        var dbData = context.UserRoleMappings.Find(userRoleMapping.ID);
                        if (dbData != null)
                        {
                            dbData.ID = userRoleMapping.ID;
                            dbData.RolesId = userRoleMapping.RolesId;
                            dbData.UserId = userRoleMapping.UserId;
                            dbData.DateModified = DateTime.Now;
                        }
                    }
                    else
                    {
                        var isExist = context.UserRoleMappings.Any(f => f.RolesId == userRoleMapping.RolesId && f.UserId == userRoleMapping.UserId);
                        if (!isExist)
                        {
                            userRoleMapping.DateCreated = DateTime.Now;
                            userRoleMapping.DateModified = DateTime.Now;
                            var userRoleMappingData = userRoleMapping.Adapt<UserRoleMapping>();
                            context.UserRoleMappings.Add(userRoleMappingData);
                        }
                    }
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public void Dispose()
        {
        }
    }
}
