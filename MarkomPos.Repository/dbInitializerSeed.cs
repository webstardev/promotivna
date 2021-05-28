using MarkomPos.Model.Model;
using MarkomPos.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Repository
{
    public class dbInitializerSeed : DropCreateDatabaseAlways<markomPosDbContext>
    {
        protected override void Seed(markomPosDbContext context)
        {
            var isRoleExist = context.Roles.Any(a => a.Name == "Super Admin");
            var role = new Roles();
            if (!isRoleExist)
            {
                role.Name = "Super Admin";
                role.DateCreated = DateTime.Now;
                context.Roles.Add(role);
                context.SaveChanges();
            }
            else
                role = context.Roles.FirstOrDefault(f => f.Name == "Super Admin");
            var IsUserExist = context.Users.Any();
            if (!IsUserExist)
            {
                var user = new User();
                user.Name = "Super Admin";
                user.Address = "India";
                user.JobDescription = "Super Admin";
                user.Username = "SuperAdmin";
                var getHashSaltPassword = PasswordUtil.GetHashSaltPassword("Admin@123");
                user.PasswordHash = getHashSaltPassword.Hash;
                user.PasswordSalt = getHashSaltPassword.Salt;
                user.DateCreated = DateTime.Now;
                context.Users.Add(user);
                context.SaveChanges();

                var userRoleMapping = new UserRoleMapping();
                userRoleMapping.RolesId = role.ID;
                userRoleMapping.UserId = user.ID;
                userRoleMapping.DateCreated = DateTime.Now;
                context.UserRoleMappings.Add(userRoleMapping);
                context.SaveChanges();
            }

            base.Seed(context);
        }
    }
}
