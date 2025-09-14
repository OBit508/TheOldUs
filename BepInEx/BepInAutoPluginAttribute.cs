using System;
using System.Diagnostics;

namespace BepInEx
{
	// Token: 0x0200003C RID: 60
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	[Conditional("CodeGeneration")]
	internal sealed class BepInAutoPluginAttribute : Attribute
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00007927 File Offset: 0x00005B27
		public BepInAutoPluginAttribute(string id = null, string name = null, string version = null)
		{
		}
	}
}
