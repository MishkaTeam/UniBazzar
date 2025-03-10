﻿namespace Domain;

public interface IUnitOfWork
{
    Task<int> CommitAsync(CancellationToken? cancellationToken = null);
}
