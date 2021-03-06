﻿using System;
using System.Linq;
using Data.Models;
using SaveScumAgent.Properties;
using SaveScumAgent.UtilityClasses;

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
                var d = db.GlobalSettings.First();
            }
        }

        private void Initialize()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory",
                Settings.Default.SaveScumAppDataDirectory.FormatWith(SpecialFolderHelper.PathsDictionary));
            SaveScumInitializer.SetupDataDirectory();
            InitializeComponent();
        }

        private void BtnNewGame_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}