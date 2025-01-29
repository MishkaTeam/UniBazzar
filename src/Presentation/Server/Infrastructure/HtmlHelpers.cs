namespace Infrastructure;

public static class HtmlHelpers : object
{
	static HtmlHelpers()
	{
	}

	public static string DefaultValue
	{
		get
		{
			//return null;
			//return string.Empty;
			return "-----";
		}
	}

	//public static Microsoft.AspNetCore.Html.IHtmlContent Ub_DisplayInteger
	//	(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, long? value)
	//{
	//	if (value.HasValue == false)
	//	{
	//		return html.Raw(value: DefaultValue);
	//	}

	//	var result =
	//		value.Value.ToString(format: "#,##0");

	//	result =
	//		Convert.DigitsToUnicode(value: result);

	//	return html.Raw(value: result);
	//}

	//public static Microsoft.AspNetCore.Html
	//	.IHtmlContent Ub_DisplayRowNumberWithTd
	//	(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, long? value)
	//{
	//	var td =
	//		new Microsoft.AspNetCore.Mvc
	//		.Rendering.TagBuilder(tagName: "td");

	//	td.AddCssClass(value: "text-center");

	//	var innerHtml =
	//		Ub_DisplayInteger(html: html, value: value);

	//	td.InnerHtml.AppendHtml(content: innerHtml);

	//	return td;
	//}

	public static Microsoft.AspNetCore.Html.IHtmlContent Ub_DisplayStringWithTd
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, string? value)
	{
		var td =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "td");

		td.AddCssClass("align-middle text-center");

		//var innerHtml =
		//	Ub_DisplayBoolean(html: html, value: value);

		td.InnerHtml.Append(value!);

		return td;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent Ub_DisplayStringWithTh
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, string? value)
	{
		var td =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "th");

		td.AddCssClass("align-middle text-center");

		//var innerHtml =
		//	Ub_DisplayBoolean(html: html, value: value);

		td.InnerHtml.Append(value!);

		return td;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent Ub_DisplayBoolean
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, bool? value)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var div =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "div");

		div.AddCssClass("d-flex justify-content-center align-items-center");

		var input =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "input");

		input.AddCssClass("form-check-input m-0");

		input.Attributes.Add
			(key: "type", value: "checkbox");

		input.Attributes.Add
			(key: "disabled", value: "disabled");

		if (value.HasValue && value.Value)
		{
			input.Attributes.Add
				(key: "checked", value: "checked");
		}

		div.InnerHtml.AppendHtml(content: input);

		return div;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent Ub_DisplayBooleanWithTd
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, bool? value)
	{
		var td =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "td");

		td.AddCssClass("align-middle text-center");

		var innerHtml =
			Ub_DisplayBoolean(html: html, value: value);

		td.InnerHtml.AppendHtml(content: innerHtml);

		return td;
	}

	//public static Microsoft.AspNetCore.Html.IHtmlContent Ub_DisplayIntegerWithTd
	//	(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, long? value)
	//{
	//	var td =
	//		new Microsoft.AspNetCore.Mvc
	//		.Rendering.TagBuilder(tagName: "td");

	//	td.Attributes.Add
	//		(key: "dir", value: "ltr");

	//	var innerHtml =
	//		Ub_DisplayInteger(html: html, value: value);

	//	td.InnerHtml.AppendHtml(content: innerHtml);

	//	return td;
	//}

	//public static Microsoft.AspNetCore.Html.IHtmlContent Ub_DisplayDateTime
	//	(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, System.DateTime? value)
	//{
	//	if (value.HasValue == false)
	//	{
	//		return html.Raw(value: DefaultValue);
	//	}

	//	var result =
	//		value.Value.ToString
	//		(format: "yyyy/MM/dd - HH:mm:ss");

	//	result =
	//		Convert.DigitsToUnicode(value: result);

	//	return html.Raw(value: result);
	//}

	//public static Microsoft.AspNetCore.Html.IHtmlContent Ub_DisplayDateTimeWithTd
	//	(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, System.DateTime? value)
	//{
	//	var td =
	//		new Microsoft.AspNetCore.Mvc
	//		.Rendering.TagBuilder(tagName: "td");

	//	td.Attributes.Add
	//		(key: "dir", value: "ltr");

	//	var innerHtml =
	//		Ub_DisplayDateTime(html: html, value: value);

	//	td.InnerHtml.AppendHtml(content: innerHtml);

	//	return td;
	//}

	public static Microsoft.AspNetCore.Html.IHtmlContent Ub_GetLinkCaptionForList
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var icon =
			TagHelpers.Utility.GetIconList();

		var span =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "span");

		span.AddCssClass(value: "mx-1");

		span.InnerHtml.Append(unencoded: "[");
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.AppendHtml(content: icon);
		span.InnerHtml.Append(unencoded: Resources.ButtonCaptions.BackToList);
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.Append(unencoded: "]");

		return span;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent Ub_GetLinkCaptionForDetails
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var icon =
			TagHelpers.Utility.GetIconDetails();

		var span =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "span");

		span.AddCssClass(value: "mx-1");

		span.InnerHtml.Append(unencoded: "[");
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.AppendHtml(content: icon);
		span.InnerHtml.Append(unencoded: Resources.ButtonCaptions.Details);
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.Append(unencoded: "]");

		return span;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent Ub_GetLinkCaptionForCreate
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var icon =
			TagHelpers.Utility.GetIconCreate();

		var span =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "span");

		//span.AddCssClass(value: "mx-1");

		//span.InnerHtml.Append(unencoded: "[");
		//span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.AppendHtml(content: icon);
		span.InnerHtml.Append(unencoded: Resources.ButtonCaptions.Create);
		//span.InnerHtml.Append(unencoded: " ");
		//span.InnerHtml.Append(unencoded: "]");

		return span;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent Ub_GetLinkCaptionForUpdate
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var icon =
			TagHelpers.Utility.GetIconUpdate();

		var span =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "span");

		span.AddCssClass(value: "mx-1");

		span.InnerHtml.Append(unencoded: "[");
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.AppendHtml(content: icon);
		span.InnerHtml.Append(unencoded: Resources.ButtonCaptions.Update);
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.Append(unencoded: "]");

		return span;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent Ub_GetLinkCaptionForDelete
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var icon =
			TagHelpers.Utility.GetIconDelete();

		var span =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "span");

		span.AddCssClass(value: "mx-1");

		span.InnerHtml.Append(unencoded: "[");
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.AppendHtml(content: icon);
		span.InnerHtml.Append(unencoded: Resources.ButtonCaptions.Delete);
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.Append(unencoded: "]");

		return span;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent Ub_GetIconDetails
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var icon =
			TagHelpers.Utility.GetIconDetails();

		return icon;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent Ub_GetIconCreate
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var icon =
			TagHelpers.Utility.GetIconCreate();

		return icon;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatGetIconUpdate
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var icon =
			TagHelpers.Utility.GetIconUpdate();

		return icon;
	}

	public static Microsoft.AspNetCore.Html.IHtmlContent DtatGetIconDelete
		(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html)
	{
		if (html is null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(html));
		}

		var icon =
			TagHelpers.Utility.GetIconDelete();

		return icon;
	}

	//------------------ Custom's

	//public static Microsoft.AspNetCore.Html.IHtmlContent TextClickTo(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, string action, string iconClass, string to)
	//{
	//	if (html is null)
	//	{
	//		throw new System
	//			.ArgumentNullException(paramName: nameof(html));
	//	}

	//	var p =
	//		new Microsoft.AspNetCore.Mvc
	//		.Rendering.TagBuilder(tagName: "p");

	//	p.AddCssClass("d-flex align-items-center fw-light");

	//	var i = $"<i class=\"flex-center border rounded fs-4 mx-2 p-1 {iconClass}\"></i>";

	//	var format = Resources.Messages.Information.ClickToThe;

	//	p.InnerHtml.AppendHtml(string.Format(format, i, action, to));

	//	return p;
	//}
}