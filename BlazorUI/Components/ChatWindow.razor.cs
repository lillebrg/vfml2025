using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Components
{
    public partial class ChatWindow
    {
        [Parameter]
        public List<string> Messages { get; set; } = new();
    }
}
