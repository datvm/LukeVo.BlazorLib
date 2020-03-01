using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LukeVo.Blazor.Demo.Services
{
    public interface IJsService
    {
        Task HighlighAllCodes();
    }

    public class JsService : IJsService
    {

        IJSRuntime jsRuntime;

        public JsService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task HighlighAllCodes()
        {
            await this.jsRuntime.InvokeVoidAsync("highlightAll");
        }

    }

}
