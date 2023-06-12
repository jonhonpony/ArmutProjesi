﻿using Armut.Model;
using EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.AdresModelleri;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<AltKategori> AltKategoriler { get; set; }
        public DbSet<KullaniciRol> KullaniciRoller { get; set; }
        public DbSet<Rol> Roller { get; set; }

        public DbSet<Il> Iller { get; set; }
        public DbSet<Ilce> Ilceler { get; set; }
        public DbSet<Mahalle> Mahalleler { get; set; }
        public DbSet<Ulke> Ulkeler { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Aktivite>().HasOne(e => e.TeklifVeren).WithMany(e => e.TeklifVern).HasForeignKey(e => e.TeklifVerenId).OnDelete(DeleteBehavior.ClientSetNull);
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Aktivite>().HasOne(e => e.TeklifIsteyen).WithMany(e => e.TeklifAln).HasForeignKey(e => e.TeklifAlanId).OnDelete(DeleteBehavior.ClientSetNull);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Ad);
                entity.HasOne(e => e.KullaniciRol).WithOne(e => e.Rol).HasForeignKey<KullaniciRol>(e => e.RolId);

            });
            modelBuilder.Entity<KullaniciRol>(entity =>
            {
                entity.HasKey(e => e.KullaniciId);
                entity.Property(e => e.RolId);

            });

            modelBuilder.Entity<Kategori>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.KategoriAdi);


            });

            modelBuilder.Entity<AltKategori>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.KategoriId });
                entity.Property(e => e.AltKategoriAdi);
                entity.Property(e => e.Aktif);
                entity.HasOne(e => e.Kategori).WithMany(e => e.AltKategoriler).HasForeignKey(e => e.KategoriId);
            });

            modelBuilder.Entity<Soru>(entity =>
            {
                entity.HasKey(e => new { e.SoruId, e.AltKategoriId });
                entity.Property(e => e.Sorular);
                entity.HasOne(e => e.AltKategori).WithMany(e => e.Sorular).HasForeignKey(e => e.AltKategoriId);
            });



            modelBuilder.Entity<Kullanici>(entity =>
            {
                entity.HasKey(e => new { e.Id });
                entity.Property(e => e.KullaniciAdi);
                entity.Property(e => e.Ad);
                entity.Property(e => e.Soyad);
                entity.Property(e => e.Email);
                entity.Property(e => e.Sifre);
                entity.Property(e => e.Sifre2);
                entity.Property(e => e.Adres);
                entity.Property(e => e.Cinsiyet);
                entity.Property(e => e.TelefonNumarası);
                entity.Property(e => e.Aktif);
                entity.Property(e => e.KayitTarihi);
                entity.HasOne(e => e.KullaniciRol).WithOne(e => e.Kullanici).HasForeignKey<KullaniciRol>(e => e.KullaniciId);


            });

            AddDataToKullaniciVeKullaniciRol(modelBuilder);
            AddDataToRol(modelBuilder);
            AddDataToKategori(modelBuilder);
            AddDataToAltKategori(modelBuilder);
            AddDataToAltKategori2(modelBuilder);
            AddDataToAltKategori3(modelBuilder);
            AddDataToAltKategori4(modelBuilder);
            AddDataToAltKategori5(modelBuilder);
            AddDataToAltKategori6(modelBuilder);
            AddDataToAltKategori7(modelBuilder);
            AddDataToAltKategori8(modelBuilder);
            AddDataToAltKategori9(modelBuilder);
            AddDataToAltKategori10(modelBuilder);
            AddDataToAltKategori11(modelBuilder);
            AddDataToAltKategori12(modelBuilder);
        }

        void AddDataToKullaniciVeKullaniciRol(ModelBuilder modelBuilder)
        {
            var kullaniciId = Guid.NewGuid();
            modelBuilder.Entity<Kullanici>().HasData(
                    new Kullanici
                    {
                        Id = kullaniciId,
                        KullaniciAdi = "networkakademi",
                        Ad = "Network",
                        Soyad = "Network",
                        Email = "Network@gmail.com",
                        Sifre = "123",
                        Sifre2 = "123",
                        Adres = "İstanbul",
                        Cinsiyet = "Kadın",
                        TelefonNumarası = "123456",
                        Aktif = true,
                        KayitTarihi = DateTime.Now,


                    }

            );
            modelBuilder.Entity<KullaniciRol>().HasData(
                   new KullaniciRol
                   {
                       KullaniciId = kullaniciId,
                       RolId = 1

                   }

           );
        }



        void AddDataToRol(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rol>().HasData(
                    new Rol
                    {
                        Id =1,
                        Ad = "Admin"
                    },
                    new Rol
                    {
                        Id = 2,
                        Ad = "Hizmet Alan"
                    },
                    new Rol
                    {
                        Id = 3,
                        Ad = "Hizmet Veren"
                    }
            );
        }

        void AddDataToKategori(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kategori>().HasData(

                      new Kategori
                      {
                          Id = 1,
                          KategoriAdi = "Temizlik",

                      },
                      new Kategori
                      {
                          Id = 2,
                          KategoriAdi = "Tadilat",

                      },
                      new Kategori
                      {
                          Id = 3,
                          KategoriAdi = "Nakliyat",

                      },
                      new Kategori
                      {
                          Id = 4,
                          KategoriAdi = "Tamir",

                      },
                      new Kategori
                      {
                          Id = 5,
                          KategoriAdi = "Özel Ders",

                      },
                       new Kategori
                       {
                           Id = 6,
                           KategoriAdi = "Sağlık",

                       },
                       new Kategori
                       {
                           Id = 7,
                           KategoriAdi = "Organizasyon",

                       },
                       new Kategori
                       {
                           Id = 8,
                           KategoriAdi = "Fotoğraf Ve Video",

                       },
                        new Kategori
                        {
                            Id = 9,
                            KategoriAdi = "Dijital ve Kurumsal",

                        },
                       new Kategori
                       {
                           Id = 10,
                           KategoriAdi = "Evcil Hayvanlar",

                       },
                       new Kategori
                       {
                           Id = 11,
                           KategoriAdi = "Oto Ve Araç",

                       },
                        new Kategori
                        {
                            Id = 12,
                            KategoriAdi = "Diğer",

                        }
                  );
        }
        //Temizlik -kategoriid=1
        void AddDataToAltKategori(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AltKategori>().HasData(

                   new AltKategori
                   {
                       Id = 1,
                       AltKategoriAdi = "Ev Temizliği",
                       KategoriId = 1,
                       Aktif = true,
                       
                   },
                    new AltKategori
                    {
                        Id = 2,
                        AltKategoriAdi = "Apartman Temizliği",
                        KategoriId = 1,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 3,
                        AltKategoriAdi = "Ofis Temizliği",
                        KategoriId = 1,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 4,
                        AltKategoriAdi = "İnşaat Sonrası Ev Temizliği",
                        KategoriId = 1,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 5,
                        AltKategoriAdi = "Dükkan/Mağaza Temizliği",
                        KategoriId = 1,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 6,
                        AltKategoriAdi = "Haşere İlaçlama",
                        KategoriId = 1,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 7,
                        AltKategoriAdi = "Halı Yıkama",
                        KategoriId = 1,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 8,
                        AltKategoriAdi = "Kuru Temizleme",
                        KategoriId = 1,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 9,
                        AltKategoriAdi = "Mobilya Temizleme",
                        KategoriId = 1,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 10,
                        AltKategoriAdi = "Cam Temziliği",
                        KategoriId = 1,
                        Aktif = true
                    }

                );
        }

        void AddDataToAltKategori2(ModelBuilder modelBuilder)


        {
            modelBuilder.Entity<AltKategori>().HasData(
                 new AltKategori
                 {
                     Id = 11,
                     AltKategoriAdi = "Boya Badana",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 12,
                     AltKategoriAdi = "Kapı Tadilat",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 13,
                     AltKategoriAdi = "Pencere Tadilat",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 14,
                     AltKategoriAdi = "Banyo Tadilat",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 15,
                     AltKategoriAdi = "Duvar Örme",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 16,
                     AltKategoriAdi = "Cam Balkon",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 17,
                     AltKategoriAdi = "Zemin Döşeme",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                  {
                      Id = 18,
                      AltKategoriAdi = "Havuz Yapımı",
                      KategoriId = 2,
                      Aktif = true
                  },
                 new AltKategori
                 {
                     Id = 19,
                     AltKategoriAdi = "Gardrop Yapımı",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 20,
                     AltKategoriAdi = "iç Mimar",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 21,
                     AltKategoriAdi = "Prefabrik Ev Yapımı",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 22,
                     AltKategoriAdi = "Koltuk Döşeme",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 23,
                     AltKategoriAdi = "Çatı Tadilatı",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 24,
                     AltKategoriAdi = "Doğalgaz Tesisat",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 25,
                     AltKategoriAdi = "Gömme Dolap Yapımı",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 26,
                     AltKategoriAdi = "Güneş Enerjisi",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 27,
                     AltKategoriAdi = "Sineklik",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 28,
                     AltKategoriAdi = "Mezar Yapımı",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 29,
                     AltKategoriAdi = "Vestiyer ve Portmanto",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 30,
                     AltKategoriAdi = "Sandalye Döşeme",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 31,
                     AltKategoriAdi = "Ses Yalıtımı",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 32,
                     AltKategoriAdi = "Sıva",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 33,
                     AltKategoriAdi = "Kaynak",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 34,
                     AltKategoriAdi = "Alçı",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 35,
                     AltKategoriAdi = "Deprem Testi",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 36,
                     AltKategoriAdi = "Stor Perde",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 37,
                     AltKategoriAdi = "Panjur",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 38,
                     AltKategoriAdi = "Hazır Rulo Çim",
                     KategoriId = 2,
                     Aktif = true
                 }, new AltKategori
                 {
                     Id = 39,
                     AltKategoriAdi = "Özel Mobilya Yapımı",
                     KategoriId = 2,
                     Aktif = true
                 }, new AltKategori
                 {
                     Id = 40,
                     AltKategoriAdi = "Mermer",
                     KategoriId = 2,
                     Aktif = true
                 }, new AltKategori
                 {
                     Id = 41,
                     AltKategoriAdi = "Panel Çit",
                     KategoriId = 2,
                     Aktif = true
                 }, new AltKategori
                 {
                     Id = 42,
                     AltKategoriAdi = "Tente Branda",
                     KategoriId = 2,
                     Aktif = true
                 }, new AltKategori
                 {
                     Id = 43,
                     AltKategoriAdi = "Bina Güçlendirme",
                     KategoriId = 2,
                     Aktif = true
                 }, new AltKategori
                 {
                     Id = 44,
                     AltKategoriAdi = "Teras Kapatma",
                     KategoriId = 2,
                     Aktif = true
                 },
                 new AltKategori
                 {
                     Id = 45,
                     AltKategoriAdi = "Mutfak",
                     KategoriId = 2,
                     Aktif = true
                 }
                 );
        }

        void AddDataToAltKategori3(ModelBuilder modelBuilder)


        {
            modelBuilder.Entity<AltKategori>().HasData(
                     new AltKategori
                     {
                         Id = 46,
                         AltKategoriAdi = "Ambar Kargo",
                         KategoriId = 3,
                         Aktif = true
                     },
                    new AltKategori
                    {
                        Id = 137,
                        AltKategoriAdi = "Araç Taşıma",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 138,
                        AltKategoriAdi = "Asansörlü Nakliyat",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 139,
                        AltKategoriAdi = "BUlaşık Makinesi Taşıma",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 140,
                        AltKategoriAdi = "Buzdolabı Taşıma",
                        KategoriId = 3,
                        Aktif = true
                    },
                     new AltKategori
                     {
                         Id = 141,
                         AltKategoriAdi = "Çamaşır Makinesi Taşıma",
                         KategoriId = 3,
                         Aktif = true
                     },
                    new AltKategori
                    {
                        Id = 142,
                        AltKategoriAdi = "Depo Kiralama",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 143,
                        AltKategoriAdi = "Doblo Nakliye",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 144,
                        AltKategoriAdi = "Eşya Depolama",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 145,
                        AltKategoriAdi = "Eşya Taşıma",
                        KategoriId = 3,
                        Aktif = true
                    },
                     new AltKategori
                     {
                         Id = 146,
                         AltKategoriAdi = "Ev İçi Eşya Taşıma",
                         KategoriId = 3,
                         Aktif = true
                     },
                    new AltKategori
                    {
                        Id = 147,
                        AltKategoriAdi = "Evden Eve Nakliyat",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 148,
                        AltKategoriAdi = "Günlük Şöför",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 149,
                        AltKategoriAdi = "Havaalanı Transfer",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 150,
                        AltKategoriAdi = "Kamyonet Kiralama",
                        KategoriId = 3,
                        Aktif = true
                    },
                     new AltKategori
                     {
                         Id = 151,
                         AltKategoriAdi = "Kamyonet Nakliye",
                         KategoriId = 3,
                         Aktif = true
                     },
                    new AltKategori
                    {
                        Id = 152,
                        AltKategoriAdi = "Kedi Taşıma",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 153,
                        AltKategoriAdi = "Koltuk Taşıma",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 154,
                        AltKategoriAdi = "Köpe Taşıma",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 155,
                        AltKategoriAdi = "Minibüs Kiralama",
                        KategoriId = 3,
                        Aktif = true
                    },
                     new AltKategori
                     {
                         Id = 156,
                         AltKategoriAdi = "Minivan Nakliye",
                         KategoriId = 3,
                         Aktif = true
                     },
                    new AltKategori
                    {
                        Id = 157,
                        AltKategoriAdi = "Moto Kurye",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 158,
                        AltKategoriAdi = "Motosiklet Taşıma",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 159,
                        AltKategoriAdi = "Ofis Taşıma",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 160,
                        AltKategoriAdi = "Oto Çekici",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 161,
                        AltKategoriAdi = "Köpe Taşıma",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 162,
                        AltKategoriAdi = "Minibüs Kiralama",
                        KategoriId = 3,
                        Aktif = true
                    },
                     new AltKategori
                     {
                         Id = 163,
                         AltKategoriAdi = "Minivan Nakliye",
                         KategoriId = 3,
                         Aktif = true
                     },
                    new AltKategori
                    {
                        Id = 164,
                        AltKategoriAdi = "Moto Kurye",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 165,
                        AltKategoriAdi = "Motosiklet Taşıma",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 166,
                        AltKategoriAdi = "Ofis Taşıma",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 167,
                        AltKategoriAdi = "Oto Çekici",
                        KategoriId = 3,
                        Aktif = true
                    },

                    new AltKategori
                    {
                        Id = 168,
                        AltKategoriAdi = "Oto Kurtarma",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 169,
                        AltKategoriAdi = "Otobüs Kiralama",
                        KategoriId = 3,
                        Aktif = true
                    },
                        new AltKategori
                        {
                            Id = 170,
                            AltKategoriAdi = "Özel Şöför",
                            KategoriId = 3,
                            Aktif = true
                        },
                    new AltKategori
                    {
                        Id = 171,
                        AltKategoriAdi = "Paletli Yük Taşıma",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 172,
                        AltKategoriAdi = "Panelvan Nakliye",
                        KategoriId = 3,
                        Aktif = true
                    },
                      new AltKategori
                      {
                          Id = 173,
                          AltKategoriAdi = "Personel Servisi",
                          KategoriId = 3,
                          Aktif = true
                      },
                    new AltKategori
                    {
                        Id = 174,
                        AltKategoriAdi = "Şehir İçi Nakliye",
                        KategoriId = 3,
                        Aktif = true
                    },
                        new AltKategori
                        {
                            Id = 175,
                            AltKategoriAdi = "Şehirler Arası Araç Taşıma",
                            KategoriId = 3,
                            Aktif = true
                        },
                    new AltKategori
                    {
                        Id = 176,
                        AltKategoriAdi = "Şehirler Arası Eşya Taşıma",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 177,
                        AltKategoriAdi = "Şehirler Arası Motosiklet Taşıma",
                        KategoriId = 3,
                        Aktif = true
                    },
                      new AltKategori
                      {
                          Id = 178,
                          AltKategoriAdi = "Şehirler Arası Nakliye",
                          KategoriId = 3,
                          Aktif = true
                      },
                    new AltKategori
                    {
                        Id = 179,
                        AltKategoriAdi = "Şehirler Arası Yük Taşıma",
                        KategoriId = 3,
                        Aktif = true
                    },
                        new AltKategori
                        {
                            Id = 180,
                            AltKategoriAdi = "Şöförlü Araç Kiralama",
                            KategoriId = 3,
                            Aktif = true
                        },
                    new AltKategori
                    {
                        Id = 181,
                        AltKategoriAdi = "Taksi",
                        KategoriId = 3,
                        Aktif = true
                    },
                    new AltKategori
                    {
                        Id = 182,
                        AltKategoriAdi = "Transfer",
                        KategoriId = 3,
                        Aktif = true
                    },
                     new AltKategori
                     {
                         Id = 183,
                         AltKategoriAdi = "Uluslararası Evden Eve Nakliyat",
                         KategoriId = 3,
                         Aktif = true
                     },
                    new AltKategori
                    {
                        Id = 184,
                        AltKategoriAdi = "Uluslararası Nakliyat",
                        KategoriId = 3,
                        Aktif = true
                    },
                     new AltKategori
                     {
                         Id = 185,
                         AltKategoriAdi = "Yük Taşıma",
                         KategoriId = 3,
                         Aktif = true
                     },
                    new AltKategori
                    {
                        Id = 186,
                        AltKategoriAdi = "Yurtdışı Kargo",
                        KategoriId = 3,
                        Aktif = true
                    });
        }
        void AddDataToAltKategori4(ModelBuilder modelBuilder)


        {
            modelBuilder.Entity<AltKategori>().HasData(
                   //////////tamir katid=4
                   new AltKategori
                   {
                       Id = 187,
                       AltKategoriAdi = "Ahşap Kapı Tamiri",
                       KategoriId = 4,
                       Aktif = true
                   },
                   new AltKategori
                   {
                       Id = 188,
                       AltKategoriAdi = "Amerikan Panel Kapı Montajı",
                       KategoriId = 4,
                       Aktif = true
                   },
                    new AltKategori
                    {
                        Id = 189,
                        AltKategoriAdi = "Amerikan Panel Kapı Tamiri",
                        KategoriId = 4,
                        Aktif = true
                    },
                     new AltKategori
                     {
                         Id = 190,
                         AltKategoriAdi = "Asansör Bakım",
                         KategoriId = 4,
                         Aktif = true
                     },
                      new AltKategori
                      {
                          Id = 191,
                          AltKategoriAdi = "Avize Montaj",
                          KategoriId = 4,
                          Aktif = true
                      },
                       new AltKategori
                       {
                           Id = 192,
                           AltKategoriAdi = "Baza Tamiri",
                           KategoriId = 4,
                           Aktif = true
                       },
                        new AltKategori
                        {
                            Id = 193,
                            AltKategoriAdi = "Beyaz Eşya Tamiri",
                            KategoriId = 4,
                            Aktif = true
                        },
                         new AltKategori
                         {
                             Id = 194,
                             AltKategoriAdi = "Bilgisayar Format Atma",
                             KategoriId = 4,
                             Aktif = true
                         },
                          new AltKategori
                          {
                              Id = 195,
                              AltKategoriAdi = "Bilgisayar Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 196,
                              AltKategoriAdi = "Bilgisayar ve Laptop Parça Değişimi",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 197,
                              AltKategoriAdi = "Bulaşık Makinesi Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 198,
                              AltKategoriAdi = "Buzdolabı Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 199,
                              AltKategoriAdi = "Cam Kestirme",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 200,
                              AltKategoriAdi = "Cama Menfez Açma",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 201,
                              AltKategoriAdi = "Çamaşır Makinesi Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 202,
                              AltKategoriAdi = "Çanak Anten Ayarlama",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 203,
                              AltKategoriAdi = "Çanak Anten Kurulumu",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 204,
                              AltKategoriAdi = "Çelik Kapı Kilit Değiştirme",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 205,
                              AltKategoriAdi = "Çelik Kapı Montajı",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 206,
                              AltKategoriAdi = "Çelik Kapı Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 207,
                              AltKategoriAdi = "Çilingir Kapı Açma",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 208,
                              AltKategoriAdi = "Dolap Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 209,
                              AltKategoriAdi = "Duşakabin Montajı",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 210,
                              AltKategoriAdi = "Duşakabin Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 211,
                              AltKategoriAdi = "Duvara Raf Montajı",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 212,
                              AltKategoriAdi = "Elektrik Hattı Çekme",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 213,
                              AltKategoriAdi = "Elektrik Montaj",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 214,
                              AltKategoriAdi = "Elektrik Proje Çizimi",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 215,
                              AltKategoriAdi = "Elektrik Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 216,
                              AltKategoriAdi = "Elektrik Tesisatı",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 217,
                              AltKategoriAdi = "Elektrikçi",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 218,
                              AltKategoriAdi = "Elektrikli Süpürge Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 219,
                              AltKategoriAdi = "Fan Temizliği ve Termal Macun Değişimi",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 220,
                              AltKategoriAdi = "Fiber Kablo Çekimi",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 221,
                              AltKategoriAdi = "Gider Açma",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 222,
                              AltKategoriAdi = "Gömme Rezervuar Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 223,
                              AltKategoriAdi = "İnternet Kablosu Çekme",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 224,
                              AltKategoriAdi = "iPhone Batarya Değişimi",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 225,
                              AltKategoriAdi = "iPhone Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 226,
                              AltKategoriAdi = "Kablo ve Hat Çekme",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 227,
                              AltKategoriAdi = "Kamera Güvenlik Sistemleri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 228,
                              AltKategoriAdi = "Kamera Sistemleri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 229,
                              AltKategoriAdi = "Kapı Kilidi Değiştirme",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 230,
                              AltKategoriAdi = "Kırmadan Su Kaçağı Tespiti",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 231,
                              AltKategoriAdi = "Klima Bakım",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 232,
                              AltKategoriAdi = "Klima Gaz Dolumu",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 233,
                              AltKategoriAdi = "Klima Montaj",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 234,
                              AltKategoriAdi = "Klima Sökme",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 235,
                              AltKategoriAdi = "Klima Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 236,
                              AltKategoriAdi = "Klozet Montajı",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 237,
                              AltKategoriAdi = "Klozet Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 238,
                              AltKategoriAdi = "Koltuk Yanık Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 239,
                              AltKategoriAdi = "Kombi Bakım",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 240,
                              AltKategoriAdi = "Kombi Montaj",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 241,
                              AltKategoriAdi = "Kombi Petek Temizleme",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 242,
                              AltKategoriAdi = "Kombi Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 243,
                              AltKategoriAdi = "Korniş Montaj",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 244,
                              AltKategoriAdi = "Laptop Tamir",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 245,
                              AltKategoriAdi = "Masa Camı Kestirme",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 246,
                              AltKategoriAdi = "Mobilya Montaj",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 247,
                              AltKategoriAdi = "Mobilya Sabitleme",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 248,
                              AltKategoriAdi = "Mobilya Sökme Taşıma ve Montaj",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 249,
                              AltKategoriAdi = "Mobilya Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 250,
                              AltKategoriAdi = "Musluk Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 251,
                              AltKategoriAdi = "Mutfak Dolabı Montajı",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 252,
                              AltKategoriAdi = "Mutfak Dolabı Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 253,
                              AltKategoriAdi = "Mutfak Dolap Kapağı Değiştirme",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 254,
                              AltKategoriAdi = "Mutfak Gider Açma",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 255,
                              AltKategoriAdi = "Mutfak Tezgahı Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 256,
                              AltKategoriAdi = "Panjur Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 257,
                              AltKategoriAdi = "Pencere Camı Değişimi",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 258,
                              AltKategoriAdi = "Pencere Kapı Ve Pimapen Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 259,
                              AltKategoriAdi = "Pencere Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 260,
                              AltKategoriAdi = "Perde Korniş Takma",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 261,
                              AltKategoriAdi = "Petek Montajı",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 262,
                              AltKategoriAdi = "Petek Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 263,
                              AltKategoriAdi = "Petek Temizliği",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 264,
                              AltKategoriAdi = "Pimapen Kapı Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 265,
                              AltKategoriAdi = "Pimapen Pencere Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 266,
                              AltKategoriAdi = "PVC Pencere Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 267,
                              AltKategoriAdi = "Samsung Telefon Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 268,
                              AltKategoriAdi = "Sifon Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 269,
                              AltKategoriAdi = "Sıhhi Tesisat",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 270,
                              AltKategoriAdi = "Su Arıtma Filtre Değişimi",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 271,
                              AltKategoriAdi = "Su Arıtma Montaj",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 272,
                              AltKategoriAdi = "Su Kaçağı Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 273,
                              AltKategoriAdi = "Su Kaçağı Tespiti",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 274,
                              AltKategoriAdi = "Su Tesisatçısı",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 275,
                              AltKategoriAdi = "Su Tesisatı Döşeme",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 276,
                              AltKategoriAdi = "Su Tesisatı Montaj",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 277,
                              AltKategoriAdi = "Sürgülü Dolap Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 278,
                              AltKategoriAdi = "Taharet Musluğu Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 279,
                              AltKategoriAdi = "Tıkanıklık Açma",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 280,
                              AltKategoriAdi = "TV Ekran Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 281,
                              AltKategoriAdi = "TV LED Değişimi",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 282,
                              AltKategoriAdi = "TV Montajı",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 283,
                              AltKategoriAdi = "TV Panel Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 284,
                              AltKategoriAdi = "TV Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 285,
                              AltKategoriAdi = "Uydu Ayarlama",
                              KategoriId = 4,
                              Aktif = true
                          },
                          new AltKategori
                          {
                              Id = 286,
                              AltKategoriAdi = "Xiaomi Telefon Tamiri",
                              KategoriId = 4,
                              Aktif = true
                          });
        }
        void AddDataToAltKategori5(ModelBuilder modelBuilder)


        {
            modelBuilder.Entity<AltKategori>().HasData(
                    ///özel ders=katid=5
                    new AltKategori
                    {
                        Id = 287,
                        AltKategoriAdi = "Almanca Özel Ders",
                        KategoriId = 5,
                        Aktif = true
                    },
                           new AltKategori
                           {
                               Id = 2,
                               AltKategoriAdi = "Arapça Özel Ders",
                               KategoriId = 5,
                               Aktif = true
                           },
                           new AltKategori
                           {
                               Id = 3,
                               AltKategoriAdi = "Bağlama Dersi",
                               KategoriId = 5,
                               Aktif = true
                           },
                           new AltKategori
                           {
                               Id = 4,
                               AltKategoriAdi = "Basketbol Özel Ders",
                               KategoriId = 5,
                               Aktif = true
                           },
                           new AltKategori
                           {
                               Id = 5,
                               AltKategoriAdi = "Bateri Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 6,
                               AltKategoriAdi = "Boks Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 7,
                               AltKategoriAdi = "Çello Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 8,
                               AltKategoriAdi = "DGS Matematik Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 9,
                               AltKategoriAdi = "Diferansiyel Denklemler Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 10,
                               AltKategoriAdi = "Dikiş Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 11,
                               AltKategoriAdi = "Diksiyon Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 12,
                               AltKategoriAdi = "Direksiyon Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 13,
                               AltKategoriAdi = "Eğitim Koçu",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 14,
                               AltKategoriAdi = "Excel Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 15,
                               AltKategoriAdi = "Fen Bilimleri Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 16,
                               AltKategoriAdi = "Fitness Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 17,
                               AltKategoriAdi = "Fizik Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 18,
                               AltKategoriAdi = "Fransızca Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 19,
                               AltKategoriAdi = "Gitar Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 20,
                               AltKategoriAdi = "Gitar Kursu",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 21,
                               AltKategoriAdi = "Gölge Öğretmen",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 22,
                               AltKategoriAdi = "Grup Pilates Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 23,
                               AltKategoriAdi = "Hızlı Okuma Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 24,
                               AltKategoriAdi = "Hukuk Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 25,
                               AltKategoriAdi = "IELTS Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 26,
                               AltKategoriAdi = "İktisat Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 27,
                               AltKategoriAdi = "İlkokul Matematik Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 28,
                               AltKategoriAdi = "İlkokul Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 29,
                               AltKategoriAdi = "İngilizce Konuşma Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 30,
                               AltKategoriAdi = "İngilizce Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 31,
                               AltKategoriAdi = "İspanyolca Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 32,
                               AltKategoriAdi = "İstatistik Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 33,
                               AltKategoriAdi = "İtalyanca Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 34,
                               AltKategoriAdi = "Keman Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 35,
                               AltKategoriAdi = "Kick Boks Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 36,
                               AltKategoriAdi = "Kimya Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 37,
                               AltKategoriAdi = "Klarnet Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 38,
                               AltKategoriAdi = "KPSS Matematik Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 39,
                               AltKategoriAdi = "Kuran Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 40,
                               AltKategoriAdi = "LGS Matematik Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 41,
                               AltKategoriAdi = "Lise Matematik Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 42,
                               AltKategoriAdi = "Matematik Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 43,
                               AltKategoriAdi = "Motosiklet Eğitimi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 44,
                               AltKategoriAdi = "Ödev Ablası",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 1,
                               AltKategoriAdi = "Öğrenci Koçu",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 2,
                               AltKategoriAdi = "Okuma Yazma Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 3,
                               AltKategoriAdi = "Online Almanca Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 4,
                               AltKategoriAdi = "Online Arapça Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 5,
                               AltKategoriAdi = "Online Calculus Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 6,
                               AltKategoriAdi = "Online Diksiyon Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 7,
                               AltKategoriAdi = "Online Eğitim Koçu",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 8,
                               AltKategoriAdi = "Online Fizik Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 9,
                               AltKategoriAdi = "Online Fransızca Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 10,
                               AltKategoriAdi = "Online Gitar Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 11,
                               AltKategoriAdi = "Online IELTS Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 12,
                               AltKategoriAdi = "Online İlkokul Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 13,
                               AltKategoriAdi = "Online İngilizce Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 14,
                               AltKategoriAdi = "Online İspanyolca Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 15,
                               AltKategoriAdi = "Online İtalyanca Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 16,
                               AltKategoriAdi = "Online LGS Matematik Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 17,
                               AltKategoriAdi = "Online Lise Matematik Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 18,
                               AltKategoriAdi = "Online Muhasebe Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 19,
                               AltKategoriAdi = "Online Öğrenci Koçu",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 20,
                               AltKategoriAdi = "Online Ortaokul Matematik Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 21,
                               AltKategoriAdi = "Online Personal Trainer",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 22,
                               AltKategoriAdi = "Online Pilates Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 23,
                               AltKategoriAdi = "Online Resim Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 24,
                               AltKategoriAdi = "Online Rusça Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 25,
                               AltKategoriAdi = "Online Şan Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 1,
                               AltKategoriAdi = "Online Türkçe Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 2,
                               AltKategoriAdi = "Online TYT AYT Matematik Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 3,
                               AltKategoriAdi = "Online Yabancılar İçin Türkçe Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 4,
                               AltKategoriAdi = "Online Yoga Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 5,
                               AltKategoriAdi = "Ortaokul Matematik Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 6,
                               AltKategoriAdi = "Otizm Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 7,
                               AltKategoriAdi = "Özel Ders",
                               KategoriId = 5,
                               Aktif = true
                           },
                           new AltKategori
                           {
                               Id = 8,
                               AltKategoriAdi = "Personal Trainer",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 9,
                               AltKategoriAdi = "Pilates Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 10,
                               AltKategoriAdi = "Piyano Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 11,
                               AltKategoriAdi = "Reformer Pilates Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 12,
                               AltKategoriAdi = "Resim Kursu",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 13,
                               AltKategoriAdi = "Resim Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 14,
                               AltKategoriAdi = "Rusça Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 15,
                               AltKategoriAdi = "Şan Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 16,
                               AltKategoriAdi = "Satranç Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 17,
                               AltKategoriAdi = "Sınıf Öğretmeni",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 18,
                               AltKategoriAdi = "Spor Koçu",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 19,
                               AltKategoriAdi = "Tenis Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 20,
                               AltKategoriAdi = "Türkçe Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 21,
                               AltKategoriAdi = "TYT AYT Matematik Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 22,
                               AltKategoriAdi = "Üniversite Fizik Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 23,
                               AltKategoriAdi = "Üniversite Kimya Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 1,
                               AltKategoriAdi = "Üniversite Matematik Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 2,
                               AltKategoriAdi = "Yabancılar İçin Türkçe Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 3,
                               AltKategoriAdi = "Yan Flüt Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 4,
                               AltKategoriAdi = "Yazılım Özel Ders",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 5,
                               AltKategoriAdi = "Yoga Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 6,
                               AltKategoriAdi = "Yüzme Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           },
                           new AltKategori
                           {
                               Id = 7,
                               AltKategoriAdi = "Zeybek Dersi",
                               KategoriId = 5,
                               Aktif = true,
                           });
        }
        void AddDataToAltKategori6(ModelBuilder modelBuilder)


        {
            modelBuilder.Entity<AltKategori>().HasData(

       new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Aile Danışmanı",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Aile Terapisi",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Astroloji Danışmanlığı",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Bioenerji Uzmanı",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Çift Terapisi",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "ACilt Bakımı",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Cinsel Terapi",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Çocuk Bakımı",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Çocuk Bakımı Ve Ev",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Yardımcısı",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Çocuk Psikoloğu",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Dil Ve Konuşma Terapisi",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Diyetisyen",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Doğum Haritası Çıkarma",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Dövme Tattoo",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "EMDR Terapisi",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Emzirme Danışmanı",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Epilasyon",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Erkek Epilasyon",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Ev Yardımcısı",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Evde Bakım Hizmetleri",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Evde Fizik Tedavi",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Evde Hasta Bakımı",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Evde Hemşire",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Evde Yaşlı Bakımı",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Evlilik Terapisi",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Fal Bakma",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Fizyoterapist",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Gelin Makyajı",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Gelin Saçı",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Hasta Bakımı",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Hastane Refakatçisi",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Hemşire",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Hipnoterapi",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "İngilizce Oyun Ablası",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "İpek Kirpik",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Kalıcı Oje Yapımı",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Kaş Kontürü",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Kayropraktik",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Klinik Psikolog",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Kuaför",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Makyaj",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Manuel Terapi",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Masaj (Erkek İçin)",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Masaj (Kadın İçin)",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Medyum",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Microblading",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Nefes Terapisi",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Nişan Makyajı",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Ombre",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Online Çift Terapisi",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Online Cinsel Terapisi",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Online Çocuk Psikoloğo",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Online Dil Ve Konuşma",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Terapisi",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Online Diyetisyen",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Online Evlilik Terapisi",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Online Pedagog",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Online Psikolog",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Online Psikolojik",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Danışman",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Online Psikoterapi",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Online Stil Danışmanı",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Online Terapi",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Online Yaşam Koçu",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Oyun Ablası",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Oyun Terapisi",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Özel Ambulans",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Özel Eğitim",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Pedagog",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Personel Trainer",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Pilates Dersi",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Protez Tırnak Yapımı",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "APsikolog",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Psikolojik Danışmanı",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Psikoterapi",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Saatlik Çocuk Bakımı",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Saç Boyama",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Saç Kaynağı",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Stil Danışmanı",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "AUzman Psikolog",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Yaşam Koçu",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Yaşlı Bakımı",
           KategoriId = 6,
           Aktif = true
       }, new AltKategori
       {
           Id = 1,
           AltKategoriAdi = "Yetişkin Psikolog",
           KategoriId = 6,
           Aktif = true
       });
        }
        void AddDataToAltKategori7(ModelBuilder modelBuilder)


        {
            modelBuilder.Entity<AltKategori>().HasData(
                   new AltKategori
                   {
                       Id = 43,
                       AltKategoriAdi = "Açılış Organizasyonu",
                       KategoriId = 7,
                       Aktif = true
                   },
              new AltKategori
              {
                  Id = 43,
                  AltKategoriAdi = "Animatör",
                  KategoriId = 7,
                  Aktif = true
              },
               new AltKategori
               {
                   Id = 43,
                   AltKategoriAdi = "Bando Takımı",
                   KategoriId = 7,
                   Aktif = true
               },
              new AltKategori
              {
                  Id = 43,
                  AltKategoriAdi = "Butik Pasta",
                  KategoriId = 7,
                  Aktif = true
              },
               new AltKategori
               {
                   Id = 43,
                   AltKategoriAdi = "Canlı Müzik",
                   KategoriId = 7,
                   Aktif = true
               },
              new AltKategori
              {
                  Id = 43,
                  AltKategoriAdi = "Catering",
                  KategoriId = 7,
                  Aktif = true
              },
               new AltKategori
               {
                   Id = 43,
                   AltKategoriAdi = "Davet Catering",
                   KategoriId = 7,
                   Aktif = true
               },
              new AltKategori
              {
                  Id = 43,
                  AltKategoriAdi = "Davul Zurnacı Kiralama",
                  KategoriId = 7,
                  Aktif = true
              },
               new AltKategori
               {
                   Id = 43,
                   AltKategoriAdi = "DJ",
                   KategoriId = 7,
                   Aktif = true
               },
             new AltKategori
             {
                 Id = 43,
                 AltKategoriAdi = "Doğum Günü Catering",
                 KategoriId = 7,
                 Aktif = true
             },
              new AltKategori
              {
                  Id = 43,
                  AltKategoriAdi = "Doğum Günü Mekanları",
                  KategoriId = 7,
                  Aktif = true
              },
               new AltKategori
               {
                   Id = 43,
                   AltKategoriAdi = "Doğum Günü Organizasyonu",
                   KategoriId = 7,
                   Aktif = true
               },
              new AltKategori
              {
                  Id = 43,
                  AltKategoriAdi = "Doğum Günü Pastası",
                  KategoriId = 7,
                  Aktif = true
              },
               new AltKategori
               {
                   Id = 43,
                   AltKategoriAdi = "Düğün Bandosu",
                   KategoriId = 7,
                   Aktif = true
               },
              new AltKategori
              {
                  Id = 43,
                  AltKategoriAdi = "Düğün Catering",
                  KategoriId = 7,
                  Aktif = true
              },
               new AltKategori
               {
                   Id = 43,
                   AltKategoriAdi = "Düğün Organizasyon",
                   KategoriId = 7,
                   Aktif = true
               },
              new AltKategori
              {
                  Id = 43,
                  AltKategoriAdi = "Düğün Orkestrası",
                  KategoriId = 7,
                  Aktif = true
              },
               new AltKategori
               {
                   Id = 43,
                   AltKategoriAdi = "Düğün Yemeği",
                   KategoriId = 7,
                   Aktif = true
               },
              new AltKategori
              {
                  Id = 43,
                  AltKategoriAdi = "Evlilik Teklifi Organizasyon",
                  KategoriId = 7,
                  Aktif = true
              },
               new AltKategori
               {
                   Id = 43,
                   AltKategoriAdi = "Garson Kiralama",
                   KategoriId = 7,
                   Aktif = true
               },
              new AltKategori
              {
                  Id = 43,
                  AltKategoriAdi = "Gelin Alma Bandosu",
                  KategoriId = 7,
                  Aktif = true
              },
               new AltKategori
               {
                   Id = 43,
                   AltKategoriAdi = "Gelin Arabası Kiralama",
                   KategoriId = 7,
                   Aktif = true
               },
              new AltKategori
              {
                  Id = 43,
                  AltKategoriAdi = "Gelinlik Dikimi",
                  KategoriId = 7,
                  Aktif = true
              },
               new AltKategori
               {
                   Id = 43,
                   AltKategoriAdi = "Hastane Odası Süsleme",
                   KategoriId = 7,
                   Aktif = true
               },
              new AltKategori
              {
                  Id = 43,
                  AltKategoriAdi = "İftar Yemeği Catering",
                  KategoriId = 7,
                  Aktif = true
              },
               new AltKategori
               {
                   Id = 43,
                   AltKategoriAdi = "Kına Organizasyon",
                   KategoriId = 7,
                   Aktif = true
               },
              new AltKategori
              {
                  Id = 43,
                  AltKategoriAdi = "Klasik Araba Kiralama",
                  KategoriId = 7,
                  Aktif = true
              },
               new AltKategori
               {
                   Id = 43,
                   AltKategoriAdi = "Kokteyl Catering",
                   KategoriId = 7,
                   Aktif = true
               },
              new AltKategori
              {
                  Id = 43,
                  AltKategoriAdi = "Masa Sandalye Kiralama",
                  KategoriId = 7,
                  Aktif = true
              },
               new AltKategori
               {
                   Id = 43,
                   AltKategoriAdi = "Mevlüt Yemeği",
                   KategoriId = 7,
                   Aktif = true
               },
              new AltKategori
              {
                  Id = 43,
                  AltKategoriAdi = "Mevlüt Yemeği Catering",
                  KategoriId = 7,
                  Aktif = true
              },
               new AltKategori
               {
                   Id = 43,
                   AltKategoriAdi = "Müzisyen",
                   KategoriId = 7,
                   Aktif = true
               },
              new AltKategori
              {
                  Id = 43,
                  AltKategoriAdi = "Nişan İkramlıkları Catering",
                  KategoriId = 7,
                  Aktif = true
              },
                new AltKategori
                {
                    Id = 43,
                    AltKategoriAdi = "Nişan Menüsü Catering",
                    KategoriId = 7,
                    Aktif = true
                },
              new AltKategori
              {
                  Id = 43,
                  AltKategoriAdi = "Nişan Organizasyon",
                  KategoriId = 7,
                  Aktif = true
              },
               new AltKategori
               {
                   Id = 43,
                   AltKategoriAdi = "Nişan Pastası",
                   KategoriId = 7,
                   Aktif = true
               },
              new AltKategori
              {
                  Id = 43,
                  AltKategoriAdi = "Palyaço",
                  KategoriId = 7,
                  Aktif = true
              },
               new AltKategori
               {
                   Id = 43,
                   AltKategoriAdi = "Sihirbaz",
                   KategoriId = 7,
                   Aktif = true
               },
              new AltKategori
              {
                  Id = 43,
                  AltKategoriAdi = "Söz Organizyon",
                  KategoriId = 7,
                  Aktif = true
              },
               new AltKategori
               {
                   Id = 43,
                   AltKategoriAdi = "Sünnet Organizasyon",
                   KategoriId = 7,
                   Aktif = true
               },
              new AltKategori
              {
                  Id = 43,
                  AltKategoriAdi = "Tabldot Yemek",
                  KategoriId = 7,
                  Aktif = true
              },
               new AltKategori
               {
                   Id = 43,
                   AltKategoriAdi = "Temsili Nikah Memuru",
                   KategoriId = 7,
                   Aktif = true
               },
               new AltKategori
               {
                   Id = 43,
                   AltKategoriAdi = "Yaş Pasta",
                   KategoriId = 7,
                   Aktif = true
               },
               new AltKategori
               {
                   Id = 43,
                   AltKategoriAdi = "Yat Kiralama",
                   KategoriId = 7,
                   Aktif = true
               },
              new AltKategori
              {
                  Id = 43,
                  AltKategoriAdi = "Yatta Evlilik Teklifi",
                  KategoriId = 7,
                  Aktif = true
              });
        }
        void AddDataToAltKategori8(ModelBuilder modelBuilder)


        {
            modelBuilder.Entity<AltKategori>().HasData(

                //fotograf ve video katid=8
                new AltKategori
                {
                    Id = 1,
                    AltKategoriAdi = "Bebek Fotoğrafçısı",
                    KategoriId = 8,
                    Aktif = true
                }, new AltKategori
                {
                    Id = 1,
                    AltKategoriAdi = "Dış Çekim Fotoğraf",
                    KategoriId = 8,
                    Aktif = true
                }, new AltKategori
                {
                    Id = 1,
                    AltKategoriAdi = "Doğum Günü Fotoğrafçısı",
                    KategoriId = 8,
                    Aktif = true
                }, new AltKategori
                {
                    Id = 1,
                    AltKategoriAdi = "Drone Çekimi",
                    KategoriId = 8,
                    Aktif = true
                }, new AltKategori
                {
                    Id = 1,
                    AltKategoriAdi = "Drone Fotoğraf Ve Video",
                    KategoriId = 8,
                    Aktif = true
                }, new AltKategori
                {
                    Id = 1,
                    AltKategoriAdi = "Düğün Fotoğrafçısı",
                    KategoriId = 8,
                    Aktif = true
                }, new AltKategori
                {
                    Id = 1,
                    AltKategoriAdi = "Düğün Video Çekimi",
                    KategoriId = 8,
                    Aktif = true
                }, new AltKategori
                {
                    Id = 1,
                    AltKategoriAdi = "Fotoğrafçı",
                    KategoriId = 8,
                    Aktif = true
                }, new AltKategori
                {
                    Id = 1,
                    AltKategoriAdi = "Hamile Fotoğraf Çekimi",
                    KategoriId = 8,
                    Aktif = true
                }, new AltKategori
                {
                    Id = 1,
                    AltKategoriAdi = "İnstagram İçin Fotoğraf Çekimi",
                    KategoriId = 8,
                    Aktif = true
                }, new AltKategori
                {
                    Id = 1,
                    AltKategoriAdi = "Mekan Fotoğraf Çekimi",
                    KategoriId = 8,
                    Aktif = true
                }, new AltKategori
                {
                    Id = 1,
                    AltKategoriAdi = "Nişan Fotoğrafçısı",
                    KategoriId = 8,
                    Aktif = true
                }, new AltKategori
                {
                    Id = 1,
                    AltKategoriAdi = "Sosyal Medya İçin Fotoğraf Çekimi",
                    KategoriId = 8,
                    Aktif = true
                }, new AltKategori
                {
                    Id = 1,
                    AltKategoriAdi = "Sosyal Medya Video Çekimi",
                    KategoriId = 8,
                    Aktif = true
                }, new AltKategori
                {
                    Id = 1,
                    AltKategoriAdi = "Stüdyo Fotoğraf Çekimi",
                    KategoriId = 8,
                    Aktif = true
                }, new AltKategori
                {
                    Id = 1,
                    AltKategoriAdi = "Tanıtım Filmi Çekimi",
                    KategoriId = 8,
                    Aktif = true
                }, new AltKategori
                {
                    Id = 1,
                    AltKategoriAdi = "Ürün Fotoğraf Çekimi",
                    KategoriId = 8,
                    Aktif = true
                }, new AltKategori
                {
                    Id = 1,
                    AltKategoriAdi = "Video Çekimi",
                    KategoriId = 8,
                    Aktif = true
                }, new AltKategori
                {
                    Id = 1,
                    AltKategoriAdi = "Video Editörü",
                    KategoriId = 8,
                    Aktif = true
                });
        }
        void AddDataToAltKategori9(ModelBuilder modelBuilder)


        {
            modelBuilder.Entity<AltKategori>().HasData(
                  //dijital ve kurumsal katid=9
                  new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "3D Animasyon Yapımı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "3D Baskı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Abiye Dikimi",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Afiş Tasarım",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Almanca Çeviri",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Almanca Yeminli Tercüme",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Ambalaj Tasarım",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Android Uygulama",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Geliştirme",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Animasyon Yapımı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Arapça Çeviri",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "AutoCAD Çizim",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Banner Tasarımı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Broşür Baskı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Broşür Dağıtım",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Broşür Tasarım",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "CV Hazırlama Danışmanlığı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Davetiye Baskı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Dijital Baskı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Dijital Pazarlama ve Reklam",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Dış Ticaret Danışmanlık",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Duvara Resim Yapma",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "E Ticaret Danışmanı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "E Ticaret Sitesi Yapımı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Elbise Dikimi",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Elbise İmalatı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Elektronik Devre Tasarımı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Freelance Yazılımcı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Fuar Hostesi",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Google Reklam Yönetimi",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Graffiti",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Grafik Tasarım",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Gümrük Müşaviri",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "3D Animasyon Yapımı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "İllüstrasyon Çizim",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "İngilizce Çeviri",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "İngilizce Makale Yazımı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "İngilizce Yeminli Tercüme",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "İş Sağlığı ve Güvenliği",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Kadın Manken",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Karakalem Çizim",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Karikatür Çizim",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Kartvizit Baskı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Kartvizit Tasarımı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Katalog Tasarımı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Kitap Baskı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Kitap Editörü",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Kitap Kapağı Tasarımı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "KOSGEB Danışmanlık",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Kutu Harf Tabela",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Limited Şirket Kurma",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Logo Tasarım",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Marka Tescil",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Metin Yazarı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Metin Yazma",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Mevcut Web Sitesi Düzenleme",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Mobil Uygulama Geliştirme",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Moda Tasarım",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Modelist",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Müzik Prodüksiyon",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Osmanlıca Çeviri",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Oyun Programlama",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Özel Güvenlik",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Özel Koruma",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Photoshop Uzmanı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Pleksi Tabela",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Python Programlama",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Reklam Tasarımı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Ressam",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Rusça Çeviri",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Şahıs Şirketi Kurma",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Senaryo Yazarı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Logo Tasarım",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "SEO Hizmeti",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "SEO Uyumlu Makale Yazımı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Seslendirme",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "SGK Danışmanlık",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Simultane Çeviri",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Site Bina ve Apartman Yönetimi",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Sosyal Medya Danışmanlığı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Sosyal Medya Tasarım",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Sosyal Medya Uzmanı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Sosyal Medya Yönetimi ve Danışmanlığı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "SPSS Analizi",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Sunum Hazırlama",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Sweatshirt İmalatı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Tabela",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Tekstil Fason Dikim ve İmalat",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Tişört Baskı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Tişört İmalatı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Vize Danışmanı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Web Site Yapımı",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Web Tasarım",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Web Tasarım Programlama",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Web Yazılım",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Yazılım Geliştirme",
                      KategoriId = 9,
                      Aktif = true
                  }, new AltKategori
                  {
                      Id = 1,
                      AltKategoriAdi = "Yeminli Mütercim Tercüman",
                      KategoriId = 9,
                      Aktif = true
                  });
        }
        void AddDataToAltKategori10(ModelBuilder modelBuilder)


        {
            modelBuilder.Entity<AltKategori>().HasData(
                    //evcil hayvanlar katid=10
                    new AltKategori
                    {
                        Id = 1,
                        AltKategoriAdi = "Evde Kedi Bakımı",
                        KategoriId = 10,
                        Aktif = true
                    },
                   new AltKategori
                   {
                       Id = 1,
                       AltKategoriAdi = "Kedi Bakımı",
                       KategoriId = 10,
                       Aktif = true
                   },
                   new AltKategori
                   {
                       Id = 1,
                       AltKategoriAdi = "Kedi Oteli",
                       KategoriId = 10,
                       Aktif = true
                   },
                   new AltKategori
                   {
                       Id = 1,
                       AltKategoriAdi = "Kedi Teli",
                       KategoriId = 10,
                       Aktif = true
                   }, new AltKategori
                   {
                       Id = 1,
                       AltKategoriAdi = "Kedi Traşı",
                       KategoriId = 10,
                       Aktif = true
                   }, new AltKategori
                   {
                       Id = 1,
                       AltKategoriAdi = "Köpek Eğitimi",
                       KategoriId = 10,
                       Aktif = true
                   }, new AltKategori
                   {
                       Id = 1,
                       AltKategoriAdi = "Köpek Gezdirme",
                       KategoriId = 10,
                       Aktif = true
                   }, new AltKategori
                   {
                       Id = 1,
                       AltKategoriAdi = "Köpek Kuaförü",
                       KategoriId = 10,
                       Aktif = true
                   }, new AltKategori
                   {
                       Id = 1,
                       AltKategoriAdi = "Köpek Oteli",
                       KategoriId = 10,
                       Aktif = true
                   }, new AltKategori
                   {
                       Id = 1,
                       AltKategoriAdi = "Köpek Pansiyonu",
                       KategoriId = 10,
                       Aktif = true
                   }, new AltKategori
                   {
                       Id = 1,
                       AltKategoriAdi = "Köpek Traşı",
                       KategoriId = 10,
                       Aktif = true
                   }, new AltKategori
                   {
                       Id = 1,
                       AltKategoriAdi = "Pet Kuaförü",
                       KategoriId = 10,
                       Aktif = true
                   });
        }
        void AddDataToAltKategori11(ModelBuilder modelBuilder)


        {
            modelBuilder.Entity<AltKategori>().HasData(
                     //oto ve araç katid=11
                     new AltKategori
                     {
                         Id = 1,
                         AltKategoriAdi = "Araç Bakım",
                         KategoriId = 11,
                         Aktif = true
                     }, new AltKategori
                     {
                         Id = 1,
                         AltKategoriAdi = "Araç Folyo Kaplama",
                         KategoriId = 11,
                         Aktif = true
                     }, new AltKategori
                     {
                         Id = 1,
                         AltKategoriAdi = "Araç Koltuk Temizleme",
                         KategoriId = 11,
                         Aktif = true
                     }, new AltKategori
                     {
                         Id = 1,
                         AltKategoriAdi = "Araç Seramik Kaplama",
                         KategoriId = 11,
                         Aktif = true
                     }, new AltKategori
                     {
                         Id = 1,
                         AltKategoriAdi = "Balata Değişimi",
                         KategoriId = 11,
                         Aktif = true
                     }, new AltKategori
                     {
                         Id = 1,
                         AltKategoriAdi = "Baskı Balata Değişimi",
                         KategoriId = 11,
                         Aktif = true
                     }, new AltKategori
                     {
                         Id = 1,
                         AltKategoriAdi = "Boyasız Göçük Düzeltme",
                         KategoriId = 11,
                         Aktif = true
                     }, new AltKategori
                     {
                         Id = 1,
                         AltKategoriAdi = "Motor Yağ Değişimi",
                         KategoriId = 11,
                         Aktif = true
                     }, new AltKategori
                     {
                         Id = 1,
                         AltKategoriAdi = "Oto Bakım",
                         KategoriId = 11,
                         Aktif = true
                     }, new AltKategori
                     {
                         Id = 1,
                         AltKategoriAdi = "Oto Boya",
                         KategoriId = 11,
                         Aktif = true
                     }, new AltKategori
                     {
                         Id = 1,
                         AltKategoriAdi = "AOto Cam Filmi",
                         KategoriId = 11,
                         Aktif = true
                     }, new AltKategori
                     {
                         Id = 1,
                         AltKategoriAdi = "Oto Ekspertiz",
                         KategoriId = 11,
                         Aktif = true
                     }, new AltKategori
                     {
                         Id = 1,
                         AltKategoriAdi = "Oto Kuaför",
                         KategoriId = 11,
                         Aktif = true
                     }, new AltKategori
                     {
                         Id = 1,
                         AltKategoriAdi = "Pasta Cila",
                         KategoriId = 11,
                         Aktif = true
                     }, new AltKategori
                     {
                         Id = 1,
                         AltKategoriAdi = "Triger Seti Değişimi",
                         KategoriId = 11,
                         Aktif = true
                     });
        }
        void AddDataToAltKategori12(ModelBuilder modelBuilder)


        {
                modelBuilder.Entity<AltKategori>().HasData(
                          //diğer katid=12
                          new AltKategori
                          {
                              Id = 1,
                              AltKategoriAdi = "Emlak Satış Danışmanı",
                              KategoriId = 12,
                              Aktif = true
                          }, new AltKategori
                          {
                              Id = 1,
                              AltKategoriAdi = "Gayrimenkul Değerleme",
                              KategoriId = 12,
                              Aktif = true
                          }, new AltKategori
                          {
                              Id = 1,
                              AltKategoriAdi = "Özel Dedektif",
                              KategoriId = 12,
                              Aktif = true
                          }, new AltKategori
                          {
                              Id = 1,
                              AltKategoriAdi = "Trafik Sigortası",
                              KategoriId = 12,
                              Aktif = true
                          });
            }

        void AddDataToSoru(ModelBuilder modelBuilder)

        {
            modelBuilder.Entity<Soru>().HasData(
                //Ev temizliği
                      new Soru
                      {
                          SoruId=1,
                          AltKategoriId=1,
                          Sorular="Evin Büyüklüğü"
                      }, 
                      new Soru
                      {
                          SoruId=2,
                          AltKategoriId=1,
                          Sorular="Banyo Sayısı"
                      },
                      new Soru
                      {
                          SoruId=3,
                          AltKategoriId=1,
                          Sorular="Kaç Saat"
                      },
                      new Soru
                      {
                          SoruId=4,
                          AltKategoriId=1,
                          Sorular="Hangi Sıklık"
                      },
                      new Soru
                      {
                          SoruId=5,
                          AltKategoriId=1,
                          Sorular="Ek Hizmet"
                      },
                      new Soru
                      {
                          SoruId=5,
                          AltKategoriId=1,
                          Sorular="il"
                      },new Soru
                      {
                          SoruId=5,
                          AltKategoriId=1,
                          Sorular="ilçe"
                      },
                      new Soru
                      {
                          SoruId=5,
                          AltKategoriId=1,
                          Sorular="Detay"
                      },
                      /////Apartman Temizliği
                      ///
                      new Soru
                      {
                          SoruId =6,
                          AltKategoriId = 2,
                          Sorular = "Daire Sayısı"
                      }, 
                      new Soru
                      {
                          SoruId =7,
                          AltKategoriId = 2,
                          Sorular = "Çöp Toplansın mı"
                      }, 
                      new Soru
                      {
                          SoruId =8,
                          AltKategoriId = 2,
                          Sorular = "Hangi Sıklık"
                      }, 
                      new Soru
                      {
                          SoruId =9,
                          AltKategoriId = 2,
                          Sorular = "il"
                      }, new Soru
                      {
                          SoruId =9,
                          AltKategoriId = 2,
                          Sorular = "ilçe"
                      },
                      new Soru
                      {
                          SoruId =10,
                          AltKategoriId = 2,
                          Sorular = "Detay"
                      },
                      ///Ofis Temizliği
                      new Soru
                      {
                          SoruId = 11,
                          AltKategoriId = 3,
                          Sorular = "Hangi Sıklık"
                      },
                      new Soru
                      {
                          SoruId = 12,
                          AltKategoriId = 3,
                          Sorular = "Kaç Metrekare"
                      },
                      new Soru
                      {
                          SoruId = 13,
                          AltKategoriId = 3,
                          Sorular = "il"
                      }, new Soru
                      {
                          SoruId = 13,
                          AltKategoriId = 3,
                          Sorular = "ilçe"
                      },new Soru
                      {
                          SoruId = 14,
                          AltKategoriId = 3,
                          Sorular = "Detay"
                      },
                      ////İnşaat sonrası ev temizliği
                      new Soru
                      {
                          SoruId = 15,
                          AltKategoriId = 4,
                          Sorular = "Evin Büyüklüğü"
                      }, new Soru
                      {
                          SoruId = 16,
                          AltKategoriId = 4,
                          Sorular = "Banyo Sayısı"
                      }, new Soru
                      {
                          SoruId = 17,
                          AltKategoriId = 4,
                          Sorular = "Kaç Metrekare"
                      }, new Soru
                      {
                          SoruId = 17,
                          AltKategoriId = 4,
                          Sorular = "İl"
                      },new Soru
                      {
                          SoruId = 17,
                          AltKategoriId = 4,
                          Sorular = "İlçe"
                      }, new Soru
                      {
                          SoruId = 18,
                          AltKategoriId = 4,
                          Sorular = "Detay"
                      },
                       //////Dükkan Mağaza Temizliği
                       ///
                       new Soru
                       {
                           SoruId = 19,
                           AltKategoriId = 5,
                           Sorular = "Dükkan Büyüklüğü"
                       }, new Soru
                       {
                           SoruId = 20,
                           AltKategoriId = 5,
                           Sorular = "Hangi Sıklık"
                       }, new Soru
                       {
                           SoruId = 21,
                           AltKategoriId = 5,
                           Sorular = "İl"
                       },new Soru
                       {
                           SoruId = 21,
                           AltKategoriId = 5,
                           Sorular = "İlçe"
                       }, new Soru
                       {
                           SoruId = 22,
                           AltKategoriId = 5,
                           Sorular = "Detay"
                       },
                       //////Haşere İlaçlama
                       new Soru
                       {
                           SoruId = 23,
                           AltKategoriId = 6,
                           Sorular = "Haşere Tipi"
                       },new Soru
                       {
                           SoruId = 24,
                           AltKategoriId = 6,
                           Sorular = "Alan Büyüklüğü"
                       },new Soru
                       {
                           SoruId = 25,
                           AltKategoriId = 6,
                           Sorular = "Mekan Tipi"
                       },new Soru
                       {
                           SoruId = 26,
                           AltKategoriId = 6,
                           Sorular = "İl"
                       },new Soru
                       {
                           SoruId = 26,
                           AltKategoriId = 6,
                           Sorular = "İlçe"
                       },new Soru
                       {
                           SoruId = 27,
                           AltKategoriId = 6,
                           Sorular = "Detay"
                       },
                       /////halı yıkama id=7
                       new Soru
                       {
                           SoruId = 28,
                           AltKategoriId = 7,
                           Sorular = "Halı Nerede Yıkansın"
                       },
                       new Soru
                       {
                           SoruId = 29,
                           AltKategoriId = 7,
                           Sorular = "Metrekaresi"
                       },
                       new Soru
                       {
                           SoruId = 30,
                           AltKategoriId = 7,
                           Sorular = "Detay"
                       },
                       new Soru
                       {
                           SoruId = 31,
                           AltKategoriId = 7,
                           Sorular = "Leke Var mı"
                       },
                       new Soru
                       {
                           SoruId = 32,
                           AltKategoriId = 7,
                           Sorular = "İl"
                       },
                       new Soru
                       {
                           SoruId = 32,
                           AltKategoriId = 7,
                           Sorular = "İlçe"
                       },new Soru
                       {
                           SoruId = 32,
                           AltKategoriId = 7,
                           Sorular = "Detay"
                       },

                       //////kuru temizleme id=8
                       new Soru
                       {
                           SoruId = 33,
                           AltKategoriId = 8,
                           Sorular = "Ürün"
                       },new Soru
                       {
                           SoruId = 33,
                           AltKategoriId = 8,
                           Sorular = "Ütü Yapılsın mı"
                       },new Soru
                       {
                           SoruId = 33,
                           AltKategoriId = 8,
                           Sorular = "Adet"
                       },new Soru
                       {
                           SoruId = 33,
                           AltKategoriId = 8,
                           Sorular = "İl"
                       },new Soru
                       {
                           SoruId = 33,
                           AltKategoriId = 8,
                           Sorular = "İlçe"
                       },new Soru
                       {
                           SoruId = 33,
                           AltKategoriId = 8,
                           Sorular = "Detay"
                       },
                       //////Mobilya temizleme
                       new Soru
                       {
                           SoruId = 34,
                           AltKategoriId = 9,
                           Sorular = "Mobilya"
                       }, new Soru
                       {
                           SoruId = 35,
                           AltKategoriId = 9,
                           Sorular = "Koltuk Minderli mi"
                       }, new Soru
                       {
                           SoruId = 36,
                           AltKategoriId = 9,
                           Sorular = "Adet"
                       }, new Soru
                       {
                           SoruId = 37,
                           AltKategoriId = 9,
                           Sorular = "İl"
                       }, new Soru
                       {
                           SoruId = 38,
                           AltKategoriId = 9,
                           Sorular = "İlçe"
                       }, new Soru
                       {
                           SoruId = 39,
                           AltKategoriId = 9,
                           Sorular = "Detay"
                       },

                      /////cam temizleme
                      new Soru
                      {
                          SoruId = 40,
                          AltKategoriId = 10,
                          Sorular = "Mekan"
                      }, new Soru
                      {
                          SoruId = 41,
                          AltKategoriId = 10,
                          Sorular = "İl"
                      },new Soru
                      {
                          SoruId = 42,
                          AltKategoriId = 10,
                          Sorular = "İlçe"
                      },new Soru
                      {
                          SoruId = 43,
                          AltKategoriId = 10,
                          Sorular = "Detay"
                      }
                      
                      );
        }

        void AddDataToCevap(ModelBuilder modelBuilder)

        {
            modelBuilder.Entity<Cevap>().HasData(
                      //Ev temizliği
                      new Cevap
                      {
                          CevapId = 1,
                          SoruId = 1,
                          
                      }
                      );
        }
    }
}

