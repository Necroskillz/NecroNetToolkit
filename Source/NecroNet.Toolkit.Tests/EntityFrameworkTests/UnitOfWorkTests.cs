using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Transactions;
using NecroNet.Toolkit.Data;
using NecroNet.Toolkit.Tests.Fakes;
using NecroNet.Toolkit.Tests.Helpers;
using NUnit.Framework;
using Moq;

namespace NecroNet.Toolkit.Tests.EntityFrameworkTests
{
	[TestFixture]
	public class UnitOfWorkTests
	{
		[TestFixtureSetUp]
		public void FixtureSetUp()
		{
			UnitOfWork.RegisterDefault<FakeObjectContext, FakeObjectContextFactory>();
		}

		[Test]
		public void UnitOfWork_Start_ShouldStartUnitOfWork()
		{
			using(var uow = UnitOfWork.Start())
			{
				Assert.That(uow, Is.Not.Null);
				Assert.That(UnitOfWork.IsStarted());
				Assert.That(uow, Is.SameAs(UnitOfWork.GetCurrent()));
			}
		}

		[Test]
		public void UnitOfWork_Current_ShouldThrowIfNoUnitOfWorkIsStarted()
		{
			Assert.That(() =>
			            	{
			            		var uow = UnitOfWork.GetCurrent();
			            	}, Throws.InvalidOperationException);
		}

		[Test]
		public void UnitOfWork_Start_ShouldThrowWhenUnitOfWorkIsAlreadyStarted()
		{
			Assert.That(() =>
			            	{
								using(UnitOfWork.Start())
			            		{
			            			UnitOfWork.Start();
			            		}
			            	}, Throws.InvalidOperationException);
		}

		[Test]
		public void UnitOfWork_Setup_ShouldThrowIfCalledMoreThanOnce()
		{
			Assert.That(() => UnitOfWork.RegisterDefault<FakeObjectContext, FakeObjectContextFactory>(), Throws.InvalidOperationException);
		}

		[Test]
		public void UnitOfWork_Start_ShouldThrowIfUnitOfWorkWasntSetUp()
		{
			var fieldInfo = typeof(UnitOfWork).GetField("UnitOfWorkFactories", BindingFlags.NonPublic | BindingFlags.Static);
			((Dictionary<string, IUnitOfWorkFactory>)fieldInfo.GetValue(null)).Remove(typeof(FakeObjectContext).FullName);

			Assert.That(() => UnitOfWork.Start(), Throws.InvalidOperationException);
		}

		[Test]
		public void UnitOfWork_DisposingUnitOfWorkShoudDisposeObjectContext()
		{
			var context = new Mock<IObjectContext>();
			var factory = new Mock<IObjectContextFactory>();
			factory.Setup(f => f.CreateObjectContext()).Returns(() => context.Object);

			var fieldInfo = typeof(UnitOfWork).GetField("UnitOfWorkFactories", BindingFlags.NonPublic | BindingFlags.Static);
			var uowFactory = ((Dictionary<string, IUnitOfWorkFactory>)fieldInfo.GetValue(null))[typeof(FakeObjectContext).FullName];

			var propertyInfo = typeof(UnitOfWorkFactory<FakeObjectContext>).GetProperty("ContextFactory");
			propertyInfo.SetValue(uowFactory, factory.Object, null);

			using(UnitOfWork.Start())
			{
			}

			context.Verify(c => c.Dispose());
		}

		[Test]
		public void UnitOfWork_Flush_ShouldCallSaveChangesOnObjectContext()
		{
			var context = new Mock<IObjectContext>();
			var factory = new Mock<IObjectContextFactory>();
			factory.Setup(f => f.CreateObjectContext()).Returns(() => context.Object);

			var fieldInfo = typeof(UnitOfWork).GetField("UnitOfWorkFactories", BindingFlags.NonPublic | BindingFlags.Static);
			var uowFactory = ((Dictionary<string, IUnitOfWorkFactory>)fieldInfo.GetValue(null))[typeof(FakeObjectContext).FullName];

			var propertyInfo = typeof(UnitOfWorkFactory<FakeObjectContext>).GetProperty("ContextFactory");
			propertyInfo.SetValue(uowFactory, factory.Object, null);

			using (var scope = UnitOfWork.Start())
			{
				scope.Flush();
			}

			context.Verify(c => c.SaveChanges());
		}

		[Test]
		public void UnitOfWork_CurrentContext_ShouldReturnUnderlyingObjectContext()
		{
			var context = new Mock<IObjectContext>();
			var factory = new Mock<IObjectContextFactory>();
			factory.Setup(f => f.CreateObjectContext()).Returns(() => context.Object);

			var fieldInfo = typeof(UnitOfWork).GetField("UnitOfWorkFactories", BindingFlags.NonPublic | BindingFlags.Static);
			var uowFactory = ((Dictionary<string, IUnitOfWorkFactory>)fieldInfo.GetValue(null))[typeof(FakeObjectContext).FullName];

			var propertyInfo = typeof(UnitOfWorkFactory<FakeObjectContext>).GetProperty("ContextFactory");
			propertyInfo.SetValue(uowFactory, factory.Object, null);

			using(UnitOfWork.Start())
			{
				Assert.That(UnitOfWork.GetCurrentContext(), Is.SameAs(context.Object));
			}
		}

		[Test]
		public void UnitOfWork_CurrentContext_ShouldThrowIfUnitOfWorkIsNotStarted()
		{
			Assert.That(() =>
			            	{
			            		var context = UnitOfWork.GetCurrentContext();
			            	}, Throws.InvalidOperationException);
		}
	}
}
