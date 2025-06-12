using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowEngine.Infrastructure.Entities;
using WorkflowEngine.Infrastructure.Entities.Links;

namespace WorkflowEngine.Infrastructure.Data
{
    /// <summary>
    /// Represents the database context for the Workflow Engine, providing access to the application's data models.
    /// </summary>
    /// <remarks>This context is used to interact with the database for managing users, projects, and node
    /// instances. It configures entity relationships, including unique constraints and cascading delete
    /// behaviors.</remarks>
    public class WorkflowEngineDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowEngineDbContext"/> class using the specified options.
        /// </summary>
        /// <remarks>This constructor is typically used by dependency injection to configure and
        /// initialize the database context.</remarks>
        /// <param name="options">The options to configure the database context. This typically includes connection settings and other
        /// database-specific configurations.</param>
        public WorkflowEngineDbContext(DbContextOptions<WorkflowEngineDbContext> options) : base(options)
        {
        }
        public DbSet<Entities.User> Users { get; set; } = default!;
        public DbSet<Entities.Project> Projects { get; set; } = default!;
        public DbSet<Entities.NodeInstance> NodeInstances { get; set; } = default!;
        public DbSet<NodeConnection> NodeConnections { get; set; }
        public DbSet<NodeDefinition> NodeDefinitions { get; set; } = default!;
        public DbSet<PrePrompt> PrePrompts { get; set; }
        public DbSet<AffiliateLinkInputConfig> AffiliateLinkInputConfigs { get; set; }
        public DbSet<AffiliateLinkInputEntry> AffiliateLinkInputEntries { get; set; }
        public DbSet<LinkProcessingHistory> LinkProcessingHistory { get; set; }
        public DbSet<AffiliateLinkEntrySchedule> AffiliateLinkEntrySchedules { get; set; }


        /// <summary>
        /// Configures the entity framework model for the context by defining entity relationships, constraints, and indexes.
        /// </summary>
        /// <remarks>This method is called during the model creation process to define the structure and relationships of
        /// the database schema. It configures the following: <list type="bullet"> <item> <description> A unique index on the
        /// <see cref="User.Username"/> property to ensure that usernames are unique. </description> </item> <item>
        /// <description> A one-to-many relationship between <see cref="Project"/> and <see cref="User"/> where a project is
        /// owned by a user. Deleting a user cascades the deletion of their projects. </description> </item> <item>
        /// <description> A one-to-many relationship between <see cref="NodeInstance"/> and <see cref="Project"/> where a node
        /// instance belongs to a project. Deleting a project cascades the deletion of its node instances. </description>
        /// </item> <item> <description> A one-to-many relationship between <see cref="NodeConnection"/> and <see
        /// cref="NodeInstance"/> for both outgoing and incoming connections. Deleting a node instance cascades the deletion of
        /// its associated connections. </description> </item> </list></remarks>
        /// <param name="modelBuilder">An instance of <see cref="ModelBuilder"/> used to configure the model for the database context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Owner)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NodeInstance>()
                .HasOne(n => n.Project)
                .WithMany(p => p.NodeInstances)
                .HasForeignKey(n => n.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NodeConnection>()
                .HasOne(nc => nc.FromNode)
                .WithMany(n => n.OutgoingConnections)
                .HasForeignKey(nc => nc.FromNodeInstanceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NodeDefinition>()
                .HasKey(d => d.Id);
            modelBuilder.Entity<NodeDefinition>().Property(d => d.Id).ValueGeneratedNever();

            modelBuilder.Entity<PrePrompt>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(p => p.Name)
                 .IsRequired()
                 .HasMaxLength(128);
                b.Property(p => p.PromptText)
                 .IsRequired();
                b.Property(p => p.CreatedAt)
                 .HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<AffiliateLinkInputConfig>(b =>
            {
                b.HasKey(c => c.NodeInstanceId);
                b
                  .HasOne(c => c.NodeInstance)
                  .WithOne()                        // one-to-one
                  .HasForeignKey<AffiliateLinkInputConfig>(c => c.NodeInstanceId)
                  .OnDelete(DeleteBehavior.Cascade);

                b.Property(c => c.Mode)
                 .IsRequired()
                 .HasMaxLength(16);
                b.Property(c => c.CsvPath)
                 .HasMaxLength(512);
            });

            modelBuilder.Entity<AffiliateLinkInputEntry>(b =>
            {
                b.HasKey(e => e.Id);

                b.Property(e => e.Name)
                 .IsRequired()
                 .HasMaxLength(128);

                b.Property(e => e.Url)
                 .IsRequired()
                 .HasMaxLength(512);

                b.Property(e => e.Description)
                 .HasMaxLength(1024);

                b.Property(e => e.PrePromptName)
                 .HasMaxLength(128);

                b
                  .HasOne(e => e.Config)
                  .WithMany(c => c.Entries)
                  .HasForeignKey(e => e.NodeInstanceId)
                  .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<LinkProcessingHistory>(b =>
            {
                b.HasKey(h => h.Id);
                b.Property(h => h.ProcessedAt).IsRequired();
                b.Property(h => h.EntryId).IsRequired();

                b.HasOne(h => h.Entry)                     // <-- point at the navigation property
                 .WithMany(e => e.History)                // <-- Entry.History nav-prop
                 .HasForeignKey(h => h.EntryId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AffiliateLinkEntrySchedule>(b =>
            {
                b.HasKey(x => x.EntryId);
                b.Property(x => x.Frequency).IsRequired().HasMaxLength(16).HasDefaultValue("Daily");
                b
                  .HasOne(x => x.Entry)
                  .WithOne(e => e.Schedule)
                  .HasForeignKey<AffiliateLinkEntrySchedule>(x => x.EntryId)
                  .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
