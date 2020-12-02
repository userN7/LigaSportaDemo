using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigaSportaDemo.Models
{
	public class FilmInfo
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime ReleaseDate { get; set; }
		public string Director { get; set; }
		public string CreatorUser { get; set; }
		public string PosterPath { get; set; }
	}
}
