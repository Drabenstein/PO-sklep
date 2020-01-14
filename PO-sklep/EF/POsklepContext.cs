using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PO_sklep.Models
{
    public partial class POsklepContext : DbContext
    {
        public POsklepContext()
        {
        }

        public POsklepContext(DbContextOptions<POsklepContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Faktura> Faktura { get; set; }
        public virtual DbSet<Kategoria> Kategoria { get; set; }
        public virtual DbSet<Klient> Klient { get; set; }
        public virtual DbSet<Konto> Konto { get; set; }
        public virtual DbSet<Opinia> Opinia { get; set; }
        public virtual DbSet<Platnosc> Platnosc { get; set; }
        public virtual DbSet<Pracownik> Pracownik { get; set; }
        public virtual DbSet<Produkt> Produkt { get; set; }
        public virtual DbSet<SposobDostawy> SposobDostawy { get; set; }
        public virtual DbSet<Stanowisko> Stanowisko { get; set; }
        public virtual DbSet<StatusZamowienia> StatusZamowienia { get; set; }
        public virtual DbSet<TypPlatnosci> TypPlatnosci { get; set; }
        public virtual DbSet<Zamowienie> Zamowienie { get; set; }
        public virtual DbSet<ZamowienieProdukt> ZamowienieProdukt { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=PO-sklep;User Id=sa;Password=epicWin132;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Faktura>(entity =>
            {
                entity.HasKey(e => e.IdFaktury)
                    .HasName("PK__Faktura__97E15267DC5D82F2");

                entity.Property(e => e.IdFaktury).HasColumnName("Id_faktury");

                entity.Property(e => e.DataWystawienia).HasColumnName("Data_wystawienia");

                entity.Property(e => e.IdZamowienia).HasColumnName("Id_zamowienia");

                entity.Property(e => e.SeriaFaktury)
                    .IsRequired()
                    .HasColumnName("Seria_faktury")
                    .HasMaxLength(40);

                entity.HasOne(d => d.IdZamowieniaNavigation)
                    .WithMany(p => p.Faktura)
                    .HasForeignKey(d => d.IdZamowienia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKFaktura629644");
            });

            modelBuilder.Entity<Kategoria>(entity =>
            {
                entity.HasKey(e => e.IdKategorii)
                    .HasName("PK__Kategori__AEFCAE3E19987DBC");

                entity.Property(e => e.IdKategorii).HasColumnName("Id_kategorii");

                entity.Property(e => e.IdNadkategorii).HasColumnName("Id_nadkategorii");

                entity.Property(e => e.NazwaKategorii)
                    .IsRequired()
                    .HasColumnName("Nazwa_kategorii")
                    .HasMaxLength(120);

                entity.HasOne(d => d.IdNadkategoriiNavigation)
                    .WithMany(p => p.InverseIdNadkategoriiNavigation)
                    .HasForeignKey(d => d.IdNadkategorii)
                    .HasConstraintName("FKKategoria547003");
            });

            modelBuilder.Entity<Klient>(entity =>
            {
                entity.HasKey(e => e.IdKlienta)
                    .HasName("PK__Klient__86B77D751F6E8314");

                entity.HasIndex(e => e.IdKonta)
                    .HasName("UQ__Klient__CA44B07D75E7A7B2")
                    .IsUnique();

                entity.Property(e => e.IdKlienta).HasColumnName("Id_klienta");

                entity.Property(e => e.Adres).HasMaxLength(255);

                entity.Property(e => e.DataUrodzenia).HasColumnName("Data_urodzenia");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.IdKonta).HasColumnName("Id_konta");

                entity.Property(e => e.ImieKlienta)
                    .IsRequired()
                    .HasColumnName("Imie_klienta")
                    .HasMaxLength(50);

                entity.Property(e => e.NazwiskoKlienta)
                    .IsRequired()
                    .HasColumnName("Nazwisko_klienta")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdKontaNavigation)
                    .WithOne(p => p.Klient)
                    .HasForeignKey<Klient>(d => d.IdKonta)
                    .HasConstraintName("FKKlient222869");
            });

            modelBuilder.Entity<Konto>(entity =>
            {
                entity.HasKey(e => e.IdKonta)
                    .HasName("PK__Konto__CA44B07C1AD1A92A");

                entity.HasIndex(e => e.Login)
                    .HasName("UQ__Konto__5E55825B8FDC27E6")
                    .IsUnique();

                entity.Property(e => e.IdKonta).HasColumnName("Id_konta");

                entity.Property(e => e.Haslo)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Opinia>(entity =>
            {
                entity.HasKey(e => e.IdOpinii)
                    .HasName("PK__Opinia__181B170F720D8FD9");

                entity.Property(e => e.IdOpinii).HasColumnName("Id_opinii");

                entity.Property(e => e.CzyPotwierdzonaZakupem)
                    .IsRequired()
                    .HasColumnName("Czy_potwierdzona_zakupem")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.IdKlienta).HasColumnName("Id_klienta");

                entity.Property(e => e.IdProduktu).HasColumnName("Id_produktu");

                entity.Property(e => e.Komentarz).HasMaxLength(1024);

                entity.HasOne(d => d.IdKlientaNavigation)
                    .WithMany(p => p.Opinia)
                    .HasForeignKey(d => d.IdKlienta)
                    .HasConstraintName("FKOpinia568679");

                entity.HasOne(d => d.IdProduktuNavigation)
                    .WithMany(p => p.Opinia)
                    .HasForeignKey(d => d.IdProduktu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOpinia194528");
            });

            modelBuilder.Entity<Platnosc>(entity =>
            {
                entity.HasKey(e => e.IdPlatnosci)
                    .HasName("PK__Platnosc__0EB5F39842EE1E13");

                entity.Property(e => e.IdPlatnosci).HasColumnName("Id_platnosci");

                entity.Property(e => e.DataPlatnosci).HasColumnName("Data_platnosci");

                entity.Property(e => e.IdTypuPlatnosci).HasColumnName("Id_typu_platnosci");

                entity.HasOne(d => d.IdTypuPlatnosciNavigation)
                    .WithMany(p => p.Platnosc)
                    .HasForeignKey(d => d.IdTypuPlatnosci)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPlatnosc582602");
            });

            modelBuilder.Entity<Pracownik>(entity =>
            {
                entity.HasKey(e => e.IdPracownika)
                    .HasName("PK__Pracowni__E9472F1C3FD4064D");

                entity.HasIndex(e => e.IdKonta)
                    .HasName("UQ__Pracowni__CA44B07D28D9CE19")
                    .IsUnique();

                entity.Property(e => e.IdPracownika).HasColumnName("Id_pracownika");

                entity.Property(e => e.IdKategorii).HasColumnName("Id_kategorii");

                entity.Property(e => e.IdKonta).HasColumnName("Id_konta");

                entity.Property(e => e.IdStanowiska).HasColumnName("Id_stanowiska");

                entity.Property(e => e.ImiePracownika)
                    .HasColumnName("Imie_pracownika")
                    .HasMaxLength(50);

                entity.Property(e => e.NazwiskoPracownika)
                    .HasColumnName("Nazwisko_pracownika")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdKategoriiNavigation)
                    .WithMany(p => p.Pracownik)
                    .HasForeignKey(d => d.IdKategorii)
                    .HasConstraintName("FKPracownik900642");

                entity.HasOne(d => d.IdKontaNavigation)
                    .WithOne(p => p.Pracownik)
                    .HasForeignKey<Pracownik>(d => d.IdKonta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPracownik475182");

                entity.HasOne(d => d.IdStanowiskaNavigation)
                    .WithMany(p => p.Pracownik)
                    .HasForeignKey(d => d.IdStanowiska)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPracownik372895");
            });

            modelBuilder.Entity<Produkt>(entity =>
            {
                entity.HasKey(e => e.IdProduktu)
                    .HasName("PK__Produkt__1F8E7F7723748FCB");

                entity.Property(e => e.IdProduktu).HasColumnName("Id_produktu");

                entity.Property(e => e.CenaNetto)
                    .HasColumnName("Cena_netto")
                    .HasColumnType("money");

                entity.Property(e => e.IdKategorii).HasColumnName("Id_kategorii");

                entity.Property(e => e.NazwaProduktu)
                    .IsRequired()
                    .HasColumnName("Nazwa_produktu")
                    .HasMaxLength(150);

                entity.Property(e => e.Opis).HasMaxLength(500);

                entity.Property(e => e.Producent)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Vat)
                    .HasColumnName("VAT")
                    .HasDefaultValueSql("((23))");

                entity.HasOne(d => d.IdKategoriiNavigation)
                    .WithMany(p => p.Produkt)
                    .HasForeignKey(d => d.IdKategorii)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKProdukt129623");
            });

            modelBuilder.Entity<SposobDostawy>(entity =>
            {
                entity.HasKey(e => e.IdSposobuDostawy)
                    .HasName("PK__Sposob_d__DE5960FD9DE4ABCE");

                entity.ToTable("Sposob_dostawy");

                entity.HasIndex(e => e.NazwaSposobuDostawy)
                    .HasName("UQ__Sposob_d__BA4551651F1853D9")
                    .IsUnique();

                entity.Property(e => e.IdSposobuDostawy).HasColumnName("Id_sposobu_dostawy");

                entity.Property(e => e.CenaDostawy)
                    .HasColumnName("Cena_dostawy")
                    .HasColumnType("money");

                entity.Property(e => e.NazwaSposobuDostawy)
                    .IsRequired()
                    .HasColumnName("Nazwa_sposobu_dostawy")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Stanowisko>(entity =>
            {
                entity.HasKey(e => e.IdStanowiska)
                    .HasName("PK__Stanowis__5733CF631F5BB8A6");

                entity.HasIndex(e => e.NazwaStanowiska)
                    .HasName("UQ__Stanowis__AEEE04305B10096F")
                    .IsUnique();

                entity.Property(e => e.IdStanowiska).HasColumnName("Id_stanowiska");

                entity.Property(e => e.NazwaStanowiska)
                    .IsRequired()
                    .HasColumnName("Nazwa_stanowiska")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StatusZamowienia>(entity =>
            {
                entity.HasKey(e => e.IdStatusu)
                    .HasName("PK__Status_z__6B2E4E754D817A83");

                entity.ToTable("Status_zamowienia");

                entity.HasIndex(e => e.NazwaStatusu)
                    .HasName("UQ__Status_z__C023B13FD79F371D")
                    .IsUnique();

                entity.Property(e => e.IdStatusu).HasColumnName("Id_statusu");

                entity.Property(e => e.NazwaStatusu)
                    .IsRequired()
                    .HasColumnName("Nazwa_statusu")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<TypPlatnosci>(entity =>
            {
                entity.HasKey(e => e.IdTypuPlatnosci)
                    .HasName("PK__Typ_plat__338FCBAA5905331F");

                entity.ToTable("Typ_platnosci");

                entity.HasIndex(e => e.NazwaTypuPlatnosci)
                    .HasName("UQ__Typ_plat__9C850379832DE26F")
                    .IsUnique();

                entity.Property(e => e.IdTypuPlatnosci).HasColumnName("Id_typu_platnosci");

                entity.Property(e => e.NazwaTypuPlatnosci)
                    .IsRequired()
                    .HasColumnName("Nazwa_typu_platnosci")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Zamowienie>(entity =>
            {
                entity.HasKey(e => e.IdZamowienia)
                    .HasName("PK__Zamowien__2E8286449B3C62F7");

                entity.Property(e => e.IdZamowienia).HasColumnName("Id_zamowienia");

                entity.Property(e => e.DataZlozenia)
                    .HasColumnName("Data_zlozenia")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.IdKlienta).HasColumnName("Id_klienta");

                entity.Property(e => e.IdPlatnosci).HasColumnName("Id_platnosci");

                entity.Property(e => e.IdSposobuDostawy).HasColumnName("Id_sposobu_dostawy");

                entity.Property(e => e.IdStatusu).HasColumnName("Id_statusu");

                entity.HasOne(d => d.IdKlientaNavigation)
                    .WithMany(p => p.Zamowienie)
                    .HasForeignKey(d => d.IdKlienta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKZamowienie162326");

                entity.HasOne(d => d.IdPlatnosciNavigation)
                    .WithMany(p => p.Zamowienie)
                    .HasForeignKey(d => d.IdPlatnosci)
                    .HasConstraintName("FKZamowienie325732");

                entity.HasOne(d => d.IdSposobuDostawyNavigation)
                    .WithMany(p => p.Zamowienie)
                    .HasForeignKey(d => d.IdSposobuDostawy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKZamowienie243163");

                entity.HasOne(d => d.IdStatusuNavigation)
                    .WithMany(p => p.Zamowienie)
                    .HasForeignKey(d => d.IdStatusu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKZamowienie243329");
            });

            modelBuilder.Entity<ZamowienieProdukt>(entity =>
            {
                entity.HasKey(e => new { e.IdProduktu, e.IdZamowienia })
                    .HasName("PK__Zamowien__4D6657136F7A9AF7");

                entity.ToTable("Zamowienie_Produkt");

                entity.Property(e => e.IdProduktu).HasColumnName("Id_produktu");

                entity.Property(e => e.IdZamowienia).HasColumnName("Id_zamowienia");

                entity.HasOne(d => d.IdProduktuNavigation)
                    .WithMany(p => p.ZamowienieProdukt)
                    .HasForeignKey(d => d.IdProduktu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKZamowienie582144");

                entity.HasOne(d => d.IdZamowieniaNavigation)
                    .WithMany(p => p.ZamowienieProdukt)
                    .HasForeignKey(d => d.IdZamowienia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKZamowienie194579");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
