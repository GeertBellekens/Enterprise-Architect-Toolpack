
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UML=TSF.UmlToolingFramework.UML;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using EAAddinFramework.Utilities;

namespace TSF.UmlToolingFramework.EANavigator
{
    [Guid("29fe6e24-9486-4d55-ac16-0200c1b477f2")]
	[ComVisible(true)]
	/// <summary>
	/// Description of NavigatorControl.
	/// </summary>
	public partial class NavigatorControl : UserControl
	{
		private const string dummyName = "None";
		
		public bool working {get;set;}
		
		public string quickSearchText {get;set;}
		private int maxNodes = 50;
		private string quickSearchEmpty = "Quick Search";
		
		//the background worker and workqueue to be able to handle a ContextItemChanged event multithreaded
		private BackgroundWorker treeBackgroundWorker;
		private List<UML.Extended.UMLItem> workQueue = new List<UML.Extended.UMLItem>();
		private DateTime lastStartTime;
		
		//the backgroundworker for the quicksearch box
		private BackgroundWorker quickSearchBackgroundWorker;
		
		//the delegate stuff for the thread save Treenode.Insert
		private delegate void MethodDelegate(object nodeObject);
		private MethodDelegate invoker; 
		
		private NavigatorSettings _settings;
		public NavigatorSettings settings 
		{
			get
			{
				return _settings;
			}
			
			set
			{
				this._settings = value;
				this.setToolbarVisibility();
			}
		}
		
		public NavigatorControl()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//set the image List of the tree to be able to show the icons
			this.NavigatorTree.ImageList = NavigatorVisuals.getInstance().imageList;
			//initialisation for background worker
			resetTreeBackgroundWorker();
			//initialise quickSearch BackgroundWorker
			initQuickSearchBackgroundWorker();
			//set quicksearch empty
			this.setQuickSearchEmpty();
			//enable and disable
			enableDisableContexMenu(null);

		}
		private void initQuickSearchBackgroundWorker()
		{
			this.quickSearchBackgroundWorker = new BackgroundWorker();
			this.quickSearchBackgroundWorker.WorkerSupportsCancellation = true;
            this.quickSearchBackgroundWorker.DoWork += new DoWorkEventHandler(quickSearchBackground_DoWork);
            //backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            this.quickSearchBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(quickSearchBackgroundRunWorkerCompleted);
			
		}
		private void quickSearchBackground_DoWork(object sender, DoWorkEventArgs e)
        {
			//pass the search string 
			e.Result = e.Argument;
			//fire the search event
			quickSearchTextChanged(sender, e);
			
        }
		
		private void quickSearchBackgroundRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
			string searchedString =  e.Result as string;
			if (searchedString != this.quickSearchText)
			{
				//search again in case the text has already changed in the meantime
				this.handleSearchTextChange(e);
			}
		}
		
		private void resetTreeBackgroundWorker()
		{
			this.treeBackgroundWorker = new BackgroundWorker();
			this.treeBackgroundWorker.WorkerSupportsCancellation = true;
            this.treeBackgroundWorker.DoWork += new DoWorkEventHandler(treeBackground_DoWork);
            //backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            this.treeBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(treeBackgroundRunWorkerCompleted);
			
		}
		
		private void treeBackground_DoWork(object sender, DoWorkEventArgs e)
        {
			UML.Extended.UMLItem element = e.Argument as UML.Extended.UMLItem;
			BackgroundWorker worker = sender as BackgroundWorker;
			if (element != null)
			{
				//pass the new node to the runworkercompleted event
            	e.Result = this.makeElementNode(element,null);
			}
 			
        }
		
		/// <summary>
		/// inserts the given nodeObject as the first treenode in the NavigatorTree
		/// </summary>
		/// <param name="nodeObject"></param>
		private void ThreadSaveInsertNode(object nodeObject) 
		{
			//collapse all nodes before adding a new one
			this.NavigatorTree.CollapseAll();
			//then add the new one
			TreeNode elementNode = (TreeNode)nodeObject;
			this.NavigatorTree.Nodes.Insert(0,elementNode);
		} 

		/// <summary>
		/// catches the event that the backgroundworker has finished.
		/// in that case we should select the returned node
		/// </summary>
		/// <param name="sender">the backgroundworder</param>
		/// <param name="e">the parameters</param>
		private void treeBackgroundRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                //nothing?
                
            }
            else if (!(e.Error == null))
            {
                //nothing?
            }
            else
            {
            	            	
            	//do the visual work for the new treenode
            	TreeNode elementNode = (TreeNode)e.Result;
            	
            	//remove duplicate
            	if (this.removeRootNode((UML.Extended.UMLItem)elementNode.Tag))
				{
            		try
            		{
						//no parentNode, add as new root node before any others
						//inserting a node sometimes causes an InvalidOperationException because its being called in the wrong thread
						//doing it using the invoke should help.
						invoker = new MethodDelegate(ThreadSaveInsertNode); 
						this.NavigatorTree.Invoke(invoker, elementNode); 
						//this.NavigatorTree.Nodes.Insert(0,elementNode);
						//remove the excess nodes
						this.removeExcessNodes();
						//expand the node
						elementNode.Expand();
						
            		}
            		catch (Exception exception)
            		{
            			MessageBox.Show(exception.Message + Environment.NewLine + exception.StackTrace,"An error occured"
            			                ,MessageBoxButtons.OK,MessageBoxIcon.Error);
            		}
				}
				else 
				{
					//first node is already the one we need
					elementNode = this.NavigatorTree.Nodes[0];
				}
            	this.NavigatorTree.SelectedNode = (TreeNode)e.Result;
            	            	
            	//check if there's still something int the queue
            	if (this.workQueue.Count > 0)
            	{
            		//get the last element in the queue
            		UML.Extended.UMLItem nextElement = this.workQueue[this.workQueue.Count -1];
            		//remove it
            		this.workQueue.RemoveAt(this.workQueue.Count -1);
            		//process it
            		this.setElement(nextElement);
            	}
            }
        }
		
		private void setToolbarVisibility()
		{
			this.toolbarVisible = settings.toolbarVisible;
			this.openInNavigatorButton.Enabled = !settings.trackSelectedElement;
		}
		/// <summary>
		/// clears all nodes
		/// </summary>
		public void clear()
		{
			this.NavigatorTree.Nodes.Clear();
			this.quickSearchBox.Text = string.Empty;
		}
		/// <summary>
		/// set the main element to navigate
		/// </summary>
		/// <param name="newElement">the element to navigate</param>
		public void setElement(UML.Extended.UMLItem newElement)
		{
			if (! treeBackgroundWorker.IsBusy)
			{
				this.startThread(newElement);
			}
			else
			{
				//backgroundworker is still busy so we add the element to the workqueue instead
				TimeSpan runtime =  (DateTime.Now - lastStartTime);
				if (runtime.TotalSeconds < 5 )
				{
					this.workQueue.Insert(0,newElement);
					//make sure the queue doesn't get too large.
					if (this.workQueue.Count > 10)
					{
						this.workQueue.RemoveAt(this.workQueue.Count -1);
					}
				}
				else
				{
					//for some reason sometimes the backgroundworker gets stuck. 
					//This always seems to happen shortly after a right click (and thus a call to EA_GetMenuItems) when a lot of elements have been clicked in a short period of time.
					//when its stuck it is calling a method on the EA API that never returns (someting like EA.Element.Name)
					//I've tried everything to abort or cancel the thread, but nothing seems to work. 
					//So the only solution that does seem to work is spawn a new thread and start using that one.
					//In the long rung this will ofcourse cause the a memory/thread leak, but from personal experience, this only seems to happen a few times a day, and only when dealing with a large (and thus slow) model.
					
					//current backgoundworker is stuck. Start a new one.
					this.resetTreeBackgroundWorker();
					//process the element
					this.startThread(newElement);
				}
			}
		}
		
		private void startThread(UML.Extended.UMLItem newElement)
		{
			if (! this.treeBackgroundWorker.IsBusy)
			{
				this.working = true;
				//start new tread here to create new element
				this.lastStartTime = DateTime.Now;
				this.treeBackgroundWorker.RunWorkerAsync(newElement);
			}
		}
		
		/// <summary>
		/// returns the tooltiptext for the given element
		/// </summary>
		/// <param name="element">the element</param>
		/// <returns>the tooltip text</returns>
		private string getToolTipText(UML.Extended.UMLItem element)
		{
			//Getting some strange RPC_E_WRONG_THREAD error while getting the owner ElementWrapper.
			//Disablign the tooltiptext for now to avoid the error
			//Enabled again after installing all addins together
			return element.fqn;
		}

		
		/// <summary>
		/// adds an elementNode to the tree
		/// </summary>
		/// <param name="element">the source element</param>
		/// <param name="parentNode">the parentNode. If null is passed then the node will be added as root node</param>
		/// <param name="nodeToReplace">the node to replace with new data</param>
		private void addElementToTree(UML.Extended.UMLItem element,TreeNode parentNode,TreeNode nodeToReplace = null)
		{
			TreeNode elementNode = this.makeElementNode(element,parentNode,nodeToReplace);
			this.enableDisableContexMenu(element);
			// select the node
			//NavigatorTree.SelectedNode = elementNode;
		}
		/// <summary>
		/// adds an elementNode to the tree
		/// </summary>
		/// <param name="element">the source element</param>
		/// <param name="parentNode">the parentNode. If null is passed then the node will be added as root node</param>
		private TreeNode makeElementNode(UML.Extended.UMLItem element,TreeNode parentNode,TreeNode nodeToReplace = null)
		{
			//create new node
			TreeNode elementNode;
			if (nodeToReplace != null)
			{
				elementNode = nodeToReplace;
				if (nodeToReplace.Text.StartsWith(EAAddin.ownerMenuPrefix.Replace("&",string.Empty)))
				{
					elementNode.Text = EAAddin.ownerMenuPrefix.Replace("&",string.Empty) + NavigatorVisuals.getInstance().getNodeName(element);
				}
				//the type name is already ok
				//remove dummy node
				this.removeDummyNode(elementNode);
			}
			else
			{
				elementNode = new TreeNode(NavigatorVisuals.getInstance().getNodeName(element));
			}
			elementNode.Tag = element;
			int imageIndex = NavigatorVisuals.getInstance().getImageIndex(element);
			elementNode.ImageIndex = imageIndex;
			elementNode.SelectedImageIndex = imageIndex;
			elementNode.ToolTipText = this.getToolTipText(element);
			
			//get sub menu option
			List<string> subMenuOptions = EAAddin.getMenuOptions(element);
			if (subMenuOptions.Count > 0)
			{
				this.removeDummyNode(elementNode);
			}			
			//add subnodes
			foreach (string subNodeName in subMenuOptions) 
			{
				TreeNode subNode = new TreeNode(subNodeName.Replace("&",String.Empty));
				int subImageIndex = NavigatorVisuals.getInstance().getFolderImageIndex(subNodeName);
				subNode.ImageIndex = subImageIndex;
				subNode.SelectedImageIndex = subImageIndex;
				subNode.Nodes.Add( new TreeNode(dummyName,NavigatorVisuals.getInstance().getDummyIndex(),NavigatorVisuals.getInstance().getDummyIndex()));
				elementNode.Nodes.Add(subNode);
				
			}
			
			if (parentNode != null)
			{
				if (nodeToReplace == null)
				{
					this.removeDummyNode(parentNode);
					parentNode.Nodes.Add(elementNode);
				}
			}
			else
			{
//				
//				//remove duplicate
//				if (this.removeRootNode(element))
//				{
//					//no parentNode, add as new root node before any others
//					this.NavigatorTree.Nodes.Insert(0,elementNode);
//					//remove the excess nodes
//					this.removeExcessNodes();
//					//expand the node
//					elementNode.Expand();
//				}
//				else 
//				{
//					//first node is already the one we need
//					elementNode = this.NavigatorTree.Nodes[0];
//				}


			}
			return elementNode;
		}

		/// <summary>
		/// removes the rootnode representig the given element
		/// unless it this node is the first rootnode.
		/// In that case it simply returns false
		/// </summary>
		/// <param name="sourceElement">the element</param>
		private bool removeRootNode(UML.Extended.UMLItem sourceElement)
		{
			if (sourceElement != null)
			{
				for (int i = this.NavigatorTree.Nodes.Count -1;i >= 0;i--) 
				{
					TreeNode node = this.NavigatorTree.Nodes[i];
					if (node != null)
					{
						if (sourceElement.Equals(node.Tag))
						{
							if( i > 0)
							{
								try
								{
									node.Remove();
								}
								catch(NullReferenceException)
								{
									//sometimes we get a nullpointer exception here for unknown reasons.
									//just ignore it and return true
									return true;
								}
								
								return true;
							}else
							{
								//if the node is the first root node then don't remove it but return false
								return false;
							}
						}
					}
				}
			}
			return true;
		}
		/// <summary>
		/// event launched just before a node is expanded
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">arguments</param>
		void NavigatorTreeBeforeExpand(object sender, TreeViewCancelEventArgs e)
		{		
			foreach (TreeNode subNode in e.Node.Nodes)
			{
				if (this.isOwnerNode(subNode))
				{
					UML.Extended.UMLItem owner = ((UML.Extended.UMLItem)e.Node.Tag).owner;
					if (owner != null)
					{
						this.addElementToTree(owner,e.Node,subNode);		
					}
				}
				else if (this.isTypeNode(subNode))
				{
					UML.Classes.Kernel.Property attribute  = e.Node.Tag as UML.Classes.Kernel.Property;
					if (attribute != null
					    && attribute.type != null)
					{
						this.addElementToTree(attribute.type,e.Node,subNode);
					}
				}
			}
			if (BeforeExpand != null)
				BeforeExpand(sender,e);
		}
		private bool isOwnerNode(TreeNode node)
		{
			return (!(node.Tag is UML.Extended.UMLItem)
			   && node.Parent.Tag is UML.Extended.UMLItem
			   && node.Text.StartsWith(EAAddin.ownerMenuPrefix.Replace("&",string.Empty)));

		}
		private bool isTypeNode(TreeNode node)
		{
			return (!(node.Tag is UML.Extended.UMLItem)
			   && node.Parent.Tag is UML.Extended.UMLItem
			   && node.Text.StartsWith(EAAddin.typeMenuPrefix.Replace("&",string.Empty)));
		}
		/// <summary>
		/// remove the dummy node(s) from the given node
		/// </summary>
		/// <param name="node">the parent ndoe</param>
		private void removeDummyNode(TreeNode node)
		{
			foreach ( TreeNode subNode in node.Nodes) 
			{
				if (subNode.Text == dummyName)
				{
					node.Nodes.Remove(subNode);
				}
			}
		}
		/// <summary>
		/// removes all rootnodes with index larger then maxnodes
		/// </summary>
		private void removeExcessNodes()
		{
			for (int i = this.NavigatorTree.Nodes.Count -1; i > this.maxNodes;i--)
			{
				try
				{
					this.NavigatorTree.Nodes.RemoveAt(i);
				}catch (Exception)
				{
					// swallow any exception
					// if a node does not exist we don't need to remove it anymore
					// this sometimes happens with rare multithreaded race conditions
				}
			}
		}
		public event TreeViewCancelEventHandler BeforeExpand;
		public void setSubNodes(TreeNode node, List<UML.Extended.UMLItem> elements)
		{
			if (elements.Count > 0)
			{
				//remove dummy node
				node.Nodes.Clear();
				//add nodes for each named element
				foreach (UML.Extended.UMLItem element in elements) 
				{
					this.addElementToTree(element,node);
				}
			}
		}
		public event TreeNodeMouseClickEventHandler NodeDoubleClick;
		
		void NavigatorTreeNodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			UML.Extended.UMLItem selectedElement = e.Node.Tag as UML.Extended.UMLItem;
			if (selectedElement != null)
			{
				if (this.settings.projectBrowserDefaultAction)
				{
					this.selectInProjectBrowser();
				}
				else 
				{
					this.openProperties();
				}
			}
//			if (NodeDoubleClick != null)
//			{
//				NodeDoubleClick(sender,e);
//			}
		}
		
		void OpenPropertiesMenuItemClick(object sender, EventArgs e)
		{
			this.openProperties();
		}
		
		void NavigatorTreeNodeMouseClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
		{
			//set the selected node also with right mouse click
			if (e.Button == MouseButtons.Right)
			{
				this.NavigatorTree.SelectedNode = e.Node;
			}
			//show context menu
			UML.Extended.UMLItem selectedElement = this.NavigatorTree.SelectedNode.Tag as UML.Extended.UMLItem;
			if (e.Button == MouseButtons.Right
			    && selectedElement != null)
			{
				//set enabled status
				this.enableDisableContexMenu(selectedElement);
				//then actually show the menu
				this.navigatorContextMenu.Show(this.NavigatorTree,e.Location);
			}
		}
		/// <summary>
		/// enables or disabled the context menu items based on the type of the selected element
		/// </summary>
		/// <param name="selectedElement">the selected element</param>
		private void enableDisableContexMenu(UML.Extended.UMLItem selectedElement)
		{
			//both options should be disabled for connectors, 
			if (selectedElement is UML.Classes.Kernel.Relationship)
			{
				this.selectBrowserMenuItem.Enabled = false;
				this.projectBrowserButton.Enabled = false;
				this.openPropertiesMenuItem.Enabled = false;
				this.propertiesButton.Enabled = false;
			}
			//or for tagged values on connectors because we can't open their properties dialog anyway.
			else if (selectedElement is UML.Profiles.TaggedValue
			         && ((UML.Profiles.TaggedValue)selectedElement).owner is UML.Classes.Kernel.Relationship)
			{	
				this.selectBrowserMenuItem.Enabled = false;
				this.projectBrowserButton.Enabled = false;
				this.openPropertiesMenuItem.Enabled = false;
				this.propertiesButton.Enabled = false;
			}
			else if (selectedElement ==null)
			{
				this.selectBrowserMenuItem.Enabled = false;
				this.openPropertiesMenuItem.Enabled = false;
				this.addToDiagramButton.Enabled = false;
				this.propertiesButton.Enabled = false;
				this.projectBrowserButton.Enabled = false;
			}
			//standard should be enabled.
			else
			{
				this.setContextMenuItemsEnabled(true);
				this.propertiesButton.Enabled = true;
				this.projectBrowserButton.Enabled = true;
				this.addToDiagramButton.Enabled = true;
			}
			    
		}
		private void setContextMenuItemsEnabled(bool enabled)
		{
			foreach (ToolStripMenuItem menuItem in this.navigatorContextMenu.Items) 
			{
				menuItem.Enabled = enabled;	
			}
		}
		
		
		void SelectBrowserMenuItemClick(object sender, EventArgs e)
		{
			this.selectInProjectBrowser();
		}
		
		void OptionsMenuItemClick(object sender, EventArgs e)
		{
			this.showOptions();
		}
		private void openProperties()
		{
			UML.Extended.UMLItem selectedElement = this.NavigatorTree.SelectedNode.Tag as UML.Extended.UMLItem;
			if (selectedElement != null)
			{
				selectedElement.openProperties();
			}
		}
		private void addToDiagram(UML.Extended.UMLItem selectedElement)
		{
			if (selectedElement != null)
			{
				selectedElement.addToCurrentDiagram();
				selectedElement.selectInCurrentDiagram();
			}
		}
		private void addToDiagram()
		{
			UML.Extended.UMLItem selectedElement = this.NavigatorTree.SelectedNode.Tag as UML.Extended.UMLItem;
			this.addToDiagram(selectedElement);
		}
		/// <summary>
		/// copies the GUID of the selected element to the clipboard
		/// </summary>
		void copyGUID()
		{
			if (this.NavigatorTree.SelectedNode != null)
			{
				UML.Extended.UMLItem selectedElement = this.NavigatorTree.SelectedNode.Tag as UML.Extended.UMLItem;
				if (selectedElement.uniqueID != null)
				{
					Clipboard.SetText(selectedElement.uniqueID);
				}
			}
		}
		
		private void selectInProjectBrowser()
		{
			UML.Extended.UMLItem selectedElement = this.NavigatorTree.SelectedNode.Tag as UML.Extended.UMLItem;
			if (selectedElement != null)
			{
				//select the context before opening the element (and changing the selected node)
				UML.Extended.UMLItem context = null;
				if (selectedElement is UML.Diagrams.Diagram)
				{
					context = this.findContext(this.NavigatorTree.SelectedNode);
				}
				//actually open element
				selectedElement.open();
				if (selectedElement is UML.Classes.Kernel.Relationship) 
				{
					this.setElement(selectedElement);
				}
				//select the context if the selected element was a diagram
				if (context != null)
				{
					((UML.Diagrams.Diagram)selectedElement).selectItem(context);
				}
			}
		}
		/// <summary>
		/// finds the context of the given node. 
		/// That is the first parent node that is a UMLItem
		/// </summary>
		/// <param name="node">the node to start searching from</param>
		/// <returns>first parent node that is a UMLItem</returns>
		private UML.Extended.UMLItem findContext(TreeNode node)
		{
			if (node.Parent != null)
			{
				if (node.Parent.Tag is UML.Extended.UMLItem)
				{
					//found it
					return node.Parent.Tag as UML.Extended.UMLItem;
				}
				else
				{
					//search further up the tree.
					return findContext(node.Parent);
				}
			}
			else
			{
				//no need to search further since there's no parent
				return null;
			}
		}
		private void showOptions()
		{
			NavigatorSettingsForm optionsForm = new NavigatorSettingsForm(this.settings);
			optionsForm.ShowDialog();
			this.setToolbarVisibility();
		}
		
		void ProjectBrowserButtonClick(object sender, EventArgs e)
		{
			this.selectInProjectBrowser();
		}
		
		void PropertiesButtonClick(object sender, EventArgs e)
		{
			this.openProperties();
		}
		
		void SettingsButtonClick(object sender, EventArgs e)
		{
			this.showOptions();
		}
		
		void AboutButtonClick(object sender, EventArgs e)
		{
			new AboutWindow().ShowDialog();
		}
		public bool toolbarVisible
		{
			get
			{
				return this.navigatorToolStripContainer.Visible;
			}
			set
			{
				if (value&& !this.navigatorToolStripContainer.Visible)
				{
					this.navigatorToolStripContainer.Show();
					this.quickSearchBox.Show();
					//move the tree control down
					this.NavigatorTree.Location =
					new Point(this.NavigatorTree.Location.X,
						          this.NavigatorTree.Location.Y + this.navigatorToolStripContainer.Size.Height);
					//make it a litter smaller
										this.NavigatorTree.Size = 
						new Size(this.NavigatorTree.Size.Width,
					                                   this.NavigatorTree.Size.Height - this.navigatorToolStripContainer.Size.Height);
					//this.navigatorToolStrip.Show();
				}
			else if (! value && this.navigatorToolStripContainer.Visible)
				{
					this.navigatorToolStripContainer.Hide();
					this.quickSearchBox.Hide();
					//move the tree control up.
					this.NavigatorTree.Location = 
					new Point(this.NavigatorTree.Location.X,
						          this.NavigatorTree.Location.Y - this.navigatorToolStripContainer.Size.Height);
					//make it a litter taller
					this.NavigatorTree.Size = 
						new Size(this.NavigatorTree.Size.Width,
					                                   this.NavigatorTree.Size.Height + this.navigatorToolStripContainer.Size.Height);
					//this.navigatorToolStrip.Hide();
				}
			}
			
		}
		public event EventHandler openInNavigatorClick;
		void OpenInNavigatorButtonClick(object sender, EventArgs e)
		{
			if (this.openInNavigatorButton != null)
			{
				openInNavigatorClick(sender,e);
			}
		}
		public event EventHandler fqnButtonClick;
		void FqnButtonClick(object sender, EventArgs e)
		{
			if (this.fqnButton != null)
			{
				fqnButtonClick(sender,e);
			}
		}
		public event EventHandler guidButtonClick;
		void GuidButtonClick(object sender, EventArgs e)
		{
			if (this.guidButtonClick != null)
			{
				guidButtonClick(sender,e);
			}
		}
		void AddToDiagramButtonClick(object sender, EventArgs e)
		{	
			this.addToDiagram();
		}
		
		void AddToDiagramMenuOptionClick(object sender, EventArgs e)
		{
			this.addToDiagram();
		}
		void CopyGUIDButtonClick(object sender, EventArgs e)
		{
			this.copyGUID();
		}
		
		void CopyGUIDMenuItemClick(object sender, EventArgs e)
		{
			this.copyGUID();
		}



		public event EventHandler quickSearchTextChanged;
		void QuickSearchComboBoxTextChanged(object sender, EventArgs e)
		{
			if(this.quickSearchBox.Text != this.quickSearchText
			  && this.quickSearchBox.Text != this.quickSearchEmpty )
			{
				this.quickSearchBox.Text = this.quickSearchText;
			}
		}
		void QuickSearchComboBoxTextUpdate(object sender, EventArgs e)
		{
			this.handleSearchTextChange(e);
		}
        public event EventHandler guidSearched;
        private void handleSearchTextChange(EventArgs e)
		{
			if (this.quickSearchBox.Text != this.quickSearchEmpty)
			{
				this.quickSearchText = this.quickSearchBox.Text;
				//close quicksearch when nothing is in the quicksearch box.
				if (quickSearchText.Length == 0)
				{
					this.quickSearchBox.DroppedDown = false;
					this.quickSearchBox.Items.Clear();
					this.quickSearchBox.SelectedItem = null;
					this.quickSearchBox.ResetText();
					//to avoid error with index = 0 (invalidArgumenException) we add an empty string
					this.quickSearchBox.Items.Add(string.Empty);
				}
				else if (quickSearchText.Length > 0 
				    && quickSearchTextChanged != null
				    && !this.quickSearchBackgroundWorker.IsBusy)
				{
                    Guid guid;
                    if (Guid.TryParse(quickSearchText, out guid))
                    {
                        guidSearched?.Invoke(quickSearchText, e);
                    }
                    else
                    {
                        this.quickSearchBackgroundWorker.RunWorkerAsync(this.quickSearchText);
                    }
				}
			}
		}
		
		private void setQuickSearchEmpty()
		{
			this.quickSearchBox.Text = this.quickSearchEmpty;
			this.quickSearchBox.ForeColor = Color.Gray;
		}
		
		public void setQuickSearchResults(List<UML.Extended.UMLItem> results,string searchedString)
		{
			
			//check if he searched string is not yet updated, else don't bother showing the results
			if (searchedString == this.quickSearchText)
			{
				this.quickSearchBox.Invoke(new MethodDelegate(threadSafeSetQuickSearchItems),results);
				

			}
		}
		/// <summary>
		/// sets the given results as items in the quicksearchcombobox
		/// </summary>
		/// <param name="nodeObject"></param>
		private void threadSafeSetQuickSearchItems(object resultsObject) 
		{
			List<UML.Extended.UMLItem> results = (List<UML.Extended.UMLItem>)resultsObject;
			this.quickSearchBox.Items.Clear();
			this.quickSearchBox.SelectedItem = null;
			
			if (results.Count > 0)
			{
				this.quickSearchBox.Items.AddRange(results.ToArray());
			}
			else
			{
				//to avoid error with index = 0 (invalidArgumenException) we add an empty string
				this.quickSearchBox.Items.Add(string.Empty);
			}
			this.quickSearchBox.DroppedDown = results.Count > 0;
							

		} 

		
		void QuickSearchComboBoxSelectionChangeCommitted(object sender, EventArgs e)
		{
			if( this.quickSearchBox.SelectedIndex >= 0)
			{
				UML.Extended.UMLItem quickSearchSelectedItem = this.quickSearchBox.SelectedItem as UML.Extended.UMLItem;
				{
					if (quickSearchSelectedItem != null)
				    {
						// if this element is to be selected in the project browser is will
						// automatically be set as the main element
						if (settings.quickSearchSelectProjectBrowser)
						{
							quickSearchSelectedItem.select();
						}
						else
						{
							this.setElement(quickSearchSelectedItem);
						}
						// also add to diagram if needed
						if (settings.quickSearchAddToDiagram)
						{
							this.addToDiagram(quickSearchSelectedItem);
						}
				    }
				}
			}
		}
		

		
		void QuickSearchComboBoxEnter(object sender, EventArgs e)
		{
			if (this.quickSearchBox.Text == this.quickSearchEmpty)
			{
				this.quickSearchBox.Text = string.Empty;
				this.quickSearchBox.ForeColor = this.ForeColor;
			}
		}
		
		void QuickSearchComboBoxLeave(object sender, EventArgs e)
		{
			if (this.quickSearchBox.Text.Length == 0)
			{
				this.setQuickSearchEmpty();
			}
			this.quickSearchBox.DroppedDown = false;
		}
		

		
	}
}
