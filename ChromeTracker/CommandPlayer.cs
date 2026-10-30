using System;
using System.Collections.Generic;
using System.Text;
using CTCommon;

namespace ChromeTracker
{
    public class CommandPlayer
    {
        private (string[] directoryPath,bool success) GetChromeDirectoryPath()
        {
            List<string> resultPath = new List<string>();
            string searchPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                @"Google\Chrome\User Data");

            if (!Directory.Exists(searchPath)) return (resultPath.ToArray(), false);

            foreach (string path in Directory.GetDirectories(searchPath))
            { 
                if(path.Contains("Profile"))resultPath.Add(path);
            }
            if (Directory.Exists(Path.Combine(searchPath, "Default"))) resultPath.Add(Path.Combine(searchPath, "Default"));

            return (resultPath.ToArray(), resultPath.Count>0);
        }
        public void ShowCommandList()
        {
            StringBuilder sb = new StringBuilder();
            foreach (CommandType value in Enum.GetValues(typeof(CommandType)))
            {
                if (((int)value) == -1) continue;
                sb.AppendLine(value.ToString());
            }
            Console.WriteLine(sb.ToString());
        }
        private async Task<(HistoryData[] data, bool success)> GetHistoryDataAsync()
        {
            List<HistoryData> result = new List<HistoryData>();
            var r1 = GetChromeDirectoryPath();

            if (!r1.success)
            {
                Console.WriteLine("Not Found chorme");
                return (result.ToArray(),false);
            }
            List<string> historyFilesPath = new List<string>();

            foreach (string findDirectory in r1.directoryPath)
            {
                if (File.Exists(Path.Combine(findDirectory, "History")))
                {
                    historyFilesPath.Add(Path.Combine(findDirectory, "History"));
                }
            }

            foreach (string historyFile in historyFilesPath)
            {
                var r2 = HistoryReader.GetHistoryData(historyFile);
                if (!r2.success) continue;
                result.Add(r2.data);
            }

            return (result.ToArray(),true);


        }
        public async Task SearchList(CancellationToken token)
        {
            StringBuilder sb = new StringBuilder();

            var r1 = await GetHistoryDataAsync();
            if (!r1.success) return;
            HistoryData[] hsds = r1.data;

            foreach (var d in hsds)
            {
                foreach (var s in d.searchList) sb.AppendLine(s);
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
