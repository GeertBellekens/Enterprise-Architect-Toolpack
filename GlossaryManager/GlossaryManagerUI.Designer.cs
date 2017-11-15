using System;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

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

		internal ComponentResourceManager resources;
		
		private void InitializeComponent() {
      this.resources = new ComponentResourceManager(typeof(GlossaryManagerUI));

			this.SuspendLayout();

			// MappingControlGUI

			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
			this.Name                = "GlossaryManagerUI";
			this.Size                = new System.Drawing.Size(994, 584);

			this.ResumeLayout(false);
		}

    public BusinessItemTabPage BusinessItems { get; private set; }
    public DataItemTabPage     DataItems     { get; private set; }
    public ColumnLinkTabPage   ColumnLinks   { get; private set; }

    public void Activate() {
      this.createTabControl();
    }

    private void createTabControl() {
			TabControl tabs = new TabControl () {
        Alignment     = TabAlignment.Top,
        Dock          = DockStyle.Fill,
			  Appearance    = TabAppearance.FlatButtons,
        SelectedIndex = 0,
        Padding       = new System.Drawing.Point(15, 7)
      };
      
      tabs.SelectedIndexChanged += new EventHandler(this.handleTabSelection);

			this.BusinessItems = new BusinessItemTabPage(this);
      tabs.Controls.Add( this.BusinessItems );

      this.DataItems = new DataItemTabPage(this);
			tabs.Controls.Add( this.DataItems );

      this.ColumnLinks = new ColumnLinkTabPage(this);
      tabs.Controls.Add( this.ColumnLinks );

			this.Controls.Add(tabs);

      // manually trigger focus on first tab
      this.BusinessItems.HandleFocus();
    }

    private void handleTabSelection(object sender, EventArgs e) {
      TabControl tabs = sender as TabControl;
      // TODO: seems to fail when you first select ColumnLinks, it then sends
      //       HandleFocus to ColumnLinks when selecting DataItems ?!
      //       All indices seem alright. ?!?!?!
      // ((FocusableTabPage)tabs.Controls[tabs.SelectedIndex]).HandleFocus();
      // temp (?!) solution:
      switch(tabs.SelectedIndex) {
        case 0: this.BusinessItems.HandleFocus(); break;
        case 1: this.DataItems.HandleFocus();     break;
        case 2: this.ColumnLinks.HandleFocus();   break;        
      }
    }

  }
}
