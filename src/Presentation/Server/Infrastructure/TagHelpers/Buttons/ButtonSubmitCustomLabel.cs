namespace Infrastructure.TagHelpers.Buttons;

[Microsoft.AspNetCore.Razor.TagHelpers.HtmlTargetElement
    (tag: "uni-button",
    ParentTag = "section-form-buttons",
    TagStructure = Microsoft.AspNetCore.Razor.TagHelpers.TagStructure.WithoutEndTag)]
public class ButtonSubmitCustomLabel :
    Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
{
    public string Label { get; set; }
    public ButtonSubmitCustomLabel() : base()
    {
    }

    public override void Process
        (Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext context,
        Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput output)
    {
        // **************************************************
        var icon =
            Utility.GetIconUpdate();
        // **************************************************

        // **************************************************
        var body =
            new Microsoft.AspNetCore.Mvc
            .Rendering.TagBuilder(tagName: "button");

        body.Attributes.Add
            (key: "type", value: "submit");

        body.AddCssClass(value: "btn");
        body.AddCssClass(value: "btn-primary");

        body.InnerHtml.AppendHtml(content: icon);
        body.InnerHtml.Append(unencoded: Label);
        // **************************************************

        // **************************************************
        output.TagName = null;

        output.TagMode =
            Microsoft.AspNetCore.Razor
            .TagHelpers.TagMode.StartTagAndEndTag;

        output.Content.SetHtmlContent(htmlContent: body);
        // **************************************************
    }
}
