using System.ComponentModel;

namespace Data.Models
{
    public class GlobalSettings : GameSettings
    {
        public GlobalSettings()
        {
            IsGlobal = true;
        }

        [DefaultValue(false)]
        public bool IsGlobal { get; internal set; }
    }
}