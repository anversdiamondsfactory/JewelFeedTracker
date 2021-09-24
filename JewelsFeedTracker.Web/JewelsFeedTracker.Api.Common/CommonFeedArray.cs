using JewelsFeedTracker.Data.Access;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace JewelsFeedTracker.Api.Common
{
    public class CommonFeedArray
    {
        private ArrayList rules = new ArrayList();
        private ArrayList currencies = new ArrayList();
        public readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);

        TimeZoneInfo tzi = TZConvert.GetTimeZoneInfo("Central Standard Time");       
        decimal start_time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        private static TimeZoneInfo INDIAN_ZONE = TZConvert.GetTimeZoneInfo("Europe/London");
        public CommonFeedArray()
        {
            rules = DataBaseHelper.GetArrayList("SELECT * FROM  currency");

            foreach (DataRow row in rules)
            {
                //currencies[row["code"]] = new ArrayList(
                //      'currency_id'   => $result['currency_id'],
                //      'title'         => $result['title'],
                //      'symbol_left'   => $result['symbol_left'],
                //      'symbol_right'  => $result['symbol_right'],
                //      'decimal_place' => $result['decimal_place'],
                //      'value'         => $result['value']
                //      );
                //          }
            }
        }

        public void endTime(DateTime dtStart)
        {
            // global $db, $basefile;
            decimal time_diff = GetUnixTimestamp(dtStart) - start_time;
           string today_date = start_time.ToString("Y-m-d H:i:s");
           decimal start_timer = Math.Round(start_time);
	       //$start_date = date("Y-m-d H:i:s",$start_timer);

        }
        public int GetUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return (int)diff.TotalSeconds;
        }

    }
    }
    
