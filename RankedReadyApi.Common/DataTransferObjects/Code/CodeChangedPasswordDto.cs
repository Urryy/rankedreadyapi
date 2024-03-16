﻿namespace RankedReadyApi.Common.DataTransferObjects.Code;

public class CodeChangedPasswordDto
{
    public string Id { get; set; }
    public string Code { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
}
