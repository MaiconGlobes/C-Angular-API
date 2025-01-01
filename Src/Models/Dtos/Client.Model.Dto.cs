using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace BaseCodeAPI.Src.Models.Entity
{
   public class ClientModelDto
   {
      [JsonPropertyName("id")]
      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
      public int Id { get; set; }

      [JsonPropertyName("credit_limit")]
      [RegularExpression(@"^\d+$", ErrorMessage = "Propriedade {0} deve conter apenas números.")]
      public double CreditLimit { get; set; }

      [JsonPropertyName("last_purchase")]
      public DateTime LastPurchase { get; set; }

      [JsonPropertyName("person_id")]
      [Required(ErrorMessage = "Propriedade {0} é obrigatória.")]
      public int PersonId { get; set; }

      [JsonPropertyName("person")]
      [Required(ErrorMessage = "Propriedade {0} é obrigatória.")]
      public PersonModelDTO Person { get; set; }
   }
}
