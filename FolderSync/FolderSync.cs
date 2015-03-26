using System;
using System.Collections.Generic;
using System.Data.HashFunction;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace FolderSync
{
    public class FolderSync
    {

        public HashAlgorithmWrapper Hasher { get; set; }

        public FolderSync(HashAlgorithm hasher)
        {
            //Hasher = hasher;
            
            var x = new xxHash();
            

        }

        public FolderSync() : this(MD5.Create())
        {
            
        }
    }
}
