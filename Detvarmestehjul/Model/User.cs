using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detvarmestehjul.Model
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime MembershipDate { get; set; }

        public User()
        {
                
        }

        public User(int id, string firstName, string lastName, string email, DateTime memberShipDate)
        {
            UserID = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            MembershipDate = memberShipDate;
        }

        public User(string firstName, string lastName, string email, DateTime memberShipDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            MembershipDate = memberShipDate;
        }

        public override string ToString()
        {
            return $"Id: {UserID}, Firstname: {FirstName}, Lastname: {LastName}, Email: {Email}, Membership start: {MembershipDate}";
        }
    }
}
