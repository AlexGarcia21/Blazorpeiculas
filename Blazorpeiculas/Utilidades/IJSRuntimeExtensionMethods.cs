using Microsoft.JSInterop;
using System.Runtime.CompilerServices;
namespace Blazorpeiculas.Utilidades;

public static class IJSRuntimeExtensionMethods
{
    public async static ValueTask<bool> Confirm(this IJSRuntime JS, string mensaje)
    {
        return await JS.InvokeAsync<bool>("confirm",mensaje);
    }

    public async static ValueTask MostrarAlertaExitosa(this IJSRuntime JS, string titulo, string cuerpo)
    {
        await JS.InvokeVoidAsync("mostrarAlerta", titulo, cuerpo, "succes");
    }

    public async static ValueTask MostrarAlertaError(this IJSRuntime JS, string titulo, string cuerpo)
    {
        await JS.InvokeVoidAsync("mostrarAlerta", titulo, cuerpo, "error");
    }
}
