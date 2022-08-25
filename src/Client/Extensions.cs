using MudBlazor;

namespace Client;

public static class Extensions
{
    public static void AddError<T>(this MudTextField<T> input, string error)
    {
        input.ErrorText = error;
        input.Error = true;
    }
}
