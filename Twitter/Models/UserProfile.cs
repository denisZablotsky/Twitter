using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Twitter.Models
{
    public class UserProfile
    {
        [ForeignKey("User")]
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime SignupData { get; set; }
        public string Age { get; set; }

        public virtual User User { get; set; }
    }
}