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
			UnitOfWork.Register<FakeObjectContext, FakeObjectContextFactory>();
		}

		[Test]
		public void UnitOfWork_Start_ShouldStartUnitOfWork()
		{
			using(var uow = UnitOfWork.Start<FakeObjectContext>())
			{
				Assert.That(uow, Is.Not.Null);
				Assert.That(UnitOfWork.IsStarted<FakeObjectContext>());
				Assert.That(uow, Is.SameAs(UnitOfWork.GetCurrent<FakeObjectContext>()));
			}
		}

		[Test]
		public void UnitOfWork_Current_ShouldThrowIfNoUnitOfWorkIsStarted()
		{
			Assert.That(() =>
			            	{
			            		var uow = UnitOfWork.Current;
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
			Assert.That(() => UnitOfWork.Setup(typeof(FakeObjectContextFactory)), Throws.InvalidOperationException);
		}

		[Test]
		public void UnitOfWork_Start_ShouldThrowIfUnitOfWorkWasntSetUp()
		{
			var fieldInfo = typeof(UnitOfWork).GetField("_unitOfWorkFactory", BindingFlags.NonPublic | BindingFlags.Static);
			fieldInfo.SetValue(null, null);

			Assert.That(() => UnitOfWork.Start(), Throws.InvalidOperationException);
		}

		[Test]
		public void UnitOfWork_DisposingUnitOfWorkShoudDisposeObjectContext()
		{
			var context = new Mock<IObjectContext>();
			var factory = new Mock<IObjectContextFactory>();
			factory.Setup(f => f.CreateObjectContext()).Returns(() => context.Object);

			var fieldInfo = typeof(UnitOfWork).GetField("_unitOfWorkFactory", BindingFlags.NonPublic | BindingFlags.Static);
			var uowFactory = fieldInfo.GetValue(null);
			var propertyInfo = typeof (UnitOfWorkFactory).GetProperty("ContextFactory");
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

			var fieldInfo = typeof(UnitOfWork).GetField("_unitOfWorkFactory", BindingFlags.NonPublic | BindingFlags.Static);
			var uowFactory = fieldInfo.GetValue(null);
			var propertyInfo = typeof(UnitOfWorkFactory).GetProperty("ContextFactory");
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

			var fieldInfo = typeof(UnitOfWork).GetField("_unitOfWorkFactory", BindingFlags.NonPublic | BindingFlags.Static);
			var uowFactory = fieldInfo.GetValue(null);
			var propertyInfo = typeof(UnitOfWorkFactory).GetProperty("ContextFactory");
			propertyInfo.SetValue(uowFactory, factory.Object, null);

			using(UnitOfWork.Start())
			{
				Assert.That(UnitOfWork.CurrentContext, Is.SameAs(context.Object));
			}
		}

		[Test]
		public void UnitOfWork_CurrentContext_ShouldThrowIfUnitOfWorkIsNotStarted()
		{
			Assert.That(() =>
			            	{
			            		var context = UnitOfWork.CurrentContext;
			            	}, Throws.InvalidOperationException);
		}
	}
}
