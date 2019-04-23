using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WidgetApi.EFCore;
using WidgetApi.Models;

namespace WidgetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WidgetController : ControllerBase
    {
        private readonly WidgetContext _context;
        private readonly TelemetryClient _telemetryClient;

        public WidgetController(WidgetContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));

            _context.ChangeTracker.QueryTrackingBehavior =
                QueryTrackingBehavior.NoTracking;

            _telemetryClient = new TelemetryClient();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Widget>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get()
        {
            var items = await _context.Widgets
                .OrderBy(w => w.Name).ToListAsync();

            if (items == null || items.Count == 0)
            {
                var message = "There are no widgets in the system!";
                _telemetryClient.TrackTrace(message, SeverityLevel.Error);
                _telemetryClient.TrackEvent("404");
                return NotFound();
            }

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var item = await _context.Widgets.FirstOrDefaultAsync(w => w.ID == id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
    }
}
