using Entity = Domain.BuildingBlocks.Aggregates.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Branches
{
	public class Branch : Entity
	{
		public Branch() 
		{
			//For EF!
		}
		
		public string Title {  get; private set; }
		public Guid StoreID { get; private set; }

		public static Branch Create(string title, Guid storeID)
		{
			var branch = new Branch(title, storeID)
			{

			};
			return branch;
		}
		private Branch(string title, Guid storeID)
		{
			Title = title;
			StoreID = storeID;
		}
		public void Update(string title, Guid storeID)
		{
			Title = title;
			StoreID = storeID;
		
		}
	}
}
