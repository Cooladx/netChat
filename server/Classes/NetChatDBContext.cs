using Microsoft.EntityFrameworkCore;

namespace netChat
{
    public class NetChatDBContext : DbContext
    {
        public NetChatDBContext(DbContextOptions<NetChatDBContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("message");
                entity.HasKey(e => e.MessageId);
                entity.Property(e => e.MessageId)
                      .HasColumnName("messageid")
                      .UseIdentityColumn();  // maps SERIAL
                entity.Property(e => e.RoomId).HasColumnName("roomid");
                entity.Property(e => e.UserId).HasColumnName("userid");
                entity.Property(e => e.Content).HasColumnName("content");
                entity.Property(e => e.Timestamp).HasColumnName("timestamp");
            });

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Room>().ToTable("room");
        }
    }
}
