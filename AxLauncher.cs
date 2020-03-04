using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Management;
using System.IO;
using Newtonsoft.Json;

namespace Wox.Plugin.AxLauncher
{
    /// <summary>
    ///  Launcher class for axc files.
    /// </summary>
    public class AxLauncher : IPlugin, ISettingProvider
    {
        /// <summary>
        ///  Settings store object.
        /// </summary>
        public static Settings settings;

        /// <summary>
        ///  Process the query.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<Result> Query(Query query)
        {
            string[] axcFiles;
            string searchPattern;
            List<Result> results = new List<Result>();

            // check if search parameter is given
            if (query.ActionParameters.Count == 0)
            {
                // no search parameter
                // show all .axc files
                searchPattern = "*.axc?";
            }
            else
            {
                // search for files *parameters*.axc
                searchPattern = "*";
                foreach (string parameter in query.ActionParameters)
                {
                    searchPattern += parameter + "*";
                }
                searchPattern += ".axc?";
            }

            if (String.Empty.Equals(settings.axcPath.Trim()))
            {
                axcFiles = new string[] { "Define the axc path in settings" };
            }
            else
            {
                axcFiles = Directory.GetFiles(settings.axcPath, searchPattern, SearchOption.AllDirectories);
            }

            foreach (string axcFile in axcFiles)
            {
                results.Add(new Result()
                {
                    Title = axcFile.Substring(axcFile.LastIndexOf("\\") + 1),
                    SubTitle = axcFile,
                    IcoPath = "Images\\" + GetExtensionWithoutPeriod(axcFile) + ".png",
                    Action = e =>
                    {
                        try
                        {
                            ProcessStartInfo startInfo = new ProcessStartInfo();
                            startInfo.FileName = axcFile;
                            Process.Start(startInfo);
                        }
                        catch
                        {
                            return false;
                        }

                        return true;
                    }
                });
            }
            return results;
        }

        private string GetExtensionWithoutPeriod(string path)
        {
            return Path.GetExtension(path).Substring(1);
        }

        /// <summary>
        ///  Init method. Loads the settings from the settings.json file.
        /// </summary>
        /// <param name="context"></param>
        public void Init(PluginInitContext context)
        {
            string settingsPath = System.IO.Path.Combine(context.CurrentPluginMetadata.PluginDirectory, "settings.json");
            try
            {
                settings = JsonConvert.DeserializeObject<Settings>(System.IO.File.ReadAllText(settingsPath));
            }
            catch
            {
                settings = new Settings();
                settings.settingsPath = settingsPath;
            }
        }

        /// <summary>
        ///  Create the settings panel for ax launcher.
        /// </summary>
        public System.Windows.Controls.Control CreateSettingPanel()
        {
            return new Setting();
        }

        /// <summary>
        ///  Save the settings object into the settings.json file.
        /// </summary>
        public static void saveSettings()
        {
            settings.saveSettings();
        }
    }
}
