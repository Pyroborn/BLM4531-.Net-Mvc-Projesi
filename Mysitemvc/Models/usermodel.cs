using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace Mysitemvc.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        public int UsermodelId { get; set; }
        public virtual Usermodel User { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
    }

    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

     
    }
    public class Usermodel
    {
        
        public int Id { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(16)]
        public string? Password { get; set; }
        public bool Locked { get; set; }

        public Usermodel(int value1, string value2, string value3)
        {
            this.Id = value1;
            this.UserName = value2;
            this.Password = value3;
        }

        public Usermodel()
        {
        
        }

    }
}
