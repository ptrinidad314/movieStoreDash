using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace movieStoreDash.Data.models
{
    public partial class sakilaContext : DbContext
    {
        public sakilaContext()
        {
        }

        public sakilaContext(DbContextOptions<sakilaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actor { get; set; }
        public virtual DbSet<ActorInfo> ActorInfo { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Classes> Classes { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerList> CustomerList { get; set; }
        public virtual DbSet<Custpayment> Custpayment { get; set; }
        public virtual DbSet<Film> Film { get; set; }
        public virtual DbSet<FilmActor> FilmActor { get; set; }
        public virtual DbSet<FilmCategory> FilmCategory { get; set; }
        public virtual DbSet<FilmList> FilmList { get; set; }
        public virtual DbSet<FilmText> FilmText { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<NicerButSlowerFilmList> NicerButSlowerFilmList { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Rental> Rental { get; set; }
        public virtual DbSet<SalesByFilmCategory> SalesByFilmCategory { get; set; }
        public virtual DbSet<SalesByStore> SalesByStore { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<StaffList> StaffList { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<Studentclass> Studentclass { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Table1> Table1 { get; set; }
        public virtual DbSet<Table2> Table2 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=localhost;port=3306;database=sakila;uid=root;password=Abc123!;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.ToTable("actor");

                entity.HasIndex(e => e.LastName)
                    .HasName("idx_actor_last_name");

                entity.Property(e => e.ActorId)
                    .HasColumnName("actor_id")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<ActorInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("actor_info");

                entity.Property(e => e.ActorId)
                    .HasColumnName("actor_id")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.FilmInfo).HasColumnName("film_info");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("address");

                entity.HasIndex(e => e.CityId)
                    .HasName("idx_fk_city_id");

                entity.HasIndex(e => e.Location)
                    .HasName("idx_location");

                entity.Property(e => e.AddressId)
                    .HasColumnName("address_id")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasColumnName("address2")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CityId)
                    .HasColumnName("city_id")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.District)
                    .IsRequired()
                    .HasColumnName("district")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasColumnName("postal_code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_address_city");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .HasColumnType("tinyint unsigned")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("city");

                entity.HasIndex(e => e.CountryId)
                    .HasName("idx_fk_country_id");

                entity.Property(e => e.CityId)
                    .HasColumnName("city_id")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.City1)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryId)
                    .HasColumnName("country_id")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_city_country");
            });

            modelBuilder.Entity<Classes>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("classes");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("country");

                entity.Property(e => e.CountryId)
                    .HasColumnName("country_id")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.Country1)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasIndex(e => e.AddressId)
                    .HasName("idx_fk_address_id");

                entity.HasIndex(e => e.LastName)
                    .HasName("idx_last_name");

                entity.HasIndex(e => e.StoreId)
                    .HasName("idx_fk_store_id");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.AddressId)
                    .HasColumnName("address_id")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.CreateDate).HasColumnName("create_date");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.StoreId)
                    .HasColumnName("store_id")
                    .HasColumnType("tinyint unsigned");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_customer_address");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_customer_store");
            });

            modelBuilder.Entity<CustomerList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("customer_list");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(91)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasColumnName("notes")
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Sid)
                    .HasColumnName("SID")
                    .HasColumnType("tinyint unsigned");

                entity.Property(e => e.ZipCode)
                    .HasColumnName("zip code")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Custpayment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("custpayment");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentId)
                    .HasColumnName("payment_id")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.RentalId).HasColumnName("rental_id");
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.ToTable("film");

                entity.HasIndex(e => e.LanguageId)
                    .HasName("idx_fk_language_id");

                entity.HasIndex(e => e.OriginalLanguageId)
                    .HasName("idx_fk_original_language_id");

                entity.HasIndex(e => e.Title)
                    .HasName("idx_title");

                entity.Property(e => e.FilmId)
                    .HasColumnName("film_id")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.LanguageId)
                    .HasColumnName("language_id")
                    .HasColumnType("tinyint unsigned");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Length)
                    .HasColumnName("length")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.OriginalLanguageId)
                    .HasColumnName("original_language_id")
                    .HasColumnType("tinyint unsigned");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("enum('G','PG','PG-13','R','NC-17')")
                    .HasDefaultValueSql("'G'");

                entity.Property(e => e.ReleaseYear)
                    .HasColumnName("release_year")
                    .HasColumnType("year");

                entity.Property(e => e.RentalDuration)
                    .HasColumnName("rental_duration")
                    .HasColumnType("tinyint unsigned")
                    .HasDefaultValueSql("'3'");

                entity.Property(e => e.RentalRate)
                    .HasColumnName("rental_rate")
                    .HasColumnType("decimal(4,2)")
                    .HasDefaultValueSql("'4.99'");

                entity.Property(e => e.ReplacementCost)
                    .HasColumnName("replacement_cost")
                    .HasColumnType("decimal(5,2)")
                    .HasDefaultValueSql("'19.99'");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.FilmLanguage)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_film_language");

                entity.HasOne(d => d.OriginalLanguage)
                    .WithMany(p => p.FilmOriginalLanguage)
                    .HasForeignKey(d => d.OriginalLanguageId)
                    .HasConstraintName("fk_film_language_original");
            });

            modelBuilder.Entity<FilmActor>(entity =>
            {
                entity.HasKey(e => new { e.ActorId, e.FilmId })
                    .HasName("PRIMARY");

                entity.ToTable("film_actor");

                entity.HasIndex(e => e.FilmId)
                    .HasName("idx_fk_film_id");

                entity.Property(e => e.ActorId)
                    .HasColumnName("actor_id")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.FilmId)
                    .HasColumnName("film_id")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.FilmActor)
                    .HasForeignKey(d => d.ActorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_film_actor_actor");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.FilmActor)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_film_actor_film");
            });

            modelBuilder.Entity<FilmCategory>(entity =>
            {
                entity.HasKey(e => new { e.FilmId, e.CategoryId })
                    .HasName("PRIMARY");

                entity.ToTable("film_category");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("fk_film_category_category");

                entity.Property(e => e.FilmId)
                    .HasColumnName("film_id")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .HasColumnType("tinyint unsigned");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.FilmCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_film_category_category");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.FilmCategory)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_film_category_film");
            });

            modelBuilder.Entity<FilmList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("film_list");

                entity.Property(e => e.Actors).HasColumnName("actors");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasColumnName("category")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Fid)
                    .HasColumnName("FID")
                    .HasColumnType("smallint unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Length)
                    .HasColumnName("length")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(4,2)")
                    .HasDefaultValueSql("'4.99'");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("enum('G','PG','PG-13','R','NC-17')")
                    .HasDefaultValueSql("'G'");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FilmText>(entity =>
            {
                entity.HasKey(e => e.FilmId)
                    .HasName("PRIMARY");

                entity.ToTable("film_text");

                entity.HasIndex(e => new { e.Title, e.Description })
                    .HasName("idx_title_description");

                entity.Property(e => e.FilmId).HasColumnName("film_id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("inventory");

                entity.HasIndex(e => e.FilmId)
                    .HasName("idx_fk_film_id");

                entity.HasIndex(e => new { e.StoreId, e.FilmId })
                    .HasName("idx_store_id_film_id");

                entity.Property(e => e.InventoryId)
                    .HasColumnName("inventory_id")
                    .HasColumnType("mediumint unsigned");

                entity.Property(e => e.FilmId)
                    .HasColumnName("film_id")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.StoreId)
                    .HasColumnName("store_id")
                    .HasColumnType("tinyint unsigned");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_inventory_film");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_inventory_store");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("language");

                entity.Property(e => e.LanguageId)
                    .HasColumnName("language_id")
                    .HasColumnType("tinyint unsigned")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(20)
                    .IsFixedLength();
            });

            modelBuilder.Entity<NicerButSlowerFilmList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("nicer_but_slower_film_list");

                entity.Property(e => e.Actors).HasColumnName("actors");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasColumnName("category")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Fid)
                    .HasColumnName("FID")
                    .HasColumnType("smallint unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Length)
                    .HasColumnName("length")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(4,2)")
                    .HasDefaultValueSql("'4.99'");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("enum('G','PG','PG-13','R','NC-17')")
                    .HasDefaultValueSql("'G'");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payment");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("idx_fk_customer_id");

                entity.HasIndex(e => e.RentalId)
                    .HasName("fk_payment_rental");

                entity.HasIndex(e => e.StaffId)
                    .HasName("idx_fk_staff_id");

                entity.Property(e => e.PaymentId)
                    .HasColumnName("payment_id")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.PaymentDate).HasColumnName("payment_date");

                entity.Property(e => e.RentalId).HasColumnName("rental_id");

                entity.Property(e => e.StaffId)
                    .HasColumnName("staff_id")
                    .HasColumnType("tinyint unsigned");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_payment_customer");

                entity.HasOne(d => d.Rental)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.RentalId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_payment_rental");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_payment_staff");
            });

            modelBuilder.Entity<Rental>(entity =>
            {
                entity.ToTable("rental");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("idx_fk_customer_id");

                entity.HasIndex(e => e.InventoryId)
                    .HasName("idx_fk_inventory_id");

                entity.HasIndex(e => e.StaffId)
                    .HasName("idx_fk_staff_id");

                entity.HasIndex(e => new { e.RentalDate, e.InventoryId, e.CustomerId })
                    .HasName("rental_date")
                    .IsUnique();

                entity.Property(e => e.RentalId).HasColumnName("rental_id");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.InventoryId)
                    .HasColumnName("inventory_id")
                    .HasColumnType("mediumint unsigned");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.RentalDate).HasColumnName("rental_date");

                entity.Property(e => e.ReturnDate).HasColumnName("return_date");

                entity.Property(e => e.StaffId)
                    .HasColumnName("staff_id")
                    .HasColumnType("tinyint unsigned");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Rental)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rental_customer");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.Rental)
                    .HasForeignKey(d => d.InventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rental_inventory");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Rental)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rental_staff");
            });

            modelBuilder.Entity<SalesByFilmCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("sales_by_film_category");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasColumnName("category")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.TotalSales)
                    .HasColumnName("total_sales")
                    .HasColumnType("decimal(27,2)");
            });

            modelBuilder.Entity<SalesByStore>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("sales_by_store");

                entity.Property(e => e.Manager)
                    .HasColumnName("manager")
                    .HasMaxLength(91)
                    .IsUnicode(false);

                entity.Property(e => e.Store)
                    .HasColumnName("store")
                    .HasMaxLength(101)
                    .IsUnicode(false);

                entity.Property(e => e.TotalSales)
                    .HasColumnName("total_sales")
                    .HasColumnType("decimal(27,2)");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("staff");

                entity.HasIndex(e => e.AddressId)
                    .HasName("idx_fk_address_id");

                entity.HasIndex(e => e.StoreId)
                    .HasName("idx_fk_store_id");

                entity.Property(e => e.StaffId)
                    .HasColumnName("staff_id")
                    .HasColumnType("tinyint unsigned")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.AddressId)
                    .HasColumnName("address_id")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Picture)
                    .HasColumnName("picture")
                    .HasColumnType("blob");

                entity.Property(e => e.StoreId)
                    .HasColumnName("store_id")
                    .HasColumnType("tinyint unsigned");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_staff_address");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_staff_store");
            });

            modelBuilder.Entity<StaffList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("staff_list");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("tinyint unsigned");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(91)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Sid)
                    .HasColumnName("SID")
                    .HasColumnType("tinyint unsigned");

                entity.Property(e => e.ZipCode)
                    .HasColumnName("zip code")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("store");

                entity.HasIndex(e => e.AddressId)
                    .HasName("idx_fk_address_id");

                entity.HasIndex(e => e.ManagerStaffId)
                    .HasName("idx_unique_manager")
                    .IsUnique();

                entity.Property(e => e.StoreId)
                    .HasColumnName("store_id")
                    .HasColumnType("tinyint unsigned")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AddressId)
                    .HasColumnName("address_id")
                    .HasColumnType("smallint unsigned");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.ManagerStaffId)
                    .HasColumnName("manager_staff_id")
                    .HasColumnType("tinyint unsigned");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Store)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_store_address");

                entity.HasOne(d => d.ManagerStaff)
                    .WithOne(p => p.StoreNavigation)
                    .HasForeignKey<Store>(d => d.ManagerStaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_store_staff");
            });

            modelBuilder.Entity<Studentclass>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("studentclass");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("students");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.StudentName)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Table1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("table1");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Table2>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("table2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
