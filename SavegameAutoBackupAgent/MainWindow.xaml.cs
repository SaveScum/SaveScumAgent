using System;
using System.Configuration;
using System.Environment;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SaveScumAgent
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            Initialize();
            /*using (var db = new SaveScumContext())
            {
                var d = db.DefaultGameSettings;
            }*/
        }

        private void Initialize()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", GetFolderPath(SpecialFolder.ApplicationData));
            SaveScumInitializer.SetupDataDirectory();
            InitializeComponent();
        }

        
    }
}