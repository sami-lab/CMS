#pragma checksum "D:\Files and Softwares\ass\backend\WebBuilder\Views\Shared\Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "35a1e378c60b18ab274bd18a5308041cf3b163e1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Error), @"mvc.1.0.view", @"/Views/Shared/Error.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Error.cshtml", typeof(AspNetCore.Views_Shared_Error))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"35a1e378c60b18ab274bd18a5308041cf3b163e1", @"/Views/Shared/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0434ad848ef2c3f9023e0add64a2b6aad9b548a2", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(38, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "D:\Files and Softwares\ass\backend\WebBuilder\Views\Shared\Error.cshtml"
 if (ViewBag.ErrorTitle == null)
{

    string email = null;
    email = Configuration["Email"] != null ? @Configuration["Email"] : "85575687@qq.com";

    

#line default
#line hidden
            BeginContext(202, 136, true);
            WriteLiteral("<h3>\r\n        An occured while processing your request. The support\r\n        team is notified and we are working on the fix\r\n    </h3>\r\n");
            EndContext();
            BeginContext(340, 29, true);
            WriteLiteral("    <h5>Please contact us on ");
            EndContext();
            BeginContext(370, 5, false);
#line 14 "D:\Files and Softwares\ass\backend\WebBuilder\Views\Shared\Error.cshtml"
                        Write(email);

#line default
#line hidden
            EndContext();
            BeginContext(375, 19, true);
            WriteLiteral("</h5>\r\n    <hr />\r\n");
            EndContext();
#line 16 "D:\Files and Softwares\ass\backend\WebBuilder\Views\Shared\Error.cshtml"
}
else
{

#line default
#line hidden
            BeginContext(406, 28, true);
            WriteLiteral("    <h1 class=\"text-danger\">");
            EndContext();
            BeginContext(435, 18, false);
#line 19 "D:\Files and Softwares\ass\backend\WebBuilder\Views\Shared\Error.cshtml"
                       Write(ViewBag.ErrorTitle);

#line default
#line hidden
            EndContext();
            BeginContext(453, 35, true);
            WriteLiteral("</h1>\r\n    <h6 class=\"text-danger\">");
            EndContext();
            BeginContext(489, 20, false);
#line 20 "D:\Files and Softwares\ass\backend\WebBuilder\Views\Shared\Error.cshtml"
                       Write(ViewBag.ErrorMessage);

#line default
#line hidden
            EndContext();
            BeginContext(509, 7, true);
            WriteLiteral("</h6>\r\n");
            EndContext();
#line 21 "D:\Files and Softwares\ass\backend\WebBuilder\Views\Shared\Error.cshtml"
}

#line default
#line hidden
            BeginContext(519, 6, true);
            WriteLiteral("\r\n\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IConfiguration Configuration { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591