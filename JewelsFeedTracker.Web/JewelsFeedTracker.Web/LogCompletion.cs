using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire.Common;
using Hangfire.Server;

namespace WebApiWithSwagger
{
    class LogCompletionAttribute : JobFilterAttribute, IServerFilter
    {
        public void OnPerforming(PerformingContext filterContext)
        {
            // Code here if you care when the execution **has begun**
        }

        public void OnPerformed(PerformedContext context)
        {
            // Check that the job completed successfully
            if (!context.Canceled && context.Exception != null)
            {
                // Here you would write to your database.
                // Example with entity framework:
                //using (var ctx = new YourDatabaseContext())
                //{
                //    ctx.Something.Add(/**/); 
                //    ctx.SaveChanges();
                //}
            }
        }
    }
}
