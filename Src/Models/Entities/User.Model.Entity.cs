using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaseCodeAPI.Src.Models.Entity
{
   [Table("user")]
   public class UserModel
   {
      private static UserModel FInstancia { get; set; }

      public static UserModel New()
      {
         FInstancia ??= new UserModel();
         return FInstancia;
      }

      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      [Column("id")]
      public int Id { get; set; }

      [Column("email", TypeName = "varchar")]
      [MaxLength(255)]
      public string Email { get; set; }

      [Column("password", TypeName = "varchar")]
      [MaxLength(100)]
      public string Password { get; set; }

      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
      [Column("refresh_token", TypeName = "text")]
      public string RefreshToken { get; set; }

      [JsonPropertyName("person_id")]
      [Column("person_id")]
      public int PersonId { get; set; }

      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
      [Required(ErrorMessage = "Propriedade {0} é obrigatória")]
      public virtual PersonModel Person { get; set; }

   }
}
