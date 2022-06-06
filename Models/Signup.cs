using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models
{
    [BsonIgnoreExtraElements]
    public class Signup
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } 


        [BsonElement("UserName")]
        [Required(ErrorMessage = "User name field is required")]
        public string Name { get; set; } = null!;


        [Required(ErrorMessage ="Email field is required")]
        [BsonElement("Email")]
        [EmailAddress(ErrorMessage ="Plaese enter valid email")]
        public string Email { get; set; } = null!;



        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date of birth field is required")]
        [BsonElement("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }



        [BsonElement("Password")]
        [Required(ErrorMessage = "Password field is required")]
        public string Password { get; set; } = null!;
    }
}
