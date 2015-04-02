using System;
using System.Reflection;
using System.Runtime.Remoting;
using Ionic.Zip;
using SaveScumAgent.Archiver.Formats;
using SevenZip;

namespace SaveScumAgent.Archiver.Tests
{

    public class MockZipFile : IZipFile
    {
        private string _archiveName;
        
        private SaveProgressEventArgs NewSaveProgressEventArgs(ZipProgressEventType eventType)
        {
            var obj = (SaveProgressEventArgs) typeof (SaveProgressEventArgs).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null, Type.EmptyTypes, null).Invoke(new object[] {_archiveName, eventType });

            return obj;
        }
        private ZipErrorEventArgs NewZipErrorEventArgs(ZipEntry entry, Exception e)
        {
            var obj = (ZipErrorEventArgs)typeof(ZipErrorEventArgs).GetConstructor(
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
            SaveProgress?.Invoke(this, NewSaveProgressEventArgs(ZipProgressEventType.Saving_Started));
        }

        public void OnSaveCompleted()
        {
            SaveProgress?.Invoke(this, NewSaveProgressEventArgs(ZipProgressEventType.Saving_Completed));
        }

        public void OnSaveWriteEntry()
        {
            SaveProgress?.Invoke(this, NewSaveProgressEventArgs(ZipProgressEventType.Saving_AfterWriteEntry));
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
        }

        public event EventHandler<SaveProgressEventArgs> SaveProgress;
        public event EventHandler<ZipErrorEventArgs> ZipError;
    }

}
