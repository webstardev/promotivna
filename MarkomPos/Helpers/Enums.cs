using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkomPos.Helpers
{
    public class Enums
    {
        // Document types (TIPOVI DOKUMENATA)
        public enum DocumentTypeEnum
        {
            Product = 10,
            Contact = 20,
            Offer = 30,
            Order = 40,
            WorkOrder = 50,
            InputInvoice = 60,
            OutputInvoice = 70,
            InputDocument = 80,
            OutputDocument = 90
        }


        // USER ACTIONS (RADNJE KORISNIKA)
        public enum UserActionModulEnum
        {
            Login = 100,
            User = 200,
            Contact = 300,
            // dodati sve module
        }

        public enum UserActionTypeEnum
        {
            Create = 10,
            Read = 20,
            Update = 30,
            Delete = 40,
            Admin = 50,
            Custom = 60
        }
    }
}
