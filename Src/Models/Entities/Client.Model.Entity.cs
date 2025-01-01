using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaseCodeAPI.Src.Models.Entity
{
   [Table("client")]
   public class ClientModel 
   {
      private static ClientModel FInstancia { get; set; }

      public static ClientModel New()
      {
         FInstancia ??= new ClientModel();
         return FInstancia;
      }

      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      [Column("id")]
      public int Id { get; set; }

      [Column("credit_limit", TypeName = "numeric(18,2)")]
      public double CreditLimit { get; set; }

      [Column("last_purchase")]
      public DateTime LastPurchase { get; set; }

      [Column("person_id")]
      public int PersonId { get; set; }

      public virtual PersonModel Person { get; set; }
   }
}
