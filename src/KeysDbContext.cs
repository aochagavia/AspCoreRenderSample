using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspCoreRenderSample
{
    class KeysDbContext : DbContext, IDataProtectionKeyContext
    {
        public KeysDbContext(DbContextOptions<KeysDbContext> options)
            : base(options) { }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    }
}