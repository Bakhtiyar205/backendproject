using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Utilities.Helpers
{
    public static class Helper
    {
        public static string GetFilePath(string root, string folder, string fileName)
        {
            return Path.Combine(root, folder, fileName);
        }

        public static void DeleteFile(string path)
        {
            if (!File.Exists(path))
            {
                File.Delete(path);
            }
        }

        internal static string GetFilePath(object webRootPath, string v, object image)
        {
            throw new NotImplementedException();
        }

        public enum UserRoles
        {
            Admin,
            Member
        }
    }

}
