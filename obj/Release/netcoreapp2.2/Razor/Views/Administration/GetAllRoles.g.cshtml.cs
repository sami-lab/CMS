#pragma checksum "D:\Files and Softwares\ass\backend\WebBuilder\Views\Administration\GetAllRoles.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "43b3874f9920516c1dafa45028e207ec54aee6ac"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Administration_GetAllRoles), @"mvc.1.0.view", @"/Views/Administration/GetAllRoles.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Administration/GetAllRoles.cshtml", typeof(AspNetCore.Views_Administration_GetAllRoles))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\Files and Softwares\ass\backend\WebBuilder\Views\_ViewImports.cshtml"
using WebBuilder;

#line default
#line hidden
#line 2 "D:\Files and Softwares\ass\backend\WebBuilder\Views\_ViewImports.cshtml"
using WebBuilder.Data;

#line default
#line hidden
#line 4 "D:\Files and Softwares\ass\backend\WebBuilder\Views\_ViewImports.cshtml"
using Microsoft.Extensions.Configuration;

#line default
#line hidden
#line 5 "D:\Files and Softwares\ass\backend\WebBuilder\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"43b3874f9920516c1dafa45028e207ec54aee6ac", @"/Views/Administration/GetAllRoles.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0434ad848ef2c3f9023e0add64a2b6aad9b548a2", @"/Views/_ViewImports.cshtml")]
    public class Views_Administration_GetAllRoles : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<IdentityRole>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(34, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "D:\Files and Softwares\ass\backend\WebBuilder\Views\Administration\GetAllRoles.cshtml"
  
    ViewBag.Title = "所有角色";

#line default
#line hidden
            BeginContext(72, 62, true);
            WriteLiteral("<div class=\"container\">\r\n    <h1>\r\n        所有角色\r\n    </h1>\r\n\r\n");
            EndContext();
#line 11 "D:\Files and Softwares\ass\backend\WebBuilder\Views\Administration\GetAllRoles.cshtml"
     if (Model.Any())
    {

        foreach (var role in Model)
        {

#line default
#line hidden
            BeginContext(214, 108, true);
            WriteLiteral("            <div class=\"card mb-3\">\r\n                <div class=\"card-header\">\r\n                    角色 Id : ");
            EndContext();
            BeginContext(323, 7, false);
#line 18 "D:\Files and Softwares\ass\backend\WebBuilder\Views\Administration\GetAllRoles.cshtml"
                       Write(role.Id);

#line default
#line hidden
            EndContext();
            BeginContext(330, 110, true);
            WriteLiteral("\r\n                </div>\r\n                <div class=\"card-body\">\r\n                    <h5 class=\"card-title\">");
            EndContext();
            BeginContext(441, 9, false);
#line 21 "D:\Files and Softwares\ass\backend\WebBuilder\Views\Administration\GetAllRoles.cshtml"
                                      Write(role.Name);

#line default
#line hidden
            EndContext();
            BeginContext(450, 51, true);
            WriteLiteral("</h5>\r\n                </div>\r\n            </div>\r\n");
            EndContext();
#line 24 "D:\Files and Softwares\ass\backend\WebBuilder\Views\Administration\GetAllRoles.cshtml"
        }
    }
    else
    {

#line default
#line hidden
            BeginContext(536, 131, true);
            WriteLiteral("        <div class=\"card\">\r\n            <div class=\"card-header\">\r\n                尚未创建任何角色\r\n\r\n            </div>\r\n        </div>\r\n");
            EndContext();
#line 34 "D:\Files and Softwares\ass\backend\WebBuilder\Views\Administration\GetAllRoles.cshtml"
    }

#line default
#line hidden
            BeginContext(672, 8, true);
            WriteLiteral("</div>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<IdentityRole>> Html { get; private set; }
    }
}
#pragma warning restore 1591
