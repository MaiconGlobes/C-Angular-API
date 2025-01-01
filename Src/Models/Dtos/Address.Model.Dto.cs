using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace BaseCodeAPI.Src.Models.Entity
{
   public class AddressModelDto
   {
      [JsonPropertyName("id")]
      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
      public int Id { get; set; }

      [JsonPropertyName("address")]
      [MaxLength(100, ErrorMessage = "Propriedade {0} deve ter no máximo 100 caracteres")]
      public string Address { get; set; }

      [JsonPropertyName("number")]
      [MaxLength(10, ErrorMessage = "Propriedade {0} deve ter no máximo 10 caracteres")]
      public string Number { get; set; }

      [JsonPropertyName("district")]
      [MaxLength(35, ErrorMessage = "Propriedade {0} deve ter no máximo 35 caracteres")]
      public string District { get; set; }

      [JsonPropertyName("city")]
      [MaxLength(100, ErrorMessage = "Propriedade {0} deve ter no máximo 100 caracteres")]
      public string City { get; set; }

      [JsonPropertyName("uf")]
      [MaxLength(2, ErrorMessage = "Propriedade {0} deve ter no máximo 2 caracteres")]
      [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Propriedade {0} deve conter apenas letras.")]
      public string Uf { get; set; }

      [JsonPropertyName("zipcode")]
      [MaxLength(8, ErrorMessage = "Propriedade {0} deve ter no máximo 8 caracteres")]
      [RegularExpression(@"^\d+$", ErrorMessage = "Propriedade {0} deve conter apenas números.")]
      public string Zipcode { get; set; }

      [JsonPropertyName("person_id")]
      [Column("person_id")]
      public int PersonId { get; set; }
   }
}
