using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsService
{
	public class AppSettings
	{
		public string WatchFolder { get; set; } = null;
		public string RepresentsTest { get; set; } = "*.txt";
		public bool IncludeSubdirectories { get; set; } = false;
	}
}
