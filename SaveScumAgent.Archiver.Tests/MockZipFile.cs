using System;
using System.Reflection;
using System.Runtime.Remoting;
using Ionic.Zip;
using SaveScumAgent.Archiver.Formats;
using SaveScumAgent.UtilityClasses;
using SevenZip;

namespace SaveScumAgent.Archiver.Tests
{

    public class MockZipFile : IZipFile
    {
        private string _archiveName;
        public bool Aborted { get; private set; }

        public PathString ArchiveName => _archiveName;

        private SaveProgressEventArgs NewSaveProgressEventArgs(ZipProgressEventType eventType)
        {
            var bf = BindingFlags.NonPublic | BindingFlags.Instance;
            var types = new[] {typeof(string), typeof(ZipProgressEventType) };
            var obj = (SaveProgressEventArgs) typeof (SaveProgressEventArgs).GetConstructor(bf,
                null, types, null).Invoke(new object[] {_archiveName, eventType });
            return obj;
        }
        private ZipErrorEventArgs NewZipErrorEventArgs(ZipEntry entry, Exception e)
        {var obj = (ZipErrorEventArgs)typeof(ZipErrorEventArgs).GetConstructor(
                  BindingFlags.NonPublic | BindingFlags.Instance,
                  null, Type.EmptyTypes, null).Invoke(new object[] { _archiveName, entry, e });
            obj.ArchiveName = _archiveName;
            return obj;
        }


        public void OnCompressionFinished()
        {
            SaveProgress?.Invoke(this, NewSaveProgressEventArgs(ZipProgressEventType.Saving_Completed));
        }
        

        public ZipEntry AddDirectory(string directoryName)
        {
            return new ZipEntry();
        }

        public ZipEntry AddDirectory(string directoryName, string directoryPathInArchive)
        {
            return new ZipEntry();
        }

        public void Dispose()
        {
            //
        }

        public void OnSaveStarted()
        {
            var e = NewSaveProgressEventArgs(ZipProgressEventType.Saving_Started);
            SaveProgress?.Invoke(this, e);
            Aborted = Aborted || (e.Cancel);

        }

        public void OnSaveCompleted()
        {
            var e = NewSaveProgressEventArgs(ZipProgressEventType.Saving_Completed);
            SaveProgress?.Invoke(this, e);
            Aborted = Aborted || (e.Cancel);
        }

        public void OnSaveWriteEntry()
        {
            var e = NewSaveProgressEventArgs(ZipProgressEventType.Saving_AfterWriteEntry);
            SaveProgress?.Invoke(this, e);
            Aborted = Aborted || (e.Cancel);
        }

        public void OnZipErrorSaving(ZipEntry entry, Exception exc)
        {
            ZipError?.Invoke(this, NewZipErrorEventArgs(entry, exc));
        }

        public void Save()
        {
            OnSaveStarted();
        }

        public void Save(string fileName)
        {
            _archiveName = fileName;
            Save();
        }

        public event EventHandler<SaveProgressEventArgs> SaveProgress;
        public event EventHandler<ZipErrorEventArgs> ZipError;
    }

}
