using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WidgetApi.Models;

namespace WidgetApi.EFCore
{
    public static class Data
    {
        public static void Seed(WidgetContext context)
        {
            if (!context.Database.IsSqlite())
            {
                return;
            }

            if (context.Widgets.Any())
            {
                return;
            }

            var widgets = new List<Widget>() {
                new Widget {  ID = 1, Name = "Cog", Shape = "Square"},
                new Widget {  ID = 2, Name = "Gear", Shape = "Round"},
                new Widget {  ID = 3, Name = "Sprocket", Shape = "Octagonal"},
                new Widget {  ID = 4, Name = "Pinion", Shape = "Triangular"},
            };

            context.Widgets.AddRange(widgets);
            context.SaveChanges();
        }
    }
}
