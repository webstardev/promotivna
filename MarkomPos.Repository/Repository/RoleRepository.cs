using MarkomPos.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Repository.Repository
{
    public class RoleRepository : IDisposable
    {
        public bool AddUpdateRole(Roles role)
        {
            using (var context = new markomPosDbContext())
            {
                try
                {
                    if (role.ID > 0)
                    {
                        var dbData = context.DeliveryTerms.Find(role.ID);
                        if (dbData != null)
                        {
                            dbData.ID = role.ID;
                            dbData.Name = role.Name;
                            dbData.DateModified = DateTime.Now;
                        }
                    }
                    else
                    {
                        var isExist = context.Roles.Any(f => f.Name == role.Name);
                        if (!isExist)
                        {
                            role.DateCreated = DateTime.Now;
                            role.DateModified = DateTime.Now;
                            context.Roles.Add(role);
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
        public bool AddUpdateUserRoleMapping(UserRoleMapping userRoleMapping)
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
                            context.UserRoleMappings.Add(userRoleMapping);
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
        public List<UserRoleMapping> getAllRoleMapping()
        {
            var userRoleMappings = new List<UserRoleMapping>();
            using (var context = new markomPosDbContext())
            {
                userRoleMappings = (from map in context.UserRoleMappings
                                    join role in context.Roles on map.RolesId equals role.ID
                                    where role.Name != "Super Admin"
                                    select map).ToList();
            }
            return userRoleMappings;
        }
        public List<Roles> getAllRole()
        {
            var roles = new List<Roles>();
            using (var context = new markomPosDbContext())
            {
                roles = context.Roles.Where(w => w.Name != "Super Admin").ToList();
            }
            return roles;
        }
        public void Dispose()
        {
        }
    }
}
