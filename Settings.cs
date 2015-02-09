using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Wox.Plugin.AxLauncher
{
    /// <summary>
    ///  Store object for the settings.
    /// </summary>
    public class Settings
    {
        public string axcPath { get; set; }
        public string settingsPath { get; set; }

        /// <summary>
        ///  Save this settings to the settings.json file.
        /// </summary>
        public void saveSettings()
        {
            try
            {
                System.IO.File.WriteAllText(this.settingsPath, JsonConvert.SerializeObject(this, Formatting.Indented));
            }
            catch
            {
                throw new Exception("Saving settings.json failed.");
            }
        }
    }
}
