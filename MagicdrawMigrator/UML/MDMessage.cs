using System.Collections.Generic;
using System.Linq;
using System;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of MDMessage.
	/// </summary>
	public class MDMessage
	{
		public string messageID {get;set;}
		public string sourceID {get;set;}
		public string targetID {get;set;}
		public string messageName {get;set;}
		public bool isAsynchronous {get;set;}
			
		public MDMessage(string messageID, string sourceID, string targetID, string messageName, bool isAsynchronous)
		{
			this.messageID = messageID;
			this.sourceID = sourceID;
			this.targetID = targetID;
			this.messageName = messageName;
			this.isAsynchronous = isAsynchronous;
		}
		
	}
}
