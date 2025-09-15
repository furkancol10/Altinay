using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Altinay.EntityFrameworkCore;
using Altinay.Domain.ProjectTracking;
using Volo.Abp.DependencyInjection;

namespace Altinay.DbMigrator
{
    public class CheckTables : ITransientDependency
    {
        private readonly AltinayDbContext _context;

        public CheckTables(AltinayDbContext context)
        {
            _context = context;
        }

        public async Task CheckDatabaseTables()
        {
            Console.WriteLine("🔍 Checking database tables...");
            
            try
            {
                // Check if TrackingComment table exists
                var commentCount = await _context.Set<TrackingComment>().CountAsync();
                Console.WriteLine($"✅ AppTrackingComment table exists with {commentCount} records");
                
                // Check if TrackingIssue table exists
                var issueCount = await _context.Set<TrackingIssue>().CountAsync();
                Console.WriteLine($"✅ AppTrackingIssue table exists with {issueCount} records");
                
                // Check if TrackingProject table exists
                var projectCount = await _context.Set<TrackingProject>().CountAsync();
                Console.WriteLine($"✅ AppTrackingProject table exists with {projectCount} records");
                
                // List all tables in Altinay schema
                Console.WriteLine("\n📋 All tables in Altinay schema:");
                var tables = await _context.Database.SqlQueryRaw<string>(
                    "SELECT table_name FROM information_schema.tables WHERE table_schema = 'Altinay' ORDER BY table_name"
                ).ToListAsync();
                
                foreach (var table in tables)
                {
                    Console.WriteLine($"  - {table}");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error checking tables: {ex.Message}");
            }
        }
    }
}
