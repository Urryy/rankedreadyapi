

using System.Runtime.InteropServices;

namespace RankedReadyApi.Business.Service.Implementations;

public static class SystemService
{
    private const string NotSupportedMessage = "Operating system not supported";

    public static string GetApplicationFolder()
    {
        var os = GetOperatingSystem();

        if (os == OSPlatform.Windows)
        {
            return GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }
        if (os == OSPlatform.Linux || os == OSPlatform.OSX)
        {
            return GetFolderPath(Environment.SpecialFolder.UserProfile);
        }

        throw new NotSupportedException(NotSupportedMessage);
    }

    private static string GetFolderPath(Environment.SpecialFolder folder)
        => Environment.GetFolderPath(folder);

    private static OSPlatform? GetOperatingSystem()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return OSPlatform.Windows;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return OSPlatform.Linux;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return OSPlatform.OSX;

        throw new NotSupportedException(NotSupportedMessage);
    }
}
