namespace Server.Infrastructure.Messages;

public enum MessageType : byte
{
	PageError,
	PageWarning,
	PageSuccess,

	ToastError,
	ToastWarning,
	ToastSuccess,
}
