namespace Modules.WalletOps.Domain.Exceptions;

public class InvalidBalanceException(string? message) : Exception(message ?? "Insufficient funds for this operation.")
{
}
