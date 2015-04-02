﻿using System;
using System.Configuration;
using System.IO;
using SaveScumAgent.Archiver;

namespace SaveScumAgent
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            SevenZipDllAssemblyLoader.Instance.Load();
        }
    }
}