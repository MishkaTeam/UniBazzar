namespace Modules.WalletOps.Application.Contracts;

public enum PurchaseType
{
    Credit,
    Bank
}

public class WalletPurchaseResponseContract
{
    public bool ShouldRedirect
    {
        get
        {
            return !string.IsNullOrWhiteSpace(RedirectLink);
        }
    }
    public string RedirectLink { get; set; }
}

public class WalletPurchaseRequestContract
{
    public Guid ReferenceId { get; set; }
    public MoneyContract Amount { get; set; }
    public PurchaseType PurchaseType { get; set; }
}
