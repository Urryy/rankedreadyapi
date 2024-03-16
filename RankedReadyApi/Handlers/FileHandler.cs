using Microsoft.AspNetCore.Mvc;
using RankedReadyApi.Business.Service.Implementations;

namespace RankedReadyApi.Handlers;

public static class FileHandler
{
    public static async Task<IResult> GetFileSkin(
        [FromQuery] string path)
    {
        var extenstion = Path.GetExtension(path);
        byte[] binaryImage = await System.IO.File.ReadAllBytesAsync(Path.Combine(SystemService.GetApplicationFolder(), "SkinsImages", path));
        return Results.File(binaryImage, $"image/{extenstion}");
    }

    public static async Task<IResult> GetFileAnnouncment(
    [FromQuery] string path)
    {
        var extenstion = Path.GetExtension(path);
        byte[] binaryImage = await System.IO.File.ReadAllBytesAsync(Path.Combine(SystemService.GetApplicationFolder(), "AnnouncementPreviews", path));
        return Results.File(binaryImage, $"image/{extenstion}");
    }
}
