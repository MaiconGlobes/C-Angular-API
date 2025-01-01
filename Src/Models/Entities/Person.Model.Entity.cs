using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaseCodeAPI.Src.Models.Entity
{
   [Table("person")]
   public class PersonModel
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      [Column("id")]
      public int Id { get; set; }

      [JsonPropertyName("name")]
      [Column("name", TypeName = "varchar")]
      [MaxLength(50)]
      public string Name { get; set; }

      [JsonPropertyName("document")]
      [Column("document", TypeName = "varchar")]
      [MinLength(11)]
      [MaxLength(14)]
      public string Document { get; set; }

      [JsonPropertyName("phone")]
      [Column("phone", TypeName = "varchar")]
      [MinLength(10)]
      [MaxLength(11)]
      public string Phone { get; set; }

      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
      public virtual UserModel User { get; set; }

      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
      public virtual ClientModel Client { get; set; }

      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
      public virtual SupplierModel Supplier { get; set; }

      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
      public virtual TransporterModel Transporter { get; set; }
      public virtual ICollection<AddressModel> Addresses { get; set; }
   }
}
