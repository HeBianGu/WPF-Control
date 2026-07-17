# Database 数据库二次开发文档

**适用项目：** `H.DataBases.Sqlite`、`H.DataBases.Share`、`H.Extensions.DataBase`、`H.Extensions.DataBase.Repository`  
**核心技术：** `Entity Framework Core`、`SQLite`、仓储模式、设置系统、数据库迁移  
**参考示例：** `Source/Tests/H.Test.Sqlite`

本文介绍 WPF-Control 中数据库能力的二次开发方式，重点说明 SQLite 数据库配置、`DbContext` 注册、实体模型、仓储模型、启动连接检测、数据库迁移和常见业务绑定方式。

---

## 1. 数据库模块定位

数据库相关能力按职责拆分为多个项目：

| 项目 | 说明 |
|---|---|
| `H.Extensions.DataBase` | 定义实体基类、仓储接口、线程 DbContext 扩展等基础能力。 |
| `H.DataBases.Share` | 提供数据库设置基类、连接服务基类、仓储实现基类。 |
| `H.DataBases.Sqlite` | 提供 SQLite 设置项、SQLite DbContext 注册扩展和连接服务。 |
| `H.Extensions.DataBase.Repository` | 提供可绑定仓储模型，适合 UI 表格增删改查。 |
| `Source/Tests/H.Test.Sqlite` | SQLite 使用示例，包含 `DbContext`、实体和迁移文件。 |

典型使用流程：

```text
定义实体 Entity
    ↓
定义 DbContext
    ↓
注册 AddDbContextBySetting<TDbContext>()
    ↓
注册仓储 IStringRepository<TEntity>
    ↓
启动时执行 IDbConnectService.Load / Database.Migrate
    ↓
业务中通过仓储增删改查
```

---

## 2. 引用项目和包

应用项目通常需要引用：

```xml
<ProjectReference Include="..\..\Extensions\H.Extensions.DataBase\H.Extensions.DataBase.csproj" />
<ProjectReference Include="..\..\Extensions\H.Extensions.DataBase.Repository\H.Extensions.DataBase.Repository.csproj" />
<ProjectReference Include="..\..\DataBases\H.DataBases.Share\H.DataBases.Share.csproj" />
<ProjectReference Include="..\..\DataBases\H.DataBases.Sqlite\H.DataBases.Sqlite.csproj" />
```

SQLite 基于 EF Core，通常需要相关 NuGet 包：

- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Sqlite`
- `Microsoft.EntityFrameworkCore.Proxies`
- `Microsoft.EntityFrameworkCore.Design`（迁移时需要）
- `Microsoft.EntityFrameworkCore.Tools`（包管理器控制台迁移时需要）

---

## 3. SQLite 配置项

SQLite 配置由 `SqliteSettable` 提供：

```csharp
[Display(Name = "数据库配置", GroupName = SettingGroupNames.GroupData)]
public class SqliteSettable : SqliteSettableBase<SqliteSettable>, ISqliteSettable, IDbSettable
{
}
```

`SqliteSettableBase` 的关键属性：

```csharp
[Display(Name = "数据库文件夹路径", Description = "数据库保存的文件夹路径")]
public string FilePath { get; set; }

[DefaultValue("data.db")]
[Display(Name = "数据库名称", Description = "数据库文件的名称")]
public string InitialCatalog { get; set; }
```

默认值规则：

```csharp
public override void LoadDefault()
{
    base.LoadDefault();
    this.FilePath = AppPaths.Instance.Data;
}
```

连接字符串：

```csharp
public override string GetConnect()
{
    return $"Data Source={this.GetDBFilePath()}";
}
```

最终数据库路径：

```text
{AppPaths.Instance.Data}\data.db
```

---

## 4. 注册 SQLite DbContext

`H.DataBases.Sqlite` 提供 `AddDbContextBySetting<TDbContext>()`：

```csharp
public static void AddDbContextBySetting<TDbContext>(this IServiceCollection services, Action<ISqliteSettable> action = null)
    where TDbContext : DbContext
{
    action?.Invoke(SqliteSettable.Instance);
    SqliteSettable.Instance.Load(out string messge);
    string connect = SqliteSettable.Instance.GetConnect();
    services.AddDbContext<TDbContext>(x => x.UseLazyLoadingProxies().UseSqlite(connect));
    services.AddSingleton<IDbConnectService, SqliteDbConnectService<TDbContext>>();
    services.AddSingleton<IDbDisconnectService, SqliteDbDisconnectService<TDbContext>>();
}
```

应用注册示例：

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddSetting();
    services.AddWindowMessage();
    services.AddWindowDialogMessage();

    services.AddDbContextBySetting<MyDataContext>(option =>
    {
        option.InitialCatalog = "myapp.db";
        // option.FilePath = AppPaths.Instance.Data;
    });
}
```

在配置管线中把 SQLite 配置加入设置页：

```csharp
protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);
    app.UseSqlite();
}
```

`UseSqlite()` 内部会执行：

```csharp
IocSetting.Instance.Add(SqliteSettable.Instance);
```

### 4.1 `UseLazyLoadingProxies` 配置

当前 SQLite 注册扩展默认启用了 EF Core 懒加载代理：

```csharp
services.AddDbContext<TDbContext>(x => x.UseLazyLoadingProxies().UseSqlite(connect));
```

`UseLazyLoadingProxies()` 的作用是：当访问实体的导航属性时，如果导航属性尚未加载，EF Core 会自动查询数据库并填充该属性。

启用条件：

- 项目引用 `Microsoft.EntityFrameworkCore.Proxies`。
- `DbContext` 使用 `UseLazyLoadingProxies()` 注册。
- 实体类不能是 `sealed`。
- 需要懒加载的导航属性必须声明为 `virtual`。
- 实体仍处于 EF Core 跟踪状态，且关联的 `DbContext` 尚未释放。

实体示例：

```csharp
public class Blog : DbModelBase
{
    [Display(Name = "名称")]
    public string Name { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}

public class Post : DbModelBase
{
    public string Title { get; set; }
    public string BlogID { get; set; }
    public virtual Blog Blog { get; set; }
}
```

访问示例：

```csharp
Blog blog = await dbContext.Set<Blog>().FirstAsync();
int count = blog.Posts.Count; // 访问 Posts 时自动查询
```

如果不希望使用懒加载，可改为显式 `Include`：

```csharp
var blogs = await dbContext.Set<Blog>()
    .Include(x => x.Posts)
    .ToListAsync();
```

也可以在自定义注册中去掉 `UseLazyLoadingProxies()`：

```csharp
services.AddDbContext<MyDataContext>(x => x.UseSqlite(connect));
```

使用建议：

- 简单后台管理、设置工具可以使用懒加载，代码更简洁。
- 大列表、批量导出、复杂报表建议使用 `Include` 或投影查询，避免 N+1 查询。
- UI 绑定导航属性时注意懒加载可能触发数据库访问，必要时提前 `Include`。

---

## 5. 多 DbContext 配置

如果应用有多个 DbContext，可使用 `AddDbContextNewSetting<TDbContext>()` 为每个上下文创建独立配置：

```csharp
services.AddDbContextNewSetting<OrderDataContext>(option =>
{
    option.InitialCatalog = "order.db";
});

services.AddDbContextNewSetting<LogDataContext>(option =>
{
    option.InitialCatalog = "log.db";
});
```

该方法会创建新的 `SqliteSettable` 实例，并使用 `typeof(TDbContext).Name` 作为 `ID` 和子目录的一部分，适合多数据库场景。

---

## 6. 定义实体模型

### 6.1 实体基类

`H.Extensions.DataBase` 提供多个实体基类：

| 类型 | 主键 | 说明 |
|---|---|---|
| `EntityBase<TPrimaryKey>` | 泛型 | 所有实体基类，包含 `ID`。 |
| `StringEntityBase` | `string` | 默认构造时生成 `Guid.NewGuid().ToString()`。 |
| `GuidEntityBase` | `Guid` | 默认构造时生成 `Guid.NewGuid()`。 |
| `DbModelBase` | `string` | 增加创建时间、修改时间、启用状态和校验支持。 |

`EntityBase<TPrimaryKey>`：

```csharp
public abstract class EntityBase<TPrimaryKey> : IEntityBase<TPrimaryKey>
{
    [Browsable(false)]
    [ReadOnly(true)]
    [Column("id", Order = 0)]
    public virtual TPrimaryKey ID { get; set; }
}
```

`DbModelBase`：

```csharp
public abstract class DbModelBase : StringEntityBase, INotifyPropertyChanged, IDataErrorInfo
{
    public DateTime CDATE { get; set; }
    public DateTime UDATE { get; set; }
    public int ISENBLED { get; set; } = 1;
}
```

### 6.2 实体示例

参考 `H.Test.Sqlite/mbc_dv_image.cs`：

```csharp
public class ImageEntity : DbModelBase
{
    [Required]
    [Display(Name = "资源名称")]
    public string Name { get; set; }

    [Display(Name = "资源类型")]
    public string MediaType { get; set; }

    [Required]
    [Display(Name = "资源路径")]
    public string Url { get; set; }

    [Display(Name = "文件大小")]
    public long Size { get; set; }
}
```

建议：

- 普通业务实体优先继承 `DbModelBase`。
- 所有需要表单编辑的属性添加 `[Display]`。
- 必填字段添加 `[Required]`。
- 主键、系统字段一般保持隐藏。

---

## 7. 定义 DbContext

参考 `H.Test.Sqlite/MyDataContext.cs`：

```csharp
public class MyDataContext : DbContext
{
    public MyDataContext(DbContextOptions<MyDataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<ImageEntity> Images { get; set; }
}
```

如果需要配置表名、索引、关系，可在 `OnModelCreating` 中配置：

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<ImageEntity>(entity =>
    {
        entity.ToTable("images");
        entity.HasIndex(x => x.Name);
        entity.Property(x => x.Name).IsRequired();
    });
}
```

### 7.1 表名、主键、字段和索引配置

常见表映射配置：

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<ImageEntity>(entity =>
    {
        entity.ToTable("images");
        entity.HasKey(x => x.ID);

        entity.Property(x => x.ID)
            .HasColumnName("id")
            .HasMaxLength(36);

        entity.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(200)
            .IsRequired();

        entity.Property(x => x.CDATE)
            .HasColumnName("created_at");

        entity.HasIndex(x => x.Name)
            .HasDatabaseName("idx_images_name");
    });
}
```

常用 API：

| API | 说明 |
|---|---|
| `ToTable("table_name")` | 配置表名。 |
| `HasKey(x => x.ID)` | 配置主键。 |
| `Property(x => x.Name)` | 配置字段。 |
| `HasColumnName("name")` | 配置列名。 |
| `HasMaxLength(200)` | 配置最大长度。 |
| `IsRequired()` | 配置非空。 |
| `HasDefaultValue(...)` | 配置默认值。 |
| `HasIndex(x => x.Name)` | 配置索引。 |
| `HasDatabaseName("idx_name")` | 配置索引名。 |

### 7.2 一对多关系配置

实体示例：

```csharp
public class Blog : DbModelBase
{
    public string Name { get; set; }
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}

public class Post : DbModelBase
{
    public string Title { get; set; }
    public string BlogID { get; set; }
    public virtual Blog Blog { get; set; }
}
```

关系映射：

```csharp
modelBuilder.Entity<Blog>(entity =>
{
    entity.ToTable("blogs");
    entity.HasKey(x => x.ID);
});

modelBuilder.Entity<Post>(entity =>
{
    entity.ToTable("posts");
    entity.HasKey(x => x.ID);

    entity.HasOne(x => x.Blog)
        .WithMany(x => x.Posts)
        .HasForeignKey(x => x.BlogID)
        .OnDelete(DeleteBehavior.Cascade);
});
```

含义：

- 一个 `Blog` 拥有多个 `Post`。
- 一个 `Post` 通过 `BlogID` 关联一个 `Blog`。
- 删除 `Blog` 时级联删除其 `Posts`。

### 7.3 一对一关系配置

实体示例：

```csharp
public class User : DbModelBase
{
    public string Name { get; set; }
    public virtual UserProfile Profile { get; set; }
}

public class UserProfile : DbModelBase
{
    public string UserID { get; set; }
    public string Avatar { get; set; }
    public virtual User User { get; set; }
}
```

关系映射：

```csharp
modelBuilder.Entity<User>(entity =>
{
    entity.ToTable("users");
    entity.HasKey(x => x.ID);
});

modelBuilder.Entity<UserProfile>(entity =>
{
    entity.ToTable("user_profiles");
    entity.HasKey(x => x.ID);

    entity.HasOne(x => x.User)
        .WithOne(x => x.Profile)
        .HasForeignKey<UserProfile>(x => x.UserID)
        .OnDelete(DeleteBehavior.Cascade);
});
```

### 7.4 多对多关系配置

EF Core 可通过中间实体显式配置多对多关系，便于扩展字段。

```csharp
public class Student : DbModelBase
{
    public string Name { get; set; }
    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
}

public class Course : DbModelBase
{
    public string Name { get; set; }
    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
}

public class StudentCourse
{
    public string StudentID { get; set; }
    public virtual Student Student { get; set; }

    public string CourseID { get; set; }
    public virtual Course Course { get; set; }
}
```

映射：

```csharp
modelBuilder.Entity<StudentCourse>(entity =>
{
    entity.ToTable("student_courses");
    entity.HasKey(x => new { x.StudentID, x.CourseID });

    entity.HasOne(x => x.Student)
        .WithMany(x => x.StudentCourses)
        .HasForeignKey(x => x.StudentID)
        .OnDelete(DeleteBehavior.Cascade);

    entity.HasOne(x => x.Course)
        .WithMany(x => x.StudentCourses)
        .HasForeignKey(x => x.CourseID)
        .OnDelete(DeleteBehavior.Cascade);
});
```

如果不需要中间实体扩展字段，也可使用 EF Core 的 `UsingEntity` 简化配置：

```csharp
modelBuilder.Entity<Student>()
    .HasMany<Course>()
    .WithMany()
    .UsingEntity(j => j.ToTable("student_courses"));
```

### 7.5 级联删除和删除行为

EF Core 使用 `OnDelete(DeleteBehavior.xxx)` 配置删除行为。

| 删除行为 | 说明 | 适用场景 |
|---|---|---|
| `DeleteBehavior.Cascade` | 删除主表时自动删除从表数据。 | 父子强依赖，如订单和订单明细。 |
| `DeleteBehavior.Restrict` | 阻止删除仍被引用的主表数据。 | 字典、分类、角色等被引用数据。 |
| `DeleteBehavior.SetNull` | 删除主表时把外键设置为 `null`。 | 从表允许孤立存在，外键必须可空。 |
| `DeleteBehavior.ClientSetNull` | EF 客户端尝试置空，数据库不一定配置级联。 | 需要由 EF 跟踪实体处理关系。 |
| `DeleteBehavior.NoAction` | 数据库不执行级联动作。 | 手动控制删除顺序。 |

级联删除示例：

```csharp
entity.HasOne(x => x.Blog)
    .WithMany(x => x.Posts)
    .HasForeignKey(x => x.BlogID)
    .OnDelete(DeleteBehavior.Cascade);
```

限制删除示例：

```csharp
entity.HasOne(x => x.Category)
    .WithMany(x => x.Products)
    .HasForeignKey(x => x.CategoryID)
    .OnDelete(DeleteBehavior.Restrict);
```

置空示例：

```csharp
public string? CategoryID { get; set; }
public virtual Category Category { get; set; }

entity.HasOne(x => x.Category)
    .WithMany(x => x.Products)
    .HasForeignKey(x => x.CategoryID)
    .OnDelete(DeleteBehavior.SetNull);
```

> 使用 `SetNull` 时，外键属性必须允许为空。例如启用可空引用类型时应声明为 `string? CategoryID`。

SQLite 注意事项：

- SQLite 支持外键和级联删除，但需要启用 foreign keys；EF Core SQLite Provider 通常会在连接中处理。
- 复杂循环级联关系可能导致迁移或运行时异常，建议使用 `Restrict` 或手动删除。
- 修改关系和级联策略后需要新增迁移，并更新数据库。

### 7.6 表关系配置后的迁移

修改以下内容后都应新增迁移：

- 新增或删除实体。
- 新增或删除 `DbSet<TEntity>`。
- 修改表名、列名、字段长度、必填约束。
- 修改主键、外键、索引。
- 修改关系和 `DeleteBehavior`。

命令示例：

```bash
dotnet ef migrations add add_blog_post_relation --project Source/Tests/H.Test.Sqlite/H.Test.Sqlite.csproj --startup-project Source/Tests/H.Test.Sqlite/H.Test.Sqlite.csproj
dotnet ef database update --project Source/Tests/H.Test.Sqlite/H.Test.Sqlite.csproj --startup-project Source/Tests/H.Test.Sqlite/H.Test.Sqlite.csproj
```

---

## 8. 设计时 DbContext Factory

执行 EF Core 迁移命令时，设计时工具需要创建 `DbContext`。建议为每个 `DbContext` 提供 `IDesignTimeDbContextFactory<TContext>`。

参考 `H.Test.Sqlite`：

```csharp
public class MyDataContextFactory : IDesignTimeDbContextFactory<MyDataContext>
{
    public MyDataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MyDataContext>();
        optionsBuilder.UseLazyLoadingProxies().UseSqlite("Data Source=Migration.db");
        return new MyDataContext(optionsBuilder.Options);
    }
}
```

说明：

- `Migration.db` 仅用于设计时生成迁移，不一定是运行时数据库。
- 运行时连接字符串由 `SqliteSettable` 决定。
- 迁移命令找不到 DbContext 时，优先检查 Factory 是否存在。

---

## 9. 数据库连接检测和迁移执行

`AddDbContextBySetting<TDbContext>()` 会注册：

```csharp
services.AddSingleton<IDbConnectService, SqliteDbConnectService<TDbContext>>();
services.AddSingleton<IDbDisconnectService, SqliteDbDisconnectService<TDbContext>>();
```

`SqliteDbConnectService<TDbContext>` 继承 `DbConnectServiceBase<TDbContext>`，连接检测逻辑：

```csharp
protected virtual bool CanConnect(DbContext db, out string message)
{
    try
    {
        db.Database.Migrate();
        return db.Database.CanConnect();
    }
    catch (Exception ex)
    {
        IocLog.Error(ex);
        message = ex.Message;
        return false;
    }
}
```

也就是说，启动连接检测时会自动执行：

```csharp
db.Database.Migrate();
```

这会把数据库升级到当前最新迁移版本。

### 9.1 启动时加载数据库

示例项目在 `OnSplashScreen` 中手动执行数据库连接服务：

```csharp
protected override void OnSplashScreen(StartupEventArgs e)
{
    base.OnSplashScreen(e);
    var loads = Ioc.Services.GetServices<IDbConnectService>();
    foreach (var load in loads)
    {
        load.Load(out string error);
    }
}
```

也可以把数据库连接服务作为启动加载服务使用，因为 `IDbConnectService` 属于数据库启动检测服务。

### 9.2 连接失败处理

连接失败时会：

1. 记录日志。
2. 弹出“数据库连接失败，是否重新配置?”。
3. 如果用户确认，显示数据库配置页。
4. 提示“数据库配置已修改，请重新启动”。

---

## 10. EF Core 数据库迁移

### 10.1 添加迁移

在解决方案根目录或项目目录执行：

```bash
dotnet ef migrations add init --project Source/Tests/H.Test.Sqlite/H.Test.Sqlite.csproj --startup-project Source/Tests/H.Test.Sqlite/H.Test.Sqlite.csproj
```

或者在 Visual Studio 包管理器控制台：

```powershell
Add-Migration init -Project Source\Tests\H.Test.Sqlite -StartupProject Source\Tests\H.Test.Sqlite
```

### 10.2 更新数据库

```bash
dotnet ef database update --project Source/Tests/H.Test.Sqlite/H.Test.Sqlite.csproj --startup-project Source/Tests/H.Test.Sqlite/H.Test.Sqlite.csproj
```

或：

```powershell
Update-Database -Project Source\Tests\H.Test.Sqlite -StartupProject Source\Tests\H.Test.Sqlite
```

### 10.3 删除最后一次迁移

```bash
dotnet ef migrations remove --project Source/Tests/H.Test.Sqlite/H.Test.Sqlite.csproj --startup-project Source/Tests/H.Test.Sqlite/H.Test.Sqlite.csproj
```

### 10.4 迁移文件位置

迁移文件通常生成在项目的 `Migrations` 目录，例如：

```text
Source/Tests/H.Test.Sqlite/Migrations/
├── 20231101115422_init.cs
├── 20231101115422_init.Designer.cs
└── MyDataContextModelSnapshot.cs
```

### 10.5 迁移开发注意事项

- 修改实体属性后，需要新增迁移。
- 修改 `OnModelCreating` 后，需要新增迁移。
- 生产环境建议保留所有迁移历史，不要随意删除已发布迁移。
- 应用启动时执行 `Database.Migrate()`，会自动应用未执行的迁移。
- 如果多个应用实例同时启动并迁移同一个数据库，需注意并发迁移风险。

---

## 11. 仓储接口

仓储接口位于 `H.Extensions.DataBase`。

### 11.1 `IRepository<TEntity, TPrimaryKey>`

主要方法：

| 方法 | 说明 |
|---|---|
| `GetList()` / `GetListAsync()` | 查询全部。 |
| `GetList(predicate)` / `GetListAsync(predicate)` | 按条件查询。 |
| `GetList(params string[] includes)` | 查询并 Include 导航属性。 |
| `GetByIDAsync(id)` | 按主键查询。 |
| `FirstOrDefaultAsync(predicate)` | 查询单个。 |
| `Insert` / `InsertAsync` | 新增。 |
| `InsertRangeAsync` | 批量新增。 |
| `Update` / `UpdateAsync` | 更新。 |
| `InsertOrUpdateAsync` | 新增或更新。 |
| `Delete` / `DeleteAsync` | 删除。 |
| `DeleteByID` / `DeleteByIDAsync` | 按主键删除。 |
| `DeleteRange` | 批量删除。 |
| `Clear` / `ClearAsync` | 清空表。 |
| `LoadPageList` | 分页查询。 |
| `Save` / `SaveAsync` | 保存更改。 |

### 11.2 `IStringRepository<TEntity>`

`IStringRepository<TEntity>` 是字符串主键实体的仓储接口：

```csharp
public interface IStringRepository<TEntity> : IRepository<TEntity, string>
    where TEntity : StringEntityBase
{
}
```

`DbModelBase` 继承 `StringEntityBase`，因此大多数业务实体可直接使用 `IStringRepository<TEntity>`。

---

## 12. 仓储实现

### 12.1 `RepositoryBase<TDbContext, TEntity, TPrimaryKey>`

`RepositoryBase` 是 EF Core 仓储基类，通过构造函数注入 `DbContext`：

```csharp
public abstract class RepositoryBase<TDbContext, TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
    where TEntity : EntityBase<TPrimaryKey>
    where TDbContext : DbContext
{
    protected readonly TDbContext _dbContext;

    public RepositoryBase(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}
```

### 12.2 `DbContextRepository<TDbContext, TEntity>`

字符串主键实体的默认仓储实现：

```csharp
public class DbContextRepository<TDbContext, TEntity>
    : RepositoryBase<TDbContext, TEntity, string>, IStringRepository<TEntity>
    where TEntity : StringEntityBase
    where TDbContext : DbContext
{
    public DbContextRepository(TDbContext dbContext) : base(dbContext)
    {
    }
}
```

注册示例：

```csharp
services.AddSingleton<IStringRepository<ImageEntity>, DbContextRepository<MyDataContext, ImageEntity>>();
```

使用示例：

```csharp
public class ImageService
{
    private readonly IStringRepository<ImageEntity> _repository;

    public ImageService(IStringRepository<ImageEntity> repository)
    {
        _repository = repository;
    }

    public async Task AddAsync(string name, string url)
    {
        await _repository.InsertAsync(new ImageEntity
        {
            Name = name,
            Url = url
        });
    }
}
```

---

## 13. 仓储 CRUD 示例

### 13.1 查询

```csharp
List<ImageEntity> all = repository.GetList();

List<ImageEntity> images = repository.GetList(x => x.MediaType == "image");

var entity = await repository.FirstOrDefaultAsync(x => x.Name == "test");
```

带 Include：

```csharp
List<OrderEntity> orders = repository.GetList(nameof(OrderEntity.Items));
```

### 13.2 新增

```csharp
await repository.InsertAsync(new ImageEntity
{
    Name = "logo",
    Url = "Assets/logo.png"
});
```

批量新增：

```csharp
await repository.InsertRangeAsync(items.ToArray());
```

### 13.3 更新

```csharp
entity.Name = "new name";
await repository.UpdateAsync(entity);
```

或在 EF 跟踪状态下直接保存：

```csharp
entity.Name = "new name";
await repository.SaveAsync();
```

### 13.4 删除

```csharp
await repository.DeleteAsync(entity);
await repository.DeleteByIDAsync(entity.ID);
await repository.DeleteAsync(x => x.MediaType == "temp");
```

### 13.5 分页

```csharp
Tuple<List<ImageEntity>, int> page = await repository.LoadPageList(
    startPage: 1,
    pageSize: 20,
    where: x => x.MediaType == "image",
    order: x => x.CDATE);

List<ImageEntity> rows = page.Item1;
int total = page.Item2;
```

---

## 14. 可绑定仓储模型

`H.Extensions.DataBase.Repository` 提供适合 UI 的仓储绑定模型。

核心类型：

| 类型 | 说明 |
|---|---|
| `IObservableSourceRepositoryBindable<TViewModel, TEntity>` | 可观察数据源仓储绑定接口。 |
| `ObservableSourceRepositoryBindable<TViewModel, TEntity>` | 默认实现，适合 `SelectBindable<TEntity>` 视图模型。 |
| `ObservableSourceRepositoryBindableBase<TViewModel, TEntity>` | 提供增删改查命令、导出、选择、消息提示等基础能力。 |
| `DateTimeObservableSourceRepositoryBindable` | 按时间维度扩展的数据源绑定模型。 |
| `TreeObservableSourceRepositoryBindable` | 树形数据源绑定模型。 |

`ObservableSourceRepositoryBindableBase` 内部通过 `DbIoc` 获取仓储：

```csharp
public IStringRepository<TEntity> Repository => DbIoc.GetService<IStringRepository<TEntity>>();
```

因此，如果使用可绑定仓储模型，需要确保仓储已在主 IOC 或 `DbIoc` 中注册。

### 14.1 定义行 ViewModel

```csharp
public class ImageItemViewModel : SelectBindable<ImageEntity>
{
    public ImageItemViewModel(ImageEntity model) : base(model)
    {
    }
}
```

### 14.2 定义仓储绑定模型

```csharp
public class ImageRepositoryBindable
    : ObservableSourceRepositoryBindable<ImageItemViewModel, ImageEntity>
{
    protected override bool Where(ImageEntity entity)
    {
        return entity.ISENBLED == 1;
    }
}
```

### 14.3 注册

```csharp
services.AddSingleton<IStringRepository<ImageEntity>, DbContextRepository<MyDataContext, ImageEntity>>();
services.AddSingleton<IObservableSourceRepositoryBindable<ImageItemViewModel, ImageEntity>, ImageRepositoryBindable>();
```

### 14.4 内置命令

`ObservableSourceRepositoryBindableBase` 提供：

| 命令 | 说明 |
|---|---|
| `AddCommand` | 新增实体，使用 `IocMessage.Form.ShowEdit`。 |
| `EditCommand` | 编辑实体并保存仓储。 |
| `DeleteCommand` | 删除当前实体。 |
| `ViewCommand` | 查看实体。 |
| `ClearCommand` | 清空数据。 |
| `SaveCommand` | 保存更改。 |
| `ExportCommand` | 导出 Excel。 |
| `PreviousCommand` / `NextCommand` | 上一个/下一个。 |
| `CheckedAllCommand` | 全选。 |
| `CheckedNoneCommand` | 取消选择。 |
| `DeleteCheckedCommand` | 删除选中。 |

---

## 15. DbIoc 数据库容器

`DbIoc` 是数据库服务专用 IOC：

```csharp
DbIoc.ConfigureServices(services =>
{
    services.AddSingleton<IStringRepository<ImageEntity>, DbContextRepository<MyDataContext, ImageEntity>>();
});
```

特点：

- 可独立于主 IOC 重建。
- `DbIoc.GetService<T>()` 找不到服务时，会回退到主 `Ioc`。
- 适合数据库连接切换、项目切换、多数据库切换场景。

重建：

```csharp
DbIoc.Rebuild();
```

---

## 16. 完整最小示例

### 16.1 实体

```csharp
public class TodoItem : DbModelBase
{
    [Required]
    [Display(Name = "标题")]
    public string Title { get; set; }

    [Display(Name = "完成")]
    public bool IsDone { get; set; }
}
```

### 16.2 DbContext

```csharp
public class TodoDataContext : DbContext
{
    public TodoDataContext(DbContextOptions<TodoDataContext> options) : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; }
}

public class TodoDataContextFactory : IDesignTimeDbContextFactory<TodoDataContext>
{
    public TodoDataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TodoDataContext>();
        optionsBuilder.UseLazyLoadingProxies().UseSqlite("Data Source=Migration.db");
        return new TodoDataContext(optionsBuilder.Options);
    }
}
```

### 16.3 注册

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddSetting();
    services.AddWindowMessage();
    services.AddWindowDialogMessage();

    services.AddDbContextBySetting<TodoDataContext>(x =>
    {
        x.InitialCatalog = "todo.db";
    });

    services.AddSingleton<IStringRepository<TodoItem>, DbContextRepository<TodoDataContext, TodoItem>>();
}

protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);
    app.UseSqlite();
}
```

### 16.4 启动连接

```csharp
protected override void OnSplashScreen(StartupEventArgs e)
{
    base.OnSplashScreen(e);

    foreach (IDbConnectService load in Ioc.Services.GetServices<IDbConnectService>())
    {
        load.Load(out string message);
    }
}
```

### 16.5 使用仓储

```csharp
public class TodoService
{
    private readonly IStringRepository<TodoItem> _repository;

    public TodoService(IStringRepository<TodoItem> repository)
    {
        _repository = repository;
    }

    public Task<int> AddAsync(string title)
    {
        return _repository.InsertAsync(new TodoItem { Title = title });
    }

    public List<TodoItem> GetAll()
    {
        return _repository.GetList();
    }
}
```

---

## 17. 二次开发建议

- SQLite 应用优先使用 `AddDbContextBySetting<TDbContext>()`，便于在设置页修改数据库路径。
- 每个 `DbContext` 都建议提供 `IDesignTimeDbContextFactory<TContext>`，方便迁移命令运行。
- 实体优先继承 `DbModelBase`，可获得主键、时间戳、属性通知和校验能力。
- 仓储优先注册 `IStringRepository<TEntity>` + `DbContextRepository<TDbContext, TEntity>`。
- UI 表格增删改查优先使用 `ObservableSourceRepositoryBindable<TViewModel, TEntity>`。
- 启动阶段执行 `IDbConnectService.Load()`，确保自动迁移和连接检测。
- 生产环境迁移前建议备份 SQLite 数据库文件。
- 不建议在 UI 线程执行大量数据库同步查询，优先使用异步方法。
- `DbContext` 默认通过 DI 管理，不要长期缓存手动 new 出来的上下文。

---

## 18. 常见问题

### 找不到数据库文件

检查：

1. `SqliteSettable.FilePath` 是否正确。
2. `SqliteSettable.InitialCatalog` 是否正确。
3. 是否调用了 `SqliteSettable.Instance.Load(...)`。
4. 目录是否存在或有写入权限。

### 启动后没有自动建表

检查：

1. 是否存在迁移文件。
2. 是否执行了 `IDbConnectService.Load()`。
3. 是否调用到 `db.Database.Migrate()`。
4. `DbContext` 是否包含对应 `DbSet<TEntity>`。

### `dotnet ef` 找不到 DbContext

解决：

- 添加 `IDesignTimeDbContextFactory<TContext>`。
- 确认启动项目和目标项目参数正确。
- 确认项目引用 `Microsoft.EntityFrameworkCore.Design`。

### 仓储注入失败

检查是否注册：

```csharp
services.AddSingleton<IStringRepository<MyEntity>, DbContextRepository<MyDataContext, MyEntity>>();
```

如果使用 `DbIoc`，确认已调用 `DbIoc.ConfigureServices(...)`。

### 迁移后实体字段没有变化

检查：

1. 是否新增迁移。
2. 是否执行 `Update-Database` 或启动时 `Database.Migrate()`。
3. 是否连接到正确数据库文件。
4. 设计时 `Migration.db` 和运行时 `data.db` 是否混淆。

### SQLite 数据库被占用

常见原因：

- 多个进程同时访问同一数据库。
- 未释放长事务或连接。
- 长时间持有 `DbContext`。

建议减少长事务，避免多实例同时迁移同一数据库。
