using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReleaseManagement.Framework.Data.Model
{
    public class AuthUserData
    {
        public AuthUserData()
        { 
            LoggedIn = false;
        }

        public bool LoggedIn { get;set; }
        public string UserId { get;set; }
        public string UserName { get;set; }
        public string DisplayName { get;set; }
    }
}
