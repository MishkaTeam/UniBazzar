namespace Server.Infrastructure.Settings;

public class CultureSettings
{
	public CultureSettings()
	{
		DefaultCulture = new CultureData()
		{
			Name = "فارسی - ایران",
			Culture = "fa-IR",
		};

		SupportedCulture =
			new List<CultureData>();
	}


	// **********
	public CultureData DefaultCulture { get; set; }
	// **********

	// **********
	public List<CultureData> SupportedCulture { get; set; }
	// **********

	public class CultureData
	{
		public string Name { get; set; }
		public string Culture { get; set; }
	}
}