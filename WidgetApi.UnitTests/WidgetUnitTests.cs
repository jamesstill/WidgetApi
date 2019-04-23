using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WidgetApi.Controllers;
using WidgetApi.EFCore;
using WidgetApi.Models;

namespace WidgetApi.UnitTests
{
    [TestClass]
    public class WidgetUnitTests
    {
        [TestMethod]
        public void Get_All_Widgets_Found()
        {
            var options = new DbContextOptionsBuilder<WidgetContext>()
                .UseInMemoryDatabase("Get_All_Widgets_Found")
                .Options;

            using (var context = new WidgetContext(options))
            {
                context.Widgets.Add(new Widget { ID = 1, Name = GetRandom(), Shape = GetRandom() });
                context.Widgets.Add(new Widget { ID = 2, Name = GetRandom(), Shape = GetRandom() });
                context.Widgets.Add(new Widget { ID = 3, Name = GetRandom(), Shape = GetRandom() });
                context.SaveChanges();

                var controller = new WidgetController(context);
                var actionResult = controller.Get().Result;
                var result = actionResult as OkObjectResult;
                var widgets = result.Value as IEnumerable<Widget>;

                Assert.IsNotNull(result);
                Assert.AreEqual(200, result.StatusCode);
                Assert.IsNotNull(widgets);
                Assert.AreEqual(3, widgets.Count());
            }
        }

        [TestMethod]
        public void Get_One_Widget_Found()
        {
            var options = new DbContextOptionsBuilder<WidgetContext>()
                .UseInMemoryDatabase("Get_One_Widget_Found")
                .Options;

            using (var context = new WidgetContext(options))
            {
                context.Widgets.Add(new Widget { ID = 1, Name = GetRandom(), Shape = GetRandom() });
                context.Widgets.Add(new Widget { ID = 2, Name = GetRandom(), Shape = GetRandom() });
                context.Widgets.Add(new Widget { ID = 3, Name = GetRandom(), Shape = GetRandom() });
                context.SaveChanges();

                var controller = new WidgetController(context);
                var actionResult = controller.Get(2).Result;
                var result = actionResult as OkObjectResult;
                var widget = result.Value as Widget;

                Assert.IsNotNull(result);
                Assert.AreEqual(200, result.StatusCode);
                Assert.IsNotNull(widget);
                Assert.AreEqual(2, widget.ID);
            }
        }

        [TestMethod]
        public void Get_All_Widgets_Not_Found()
        {
            var options = new DbContextOptionsBuilder<WidgetContext>()
                .UseInMemoryDatabase("Get_All_Widgets_Not_Found")
                .Options;

            using (var context = new WidgetContext(options))
            {
                var controller = new WidgetController(context);
                var actionResult = controller.Get().Result;
                var result = actionResult as NotFoundResult;

                Assert.IsNotNull(result);
                Assert.AreEqual(404, result.StatusCode);
            }
        }

        private static string GetRandom()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
