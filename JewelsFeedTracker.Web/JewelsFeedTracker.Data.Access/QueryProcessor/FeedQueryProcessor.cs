using JewelsFeedTracker.Data.Access;
using JewelsFeedTracker.Utility;
using JewelsFeedTracker.Utility.RowDataManager;
using Serilog;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace JewelsFeedTracker.Data.Access.QueryProcessor
{
    // Bulk data Processing logic is written here.
    public class FeedQueryProcessor : IFeedQueryProcessor
    {
        /// <summary>
        /// Save Bulk Feed
        /// </summary>
        /// <param name="sourceDt"></param>
        /// <param name="feedName"></param>
        /// <returns></returns>
        public async Task<bool> SaveFeed(DataTable sourceDt, string feedName)
        {
            if (sourceDt.Rows.Count > 0)
            {
                DataBaseHelper.BulkCopy(sourceDt, feedName);
                Log.Information(feedName + " feed redords " + sourceDt.Rows.Count + " has been saved successfully.");
            }
            //DataFormatter.SaveFileLocalFolder(DataFormatter.ToDatableByCSV(RawDataUrl), DataFormatter.SetFeedFileName(FeedIdentifier.HARIKRISHNA.ToString(), 'R'));
            //DataFormatter.ToListByDataTable<DfrStock>(DataFormatter.ToDatableByCSV(RawDataUrl), DataFormatter.SetFeedFileName(FeedIdentifier.HARIKRISHNA.ToString(), 'F'));
            return await Task.FromResult(true);
           
        }
    }
}