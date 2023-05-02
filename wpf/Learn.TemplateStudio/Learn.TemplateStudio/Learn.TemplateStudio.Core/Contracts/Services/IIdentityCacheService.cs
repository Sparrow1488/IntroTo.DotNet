﻿namespace Learn.TemplateStudio.Core.Contracts.Services;

public interface IIdentityCacheService
{
    void SaveMsalToken(byte[] token);

    byte[] ReadMsalToken();
}
