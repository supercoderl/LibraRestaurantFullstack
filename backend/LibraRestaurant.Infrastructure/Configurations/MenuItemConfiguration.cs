using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Infrastructure.Configurations
{
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder
                .HasKey(x => x.ItemId);

            builder
                .Property(item => item.Title)
                .IsRequired()
                .HasMaxLength(MaxLengths.MenuItem.Title);

            builder
                .Property(item => item.Slug)
                .IsRequired()
                .HasMaxLength(MaxLengths.MenuItem.Slug);

            builder
                .Property(item => item.Summary)
                .HasMaxLength(MaxLengths.MenuItem.Summary);

            builder
                .Property(item => item.SKU)
                .IsRequired()
                .HasMaxLength(MaxLengths.MenuItem.SKU);

            builder
                .Property(item => item.Picture);

            builder
                .Property(item => item.Price)
                .IsRequired()
                .HasColumnType("money");

            builder
                .Property(item => item.Quantity)
                .IsRequired()
                .HasColumnType("integer");

            builder
                .Property(item => item.Recipe)
                .HasMaxLength(MaxLengths.MenuItem.Repice);

            builder
                .Property(item => item.Instruction)
                .HasMaxLength(MaxLengths.MenuItem.Instruction);

            builder.HasData(new MenuItem(
                Ids.Seed.NumberId,
                "Nem lụi nướng mía",
                "nem-lui-nuong-mia",
                "Nem lụi được biết đến là đặc sản của vùng đất kinh kỳ đồng thời là lựa chọn mà mọi tín đồ yêu thích ẩm thực không thể bỏ qua. Món ăn hấp dẫn ngay từ cái nhìn đầu tiên với màu sắc vàng ươm cùng mùi vị thơm lừng sau khi được nướng lên. Thực khách sẽ cảm nhận trọn vẹn vị đậm đà pha chút mềm dai của thịt heo, giò sống hài hòa với các gia vị đặc biệt. Thêm vào đó, Nem lụi TASTY còn ngon hơn khi dùng kèm bánh tráng, bún tươi, rau sống và nước chấm sền sệt, vị bùi ngậy do chính các đầu bếp TASTY sáng tạo.",
                "FD000001",
                null,
                160000,
                10,
                "Mỡ gáy, thịt nạc mông, giò sống heo, mía cây, màu thực phẩm, chất tạo độ dai thực phẩm, bột nở, bột bắp, tiêu đen, tiêu sọ trắng, sả cây, hành tím, tỏi, mật ong, mắm khô, bột ngũ vị hương, bột ngọt, đường cát",
                "Ngon hơn khi dùng nóng",
                DateTime.Now,
                null));
        }
    }
}
