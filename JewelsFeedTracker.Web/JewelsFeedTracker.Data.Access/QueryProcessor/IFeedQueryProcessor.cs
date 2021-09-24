using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace JewelsFeedTracker.Data.Access.QueryProcessor
{
    public interface IFeedQueryProcessor
    {
        /// <summary>
        /// Save Bulk Feed
        /// </summary>
        /// <param name="sourceDt"></param>
        /// <param name="feedName"></param>
        /// <returns></returns>
        Task<bool> SaveFeed(DataTable sourceDt, string feedName);

    }
}