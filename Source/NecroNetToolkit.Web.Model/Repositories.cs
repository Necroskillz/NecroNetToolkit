﻿// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

using NecroNet.Toolkit.Data;

namespace NecroNetToolkit.Web.Model
{
	public abstract partial class AllDealsEntitiesRepositoryBase<TEntity> : EdmRepositoryBase<AllDealsEntities, TEntity>
		where TEntity : class
	{
		private readonly IUnitOfWorkManager _unitOfWorkManager;
		
		protected AllDealsEntitiesRepositoryBase(IUnitOfWorkManager unitOfWorkManager)
		{
			_unitOfWorkManager = unitOfWorkManager;
		}
		
		protected override AllDealsEntities ObjectContext
		{
			get
			{
				return _unitOfWorkManager.GetCurrent<AllDealsEntities>().Context.AsActual<AllDealsEntities>();
			}
		}
	}

	// Entity Set : ActualDeals | Type Name : ActualDeal
	public partial interface IActualDealRepository : IRepository<ActualDeal>
	{
	}
	
	[EntitySetName("ActualDeals")]
	public partial class ActualDealRepository : AllDealsEntitiesRepositoryBase<ActualDeal>, IActualDealRepository
	{
		public ActualDealRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : Categories | Type Name : Category
	public partial interface ICategoryRepository : IRepository<Category>
	{
	}
	
	[EntitySetName("Categories")]
	public partial class CategoryRepository : AllDealsEntitiesRepositoryBase<Category>, ICategoryRepository
	{
		public CategoryRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : Cities | Type Name : City
	public partial interface ICityRepository : IRepository<City>
	{
	}
	
	[EntitySetName("Cities")]
	public partial class CityRepository : AllDealsEntitiesRepositoryBase<City>, ICityRepository
	{
		public CityRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : HistoryDeals | Type Name : HistoryDeal
	public partial interface IHistoryDealRepository : IRepository<HistoryDeal>
	{
	}
	
	[EntitySetName("HistoryDeals")]
	public partial class HistoryDealRepository : AllDealsEntitiesRepositoryBase<HistoryDeal>, IHistoryDealRepository
	{
		public HistoryDealRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : Indexes | Type Name : Index
	public partial interface IIndexRepository : IRepository<Index>
	{
	}
	
	[EntitySetName("Indexes")]
	public partial class IndexRepository : AllDealsEntitiesRepositoryBase<Index>, IIndexRepository
	{
		public IndexRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : Logs | Type Name : Log
	public partial interface ILogRepository : IRepository<Log>
	{
	}
	
	[EntitySetName("Logs")]
	public partial class LogRepository : AllDealsEntitiesRepositoryBase<Log>, ILogRepository
	{
		public LogRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : MetaCategories | Type Name : MetaCategory
	public partial interface IMetaCategoryRepository : IRepository<MetaCategory>
	{
	}
	
	[EntitySetName("MetaCategories")]
	public partial class MetaCategoryRepository : AllDealsEntitiesRepositoryBase<MetaCategory>, IMetaCategoryRepository
	{
		public MetaCategoryRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : Servers | Type Name : Server
	public partial interface IServerRepository : IRepository<Server>
	{
	}
	
	[EntitySetName("Servers")]
	public partial class ServerRepository : AllDealsEntitiesRepositoryBase<Server>, IServerRepository
	{
		public ServerRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : States | Type Name : State
	public partial interface IStateRepository : IRepository<State>
	{
	}
	
	[EntitySetName("States")]
	public partial class StateRepository : AllDealsEntitiesRepositoryBase<State>, IStateRepository
	{
		public StateRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	public abstract partial class DoplatkyEntitiesRepositoryBase<TEntity> : EdmRepositoryBase<DoplatkyEntities, TEntity>
		where TEntity : class
	{
		private readonly IUnitOfWorkManager _unitOfWorkManager;
		
		protected DoplatkyEntitiesRepositoryBase(IUnitOfWorkManager unitOfWorkManager)
		{
			_unitOfWorkManager = unitOfWorkManager;
		}
		
		protected override DoplatkyEntities ObjectContext
		{
			get
			{
				return _unitOfWorkManager.GetCurrent<DoplatkyEntities>().Context.AsActual<DoplatkyEntities>();
			}
		}
	}

	// Entity Set : ATCs | Type Name : ATC
	public partial interface IATCRepository : IRepository<ATC>
	{
	}
	
	[EntitySetName("ATCs")]
	public partial class ATCRepository : DoplatkyEntitiesRepositoryBase<ATC>, IATCRepository
	{
		public ATCRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : Comments | Type Name : Comment
	public partial interface ICommentRepository : IRepository<Comment>
	{
	}
	
	[EntitySetName("Comments")]
	public partial class CommentRepository : DoplatkyEntitiesRepositoryBase<Comment>, ICommentRepository
	{
		public CommentRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : Countries | Type Name : Country
	public partial interface ICountryRepository : IRepository<Country>
	{
	}
	
	[EntitySetName("Countries")]
	public partial class CountryRepository : DoplatkyEntitiesRepositoryBase<Country>, ICountryRepository
	{
		public CountryRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : DDDMeasureUnits | Type Name : DDDMeasureUnit
	public partial interface IDDDMeasureUnitRepository : IRepository<DDDMeasureUnit>
	{
	}
	
	[EntitySetName("DDDMeasureUnits")]
	public partial class DDDMeasureUnitRepository : DoplatkyEntitiesRepositoryBase<DDDMeasureUnit>, IDDDMeasureUnitRepository
	{
		public DDDMeasureUnitRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : Deliveries | Type Name : Delivery
	public partial interface IDeliveryRepository : IRepository<Delivery>
	{
	}
	
	[EntitySetName("Deliveries")]
	public partial class DeliveryRepository : DoplatkyEntitiesRepositoryBase<Delivery>, IDeliveryRepository
	{
		public DeliveryRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : Drugs | Type Name : Drug
	public partial interface IDrugRepository : IRepository<Drug>
	{
	}
	
	[EntitySetName("Drugs")]
	public partial class DrugRepository : DoplatkyEntitiesRepositoryBase<Drug>, IDrugRepository
	{
		public DrugRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : Forms | Type Name : Form
	public partial interface IFormRepository : IRepository<Form>
	{
	}
	
	[EntitySetName("Forms")]
	public partial class FormRepository : DoplatkyEntitiesRepositoryBase<Form>, IFormRepository
	{
		public FormRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : IndicationGroups | Type Name : IndicationGroup
	public partial interface IIndicationGroupRepository : IRepository<IndicationGroup>
	{
	}
	
	[EntitySetName("IndicationGroups")]
	public partial class IndicationGroupRepository : DoplatkyEntitiesRepositoryBase<IndicationGroup>, IIndicationGroupRepository
	{
		public IndicationGroupRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : Organizations | Type Name : Organization
	public partial interface IOrganizationRepository : IRepository<Organization>
	{
	}
	
	[EntitySetName("Organizations")]
	public partial class OrganizationRepository : DoplatkyEntitiesRepositoryBase<Organization>, IOrganizationRepository
	{
		public OrganizationRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : Pharmacies | Type Name : Pharmacy
	public partial interface IPharmacyRepository : IRepository<Pharmacy>
	{
	}
	
	[EntitySetName("Pharmacies")]
	public partial class PharmacyRepository : DoplatkyEntitiesRepositoryBase<Pharmacy>, IPharmacyRepository
	{
		public PharmacyRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : Places | Type Name : Place
	public partial interface IPlaceRepository : IRepository<Place>
	{
	}
	
	[EntitySetName("Places")]
	public partial class PlaceRepository : DoplatkyEntitiesRepositoryBase<Place>, IPlaceRepository
	{
		public PlaceRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : PopularSearches | Type Name : PopularSearch
	public partial interface IPopularSearchRepository : IRepository<PopularSearch>
	{
	}
	
	[EntitySetName("PopularSearches")]
	public partial class PopularSearchRepository : DoplatkyEntitiesRepositoryBase<PopularSearch>, IPopularSearchRepository
	{
		public PopularSearchRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : PostalCodes | Type Name : PostalCode
	public partial interface IPostalCodeRepository : IRepository<PostalCode>
	{
	}
	
	[EntitySetName("PostalCodes")]
	public partial class PostalCodeRepository : DoplatkyEntitiesRepositoryBase<PostalCode>, IPostalCodeRepository
	{
		public PostalCodeRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : Posts | Type Name : Post
	public partial interface IPostRepository : IRepository<Post>
	{
	}
	
	[EntitySetName("Posts")]
	public partial class PostRepository : DoplatkyEntitiesRepositoryBase<Post>, IPostRepository
	{
		public PostRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : Prices | Type Name : Price
	public partial interface IPriceRepository : IRepository<Price>
	{
	}
	
	[EntitySetName("Prices")]
	public partial class PriceRepository : DoplatkyEntitiesRepositoryBase<Price>, IPriceRepository
	{
		public PriceRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : Roles | Type Name : Role
	public partial interface IRoleRepository : IRepository<Role>
	{
	}
	
	[EntitySetName("Roles")]
	public partial class RoleRepository : DoplatkyEntitiesRepositoryBase<Role>, IRoleRepository
	{
		public RoleRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : Searches | Type Name : Search
	public partial interface ISearchRepository : IRepository<Search>
	{
	}
	
	[EntitySetName("Searches")]
	public partial class SearchRepository : DoplatkyEntitiesRepositoryBase<Search>, ISearchRepository
	{
		public SearchRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : SignUpRequests | Type Name : SignUpRequest
	public partial interface ISignUpRequestRepository : IRepository<SignUpRequest>
	{
	}
	
	[EntitySetName("SignUpRequests")]
	public partial class SignUpRequestRepository : DoplatkyEntitiesRepositoryBase<SignUpRequest>, ISignUpRequestRepository
	{
		public SignUpRequestRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : Users | Type Name : User
	public partial interface IUserRepository : IRepository<User>
	{
	}
	
	[EntitySetName("Users")]
	public partial class UserRepository : DoplatkyEntitiesRepositoryBase<User>, IUserRepository
	{
		public UserRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}

	// Entity Set : Wrappings | Type Name : Wrapping
	public partial interface IWrappingRepository : IRepository<Wrapping>
	{
	}
	
	[EntitySetName("Wrappings")]
	public partial class WrappingRepository : DoplatkyEntitiesRepositoryBase<Wrapping>, IWrappingRepository
	{
		public WrappingRepository(IUnitOfWorkManager unitOfWorkManager) : base(unitOfWorkManager)
		{
		}
	}


	// Dependency injection configuration
	public class TestNinjectModule : global::Ninject.Modules.NinjectModule
	{
		public override void Load()
		{
			// AllDealsEntities
			Kernel.Bind<IActualDealRepository>().To<ActualDealRepository>().InSingletonScope();
			Kernel.Bind<IRepository<ActualDeal>>().To<ActualDealRepository>().InSingletonScope();
			Kernel.Bind<ICategoryRepository>().To<CategoryRepository>().InSingletonScope();
			Kernel.Bind<IRepository<Category>>().To<CategoryRepository>().InSingletonScope();
			Kernel.Bind<ICityRepository>().To<CityRepository>().InSingletonScope();
			Kernel.Bind<IRepository<City>>().To<CityRepository>().InSingletonScope();
			Kernel.Bind<IHistoryDealRepository>().To<HistoryDealRepository>().InSingletonScope();
			Kernel.Bind<IRepository<HistoryDeal>>().To<HistoryDealRepository>().InSingletonScope();
			Kernel.Bind<IIndexRepository>().To<IndexRepository>().InSingletonScope();
			Kernel.Bind<IRepository<Index>>().To<IndexRepository>().InSingletonScope();
			Kernel.Bind<ILogRepository>().To<LogRepository>().InSingletonScope();
			Kernel.Bind<IRepository<Log>>().To<LogRepository>().InSingletonScope();
			Kernel.Bind<IMetaCategoryRepository>().To<MetaCategoryRepository>().InSingletonScope();
			Kernel.Bind<IRepository<MetaCategory>>().To<MetaCategoryRepository>().InSingletonScope();
			Kernel.Bind<IServerRepository>().To<ServerRepository>().InSingletonScope();
			Kernel.Bind<IRepository<Server>>().To<ServerRepository>().InSingletonScope();
			Kernel.Bind<IStateRepository>().To<StateRepository>().InSingletonScope();
			Kernel.Bind<IRepository<State>>().To<StateRepository>().InSingletonScope();
			// DoplatkyEntities
			Kernel.Bind<IATCRepository>().To<ATCRepository>().InSingletonScope();
			Kernel.Bind<IRepository<ATC>>().To<ATCRepository>().InSingletonScope();
			Kernel.Bind<ICommentRepository>().To<CommentRepository>().InSingletonScope();
			Kernel.Bind<IRepository<Comment>>().To<CommentRepository>().InSingletonScope();
			Kernel.Bind<ICountryRepository>().To<CountryRepository>().InSingletonScope();
			Kernel.Bind<IRepository<Country>>().To<CountryRepository>().InSingletonScope();
			Kernel.Bind<IDDDMeasureUnitRepository>().To<DDDMeasureUnitRepository>().InSingletonScope();
			Kernel.Bind<IRepository<DDDMeasureUnit>>().To<DDDMeasureUnitRepository>().InSingletonScope();
			Kernel.Bind<IDeliveryRepository>().To<DeliveryRepository>().InSingletonScope();
			Kernel.Bind<IRepository<Delivery>>().To<DeliveryRepository>().InSingletonScope();
			Kernel.Bind<IDrugRepository>().To<DrugRepository>().InSingletonScope();
			Kernel.Bind<IRepository<Drug>>().To<DrugRepository>().InSingletonScope();
			Kernel.Bind<IFormRepository>().To<FormRepository>().InSingletonScope();
			Kernel.Bind<IRepository<Form>>().To<FormRepository>().InSingletonScope();
			Kernel.Bind<IIndicationGroupRepository>().To<IndicationGroupRepository>().InSingletonScope();
			Kernel.Bind<IRepository<IndicationGroup>>().To<IndicationGroupRepository>().InSingletonScope();
			Kernel.Bind<IOrganizationRepository>().To<OrganizationRepository>().InSingletonScope();
			Kernel.Bind<IRepository<Organization>>().To<OrganizationRepository>().InSingletonScope();
			Kernel.Bind<IPharmacyRepository>().To<PharmacyRepository>().InSingletonScope();
			Kernel.Bind<IRepository<Pharmacy>>().To<PharmacyRepository>().InSingletonScope();
			Kernel.Bind<IPlaceRepository>().To<PlaceRepository>().InSingletonScope();
			Kernel.Bind<IRepository<Place>>().To<PlaceRepository>().InSingletonScope();
			Kernel.Bind<IPopularSearchRepository>().To<PopularSearchRepository>().InSingletonScope();
			Kernel.Bind<IRepository<PopularSearch>>().To<PopularSearchRepository>().InSingletonScope();
			Kernel.Bind<IPostalCodeRepository>().To<PostalCodeRepository>().InSingletonScope();
			Kernel.Bind<IRepository<PostalCode>>().To<PostalCodeRepository>().InSingletonScope();
			Kernel.Bind<IPostRepository>().To<PostRepository>().InSingletonScope();
			Kernel.Bind<IRepository<Post>>().To<PostRepository>().InSingletonScope();
			Kernel.Bind<IPriceRepository>().To<PriceRepository>().InSingletonScope();
			Kernel.Bind<IRepository<Price>>().To<PriceRepository>().InSingletonScope();
			Kernel.Bind<IRoleRepository>().To<RoleRepository>().InSingletonScope();
			Kernel.Bind<IRepository<Role>>().To<RoleRepository>().InSingletonScope();
			Kernel.Bind<ISearchRepository>().To<SearchRepository>().InSingletonScope();
			Kernel.Bind<IRepository<Search>>().To<SearchRepository>().InSingletonScope();
			Kernel.Bind<ISignUpRequestRepository>().To<SignUpRequestRepository>().InSingletonScope();
			Kernel.Bind<IRepository<SignUpRequest>>().To<SignUpRequestRepository>().InSingletonScope();
			Kernel.Bind<IUserRepository>().To<UserRepository>().InSingletonScope();
			Kernel.Bind<IRepository<User>>().To<UserRepository>().InSingletonScope();
			Kernel.Bind<IWrappingRepository>().To<WrappingRepository>().InSingletonScope();
			Kernel.Bind<IRepository<Wrapping>>().To<WrappingRepository>().InSingletonScope();
		}
	}
}

