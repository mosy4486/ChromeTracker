using System;
using System.Collections.Generic;
using System.Text;

namespace ChromeTracker
{
    public static class HistoryReader
    {
        public static (HistoryData data, bool success) GetHistoryData(string historyFilePath)
        { 
            HistoryData data = new HistoryData();
            if(!File.Exists(historyFilePath)) return(data,false);






            return(data, true);
        }
    }


}
