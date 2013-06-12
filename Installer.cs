using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages.Configuration;

namespace OptimizelyScriptControl
{
    public class Installer
    {
        /// <summary>
        /// This is the actual method that is called by ASP.NET even before application start. Sweet!
        /// </summary>
        public static void PreApplicationStart()
        {
            // With this method we subscribe for the Sitefinity Bootstrapper_Initialized event, which is fired after initialization of the Sitefinity application
            Bootstrapper.Initialized += (new EventHandler<ExecutedEventArgs>(Installer.Bootstrapper_Initialized));
        }

        /// <summary>
        /// Use this method add logic on bootstrapper intialized event
        /// </summary>
        /// <param name="sender">The Sitefinity App</param>
        /// <param name="e">Event</param>
        private static void Bootstrapper_Initialized(object sender, ExecutedEventArgs e)
        {
            if (e.CommandName != "RegisterRoutes" || !Bootstrapper.IsDataInitialized)
            {
                return;
            }

            InstallWidget();
        }

        /// <summary>
        /// Install the widget itself
        /// </summary>
        private static void InstallWidget()
        {
            // get the configManager
            var configManager = Config.GetManager();
            var config = configManager.GetSection<ToolboxesConfig>();

            var controls = config.Toolboxes["PageControls"];
            var sectionName = "ScriptsAndStylesControlsSection";
            var section = controls.Sections.Where<ToolboxSection>(e => e.Name == sectionName).FirstOrDefault();

            if (section == null)
            {
                section = new ToolboxSection(controls.Sections)
                {
                    Name = sectionName,
                    Title = sectionName,
                    Description = sectionName,
                };
                controls.Sections.Add(section);
            }

            var controlName = "OptimizelyScriptControl";
            var controlTitle = "Optimizely Script";
            var controlType = typeof(OptimizelyScriptControl);
            if (!section.Tools.Any<ToolboxItem>(e => e.Name == controlName))
            {
                var tool = new ToolboxItem(section.Tools)
                {
                    Name = controlName,
                    Title = controlTitle,
                    Description = controlName,
                    ControlType = controlType.AssemblyQualifiedName,
                    CssClass = "sfLanguageSelectorIcn"
                };
                section.Tools.Add(tool);
            }

            configManager.SaveSection(config);
        }
    }
}
