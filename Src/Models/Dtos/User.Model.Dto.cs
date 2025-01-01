using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace BaseCodeAPI.Src.Models.Entity
{
   public class UserModelDto
   {
      [JsonPropertyName("id")]
      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
      public int Id { get; set; }

      [JsonPropertyName("email")]
      [Required(ErrorMessage = "Propriedade {0} é obrigatória")]
      [MaxLength(255, ErrorMessage = "Propriedade {0} deve ter no máximo 255 caracteres")]
      [EmailAddress(ErrorMessage = "Propriedade {0} deve ser um endereço de e-mail válido")]
      public string Email { get; set; }

      [JsonPropertyName("password")]
      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
      [Required(ErrorMessage = "Propriedade {0} é obrigatória")]
      [MaxLength(100, ErrorMessage = "Propriedade {0} deve ter no máximo 100 caracteres")]
      public string Password { get; set; }

      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
      [JsonPropertyName("refresh_token")]
      [Column("refresh_token", TypeName = "text")]
      public string RefreshToken { get; set; }

      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
      [JsonPropertyName("token")]
      public string Token { get; set; }

      [JsonPropertyName("pessoa_id")]
      public int PessoaId { get; set; }

      public PersonModelDTO Person { get; set; }
   }
}
