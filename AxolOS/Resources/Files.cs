using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxolOS.Resources
{
	public static class Files
	{
		[ManifestResourceStream(ResourceName = "AxolOS.Resources.Wallpapers.Wllp1.bmp")] public static byte[] AxolOSBackgroundRaw;
		[ManifestResourceStream(ResourceName = "AxolOS.Resources.Cursors.Cursor1.bmp")] public static byte[] AxolOSCursorRaw;
	}
}
