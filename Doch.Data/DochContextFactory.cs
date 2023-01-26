using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Doch.Data
{
    public class DochContextFactory : IDesignTimeDbContextFactory<DochContext>
    {
        public DochContext CreateDbContext(string[] args)
        {
            DirectoryInfo dir = new(Environment.CurrentDirectory);
            IConfigurationBuilder cb = new ConfigurationBuilder()
                .SetBasePath(dir.Parent.FullName + "\\Doch.API")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfigurationRoot config = cb.Build();
            var cnnString = config.GetConnectionString("SqlServer");
            var ob = new DbContextOptionsBuilder<DochContext>()
                .UseSqlServer(cnnString);
            return new DochContext(ob.Options);
        }
    }
}
