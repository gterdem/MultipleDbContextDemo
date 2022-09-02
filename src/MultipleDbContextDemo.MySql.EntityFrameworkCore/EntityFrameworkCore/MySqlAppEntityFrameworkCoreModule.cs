using EfCoreMultiContextApp.MySql.TestMySqlEntities;
using Microsoft.Extensions.DependencyInjection;
using MultipleDbContextDemo.MySql.TestMySqlEntities;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;

namespace MultipleDbContextDemo.MySql.EntityFrameworkCore;

[DependsOn(
    typeof(MultipleDbContextDemoDomainModule),
    typeof(AbpEntityFrameworkCoreMySQLModule)
    )]
public class MySqlAppEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        MySqlAppEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<MySqlAppDbContext>(options =>
        {
            /* Remove "includeAllEntities: true" to create
             * default repositories only for aggregate roots */
            options.AddRepository<TestMySqlEntity, EfCoreTestMySqlEntityRepository>();

        });

        Configure<AbpDbContextOptions>(options =>
        {
            /* The main point to change your DBMS.
             * See also MultipleDbContextDemoDbContextFactory for EF Core tooling. */
            options.UseMySQL();
        });
    }
}