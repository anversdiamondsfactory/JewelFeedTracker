using Hangfire;
using JewelsFeedTracker.Data.Models.Models;
using JewelsFeedTracker.FactoryManager;
using JewelsFeedTracker.Utility;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml;
using WebApiWithSwagger.ErrorHandler;

namespace WebApiWithSwagger.HangfireJob
{
    public class FeedProcesser
    {
        /// <summary>
        /// ProcessFineStarFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        [Hangfire.JobDisplayName("FineStar")]
        [AutomaticRetry(Attempts = 3)]
        public  void ProcessFineStarFeed(FeedFactory<FineStar> factory, string rawUrl)
        {
            var list = factory.GetFeedData(rawUrl);
            if (list == null)
                return;
            Console.Beep();
            Log.Information("FineStar Feed finshed --");

        }
        /// <summary>
        /// ProcessDfeFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        [Hangfire.JobDisplayName("Dfe")]
        [AutomaticRetry(Attempts = 3)]
        public  void ProcessDfeFeed(FeedFactory<DfrStock> factory, string rawUrl)
        {
            var list = factory.GetFeedData(rawUrl);
            if (list == null)
                return;
            Console.Beep();
            Log.Information("DFR Feed finshed --");
        }

        /// <summary>
        /// ProcessRedeximFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        [Hangfire.JobDisplayName("Redexim")]
        [AutomaticRetry(Attempts = 3)]
        public void ProcessRedeximFeed(FeedFactory<Redexim> factory, string rawUrl)
        {
            var list = factory.GetFeedData(rawUrl);
            if (list == null)
                return;
            Console.Beep();
            Log.Information("Redexim Feed finished --");
        }
        /// <summary>
        /// ProcessSagarFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        public  void ProcessSagarFeed(FeedFactory<tranStock> factory, string rawUrl)
        {
            var list = factory.GetFeedData(rawUrl);
            if (list == null)
                return;
            Console.Beep();
            Log.Information("Sagar Feed finished --");
        }

        /// <summary>
        /// ProcessSagarFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        public  void ProcessJbBrotherFeed(FeedFactory<JbBrother> factory, string rawUrl)
        {
            var list = factory.GetFeedData(rawUrl);
            if (list == null)
                return;
            Console.Beep();
            Log.Information("Jb Brother Feed Finihed --");
        }
        /// <summary>
        /// ProcessGlowStarFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        public void ProcessGlowStarFeed(FeedFactory<PKTDTL> factory, string rawUrl)
        {
            var list = factory.GetFeedData(rawUrl);
            if (list == null)
                return;
            Console.Beep();
            Log.Information("Glow Star Feed Finished --");
        }
        /// <summary>
        /// ProcessHvkFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        public  void ProcessHvkFeed(FeedFactory<Row> factory, string rawUrl)
        {           
            var list = factory.GetFeedData(rawUrl);
            if (list == null)
                return;
            Console.Beep();
            Log.Information("Hvk Feed Finished --");
        }

        /// <summary>
        /// ProcessHarikrishnaFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        public  void ProcessHarikrishnaFeed(FeedFactory<HariStock> factory, string rawUrl)
        {
            var list = factory.GetFeedData(rawUrl);
            if (list == null)
                return;
            Console.Beep();
            Log.Information("Hari Krishna Feed Finished --");
        }

        /// <summary>
        /// ProcessAkarshFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        public  void ProcessAkarshFeed(FeedFactory<Akarsh> factory, string rawUrl)
        {
            var list = factory.GetFeedData(rawUrl);
            if (list == null)
                return;
            Console.Beep();
            Log.Information("Akarsh Feed Finshed --");
        }

        /// <summary>
        /// ProcessDharmanandanFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        public  void ProcessDharmanandanFeed(FeedFactory<JewelsFeedTracker.Data.Models.Models.DataList> factory, string rawUrl)
        {
            var list = factory.GetFeedData(rawUrl);
            if (list == null)
                return;
            Console.Beep();
            Log.Information("Dharman Feed finhed --");
        }

        /// <summary>
        /// ProcessRapnetFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        public  void ProcessRapnetFeed(FeedFactory<ChildRapnet> factory, string rawUrl)
        {
            var list = factory.GetFeedData(rawUrl);
            if (list == null)
                return;
            Console.Beep();
            Log.Information("Rapnet Feed finished --");
        }
        /// <summary>
        /// ProcessDiamondSrdFeed
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        public  void ProcessDiamondSrdFeed(FeedFactory<Table> factory, string rawUrl)
        {
            var list = factory.GetFeedData(rawUrl);
            if (list == null)
                return;
            Console.Beep();
            Log.Information("DiamondSrd Feed Finished --");
        }

    }

}
