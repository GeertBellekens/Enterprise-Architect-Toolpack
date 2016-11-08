
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
			_mappingSet = mappingSet;
			//TODO: show the mapping
		}
	}
}
