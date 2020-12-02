using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigaSportaDemo.Models
{
	public class Statics
	{
		static public readonly string rootUrl = "/";
		static public List<string> FilmsTableColNames = new List<string> { "Название", "Описание", "Год выпуска", "Режиссёр", "Создатель","Файл постера"};
		static public List<string> users = new List<string>() { "user1", "user2", "user3" };
		static public string postersPath = "/Files/";
	}
}
