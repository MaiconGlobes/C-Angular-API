using BaseCodeAPI.Src.Models;
using BaseCodeAPI.Src.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace BaseCodeAPI.Src
{
   internal class Context : DbContext
   {
      public DbSet<PersonModel> PERSON { get; set; }
      public DbSet<UserModel> USER { get; set; }
      public DbSet<ClientModel> CLIENT { get; set; }
      public DbSet<SupplierModel> SUPPLIER { get; set; }
      public DbSet<TransporterModel> TRANSPORTER { get; set; }
      public DbSet<AddressModel> ADDRESS { get; set; }

      /// <summary>
      /// Sobrescreve o método OnConfiguring para configurar as opções do provedor de banco de dados MySQL no Entity Framework Core, se as opções não estiverem configuradas.
      /// </summary>
      /// <param name="optionsBuilder">O construtor de opções do DbContext para configurar as opções do provedor de banco de dados.</param>
      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         if (!optionsBuilder.IsConfigured)
         {
            var serverVersion = new MySqlServerVersion(new Version(5, 7, 30));
            var mySQLConnection = EnvironmentsSettings.New()._iConfigRoot.GetConnectionString("MySQL5Connection");

            optionsBuilder.UseMySql(mySQLConnection, serverVersion)
                          .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
         }
      }

      /// <summary>
      /// Sobrescreve o método OnModelCreating para configurar o modelo de dados do Entity Framework Core usando o Fluent API.
      /// </summary>
      /// <param name="modelBuilder">O construtor de modelos usado para configurar as entidades e relacionamentos no contexto do banco de dados.</param>
      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<PersonModel>().Property(person => person.Id).ValueGeneratedOnAdd();
         modelBuilder.Entity<UserModel>().Property(user => user.Id).ValueGeneratedOnAdd();
         modelBuilder.Entity<ClientModel>().Property(client => client.Id).ValueGeneratedOnAdd();
         modelBuilder.Entity<SupplierModel>().Property(carrier => carrier.Id).ValueGeneratedOnAdd();
         modelBuilder.Entity<AddressModel>().Property(address => address.Id).ValueGeneratedOnAdd();

         modelBuilder.Entity<PersonModel>().HasIndex(person => person.Document).IsUnique();
         modelBuilder.Entity<UserModel>().HasIndex(user => user.Email).IsUnique();

         modelBuilder.Entity<PersonModel>().Property(person => person.Name).IsRequired();
         modelBuilder.Entity<PersonModel>().Property(person => person.Document).IsRequired();

         modelBuilder.Entity<UserModel>().Property(user => user.Email).IsRequired();
         modelBuilder.Entity<UserModel>().Property(user => user.Password).IsRequired();
         modelBuilder.Entity<UserModel>().Property(user => user.PersonId).IsRequired();

         modelBuilder.Entity<ClientModel>().Property(client => client.PersonId).IsRequired();
         modelBuilder.Entity<SupplierModel>().Property(carrier => carrier.PersonId).IsRequired();
         modelBuilder.Entity<AddressModel>().Property(address => address.PersonId).IsRequired();

         modelBuilder.Entity<PersonModel>()
                     .HasOne(user => user.User)
                     .WithOne(user => user.Person)
                     .HasForeignKey<UserModel>(user => user.PersonId)
                     .IsRequired()
                     .OnDelete(DeleteBehavior.Cascade);

         modelBuilder.Entity<PersonModel>()
                     .HasMany(person => person.Addresses)
                     .WithOne(address => address.Person)
                     .HasForeignKey(address => address.PersonId)
                     .OnDelete(DeleteBehavior.Cascade);

         modelBuilder.Entity<PersonModel>()
                     .HasOne(person => person.Client)
                     .WithOne(client => client.Person)
                     .HasForeignKey<ClientModel>(client => client.PersonId)
                     .IsRequired()
                     .OnDelete(DeleteBehavior.Cascade);

         modelBuilder.Entity<PersonModel>()
                     .HasOne(person => person.Supplier)
                     .WithOne(carrier => carrier.Person)
                     .HasForeignKey<SupplierModel>(carrier => carrier.PersonId)
                     .IsRequired()
                     .OnDelete(DeleteBehavior.Cascade);

         modelBuilder.Entity<PersonModel>()
                     .HasOne(transporter => transporter.Transporter)
                     .WithOne(transporter => transporter.Person)
                     .HasForeignKey<TransporterModel>(transporter => transporter.PersonId)
                     .IsRequired()
                     .OnDelete(DeleteBehavior.Cascade);

         modelBuilder.Entity<AddressModel>()
                     .HasIndex(address => new { address.PersonId, address.Number, address.ZipCode })
                     .IsUnique()
                     .HasDatabaseName("UK_address_for_person_number_zipcode");

         modelBuilder.Entity<UserModel>()
                     .HasIndex(user => new { user.PersonId, user.Email })
                     .IsUnique()
                     .HasDatabaseName("UK_user_for_person_email");

         //modelBuilder.Entity<PersonModel>().HasData(PopularDataUtils.Instancia().PopularPerson());
         //modelBuilder.Entity<UserModel>().HasData(PopularDataUtils.Instancia().PopularUsers());
         //modelBuilder.Entity<AddressModel>().HasData(PopularDataUtils.Instancia().PopularAddress());

         base.OnModelCreating(modelBuilder);
      }
   }
}
