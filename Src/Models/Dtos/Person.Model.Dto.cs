using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseCodeAPI.Src.Models.Entity
{
   public class PersonModelDTO
   {
      [JsonPropertyName("id")]
      //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
      public int Id { get; set; }

      [JsonPropertyName("name")]
      [Required(ErrorMessage = "Propriedade {0} é obrigatória.")]
      [MaxLength(50, ErrorMessage = "Propriedade {0} deve ter no máximo 50 caracteres")]
      public string Name { get; set; }

      [JsonPropertyName("document")]
      [Required(ErrorMessage = "Propriedade {0} é obrigatória.")]
      [MinLength(11, ErrorMessage = "Propriedade {0} deve ter no mínimo 11 caracteres")]
      [MaxLength(14, ErrorMessage = "Propriedade {0} deve ter no máximo 14 caracteres")]
      [RegularExpression(@"^\d+$", ErrorMessage = "Propriedade {0} deve conter apenas números.")]
      public string Document { get; set; }

      [JsonPropertyName("phone")]
      [MinLength(10, ErrorMessage = "Propriedade {0} deve ter no mínimo 10 caracteres")]
      [MaxLength(11, ErrorMessage = "Propriedade {0} deve ter no máximo 11 caracteres")]
      [RegularExpression(@"^\d+$", ErrorMessage = "Propriedade {0} deve conter apenas números.")]
      public string Phone { get; set; }

      [JsonPropertyName("addresses")]
      public virtual ICollection<AddressModelDto> Addresses { get; set; }
   }
}
