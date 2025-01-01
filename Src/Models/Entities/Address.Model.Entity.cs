using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaseCodeAPI.Src.Models.Entity
{
   [Table("address")]
   public class AddressModel
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      [Column("id")]
      public int Id { get; set; }

      [JsonPropertyName("address")]
      [Column("address", TypeName = "varchar")]
      [MaxLength(100, ErrorMessage = "Propriedade {0} deve ter no máximo 100 caracteres")]
      public string Address { get; set; }

      [JsonPropertyName("number")]
      [Column("number", TypeName = "varchar")]
      [MaxLength(10, ErrorMessage = "Propriedade {0} deve ter no máximo 10 caracteres")]
      public string Number { get; set; }

      [JsonPropertyName("district")]
      [Column("district", TypeName = "varchar")]
      [MaxLength(35, ErrorMessage = "Propriedade {0} deve ter no máximo 35 caracteres")]
      public string District { get; set; }

      [JsonPropertyName("city")]
      [Column("city", TypeName = "varchar")]
      [MaxLength(100, ErrorMessage = "Propriedade {0} deve ter no máximo 100 caracteres")]
      public string City { get; set; }

      [JsonPropertyName("uf")]
      [Column("uf", TypeName = "varchar")]
      [MaxLength(2, ErrorMessage = "Propriedade {0} deve ter no máximo 2 caracteres")]
      public string Uf { get; set; }

      [JsonPropertyName("zip_code")]
      [Column("zip_code", TypeName = "varchar")]
      [MaxLength(8, ErrorMessage = "Propriedade {0} deve ter no máximo 8 caracteres")]
      public string ZipCode { get; set; }

      [JsonPropertyName("person_id")]
      [Column("person_id")]
      public int PersonId { get; set; }

      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
      [Required(ErrorMessage = "Propriedade {0} é obrigatória")]
      public virtual PersonModel Person { get; set; }
   }
}
