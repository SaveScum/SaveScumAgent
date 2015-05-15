using System;
using System.IO;

namespace SaveScumAgent.UtilityClasses
{
    public class PathString
    {
        private readonly string _value;

        public PathString(string value)
        {
            _value = value;
        }

        public static bool IsFolderSubfolderOf(DirectoryInfo possibleSubDir, DirectoryInfo possibleParentDir)
        {
            if (possibleSubDir != null)
            {
                return possibleParentDir.FullName.Equals(possibleSubDir.FullName) ||
                       IsFolderSubfolderOf(possibleSubDir.Parent, possibleParentDir);
            }
            return false;
        }

        public string GetRelativePathFromAbsolute(PathString basePath)
        {
            if (!IsAbsoluteUrl())
                throw new InvalidOperationException("Must be an absolute path");

            if (!basePath.IsAbsoluteUrl())
                throw new ArgumentException("Must be an absolute path", "basePath");


            var fullUri = new Uri(this, UriKind.Absolute);
            var slashedPath = basePath.WithTrailingSlash();
            var baseUri = new Uri(slashedPath, UriKind.Absolute);
            return baseUri.MakeRelativeUri(fullUri).ToString().Replace("/", Path.DirectorySeparatorChar.ToString());
        }

        public string WithTrailingSlash()
        {
            var slash = Path.DirectorySeparatorChar.ToString();

            return !_value.EndsWith(slash) ? _value + slash : _value;
        }

        public string WithoutTrailingSlash()
        {
            var slash = Path.DirectorySeparatorChar.ToString();

            return _value.EndsWith(slash) ? (_value.TrimEnd(slash.ToCharArray())) : _value;
        }

        public bool IsFolderSubfolderOf(PathString possibleParentDir)
        {
            if (!Path.IsPathRooted(possibleParentDir))
                throw new ArgumentException("Must be an absolute path", "possibleParentDir");

            return !Path.IsPathRooted(this) ||
                   IsFolderSubfolderOf(new DirectoryInfo(this).Parent,
                       new DirectoryInfo(possibleParentDir.WithoutTrailingSlash()));
        }

        public string FullPath()
        {
            return IsAbsoluteUrl() ? Path.GetFullPath(_value) : null;
        }

        public bool IsAbsoluteUrl()
        {
            Uri result;
            return Uri.TryCreate(this, UriKind.Absolute, out result);
        }

        public override string ToString()
        {
            return _value;
        }

        public static implicit operator string(PathString d)
        {
            return d._value;
        }

        public static implicit operator PathString(string d)
        {
            return new PathString(d);
        }
    }
}