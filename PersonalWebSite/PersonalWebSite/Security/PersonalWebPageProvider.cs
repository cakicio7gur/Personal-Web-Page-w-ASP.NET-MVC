using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using PersonalWebSite.Models;


namespace PersonalWebSite.Security
{

    public class PersonalWebPageProvider : RoleProvider
    {
        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        public override string[] GetRolesForUser(string username)
        {
            Models.PersonalWebPageDBEntities1 db = new Models.PersonalWebPageDBEntities1();
            var kullanici = db.Uye.FirstOrDefault(m => m.UyeDetay.kullaniciAdi == username);
            return new string[] { kullanici.Rol.rol1 };
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}