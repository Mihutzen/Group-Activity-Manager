using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class MyUser
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AccountType { get; set; }
        

        /*
        public static void ShowError()
        {
            Nepotrivire nepotrivire = new Nepotrivire();
            nepotrivire.ShowDialog();
            return;
        }
        */

        public static bool IsEqual(MyUser user1, MyUser user2)
        {
            if (user1 == null || user2 == null) {
                Nepotrivire nepotrivire = new Nepotrivire();
                nepotrivire.ShowDialog();
                return false;
            }

            else if (user1.Username != user2.Username)
            {
                Nepotrivire nepotrivire = new Nepotrivire();
                nepotrivire.ShowDialog();
                return false;
            }

            else if (user1.Password != user2.Password)
            {
                Nepotrivire nepotrivire = new Nepotrivire();
                nepotrivire.ShowDialog();
                return false;
            }

            return true;
        }
    }
}
