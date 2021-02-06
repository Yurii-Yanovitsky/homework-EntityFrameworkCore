using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Linq;

namespace MyApplyConfigurationsApp
{
    public class MessageEntityTypeConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasOne(m => m.UserFrom)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.UserFromID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.ToUser)
                .WithMany(u => u.RecivedMessages)
                .HasForeignKey(m => m.ToUserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(m => m.Text)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(date => date.Date).HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
