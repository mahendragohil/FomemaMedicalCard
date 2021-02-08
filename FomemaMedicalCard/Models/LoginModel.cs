using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FomemaMedicalCard.Models
{
    public class LoginModel
    {
        private string userId;
        public string UserId   
        {
            get
            {
                return this.userId;
            }
            set
            {
                this.userId = value;
            }
        }
        private string password;
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

    }
}