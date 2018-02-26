using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DkrMosTenders.Model;
using DkrMosTenders.Web.Models;

namespace DkrMosTenders.Web.Models
{
    public class TendersContext : DbContext
    {
        public TendersContext (DbContextOptions<TendersContext> options)
            : base(options)
        {
            if (Database.EnsureCreated())
            {
                var uao = new District { ShortName = "ЮАО", FullName = "Южный Административный округ" };
                var uvao = new District { ShortName = "ЮВАО", FullName = "Юго-Восточный Административный округ" };
                var uzao = new District { ShortName = "ЮЗАО", FullName = "Юго-Западный Административный округ" };
                var svao = new District { ShortName = "СВАО", FullName = "Северо-Восточный Административный округ" };
                var zao = new District { ShortName = "ЗАО", FullName = "Западный Административный округ" };
                
                Tenders.Add(new Tender
                    {
                        DkrNumber = @"КР-003129-18",
                        FullName = @"завершение работ по капитальному ремонту общего имущества в многоквартирном (ых) доме (ах)",
                        Objects = new List<TenderObject>
                        {
                            new TenderObject { Building = new Building { Address = @"Докукина ул. 3 к.1", District = svao }}
                        },
                        MaxPrice = 3_515_518.56m,
                        UrlDkr = @"https://www.mos.ru/dkr/documents/view/212143220/"
                    });
                Tenders.Add(new Tender
                {
                    DkrNumber = @"КР-003131-18",
                    FullName = @"завершение работ по капитальному ремонту общего имущества в многоквартирном (ых) доме (ах)",
                    Objects = new List<TenderObject>
                        {
                            new TenderObject { Building = new Building { Address = @"Ленинский просп. 61/1", District = uzao }}
                        },
                    MaxPrice = 97_616_787.14m,
                    UrlDkr = @"https://www.mos.ru/dkr/documents/view/212364220/"
                });
                Tenders.Add(new Tender
                {
                    DkrNumber = @"КР-003132-18",
                    FullName = @"завершение работ по капитальному ремонту общего имущества в многоквартирном (ых) доме (ах)",
                    Objects = new List<TenderObject>
                        {
                            new TenderObject { Building = new Building { Address = @"Филевская 2-я ул. 3", District = zao }}
                        },
                    MaxPrice = 44_708_130.60m,
                    UrlDkr = @"https://www.mos.ru/dkr/documents/view/212363220/"
                });
                Tenders.Add(new Tender
                {
                    DkrNumber = @"КР-003130-18",
                    FullName = @"завершение работ по капитальному ремонту общего имущества в многоквартирном (ых) доме (ах)",
                    Objects = new List<TenderObject>
                        {
                            new TenderObject { Building = new Building { Address = @"Донская ул. 24", District = uao }}
                        },
                    MaxPrice = 7_292_813.01m,
                    UrlDkr = @"https://www.mos.ru/dkr/documents/view/212363220/"
                });
                SaveChanges();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<District>()
                .HasIndex(d => d.ShortName)
                .IsUnique();

            modelBuilder.Entity<Tender>()
                .HasMany<TenderObject>(t => t.Objects)
                .WithOne(t => t.Tender)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TenderObject>()
                .HasKey(t => new { t.TenderId, t.BuildingId });

            modelBuilder.Entity<TenderObject>()
                .HasOne(t => t.Tender)
                .WithMany(t => t.Objects);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<District> Districts { get; set; }
        public DbSet<Tender> Tenders { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<TenderObject> TendersObjects { get; set; }
        public DbSet<DkrMosTenders.Web.Models.DistrictViewModel> DistrictViewModel { get; set; }
    }
}
