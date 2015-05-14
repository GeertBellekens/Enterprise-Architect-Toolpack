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
		public string name {get;set;}
        public string version {get;set;}
        public bool load {get;set;}
        public string dllPath {get;set;}
		public AddinConfig(ConnectionStringSettings  connectionString )
		{
			this.name = connectionString.Name;
			string[] conStrSplitted = connectionString.ConnectionString.Split(';');
			this.dllPath = conStrSplitted[0];
			this.load = bool.Parse(conStrSplitted[1]);
			this.version = conStrSplitted[2];
		}
		public AddinConfig(string filePath)
		{
			this.name = new FileInfo(filePath).Directory.Name;
			this.dllPath = Path.GetFileName(filePath);
			this.version = FileVersionInfo.GetVersionInfo(filePath).FileVersion;
			this.load = true;
		}
	}

}
