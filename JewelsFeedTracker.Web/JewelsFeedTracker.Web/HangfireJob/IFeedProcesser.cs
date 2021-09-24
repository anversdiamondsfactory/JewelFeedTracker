using System.Linq;
using System.Threading.Tasks;
using JewelsFeedTracker.Api.Models;
using JewelsFeedTracker.Data.Models;
using System.Collections.Generic;
using JewelsFeedTracker.Data.Models.Models;
using JewelsFeedTracker.FactoryManager;
using Hangfire;

namespace WebApiWithSwagger.HangfireJob
{
    public interface IFeedProcesser
    {
        /// <summary>
        /// ProcessFineStarFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        public Task ProcessFineStarFeed(FeedFactory<FineStar> factory, string rawUrl);
        /// <summary>
        /// ProcessDfeFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        public Task ProcessDfeFeed(FeedFactory<DfrStock> factory, string rawUrl);
        /// <summary>
        ///ProcessRedeximFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        public Task ProcessRedeximFeed(FeedFactory<Redexim> factory, string rawUrl);
        /// <summary>
        ///ProcessSagarFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        public Task ProcessSagarFeed(FeedFactory<tranStock> factory, string rawUrl);
        /// <summary>
        /// ProcessJbBrotherFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        public Task ProcessJbBrotherFeed(FeedFactory<JbBrother> factory, string rawUrl);
        /// <summary>
        /// ProcessGlowStarFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        public Task ProcessGlowStarFeed(FeedFactory<PKTDTL> factory, string rawUrl);
        /// <summary>
        /// ProcessHvkFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        public Task ProcessHvkFeed(FeedFactory<Row> factory, string rawUrl);
        /// <summary>
        /// ProcessHarikrishnaFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        public Task ProcessHarikrishnaFeed(FeedFactory<HariStock> factory, string rawUrl);

        /// <summary>
        /// ProcessAkarshFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        public Task ProcessAkarshFeed(FeedFactory<Akarsh> factory, string rawUrl);

        /// <summary>
        /// ProcessDharmanandanFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        public Task ProcessDharmanandanFeed(FeedFactory<Akarsh> factory, string rawUrl);

        /// <summary>
        /// ProcessRapnetFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        public Task ProcessRapnetFeed(FeedFactory<Akarsh> factory, string rawUrl);

    }
   
}