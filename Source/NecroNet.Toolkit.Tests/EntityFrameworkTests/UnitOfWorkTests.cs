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
		public void UnitOfWork_GetCurrent_ShouldThrowIfNoUnitOfWorkIsStarted()
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
		public void UnitOfWork_RegisterDefault_ShouldThrowIfCalledMoreThanOnce()
		{
			Assert.That(() => UnitOfWork.RegisterDefault<FakeObjectContext, FakeObjectContextFactory>(), Throws.InvalidOperationException);
		}

		[Test]
		public void UnitOfWork_Start_ShouldThrowIfUnitOfWorkWasntSetUp()
		{
			var fieldInfo = typeof(UnitOfWork).GetField("UnitOfWorkFactories", BindingFlags.NonPublic | BindingFlags.Static);
			((Dictionary<string, IUnitOfWorkFactory>)fieldInfo.GetValue(null)).Remove(typeof(FakeObjectContext).FullName);

			Assert.That(() => UnitOfWork.Start(), Throws.InvalidOperationException);

			UnitOfWork.Register<FakeObjectContext, FakeObjectContextFactory>();
		}

		[Test]
		public void UnitOfWork_DisposingUnitOfWorkShoudDisposeObjectContext()
		{
			var context = new Mock<IObjectContext>();
			var factory = new Mock<IObjectContextFactory>();
			factory.Setup(f => f.CreateObjectContext()).Returns(() => context.Object);

			var fieldInfo = typeof(UnitOfWork).GetField("UnitOfWorkFactories", BindingFlags.NonPublic | BindingFlags.Static);
			var uowFactory = ((Dictionary<string, IUnitOfWorkFactory>)fieldInfo.GetValue(null))[typeof(FakeObjectContext).FullName];

            var propertyInfo = typeof(UnitOfWorkFactory<FakeObjectContext>).GetProperty("ContextFactory", BindingFlags.NonPublic | BindingFlags.Instance);
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

            var propertyInfo = typeof(UnitOfWorkFactory<FakeObjectContext>).GetProperty("ContextFactory", BindingFlags.NonPublic | BindingFlags.Instance);
			propertyInfo.SetValue(uowFactory, factory.Object, null);

			using (var scope = UnitOfWork.Start())
			{
				scope.Flush();
			}

			context.Verify(c => c.SaveChanges());
		}

		[Test]
		public void UnitOfWork_GetCurrentContext_ShouldReturnUnderlyingObjectContext()
		{
			var context = new Mock<IObjectContext>();
			var factory = new Mock<IObjectContextFactory>();
			factory.Setup(f => f.CreateObjectContext()).Returns(() => context.Object);

			var fieldInfo = typeof(UnitOfWork).GetField("UnitOfWorkFactories", BindingFlags.NonPublic | BindingFlags.Static);
			var uowFactory = ((Dictionary<string, IUnitOfWorkFactory>)fieldInfo.GetValue(null))[typeof(FakeObjectContext).FullName];

            var propertyInfo = typeof(UnitOfWorkFactory<FakeObjectContext>).GetProperty("ContextFactory", BindingFlags.NonPublic | BindingFlags.Instance);
			propertyInfo.SetValue(uowFactory, factory.Object, null);

			using(var scope = UnitOfWork.Start())
			{
				Assert.That(scope.Context, Is.SameAs(context.Object));
			}
		}

		[Test]
		public void UnitOfWork_GetCurrentContext_ShouldThrowIfUnitOfWorkIsNotStarted()
		{
			Assert.That(() =>
			                {
			                    IUnitOfWork unitOfWork;
			                    using (unitOfWork = UnitOfWork.Start())
			                    {
			                        
			                    }

			                    var context = unitOfWork.Context;
			                }, Throws.InvalidOperationException);
		}

		[Test]
		public void UnitOfWork_StartOfType_ShouldStartUnitOfWork()
		{
			using (var uow = UnitOfWork.Start<FakeObjectContext>())
			{
				Assert.That(uow, Is.Not.Null);
				Assert.That(UnitOfWork.IsStarted<FakeObjectContext>());
				Assert.That(uow, Is.SameAs(UnitOfWork.GetCurrent()));
			}
		}

		[Test]
		public void UnitOfWork_GetCurrentOfType_ShouldThrowIfNoUnitOfWorkIsStarted()
		{
			Assert.That(() =>
			{
				var uow = UnitOfWork.GetCurrent<FakeObjectContext>();
			}, Throws.InvalidOperationException);
		}

		[Test]
		public void UnitOfWork_StartOfType_ShouldThrowWhenUnitOfWorkIsAlreadyStarted()
		{
			Assert.That(() =>
			{
				using (UnitOfWork.Start<FakeObjectContext>())
				{
					UnitOfWork.Start<FakeObjectContext>();
				}
			}, Throws.InvalidOperationException);
		}

		[Test]
		public void UnitOfWork_Register_ShouldThrowIfCalledMoreThanOnceForTheSameType()
		{
			Assert.That(() => UnitOfWork.Register<FakeObjectContext, FakeObjectContextFactory>(), Throws.InvalidOperationException);
		}

		[Test]
		public void UnitOfWork_StartOfType_ShouldThrowIfUnitOfWorkWasntSetUp()
		{
			var fieldInfo = typeof(UnitOfWork).GetField("UnitOfWorkFactories", BindingFlags.NonPublic | BindingFlags.Static);
			((Dictionary<string, IUnitOfWorkFactory>)fieldInfo.GetValue(null)).Remove(typeof(FakeObjectContext).FullName);

			Assert.That(() => UnitOfWork.Start<FakeObjectContext>(), Throws.InvalidOperationException);

			UnitOfWork.Register<FakeObjectContext, FakeObjectContextFactory>();
		}

		[Test]
		public void UnitOfWork_DisposingUnitOfWorkOfTypeShoudDisposeObjectContext()
		{
			var context = new Mock<IObjectContext>();
			var factory = new Mock<IObjectContextFactory>();
			factory.Setup(f => f.CreateObjectContext()).Returns(() => context.Object);

			var fieldInfo = typeof(UnitOfWork).GetField("UnitOfWorkFactories", BindingFlags.NonPublic | BindingFlags.Static);
			var uowFactory = ((Dictionary<string, IUnitOfWorkFactory>)fieldInfo.GetValue(null))[typeof(FakeObjectContext).FullName];

            var propertyInfo = typeof(UnitOfWorkFactory<FakeObjectContext>).GetProperty("ContextFactory", BindingFlags.NonPublic | BindingFlags.Instance);
			propertyInfo.SetValue(uowFactory, factory.Object, null);

			using (UnitOfWork.Start<FakeObjectContext>())
			{
			}

			context.Verify(c => c.Dispose());
		}

        [Test]
        public void IObjectContext_AsActual_ShouldReturnUnwrappedObjectContext()
        {
            using (var scope = UnitOfWork.Start())
            {
                var context = scope.Context.AsActual<FakeObjectContext>();

                Assert.That(context, Is.TypeOf<FakeObjectContext>());
            }
        }
	}
}
