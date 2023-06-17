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

    [Produces("application/json")]
    public static Dictionary<string, string> SuccessMasuk(string weight, string door, string truckid)
    {
        var result = new Dictionary<string, string>
        {
            { "success", "yes" },
            { "WeightBefore", weight },
            { "DoorBefore", door },
            { "TruckBefore", truckid }
        };

        return result;
    }

    [Produces("application/json")]
    public static Dictionary<string, string> SuccessKeluar(string weight, string door, string truckid)
    {
        var result = new Dictionary<string, string>
        {
            { "success", "yes" },
            { "WeightBefore", weight },
            { "DoorBefore", door },
            { "TruckBefore", truckid }
        };

        return result;
    }

    [Produces("application/json")]
    public static Dictionary<string, bool> Unverified()
    {
        var result = new Dictionary<string, bool>
        {
            { "unverified", true }
        };
        return result;
    }

    [Produces("application/json")]
    public static Dictionary<string, bool> Blocked()
    {
        var result = new Dictionary<string, bool>
        {
            { "blocked", true }
        };
        return result;
    }

    [Produces("application/json")]
    public static Dictionary<string, bool> Retribusi()
    {
        var result = new Dictionary<string, bool>
        {
            { "retribusi", true }
        };
        return result;
    }

    [Produces("application/json")]
    public static Dictionary<string, bool> DoubleTap()
    {
        var result = new Dictionary<string, bool>
        {
            { "doubleTap", true }
        };
        return result;
    }
}
