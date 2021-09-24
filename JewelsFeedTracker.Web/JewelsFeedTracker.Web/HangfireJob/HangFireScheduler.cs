using System.Linq;
using System.Threading.Tasks;
using JewelsFeedTracker.Api.Models;
using JewelsFeedTracker.Data.Models;
using System.Collections.Generic;
using JewelsFeedTracker.Data.Models.Models;
using JewelsFeedTracker.FactoryManager;
using Hangfire;
using System;
using Serilog;

namespace WebApiWithSwagger.HangfireJob
{
    public static class HangFireScheduler
    {
        // Feed files job handling logic with CRON Value is written here 
        public static void FeedJobScheduler(string cronValue)
        {
            string url = string.Empty;
             FeedProcesser feedProcesser = new FeedProcesser();
            #region FineStar Feed 
            FeedFactory<FineStar> finestarFactory = new FineStarFactory<FineStar>();
            url = "https://finestardiamonds.com/api/Stock/GetFullStockInventory?Username=karan.jhaveri@navgrahaa.com&Password=karan123&Company=NAVGRAHAAJEWELSPRIVATELIMITED";
            RecurringJob.AddOrUpdate(() => feedProcesser.ProcessFineStarFeed(finestarFactory, url), cronValue);
            #endregion
            #region DfrStock Feed 
            url = "http://dfe.diamondsfactory.com/pd/DFR_Stock_Stone.csv";
            FeedFactory<DfrStock> dfeFactory = new DFEFactory<DfrStock>();
            RecurringJob.AddOrUpdate(() => feedProcesser.ProcessDfeFeed(dfeFactory, url), cronValue);
            #endregion
            #region Diamondsrd Feed 
            url = "http://api.srd.world/webservice.asmx?WSDL";
            FeedFactory<Table> diamondsrdFactory = new DiamondSrdFactory<Table>();
            RecurringJob.AddOrUpdate(() => feedProcesser.ProcessDiamondSrdFeed(diamondsrdFactory, url), cronValue);
            #endregion
            #region Redexim Feed 
            url = "http://183.87.182.182:85/Certified%20Stock%20List%20!!!.csv";
            FeedFactory<Redexim> redeximFactory = new RedeximFactory<Redexim>();
            RecurringJob.AddOrUpdate(() => feedProcesser.ProcessRedeximFeed(redeximFactory, url), cronValue);
            #endregion
            #region JbBrother Feed 
            url = "https://services.diamondsfactory.com/index.php?route=api/download_stone_feed&name=jbbrother";
            FeedFactory<JbBrother> jbBrotherFactory = new JbBrotherFactory<JbBrother>();
            RecurringJob.AddOrUpdate(() => feedProcesser.ProcessJbBrotherFeed(jbBrotherFactory, url), cronValue);
            #endregion
            #region GlowStar Feed 
            url = "https://www.glowstaronline.com/inventory/website/navgrahaa.php?un=NavGrahaa&p=bd339db4a2fc08665267ae07989f0e04";
            FeedFactory<PKTDTL> glowStarFactory = new GlowStarFactory<PKTDTL>();
            RecurringJob.AddOrUpdate(() => feedProcesser.ProcessGlowStarFeed(glowStarFactory, url), cronValue);
            #endregion
            #region HVK Feed 
            url = "http://stock.hvkonline.com/HVK_API_WebService.asmx?WSDL";
            FeedFactory<Row> hvkFactory = new HvkFactory<Row>();
            RecurringJob.AddOrUpdate(() => feedProcesser.ProcessHvkFeed(hvkFactory, url), cronValue);
            #endregion
            #region Harikrishna Feed 
            url = "https://services.diamondsfactory.com/index.php?route=api/download_stone_feed&name=harikrishna";
            FeedFactory<HariStock> harikrishnaFactory = new HariKrishnaFactory<HariStock>();
            RecurringJob.AddOrUpdate(() => feedProcesser.ProcessHarikrishnaFeed(harikrishnaFactory, url), cronValue);
            #endregion
            #region Akarsh Feed 
            url = "http://akarshexports.com/getfullstock.asmx?WSDL";
            FeedFactory<Akarsh> akarshFactory = new AkarshFactory<Akarsh>();
            RecurringJob.AddOrUpdate(() => feedProcesser.ProcessAkarshFeed(akarshFactory, url), cronValue);
            #endregion
            #region Dharmanandan Feed 
            url = "http://www.dharamhk.com/dharamwebapi/";
            FeedFactory<JewelsFeedTracker.Data.Models.Models.DataList> dharmanandanFactory = new DharmanandanFactory<JewelsFeedTracker.Data.Models.Models.DataList>();
            RecurringJob.AddOrUpdate(() => feedProcesser.ProcessDharmanandanFeed(dharmanandanFactory, url), cronValue);
            #endregion
            #region Rapnet Feed 
            url = "http://technet.rapaport.com/HTTP/DLS/GetFile.aspx?ShapeIDs=1,2,3,4,9,17,7,8,11,15,16&WeightFrom=0.20&WeightTo=30.00&ColorIDs=1,2,3,4,5,6,7,8&ClarityIDs=1,2,3,4,5,6,7,8&LabIDs=1,4,5,2,10,11,34,35,38&SortBy=Owner&White=1&Fancy=1&Programmatically=yes&Version=1.0&UseCheckedCulommns=1&cCT=1&cCERT=1&cCLAR=1&cCOLR=1&cCRTCM=1&cCountry=1&cCITY=1&cCulet=1&cCuletSize=1&cCuletCondition=1&cCUT=1&cDPTH=1&cFLR=1&cGIRDLE=1&cLOTNN=1&cMEAS=1&cMeasLength=1&cMeasWidth=1&cMeasDepth=1&cPOL=1&cPX=1&cDPX=1&cTPr=1&cCashTot=1&cRapSpec=1&cRatio=1&cCashDisc=1&cCash=1&cOWNER=1&cAct=1&cNC=1&cSHP=1&cSTATE=1&cSTOCK_NO=1&cSYM=1&cTBL=1&cSTONES=1&cCertificateImage=1&cImageURL=1&cCertID=1&cAvailability=1&cFluorColor=1&cFluorIntensity=1&cDateUpdated=1&ticket=";
            FeedFactory<ChildRapnet> rapnetFactory = new RapnetFactory<ChildRapnet>();
            RecurringJob.AddOrUpdate(() => feedProcesser.ProcessRapnetFeed(rapnetFactory, url), cronValue);
            #endregion
            #region Sagar Feed 
            url = "http://www.sagarenterprise.in/api/StockApi.aspx?uname=paulkumar009&pwd=UGF1bEAxMjM=-aV4BNaNOWEU=";
            FeedFactory<tranStock> sagarFactory = new RapnetFactory<tranStock>();
            RecurringJob.AddOrUpdate(() => feedProcesser.ProcessSagarFeed(sagarFactory, url), cronValue);
            #endregion
        }
    }
}
