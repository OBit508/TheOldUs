using System;
using System.Diagnostics;

namespace BepInEx.Preloader.Core.Patching
{
	// Token: 0x0200003D RID: 61
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	[Conditional("CodeGeneration")]
	internal sealed class PatcherAutoPluginAttribute : Attribute
	{
		// Token: 0x0600011B RID: 283 RVA: 0x0000792F File Offset: 0x00005B2F
		public PatcherAutoPluginAttribute(string id = null, string name = null, string version = null)
		{
		}
	}
}
