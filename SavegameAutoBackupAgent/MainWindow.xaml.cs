using System;
using System.Configuration;
using System.IO;
using SaveScumAgent.Archiver;
using SaveScumAgent.Models;

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
            using (var db = new SaveScumContext())
            {
                //
            }

        }
    }
}