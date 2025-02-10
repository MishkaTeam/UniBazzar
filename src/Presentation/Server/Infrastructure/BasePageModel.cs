using Server.Infrastructure.Messages;

namespace Infrastructure;

public abstract class BasePageModel :
	Microsoft.AspNetCore.Mvc.RazorPages.PageModel, IMessageHandler
{
	public BasePageModel() : base()
	{
	}

	public bool AddPageError(string? message)
	{
		return AddMessage
			(type: MessageType.PageError, message: message);
	}

	public bool AddPageWarning(string? message)
	{
		return AddMessage
			(type: MessageType.PageWarning, message: message);
	}

	public bool AddPageSuccess(string? message)
	{
		return AddMessage
			(type: MessageType.PageSuccess, message: message);
	}

	public bool AddToastError(string? message)
	{
		return AddMessage
			(type: MessageType.ToastError, message: message);
	}

	public bool AddToastWarning(string? message)
	{
		return AddMessage
			(type: MessageType.ToastWarning, message: message);
	}

	public bool AddToastSuccess(string? message)
	{
		return AddMessage
			(type: MessageType.ToastSuccess, message: message);
	}

	public bool AddMessage(MessageType type, string? message)
	{
		return Utility.AddMessage
			(tempData: TempData, type: type, message: message);
	}

	protected string SetReturnUrl(string? returnUrl)
	{
		if (string.IsNullOrWhiteSpace(value: returnUrl))
		{
			returnUrl = "./Index";
		}

		return returnUrl;
	}
}
