using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groovy.Services
{
    public class JavaScriptInterop
    {
        private readonly IJSRuntime _jsRuntime;

        public JavaScriptInterop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string> Eval(string script)
        {
            return await _jsRuntime.InvokeAsync<string>("eval", script);
        }
    }
}
