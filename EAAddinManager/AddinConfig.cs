/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 3/05/2015
 * Time: 7:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace EAAddinManager
{
	/// <summary>
	/// Description of AddinConfig.
	/// </summary>
	public class AddinConfig
	{
		public string name {
			get
			{
				return new DirectoryInfo(this.directory).Name;
			}
		}
		private string _directory;
		public string directory 
		{get
			{
				return _directory;
			}
			set
			{
				_directory = value;
				this.resolveDllPath();
			}
		}
        public string version {get;set;}
        public bool load {get;set;}
        public string dllPath {get;set;}
		public AddinConfig(ConnectionStringSettings  connectionString )
		{
			this.directory = connectionString.Name;
			string[] conStrSplitted = connectionString.ConnectionString.Split(';');
			this.dllPath = conStrSplitted[0];
			this.load = bool.Parse(conStrSplitted[1]);
			this.version = conStrSplitted[2];
		}
		public AddinConfig(string filePath)
		{
			this.directory = Path.GetDirectoryName(filePath);
			this.dllPath = Path.GetFileName(filePath);
			this.version = FileVersionInfo.GetVersionInfo(filePath).FileVersion;
			this.load = true;
		}
		internal ConnectionStringSettings getConnectionString()
		{
			ConnectionStringSettings connectionString = new ConnectionStringSettings();
			connectionString.ConnectionString = string.Join(";", new string[]{this.dllPath, this.load.ToString(), this.version});
			connectionString.Name = this.directory;
			connectionString.ProviderName = "EA Addin Manager";
			return connectionString;
		}
		private void resolveDllPath()
		{
			if (!string.IsNullOrEmpty(this.dllPath))
			{
				//if the file doesn't exist then find recursively in the current directory
				if (!File.Exists(this.directory + "\\" +this.dllPath))
				{
					string dllFullPath = this.findFile(this.directory,Path.GetFileName(this.dllPath));
					//get the subpath for the dllpath
					dllPath = dllFullPath.Replace(this.directory, string.Empty);
				}
			}
		}
		private string findFile(string directory, string filename)
		{
			//first check files in this directory
			foreach (string file in Directory.GetFiles(directory, filename)) 
			{
				return file;
			}
			//not found so we search subdirectories
			foreach (string  subdirectory in Directory.GetDirectories(directory)) 
			{
				string file = findFile(subdirectory, filename);
				if (file != string.Empty)
				{
					return file;
				}
			}
			//still not found, return empty string
			return string.Empty;
		}
	}

}
