namespace Modules.WalletOps.Domain.Exceptions;

public class WalletDeactivateException(string? message) : Exception(message ?? "Operation cannot be performed on a non-active wallet.")
{
}
