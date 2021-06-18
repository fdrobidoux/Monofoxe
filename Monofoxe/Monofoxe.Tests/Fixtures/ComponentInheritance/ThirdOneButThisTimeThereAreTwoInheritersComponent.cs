using System.Diagnostics.CodeAnalysis;

namespace Monofoxe.Tests.Fixtures.ComponentInheritance
{
	[ExcludeFromCodeCoverage]
	public class ThirdOneButThisTimeThereAreTwoInheritersComponent : LaterOnItsStillAnComponentAbstract
	{
		public override void Initialize()
		{
		}

		public override void FixedUpdate()
		{
		}

		public override void Update()
		{
		}

		public override void Draw()
		{
		}

		public override void Destroy()
		{
		}
	}
}
