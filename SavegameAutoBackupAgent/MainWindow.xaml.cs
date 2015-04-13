using System;
using System.Configuration;
using System.Environment;
using System.IO;
using System.Linq;
using System.Reflection;
using Data.Models;

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
           using (var db = new SaveScumContext())
            {
                //var d = db.DefaultSettings;
            }
        }

        private void Initialize()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(GetFolderPath(SpecialFolder.ApplicationData), "SaveScum"));
            SaveScumInitializer.SetupDataDirectory();
            InitializeComponent();
        }

        
    }
}