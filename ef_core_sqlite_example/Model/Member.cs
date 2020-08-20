using System;
namespace ef_core_sqlite_example.Model
{
    public class Member
    {
        //public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; } 
    }
}
