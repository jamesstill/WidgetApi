using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WidgetApi.Models;

namespace WidgetApi.EFCore
{
    public class WidgetMap : IEntityTypeConfiguration<Widget>
    {
        public void Configure(EntityTypeBuilder<Widget> builder)
        {
            builder.HasKey(t => t.ID);
            builder.ToTable("Widget");
            builder.Property(t => t.ID);
            builder.Property(t => t.Name);
            builder.Property(t => t.Shape);
        }
    }
}
