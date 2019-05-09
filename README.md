## Core.Infrastructure
Core.Infrastructure .Net Core 2.x support !

## IoC
ASP.NET Core Dependency 

## Principles
SOLID <br/>
Domain Driven Design

## Persistance
EntityFramework Core<br/>
Dapper<br/>

## Object Mappers
AutoMapper

## Cache
In-Memory
Redis

## Object Validation
FluentValidation

## Log
Serilog support
Elasticsearch
Kibana

## Documentation
Swagger


## Benefits
 - Conventional Dependency Registering
 - Async await first 
 - Multi Tenancy
 - GlobalQuery Filtering
 - Domain Driven Design Concepts
 - Repository and UnitofWork pattern implementations
 - Object to object mapping with abstraction
 - Net Core 2.x support
 - Auto object validation support
 - Aspect Oriented Programming
 - Simple Usage
 - Modularity
 - Event Sourcing
 
   

***Basic Usage***

     WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>();
                         
***MultiTenancy Activation***

    var connectionString = config["mysqlconnection:connectionString"];
            services.AddDbContext<ACv2Context>(o => o.UseSqlServer(connectionString));
***Conventional Registration***	 	

      services.AddScoped<IUserStoreService, UserStoreService>();
                             ...
                         })

***FluentValidators Activation***

     services.ConfigureFluentValidation();

     public static void ConfigureFluentValidation(this IServiceCollection services)
        {
            //services.AddTransient<IValidator<IValidator<Domain.Context.Entities.Galley>>, GalleyValidatorValidator>();
            services.AddTransient<IValidator<RefType>, RefTypeValidator>();
            services.AddTransient<IValidator<RefType>, RefTypeValidator>();
                RuleFor(t => t.Name).NotEmpty().MinimumLength(3);
                ...
            }
        }

  
***AutoMapper Activation***

	 services.AddAutoMapper();
	 
***Swagger Activation***

	 services.ConfigureSwagger();


***Serilog Activation***
 

        services.ConfigureLogger(Configuration);
		
		 Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "Core.Infrastructure.Presentation.API")
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                //.WriteTo.File(new JsonFormatter(), "log.json")
                //.ReadFrom.Configuration(configuration)
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                    FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
                    EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                       EmitEventFailureHandling.WriteToFailureSink |
                                       EmitEventFailureHandling.RaiseCallback,
                    FailureSink = new FileSink("log.json", new JsonFormatter(), null)
                })
                .MinimumLevel.Verbose()
                .CreateLogger();
            Log.Information("WebApi is Starting...");
		
		

***Interceptors Activation***

     public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                Log.Write(LogEventLevel.Information, "Service path is:" + context.Request.Path.Value,
                    context.Request.Body);
                await next(context);
            }
            catch (HttpRequestException ex)
            {
                Log.Write(LogEventLevel.Error, ex.Message, "Service path is:" + context.Request.Path.Value, ex);
                await HandleExceptionAsync(context, ex);
            }
            catch (AuthenticationException ex)
            {
                Log.Write(LogEventLevel.Error, ex.Message, "Service path is:" + context.Request.Path.Value, ex);
                await HandleExceptionAsync(context, ex);
            }
            catch (BusinessException ex)
            {
                Log.Write(LogEventLevel.Error, ex.Message, "Service path is:" + context.Request.Path.Value, ex);
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                Log.Write(LogEventLevel.Error, ex.Message, ex.Source, ex.TargetSite, ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, object exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            var message = string.Empty;
            var RC = string.Empty;

            if (exception.GetType() == typeof(HttpRequestException))
            {
                code = HttpStatusCode.NotFound;
                RC = ResponseMessage.NotFound;
                message = BusinessException.GetDescription(RC);
            }
            else if (exception.GetType() == typeof(AuthenticationException))
            {
                code = HttpStatusCode.Unauthorized;
                RC = ResponseMessage.Unauthorized;
                message = BusinessException.GetDescription(RC);
            }
            else if (exception.GetType() == typeof(BusinessException))
            {
                var businesException = (BusinessException) exception;
                message = BusinessException.GetDescription(businesException.RC, businesException.param1);
                code = HttpStatusCode.InternalServerError;
                RC = businesException.RC;
            }
            else if (exception.GetType() == typeof(Exception))
            {
                code = HttpStatusCode.BadRequest;
                RC = ResponseMessage.BadRequest;
                message = BusinessException.GetDescription(RC);
            }

            var response = new Error
            {
                Message = message,
                RC = RC
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

***AggregateRoot definations***

   public interface IRepository<T> where T : class
    {
        /// <summary>
        ///     UserManager
        /// </summary>
        UserManager<IdentityUser<string>> UserManager { get; set; }

        /// <summary>
        ///     RoleManager
        /// </summary>
        RoleManager<IdentityUserRole<string>> RoleManager { get; set; }

        IEnumerable<T> FindAll();
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteBulk(IEnumerable<T> entity);
        void InsertBulk(IEnumerable<T> entity);
        void UpdateBulk(IEnumerable<T> entity);
        void Save();
        T GetByKey(int key);
        T GetByKey(string key);
        T GetByKey(object key);
        /// <summary>
        /// Query Method
        /// </summary>
        /// <returns>RepositoryQueryHelper (Sorgu Yardımcı Sınıfı)</returns>
        IRepositoryQueryHelper<T> Query();

        /// <summary>
        /// To Set Data to Table, Definition Some Helper Parameters
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IQueryable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includeProperties = null,
            int? page = null,
            int? pageSize = null);
    }
	
	
        
***Application Service definations***

    // Query methods all comes from base class. You can override if you want!
     public class RefTypeService : IRefTypeService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public RefTypeService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
		
		....
	}
	
    // CRUD methods all comes from base class. You can override if you want!
     public class RefTypeService : IRefTypeService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public RefTypeService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
		
		....
	}
	
	
***CommandHandlers definations***

     /// <summary>
        /// To filter Operations
        /// </summary>
        /// <param name="pFilter">filter exp. : <code>.Filter(x=>x.EntityProperty != null)</code></param>
        /// <returns>Added filter functionality to RepositoryQueryHelper Class</returns>
        IRepositoryQueryHelper<T> Filter(Expression<Func<T, bool>> pFilter);
        /// <summary>
        /// To Order Operations
        /// </summary>
        /// <param name="orderBy">Order Exp. <code>.OrderBy(x => x.OrderBy(y => y.EntityProperty).ThenBy(z => z.EntityProperty2))</code></param>
        /// <returns>Added filter functionality to RepositoryQueryHelper Class</returns>
        IRepositoryQueryHelper<T> OrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);
        /// <summary>
        /// To GroupBy Operations
        /// </summary>
        /// <param name="groupBy">GroupBy Operations <code>.OrderBy(x => x.OrderBy(y => y.EntityProperty).ThenBy(z => z.EntityProperty2))</code></param>
        /// <returns>Added GroupBy functionality to RepositoryQueryHelper Class</returns>
        IRepositoryQueryHelper<T> GroupBy(Func<IQueryable<T>, IGrouping<int, GroupCountResult>> groupBy);
        /// <summary>
        /// To Include Operations (Set Included Tables Data)
        /// </summary>
        /// <param name="expression">Include Exp. <code>.Include(x=>x.EntityName)</code></param>
        /// <returns>Added Include functionality to RepositoryQueryHelper Class</returns>
        IRepositoryQueryHelper<T> Include(Expression<Func<T, object>> expression);
        /// <summary>
        /// To paging by filtered, ordered and included or not
        /// </summary>
        /// <param name="pPage">Page Number</param>
        /// <param name="pPageSize">Data number</param>
        /// <param name="totalCount">Toplam Kayıt Sayısı</param>
        /// <returns>Data List</returns>
        IEnumerable<T> GetPage(
            int pPage, int pPageSize, out int totalCount);
        /// <summary>
        /// To get all data by filtered, ordered and included or not
        /// </summary>
        /// <returns>Data List</returns>
        IEnumerable<T> Get(bool isAsNoTracking = false);

        /// <summary>
        /// To get all data by filtered, ordered and included or not
        /// </summary>
        /// <returns>Data List</returns>
        T GetFirst();
		
		

***Dapper Repository definations***
 

  public class RefTypeDapperRepository : IDeliveryPlanDetailDapperRepository
    {
        private static string cnString =
            "Data Source = 10.22.0.201; Initial Catalog = ACv2; Persist Security Info=True;User ID = usr_webapp; Password=passw0rd;";
			...
			
			 public IEnumerable<DeliveryPlanDetail> GetRefType(GetDeliveriyPlanDetailRequestDTO request)
        {
            IEnumerable<DeliveryPlanDetail> entities = new List<DeliveryPlanDetail>();
            using (IDbConnection con = new SqlConnection(cnString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameters=new DynamicParameters();
                parameters.Add("@id", request.Id);
                parameters.Add("@insertDate",request.InsertDate);
               

                StringBuilder query= new StringBuilder();
                query.Append("select * from RefType rt");
                query.Append(" where");
                query.Append(" rt.Id == @id)");
                query.Append(" rt.InsertDate >= @insertDate ");

                entities = con.Query<RefTYpe>(query.ToString(),parameters).ToList();
            }

            return entities;
        }
			}


***EntityFrameworkCore definations***
   

     public class Context : IdentityDbContext<IdentityUser>
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<RefType> RefType { get; set; }
        public virtual DbSet<RefValue> RefValue { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(
                    "Data Source=10.22.0.161;Initial Catalog=DOCO_TEST;Integrated Security=SSPI; Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
     

***Log Service definations***

              public static ResponseBaseDTO Make(T entity, string methodName)
        {
           
            string message = string.Empty;
            if (entity!=null)
                message = ResponseMessage.GetDescription(ResponseMessage.Success, methodName);
            else
                message = ResponseMessage.GetDescription(ResponseMessage.NotFound, methodName);
            ResponseBaseDTO response= new ResponseBaseDTO
            {
                Data = entity,
                Message = message,
                Information = new Information
                {
                    TrackId = Guid.NewGuid().ToString()
                },
                RC = ResponseMessage.Success
            };
           Log.Write(LogEventLevel.Information, message,response);
           return response;
        }

        public static ResponseBaseDTO Make(IEnumerable<T> entity, string methodName)
        {
            string message = string.Empty;
            if (entity.Count() > 0)
                message = ResponseMessage.GetDescription(ResponseMessage.Success, methodName);
            else
                message = ResponseMessage.GetDescription(ResponseMessage.NotFound, methodName);

            ResponseBaseDTO response = new ResponseBaseDTO
            {
                Data = entity,
                Message = message,
                Information = new Information
                {
                    TrackId = Guid.NewGuid().ToString()
                },
                RC = ResponseMessage.Success
            };
            Log.Write(LogEventLevel.Information, message, response);
            return response;
        }
