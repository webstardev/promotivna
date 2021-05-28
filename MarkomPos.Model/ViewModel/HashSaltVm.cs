using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Model.ViewModel
{
    public class HashSaltVm
    {
        public string Hash { get; set; }
        public string Salt { get; }

        public HashSaltVm(string hash, string salt)
        {
            Hash = hash;
            Salt = salt;
        }
    }
}
