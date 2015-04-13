using System.ComponentModel;

namespace Data.Models
{
    public class GlobalSettings : GameSettings
    {
        [DefaultValue(false)]
        public bool IsGlobal { get; internal set; }

        public GlobalSettings()
        {
            IsGlobal = true;
        }
    }
}