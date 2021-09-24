using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelsFeedTracker.FactoryManager
{
    /// <summary>
    /// The 'Creator' Abstract Class
    /// </summary>
    public abstract class FeedFactory<T>
    {
        public abstract Task GetFeedData(string RawDataUrl);
    }
}
