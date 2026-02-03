using Microsoft.EntityFrameworkCore;

namespace BoscovsBackend.Data
{
    public class S3DbContext : DbContext
    {
        public S3DbContext(DbContextOptions<S3DbContext> options) : base(options) { }

        // We will add S3_PurchaseOrders and VendorMaster here later
    }
}