using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using System.Web.UI.WebControls;

namespace OptimizelyScriptControl.Designer
{
    public class OptimizelyScriptControlDesigner : ControlDesignerBase
    {       
        protected override void InitializeControls(GenericContainer container)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            
            string css = "<link href=\"" + this.Page.ClientScript.GetWebResourceUrl(this.GetType(),
    "OptimizelyScriptControl.Resources.WidgetStyles.css") + "\" type=\"text/css\" rel=\"stylesheet\" />";

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "WidgetCssFile", css, false);
        }

        public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            var descriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
            var descriptor = (ScriptControlDescriptor)descriptors.Last();
            descriptor.AddComponentProperty("urlTextBox", this.UrlTextBox.ClientID);
            descriptor.AddElementProperty("errorLabel", this.ErrorLabel.ClientID);
            return descriptors;
        }

        /// <summary>
        /// The textx box used for managing the input of the widget designer
        /// </summary>
        public RadTextBox UrlTextBox
        {
            get
            {
                return this.Container.GetControl<RadTextBox>("UrlTextBox", true);
            }
        }

        /// <summary>
        /// The textx box used for managing the input of the widget designer
        /// </summary>
        public Label ErrorLabel
        {
            get
            {
                return this.Container.GetControl<Label>("ErrorLabel", true);
            }
        }

        /// <summary>
        /// This is the embedded control template for the custom Forms Control Designer.
        /// </summary>
        protected override string LayoutTemplateName
        {
            get
            {
                return OptimizelyScriptControlDesigner.layoutTemplateName;
            }
        }

        public override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
        {
            var res = new List<ScriptReference>(base.GetScriptReferences());
            var assemblyName = this.GetType().Assembly.GetName().ToString();
            res.Add(new ScriptReference("OptimizelyScriptControl.Designer.OptimizelyScriptControlDesigner.js", assemblyName));
            return res.ToArray();
        }

        private const string layoutTemplateName = "OptimizelyScriptControl.Designer.OptimizelyScriptControlDesigner.ascx";
    }
}
