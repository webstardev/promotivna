using MarkomPos.Model.ViewModel;
using MarkomPos.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Repository.Repository
{
    public class AccountRepository : IDisposable
    {
        public bool IsValid(LoginVm loginVm)
        {
            using (var context = new markomPosDbContext())
            {
                var user = context.Users.FirstOrDefault(a => a.Username == loginVm.Username);

                if (user == null)
                    return false;

                bool isMatch = false;
                if ((user.PasswordHash != null && user.PasswordSalt != null) && (user.PasswordHash != "" && user.PasswordSalt != ""))
                    isMatch = PasswordUtil.VerifyPassword(loginVm.Password, user.PasswordHash, user.PasswordSalt);

                if (!isMatch)
                    return false;
                else
                    return true;
            }
        }
        public void Dispose()
        {
        }
    }
}
