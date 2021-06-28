using MarkomPos.Model.Enum;
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
    public class dbInitializerSeed : CreateDatabaseIfNotExists<markomPosDbContext>
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

            var uom = context.UnitOfMeasures.Any();
            if (!uom)
                UomSeed(context);
            var pgs = context.ProductGroups.Any();
            if (!pgs)
                ProductGroupSeed(context);

            var pm = context.PaymentMethods.Any();
            if (!pm)
                PaymentMethodSeed(context);

            var delTerms = context.DeliveryTerms.Any();
            if (!delTerms)
                DeliveryTermSeed(context);

            var docPar = context.DocumentParities.Any();
            if (!docPar)
                DocumentParitySeed(context);

            base.Seed(context);
        }

        private void UomSeed(markomPosDbContext context)
        {
            var komad = new UnitOfMeasure();
            komad.Name = "KOMAD";
            komad.DisplayName = "kom";
            komad.DateCreated = DateTime.Now;

            var arak = new UnitOfMeasure();
            arak.Name = "ARAK";
            arak.DisplayName = "arak";
            arak.DateCreated = DateTime.Now;

            context.UnitOfMeasures.Add(komad);
            context.UnitOfMeasures.Add(arak);
            context.SaveChanges();
        }
        private void ProductGroupSeed(markomPosDbContext context)
        {
            var pg1 = new ProductGroup();
            pg1.Name = "ROBA";
            pg1.DisplayName = "ROBA";
            pg1.productGroupType = ProductGroupTypeEnum.Main;
            pg1.DateCreated = DateTime.Now;

            var pg2 = new ProductGroup();
            pg2.Name = "MATERIJAL";
            pg2.DisplayName = "MATERIJAL";
            pg1.productGroupType = ProductGroupTypeEnum.Main;
            pg2.DateCreated = DateTime.Now;

            context.ProductGroups.Add(pg1);
            context.ProductGroups.Add(pg2);
            context.SaveChanges();

            var pg11 = new ProductGroup();
            pg11.Name = "PAKIRANJE";
            pg11.DisplayName = "PAKIRANJE";
            pg11.ParrentGroupId = pg1.ID;
            pg11.productGroupType = ProductGroupTypeEnum.Sub;
            pg11.DateCreated = DateTime.Now;

            var pg12 = new ProductGroup();
            pg12.Name = "DISPLAY";
            pg12.DisplayName = "DISPLAY";
            pg12.ParrentGroupId = pg1.ID;
            pg12.productGroupType = ProductGroupTypeEnum.Sub;
            pg12.DateCreated = DateTime.Now;

            var pg21 = new ProductGroup();
            pg21.Name = "PAPIR/KARTON/NALJEPNICA";
            pg21.DisplayName = "PAPIR/KARTON/NALJEPNICA";
            pg21.ParrentGroupId = pg2.ID;
            pg21.productGroupType = ProductGroupTypeEnum.Sub;
            pg21.DateCreated = DateTime.Now;

            context.ProductGroups.Add(pg11);
            context.ProductGroups.Add(pg12);
            context.ProductGroups.Add(pg21);
            context.SaveChanges();

            var pg111 = new ProductGroup();
            pg111.Name = "KUTIJA";
            pg111.DisplayName = "KUTIJA";
            pg111.ParrentGroupId = pg11.ID;
            pg111.productGroupType = ProductGroupTypeEnum.Basic;
            pg111.DateCreated = DateTime.Now;

            var pg112 = new ProductGroup();
            pg112.Name = "TRANSPORT KUTIJA";
            pg112.DisplayName = "TRANSPORT KUTIJA";
            pg112.ParrentGroupId = pg11.ID;
            pg112.productGroupType = ProductGroupTypeEnum.Basic;
            pg112.DateCreated = DateTime.Now;

            var pg121 = new ProductGroup();
            pg121.Name = "STALAK";
            pg121.DisplayName = "STALAK";
            pg121.ParrentGroupId = pg12.ID;
            pg121.productGroupType = ProductGroupTypeEnum.Basic;
            pg121.DateCreated = DateTime.Now;

            var pg211 = new ProductGroup();
            pg211.Name = "PAPIR";
            pg211.DisplayName = "PAPIR";
            pg211.ParrentGroupId = pg21.ID;
            pg211.productGroupType = ProductGroupTypeEnum.Basic;
            pg211.DateCreated = DateTime.Now;

            context.ProductGroups.Add(pg111);
            context.ProductGroups.Add(pg112);
            context.ProductGroups.Add(pg121);
            context.ProductGroups.Add(pg211);
            context.SaveChanges();
        }
        private void PaymentMethodSeed(markomPosDbContext context)
        {
            var pmGot = new PaymentMethod();
            pmGot.Name = "GOTOVINA";
            pmGot.DisplayName = "GOT";
            pmGot.DateCreated = DateTime.Now;

            var pmVir = new PaymentMethod();
            pmVir.Name = "VIRMAN";
            pmVir.DisplayName = "VIR";
            pmVir.DateCreated = DateTime.Now;

            context.PaymentMethods.Add(pmGot);
            context.PaymentMethods.Add(pmVir);
            context.SaveChanges();
        }
        private void DeliveryTermSeed(markomPosDbContext context)
        {
            var dt1 = new DeliveryTerm();
            dt1.Name = "ODMAH";
            dt1.DisplayName = "odmah";
            dt1.DateCreated = DateTime.Now;

            var dt2 = new DeliveryTerm();
            dt2.Name = "TJEDAN";
            dt2.DisplayName = "5-7 dana";
            dt2.DateCreated = DateTime.Now;

            context.DeliveryTerms.Add(dt1);
            context.DeliveryTerms.Add(dt2);
            context.SaveChanges();
        }
        private void DocumentParitySeed(markomPosDbContext context)
        {
            var dp1 = new DocumentParity();
            dp1.Name = "SKLADIŠTE KUPCA";
            dp1.DisplayName = "SKLADIŠTE KUPCA";
            dp1.DateCreated = DateTime.Now;

            var dp2 = new DocumentParity();
            dp2.Name = "SKLADIŠTE MARKOM";
            dp2.DisplayName = "SKLADIŠTE MARKOM";
            dp2.DateCreated = DateTime.Now;

            context.DocumentParities.Add(dp1);
            context.DocumentParities.Add(dp2);
            context.SaveChanges();
        }
    }
}
