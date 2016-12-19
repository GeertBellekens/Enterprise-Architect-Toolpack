
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EAMapping
{
	/// <summary>
	/// Description of MappingControl.
	/// </summary>
	public partial class MappingControl : UserControl
	{
		public MappingControl()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		private MappingFramework.MappingSet _mappingSet;
		public void loadMappingSet(MappingFramework.MappingSet mappingSet)
		{
			//temporary code to show mapping in a simple textbox.
			_mappingSet = mappingSet;
			this.tempTextBox.Clear();
			string mappingString = string.Empty;
			foreach (var mapping in mappingSet.mappings) 
			{
				mappingString += "From: " + mapping.source.fullMappingPath
				+ " To: " + mapping.target.fullMappingPath  + Environment.NewLine;
			}
			this.tempTextBox.Text = mappingString;
		}
	}
}
