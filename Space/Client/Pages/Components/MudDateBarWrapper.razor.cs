using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Extensions;
using System.Globalization;
using Microsoft.AspNetCore.Components.Web;

namespace Space.Client.Pages.Components
{
    public partial class MudDateBarWrapper
    {

        private DateRange _dateRange = new DateRange(DateTime.UtcNow.AddYears(-10).StartOfMonth(CultureInfo.InvariantCulture),
                DateTime.UtcNow.EndOfMonth(CultureInfo.InvariantCulture));

        public EventCallback<DateRange> DateChanged { get; set; }

        public MudDateBarWrapper()
        {

        }

        public async Task DateChange(DateRange range)
        {
            await DateChanged.InvokeAsync(range);
        }
    }
}