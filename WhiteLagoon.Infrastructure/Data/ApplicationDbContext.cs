using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Domain.Entities;

namespace WhiteLagoon.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Villa",
                    Description = "A beautiful villa located in the heart of White Lagoon.",
                    ImageUrl = "https://placehold.co/600x400",
                    Occupancy = 4,
                    Price = 150000,
                    Sqft = 1200,
                },

                new Villa
                {
                    Id = 2,
                    Name = "Villa 2",
                    Description = "Another villa in White Lagoon.",
                    ImageUrl = "https://via.placeholder.com/600x400",
                    Occupancy = 3,
                    Price = 120000,
                    Sqft = 1000,
                },

                new Villa
                {
                    Id = 3,
                    Name = "Villa 3",
                    Description = "A third villa in White Lagoon.",
                    ImageUrl = "https://via.placeholder.com/600x400",
                    Occupancy = 2,
                    Price = 90000,
                    Sqft = 800,
                }
            );

            //modelBuilder.Entity<VillaNumber>().HasData(
            //    new VillaNumber
            //    {
            //        Villa_Number = 101,
            //        VillaId = 1,
            //    },
            //    new VillaNumber
            //    {
            //        Villa_Number = 102,
            //        VillaId = 1,
            //    },
            //    new VillaNumber
            //    {
            //        Villa_Number = 103,
            //        VillaId = 1,
            //    },
            //    new VillaNumber
            //    {
            //        Villa_Number = 104,
            //        VillaId = 1,
            //    },
            //    new VillaNumber
            //    {
            //        Villa_Number = 201,
            //        VillaId = 2,
            //    },
            //    new VillaNumber
            //    {
            //        Villa_Number = 202,
            //        VillaId = 2,
            //    },
            //    new VillaNumber
            //    {
            //        Villa_Number = 203,
            //        VillaId = 2,
            //    },
            //    new VillaNumber
            //    {
            //        Villa_Number = 301,
            //        VillaId = 3,
            //    },
            //    new VillaNumber
            //    {
            //        Villa_Number = 302,
            //        VillaId = 3,
            //    }
            //);
        }
    }
}
