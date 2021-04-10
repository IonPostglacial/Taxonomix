using System.Threading.Tasks;
using System;
using Microsoft.JSInterop;

namespace Taxonomix.Utils
{
    public static class FileUtil
    {
        public async static Task SaveAs(IJSRuntime js, string filename, string data)
        {
            await js.InvokeAsync<object>(
                "saveAsFile",
                filename,
                data);
        }            
    }
}