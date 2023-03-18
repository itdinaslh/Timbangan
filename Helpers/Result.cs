using Microsoft.AspNetCore.Mvc;

namespace Timbangan.Helpers;

public static class Result
{
    [Produces("application/json")]
    public static Dictionary<string, bool> Success()
    {
        var result = new Dictionary<string, bool>
        {
            { "success", true }
        };
        return result;
    }

    [Produces("application/json")]
    public static Dictionary<string, bool> Failed()
    {
        var result = new Dictionary<string, bool>
        {
            { "failed", true }
        };

        return result;
    }
}
