using System.ComponentModel.DataAnnotations;
namespace Mysitemvc.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string NameSurname { get; set; }
        [Required]
        public string Password { get; set; }
        public bool Locked { get; set; }
    }
}
