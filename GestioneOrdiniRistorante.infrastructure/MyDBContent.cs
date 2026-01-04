using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestioneOrdiniRistorante.Models;
using GestioneOrdiniRistorante.Models.Entities;
using Microsoft.Extensions.Configuration;

namespace GestioneOrdiniRistorante.Infrastructure
{
    public class MyDBContent :  DbContext
    {
        public DbSet<Prodotto> Prodotto { get; set; }
        public DbSet<Utente> Utente { get; set; }

        public DbSet<Ordine> Ordine { get; set; }

        public MyDBContent()
        {

        }
        public MyDBContent(DbContextOptions<MyDBContent> config) : base(config)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            Console.WriteLine("connessione in corso");
                var connectionString = @"Server=localhost\MSSQLSERVER01;Database=master;TrustServerCertificate=True;Integrated Security=True;";
            optionsBuilder.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 3,         // Number of retry attempts
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null))
                              .LogTo(Console.WriteLine)
                              .EnableSensitiveDataLogging();
            Console.WriteLine("Connessione completata");
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Utente>()
                .HasKey(o => o.Id);  // Impostazione esplicita della chiave primaria

            modelBuilder.Entity<Ordine>()
                .HasKey(o => o.Numero_Ordine);  // Impostazione esplicita della chiave primaria

            modelBuilder.Entity<Prodotto>()
                .Property(e => e.Prezzo);

            modelBuilder.Entity<Prodotto>()
                .HasKey(o => o.Id);  // Impostazione esplicita della chiave primaria

            modelBuilder.Entity<ProdottoInOrdine>()
                .HasKey(op => new { op.OrdineId, op.ProdottoId });

            modelBuilder.Entity<ProdottoInOrdine>()
                .HasKey(pio => pio.Id); // Imposta l'Id come chiave primaria

            modelBuilder.Entity<ProdottoInOrdine>()
                .Property(pio => pio.Id)
                .ValueGeneratedOnAdd(); // Indica che l'Id è auto-incrementante

            modelBuilder.Entity<ProdottoInOrdine>()
                .HasOne(pio => pio.Ordine)
                .WithMany(o => o.ProdottiInOrdine)
                .HasForeignKey(pio => pio.OrdineId);

            modelBuilder.Entity<ProdottoInOrdine>()
                .HasOne(pio => pio.Prodotto)
                .WithMany(p => p.ProdottiInOrdine) // Associazione bidirezionale
                .HasForeignKey(pio => pio.ProdottoId);
        }



    }
}
