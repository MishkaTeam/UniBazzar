﻿namespace Infrastructure.TagHelpers;

[Microsoft.AspNetCore.Razor.TagHelpers
	.HtmlTargetElement(tag: Constants.TagHelper.FullSelect,
	TagStructure = Microsoft.AspNetCore.Razor.TagHelpers.TagStructure.WithoutEndTag)]
public class FullSelectTagHelper :
	Microsoft.AspNetCore.Razor.TagHelpers.TagHelper

{
	public FullSelectTagHelper
		(Microsoft.AspNetCore.Mvc.ViewFeatures.IHtmlGenerator generator) : base()
	{
		Generator = generator;
	}

	private Microsoft.AspNetCore.Mvc.ViewFeatures.IHtmlGenerator Generator { get; }

	[Microsoft.AspNetCore.Mvc.ViewFeatures.ViewContext]
	[Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeNotBound]
	public Microsoft.AspNetCore.Mvc.Rendering.ViewContext? ViewContext { get; set; }

	[Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName(name: "asp-for")]
	public Microsoft.AspNetCore.Mvc.ViewFeatures.ModelExpression? For { get; set; }

	[Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName(name: "asp-items")]
	public System.Collections.Generic.IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>? Items { get; set; }

	[Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName(name: "asp-option-label")]
	public string? OptionLabel	{ get; set; }

	public override async System.Threading.Tasks.Task ProcessAsync
		(Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext context,
		Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput output)
	{
		if (For is null)
		{
			throw new System.Exception
				(message: $"'{nameof(For)}' property is null ");
		}

		if (Items is null)
		{
			throw new System.Exception
				(message: $"'{nameof(Items)}' property is null ");
		}

		if (ViewContext is null)
		{
			throw new System.Exception
				(message: $"'{nameof(ViewContext)}' property is null ");
		}

		// **************************************************
		var div =
			new Microsoft.AspNetCore.Mvc
			.Rendering.TagBuilder(tagName: "div");

		div.AddCssClass(value: "mb-3");
		// **************************************************

		// **************************************************
		var label =
			await
			Utility.GenerateLabelAsync
			(generator: Generator, viewContext: ViewContext, @for: For);

		div.InnerHtml.AppendHtml(encoded: label);
		// **************************************************

		// **************************************************
		var select =
			await
			Utility.GenerateSelectAsync
			(generator: Generator, viewContext: ViewContext, @for: For, selectList: Items);

		div.InnerHtml.AppendHtml(encoded: select);
		// **************************************************

		// **************************************************
		var validationMessage =
			await
			Utility.GenerateValidationMessageAsync
			(generator: Generator, viewContext: ViewContext, @for: For);

		div.InnerHtml.AppendHtml(encoded: validationMessage);
		// **************************************************

		// **************************************************
		output.TagName = null;

		output.TagMode =
			Microsoft.AspNetCore.Razor
			.TagHelpers.TagMode.StartTagAndEndTag;

		output.Content.SetHtmlContent(htmlContent: div);
		// **************************************************
	}
}
