using System;

namespace Monofoxe.Engine.EC
{
	[AttributeUsage(AttributeTargets.Class)]
	public class RestrictSingleInstancePerEntityAttribute : Attribute
	{
		public RestrictSingleInstancePerEntityAttribute()
		{
			
		}
	}
}
