using Entities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Services.Contracts;

namespace StoreApp.Infrastructure.TagHelpers
{
    [HtmlTargetElement("div", Attributes ="products")]
    public class LatestProductTagHelper:TagHelper
    {

        private readonly IServiceManager _manager;
       [HtmlAttributeName("number")]
        public int Number { get; set; }

        public LatestProductTagHelper(IServiceManager manager) //our source of data, we used to get the latestproducts from repo
        {
            _manager = manager;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output) 
        {
            TagBuilder div = new TagBuilder("div");
            div.Attributes.Add("class", "my-3");

            TagBuilder h6 = new TagBuilder("h6");
            h6.Attributes.Add("class", "lead");

            TagBuilder icon = new TagBuilder("i");
            icon.Attributes.Add("class", "fa fa-box text-secondary");

            h6.InnerHtml.AppendHtml(icon); //insert icon into h6 tag
            h6.InnerHtml.AppendHtml(" Latest Products"); //insert the text into h6 tag

            TagBuilder ul = new TagBuilder("ul");
            var products = _manager.ProductService.GetLatestProducts(Number, false);
            foreach (Product product in products) //the link for the products using the method above
            {
                TagBuilder li = new TagBuilder("li"); //list item
                TagBuilder a = new TagBuilder("a"); //for link
                a.Attributes.Add("href",$"/product/get/{product.ProductId}");

                a.InnerHtml.AppendHtml(product.ProductName); //insert the productname in a tag

                li.InnerHtml.AppendHtml(a); // insert the li inside a tag
                ul.InnerHtml.AppendHtml(li); //insert li into ul tag

            }


            div.InnerHtml.AppendHtml(h6); //we add h6 to div
            div.InnerHtml.AppendHtml(ul); //we add ul to div
            output.Content.AppendHtml(div); //we add the div to the output

        }
        

    }
}

//this line down was in footer. We moved it to LatestProductTagHelper.cs file. so we can delete it from _Footer.cshtml.


/*
<div class="my-3">
                    <h6 class="lead">
                        <i class="fa fa-box text-secondary"></i>
                        Latest Products
                    </h6>
                        <ul>
                            <li><a href="#">Keyboard</a></li>
                            <li><a href="#">paint</a></li>
                            <li><a href="#">book</a></li>
                            <li><a href="#">flower</a></li>
                            <li><a href="#">water</a></li>
                        </ul>
                </div>
                
*/