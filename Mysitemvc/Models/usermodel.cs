using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace Mysitemvc.Models
{
    //class for Usermodel UserName & Password class
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
