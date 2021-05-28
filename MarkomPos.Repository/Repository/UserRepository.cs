using MarkomPos.Model.Model;
using MarkomPos.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Repository.Repository
{
    public class UserRepository : IDisposable
    {
        public bool AddUser(User user)
        {
            using (var context = new markomPosDbContext())
            {
                try
                {
                    if (user.ID > 0)
                    {
                        var dbData = context.Users.Find(user.ID);
                        if (dbData != null)
                        {
                            dbData.ID = user.ID;
                            dbData.Name = user.Name;
                            dbData.Surname = user.Surname;
                            dbData.Address = user.Address;
                            dbData.JobDescription = user.JobDescription;
                            dbData.Active = true;
                            dbData.Email = user.Email;
                            dbData.MobilePhone = user.MobilePhone;
                            dbData.Note = user.Note;
                            dbData.Note2 = user.Note2;
                            dbData.ColorHex = user.ColorHex;
                            dbData.Username = user.Username;
                            dbData.DateModified = DateTime.Now;
                        }
                    }
                    else
                    {
                        var isExist = context.Users.Any(f => f.Username == user.Username);
                        if (!isExist)
                        {
                            var getHashSaltPassword = PasswordUtil.GetHashSaltPassword(user.Password);
                            user.PasswordHash = getHashSaltPassword.Hash;
                            user.PasswordSalt = getHashSaltPassword.Salt;

                            user.DateCreated = DateTime.Now;
                            user.DateModified = DateTime.Now;
                            context.Users.Add(user);
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
        public List<User> getAllUser()
        {
            using (var context = new markomPosDbContext())
            {
                var users = new List<User>();
                users = (from user in context.Users
                         join urm in context.UserRoleMappings on user.ID equals urm.UserId into userRole
                         from urm in userRole.DefaultIfEmpty()
                         where urm.RolesId != 1
                         select user).ToList();
                return users;
            }
        }

        public void Dispose()
        {
        }
    }
}
