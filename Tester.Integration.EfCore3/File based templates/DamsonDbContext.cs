// ------------------------------------------------------------------------------------------------
// This code was generated by EntityFramework Reverse POCO Generator (http://www.reversepoco.co.uk/).
// Created by Simon Hughes (https://about.me/simon.hughes).
//
// Registered to: Simon Hughes
// Company      : Reverse POCO
// Licence Type : Commercial
// Licences     : 1
// Valid until  : 03 NOV 2020
//
// Do not make changes directly to this file - edit the template instead.
//
// The following connection settings were used to generate this file:
//     Connection String Name: "McsfMultiDatabase"
//     Connection String:      "Data Source=(local);Initial Catalog=EfrpgTest;Integrated Security=True"
//     Multi-context settings: "Data Source=(local);Initial Catalog=EfrpgTest_Settings;Integrated Security=True"
// ------------------------------------------------------------------------------------------------
// Database Edition       : Developer Edition (64-bit)
// Database Engine Edition: Enterprise
// Database Version       : 14.0.2027.2

// <auto-generated>
// ReSharper disable CheckNamespace
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable NotAccessedVariable
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantCast
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// ReSharper disable UsePatternMatching
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tester;

namespace Tester.Integration.EfCore3.File_based_templatesPlum
{
    #region Database context interface

    public interface IDamsonDbContext : IDisposable
    {
        DbSet<NoPrimaryKey> NoPrimaryKeys { get; set; } // NoPrimaryKeys
        DbSet<Synonyms_Parent> Synonyms_Parents { get; set; } // Parent

        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
        DatabaseFacade Database { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        string ToString();
    }

    #endregion

    #region Database context

    public class DamsonDbContext : DbContext, IDamsonDbContext
    {
        private readonly IConfiguration _configuration;

        public DamsonDbContext()
        {
        }

        public DamsonDbContext(DbContextOptions<DamsonDbContext> options)
            : base(options)
        {
        }

        public DamsonDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<NoPrimaryKey> NoPrimaryKeys { get; set; } // NoPrimaryKeys
        public DbSet<Synonyms_Parent> Synonyms_Parents { get; set; } // Parent

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured && _configuration != null)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString(@"McsfMultiDatabase"));
            }
        }

        public bool IsSqlParameterNull(SqlParameter param)
        {
            var sqlValue = param.SqlValue;
            var nullableValue = sqlValue as INullable;
            if (nullableValue != null)
                return nullableValue.IsNull;
            return (sqlValue == null || sqlValue == DBNull.Value);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new NoPrimaryKeyConfiguration());
            modelBuilder.ApplyConfiguration(new Synonyms_ParentConfiguration());
        }

    }

    #endregion

    #region Database context factory

    public class DamsonDbContextFactory : IDesignTimeDbContextFactory<DamsonDbContext>
    {
        public DamsonDbContext CreateDbContext(string[] args)
        {
            return new DamsonDbContext();
        }
    }

    #endregion

    #region POCO classes

    // NoPrimaryKeys
    public class NoPrimaryKey
    {
        public string Description { get; set; } // Description (Primary key) (length: 10)

        // Reverse navigation

        /// <summary>
        /// Child Synonyms_Parents where [Parent].[ParentName] point to this entity (CustomNameForForeignKey)
        /// </summary>
        public virtual ICollection<Synonyms_Parent> ChildFkName { get; set; } // Parent.CustomNameForForeignKey

        public NoPrimaryKey()
        {
            ChildFkName = new List<Synonyms_Parent>();
        }
    }

    // Parent
    public class Synonyms_Parent
    {
        public int ParentId { get; set; } // ParentId (Primary key)
        public string ParentName { get; set; } // ParentName (length: 100)

        // Foreign keys

        /// <summary>
        /// Parent NoPrimaryKey pointed by [Parent].([ParentName]) (CustomNameForForeignKey)
        /// </summary>
        public virtual NoPrimaryKey ParentFkName { get; set; } // CustomNameForForeignKey
    }


    #endregion

    #region POCO Configuration

    // NoPrimaryKeys
    public class NoPrimaryKeyConfiguration : IEntityTypeConfiguration<NoPrimaryKey>
    {
        public void Configure(EntityTypeBuilder<NoPrimaryKey> builder)
        {
            builder.ToTable("NoPrimaryKeys", "dbo");
            builder.HasKey(x => x.Description);

            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(10).ValueGeneratedNever();
        }
    }

    // Parent
    public class Synonyms_ParentConfiguration : IEntityTypeConfiguration<Synonyms_Parent>
    {
        public void Configure(EntityTypeBuilder<Synonyms_Parent> builder)
        {
            builder.ToTable("Parent", "Synonyms");
            builder.HasKey(x => x.ParentId).HasName("PK_Parent").IsClustered();

            builder.Property(x => x.ParentId).HasColumnName(@"ParentId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.ParentName).HasColumnName(@"ParentName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);

            // Foreign keys
            builder.HasOne(a => a.ParentFkName).WithMany(b => b.ChildFkName).HasForeignKey(c => c.ParentName).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("CustomNameForForeignKey");
        }
    }


    #endregion

}
// </auto-generated>

