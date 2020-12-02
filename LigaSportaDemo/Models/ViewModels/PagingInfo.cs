using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigaSportaDemo.Models.ViewModels
{
	public class PagingInfo
	{
		public PagingInfo(int itemsPerPage = 4)
		{
			this.ItemsPerPage = itemsPerPage;
		}
		private int currentPage;
		public int TotalItems { get; set; }
		public int ItemsPerPage { get; set; }
		public int CurrentPage {
			get {
				currentPage = currentPage > TotalPages ? TotalPages : currentPage;
				return currentPage; }			
			set { currentPage = value; }
		}

		public int TotalPages =>
			(int)Math.Ceiling((decimal)TotalItems / (ItemsPerPage==0?1: ItemsPerPage));
	}
}
