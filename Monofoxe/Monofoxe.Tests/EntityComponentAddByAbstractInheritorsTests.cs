using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Monofoxe.Engine.EC;
using Monofoxe.Engine.SceneSystem;
using Monofoxe.Tests.Fixtures.ComponentInheritance;
using NUnit.Framework;

namespace Tests
{
	public class EntityComponentAddByAbstractInheritorsTests
	{
		private static Entity GetTestEntity() => SceneMgr.GetScene("default")["layer1"].Entities[0];

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			var scene = SceneMgr.CreateScene("default");
			var layer = scene.CreateLayer("layer1");
		}
		
		[SetUp]
		public void Setup()
		{
			new Entity(SceneMgr.GetScene("default")["layer1"]);
		}

		[TearDown]
		public void TearDown()
		{
			var entity = GetTestEntity();
			entity.DestroyEntity();
			SceneMgr.GetScene("default")["layer1"].UpdateEntityList();
		}

		[Test(Author = "@fdrobidoux")]
		public void AddingAComponent_WithOneAbstractInheritanceBeforeComponent_WillWork()
		{
			var entity = GetTestEntity();
			var firstImplementedComponent = new FirstImplementedComponent();
			
			entity.AddComponent(firstImplementedComponent);

			Assert.AreSame(firstImplementedComponent, entity.GetComponent<AnAbstractComponent>());
			Assert.AreSame(firstImplementedComponent, entity.GetComponent<FirstImplementedComponent>());
		}

		[Test(Author = "@fdrobidoux")]
		[ExcludeFromCodeCoverage]
		public void WithTwoInheritingOfSameAbstractMethod_ItWillThrowAnException()
		{
			var entity = GetTestEntity();
			var firstImplementedComponent = new FirstImplementedComponent();
			
			entity.AddComponent(firstImplementedComponent);
			
			var secondImplementedComponent = new SecondImplementedComponent();
			Assert.Throws<ArgumentException>(delegate { entity.AddComponent(secondImplementedComponent); });
		}

		[Test(Author = "@fdrobidoux")]
		public void StillWorks_WithMultipleAbstractClassesInOne()
		{
			var entity = GetTestEntity();
			var thirdImplementedComponent = new ThirdOneButThisTimeThereAreTwoInheritersComponent();
			
			entity.AddComponent(thirdImplementedComponent);
			
			Assert.AreSame(thirdImplementedComponent, entity.GetComponent<ThirdOneButThisTimeThereAreTwoInheritersComponent>());
			Assert.AreSame(thirdImplementedComponent, entity.GetComponent<LaterOnItsStillAnComponentAbstract>());
			Assert.AreSame(thirdImplementedComponent, entity.GetComponent<AnAbstractComponent>());
		}

		[Test(Author = "@fdrobidoux")]
		public void RemovingComponents_Works_WithComponentInstance()
		{
			var entity = GetTestEntity();
			var firstImplementedComponent = new FirstImplementedComponent();
			
			entity.AddComponent(firstImplementedComponent);
			
			Assert.Contains(firstImplementedComponent, entity.GetAllComponents());
			Assert.True(entity.HasComponent<FirstImplementedComponent>());
			
			var returnedComponent = entity.RemoveComponent(firstImplementedComponent.GetType());
			
			Assert.AreSame(returnedComponent, firstImplementedComponent);
			Assert.False(entity.HasComponent<FirstImplementedComponent>());
			Assert.False(entity.GetAllComponents().Contains(firstImplementedComponent));
		}
		
		[Test(Author = "@fdrobidoux")]
		public void RemovingComponents_Works_WithComponentInstance_CastedToAbstract()
		{
			var entity = GetTestEntity();
			var firstImplementedComponent = new FirstImplementedComponent();
			
			entity.AddComponent(firstImplementedComponent);
			
			Assert.True(entity.HasComponent<FirstImplementedComponent>());
			Assert.True(entity.HasComponent<AnAbstractComponent>());
			Assert.Contains(firstImplementedComponent, entity.GetAllComponents());

			var anAbstractComponent = (AnAbstractComponent) firstImplementedComponent;
			var returnedComponent = entity.RemoveComponent(anAbstractComponent.GetType());
			
			Assert.AreSame(returnedComponent, anAbstractComponent);
			Assert.False(entity.HasComponent<FirstImplementedComponent>());
			Assert.False(entity.HasComponent<AnAbstractComponent>());
			Assert.False(entity.GetAllComponents().Contains(firstImplementedComponent));
			Assert.False(entity.GetAllComponents().Contains(anAbstractComponent));
		}

		private static Type[] TypesInheritable =
		{
			typeof(FirstImplementedComponent),
			typeof(AnAbstractComponent)
		};
		
		[Test(Author = "@fdrobidoux")]
		[TestCaseSource(nameof(TypesInheritable))]
		public void RemovingComponents_Works_WithGenericAttribute(Type typeToTest)
		{
			var entity = GetTestEntity();
			var firstImplementedComponent = new FirstImplementedComponent();
			
			entity.AddComponent(firstImplementedComponent);
			
			var returnedComponent = entity.RemoveComponent(typeToTest);
			
			Assert.IsInstanceOf(typeToTest, returnedComponent);
			Assert.False(entity.HasComponent(typeToTest));
			Assert.False(entity.GetAllComponents().Contains(firstImplementedComponent));
		}
		
		[Test(Author = "@fdrobidoux")]
		public void RemovingComponents_Works_WithAbstractAsGenericAttribute()
		{
			var entity = GetTestEntity();
			var firstImplementedComponent = new FirstImplementedComponent();
			
			entity.AddComponent(firstImplementedComponent);
			
			var returnedComponent = entity.RemoveComponent<AnAbstractComponent>();
			
			Assert.IsInstanceOf(typeof(AnAbstractComponent), returnedComponent);
			Assert.False(entity.HasComponent(firstImplementedComponent.GetType()));
			Assert.False(entity.HasComponent(typeof(AnAbstractComponent)));
			Assert.False(entity.GetAllComponents().Contains(firstImplementedComponent));
		}
		
		[Test(Author = "@fdrobidoux")]
		public void RemovingComponents_Works_WithImplementationClassAsGenericAttribute()
		{
			var entity = GetTestEntity();
			
			var firstImplementedComponent = new FirstImplementedComponent();
			entity.AddComponent(firstImplementedComponent);
			
			var returnedComponent = entity.RemoveComponent<FirstImplementedComponent>();
			
			Assert.IsInstanceOf(typeof(FirstImplementedComponent), returnedComponent);
			Assert.False(entity.HasComponent<FirstImplementedComponent>());
			Assert.False(entity.HasComponent<AnAbstractComponent>());
			Assert.False(entity.GetAllComponents().Contains(firstImplementedComponent));
		}
	}
}
