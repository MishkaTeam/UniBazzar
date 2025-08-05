namespace Modules.WalletOps.Domain.Exceptions;

internal class InvalidBalanceException(string? message) : Exception(message ?? "Insufficient funds for this operation.")
{
}
internal class WalletDeactivateException(string? message) : Exception(message ?? "Operation cannot be performed on a non-active wallet.")
{
}
internal class DomainException(string message) : Exception(message)
{
}
