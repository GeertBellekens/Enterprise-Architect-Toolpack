using System.ComponentModel;

namespace GlossaryManager {

	partial class GlossaryManagerUI	{

		private IContainer components = null;
		
		protected override void Dispose(bool disposing)	{
			if( disposing ) {
				if( components != null ) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		private void InitializeComponent() {
			ComponentResourceManager resources =
        new ComponentResourceManager(typeof(GlossaryManagerUI));

			this.SuspendLayout();

			// MappingControlGUI

			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "GlossaryManagerUI";
			this.Size = new System.Drawing.Size(994, 584);

			this.ResumeLayout(false);

		}
	}
}
