using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigaSportaDemo.Models.ViewModels
{
	public class FilmsListViewModel
	{
		public IEnumerable<FilmInfo> Films { get; set; }
		public PagingInfo PagingInfo { get; set; }
	}
}
