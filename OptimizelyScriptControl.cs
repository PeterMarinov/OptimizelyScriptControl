using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using OptimizelyScriptControl.Designer;

namespace OptimizelyScriptControl
{
    [ControlDesigner(typeof(OptimizelyScriptControlDesigner))]
    public class OptimizelyScriptControl : SimpleView
    {
        #region Properties

        public string OptimizelyCodeUrl
        {
            get
            {
                return this.optimizelyCodeUrl;
            }

            set
            {
                this.optimizelyCodeUrl = value;
            }
        }

        private bool IsEdit
        {
            get
            {
                var isEdit = false;
                if (this.IsDesignMode() && !this.IsPreviewMode())
                {
                    isEdit = true;
                }

                return isEdit;
            }
        }

        public override string LayoutTemplatePath
        {
            get
            {
                return OptimizelyScriptControl.layoutTemplatePath;
            }
        }
        #endregion

        #region Methods

        protected override void InitializeControls(GenericContainer container)
        {
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (this.Page != null && !this.IsEdit)
            {
                if (!this.OptimizelyCodeUrl.IsNullOrEmpty())
                {
                    HtmlGenericControl js = new HtmlGenericControl("script");
                    js.Attributes["type"] = "text/javascript";
                    js.InnerHtml = string.Format(this.optimizelyInitializingScript, this.OptimizelyCodeUrl);
                    Page.Header.Controls.Add(js);
                }
            }
        }

        #endregion

        #region Private fields

        private string optimizelyCodeUrl;
        private string optimizelyInitializingScript = @"(function () {{
        var optm = document.createElement('script'); optm.type = 'text/javascript';
        optm.src = '{0}';
        var h = document.getElementsByTagName('head')[0]; h.insertBefore(optm, h.children[0]);
        }})();";

        public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.PublicControls.FileEmbedControl.ascx");

        #endregion
    }
}
