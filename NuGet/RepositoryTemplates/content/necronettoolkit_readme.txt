
To use NecroNetToolkit with repository generator you need to follow a few simple steps:

1. After you've created your model, create a partial class of your EntityFramework object context and implement IObjectContext interface.

public partial class MyEntities : IObjectContext
{
}

2. Create a factory class that implements IObjectContextFactory.

public class MyEntitiesFactory : IObjectContextFactory
{
    public IObjectContext CreateObjectContext()
    {
		return new MyEntities();
    }
}

3. Register the factory type and object context type with UnitOfWork. This needs to be done only once at application startup.

UnitOfWork.RegisterDefault<MyEntities, MyEntitiesFactory>();

4. (optional) Register IUnitOfWorkManager with you dependency injection framework.

5. Set correct settings in Repositories.tt.settings.t4 suitable for your project. Set OmitEdmx if you generate code first classes from .edmx file and IsPOCO if you use EF POCO generator.
   You can optionally choose your DI framework and the template will generate a class that registers all repositories with their generic variants.

6. (optional) Register the generated class with your DI framework.

7. (optional) Implement IRepositoryFactory to easier access your repositories in a generic way, and register it with your DI framework

public class NinjectRepositoryFactory : IRepositoryFactory
{
	private readonly IKernel _kernel;

	public NinjectRepositoryFactory(IKernel kernel)
	{
		_kernel = kernel;
	}

	IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
	{
		return _kernel.Get<IRepository<TEntity>>();
	}
}

8a. (DI way) Inject IUnitOfWorkManager and repositories into your class and start working with your database
8b. (not recommended) new up repositories and use static UnitOfWork class