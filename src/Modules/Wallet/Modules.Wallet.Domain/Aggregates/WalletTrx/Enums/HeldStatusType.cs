namespace Modules.WalletOps.Domain.Aggregates.WalletTrx;

/// <summary>
/// There is 3 main type that if any of them got any error during the action, 
/// the state goes to any of error types 
/// </summary>
public enum HeldStatusType
{
    Held,
    Released,
    Expire,
    Finalized,

    Held_Error,
    Release_Error,
    Expire_Error,
    Finalized_Error
}
