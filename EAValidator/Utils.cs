using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Schema;

namespace EAValidator
{
    class Utils
    {
        public static string selectDirectory(string path)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.ShowNewFolderButton = true;
                fbd.SelectedPath = path;
                DialogResult result = fbd.ShowDialog();
                return fbd.SelectedPath;
            }
        }

        public static bool FileOrDirectoryExists(string name)
        {
            // Check if the File or Directory exists
            return (Directory.Exists(name) || File.Exists(name));
        }

        public static string[] getSubdirectoriesForDirectory(string dir)
        {
            // Get the file names (incl. path) of the SQL files inside a given directory
            string[] subdirectories = Directory.GetDirectories(dir);
            return subdirectories;
        }

        public static string[] getFilesFromDirectory(string dir, string extension)
        {
            // Get the file names (incl. path) of the files inside a given directory
            string documenttype = "*." + extension;
            string[] Files = Directory.GetFiles(dir, documenttype);  
            return Files;
        }

        public static string ReplaceXMLCharsInQuery(string xmltext)
        {
            // Replace characters needed in sql-query but could not be added directly in XML (i.e.: <, >, ", ', &)
            xmltext = xmltext.Replace("&lt;", "<");
            xmltext = xmltext.Replace("&gt;", ">");
            xmltext = xmltext.Replace("&apos;", "'");
            xmltext = xmltext.Replace("&amp;", "&");

            return xmltext;
        }


    }
}
