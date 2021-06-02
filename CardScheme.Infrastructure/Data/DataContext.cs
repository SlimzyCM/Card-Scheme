using System;
using System.Collections.Generic;
using System.Text;
using CardScheme.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CardScheme.Infrastructure.Data
{
    /// <summary>
    /// DataBase context class
    /// </summary>
    public class DataContext : DbContext
    {
        public DbSet<CardTable> CardTables { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

    }
}
