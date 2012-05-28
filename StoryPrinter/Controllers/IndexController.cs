using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using StoryPrinter.Models;

namespace StoryPrinter.Controllers
{
    public class IndexController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string data, int columnCount = 9)
        {
            var arr = data.Split('|')
                .Skip(1)
                .Select(x => x.Trim())
                .ToList();

            var header = arr.Take(columnCount);
            var table = new DataTable();
            foreach (var columnName in header)
            {
                table.Columns.Add(columnName);
            }

            var rowData = arr.Skip(columnCount).ToList();
            for (var i = 0; i < rowData.Count; i += columnCount)
            {
                if (string.IsNullOrEmpty(rowData[i]) == true) continue;
                var row = table.NewRow();
                row.ItemArray = rowData.GetRange(i, columnCount).ToArray();
                table.Rows.Add(row);
            }

            return View("Visualizer", CreateVisualizerViewModel(table));
        }

        private VisualizerViewModel CreateVisualizerViewModel(DataTable table)
        {
            Func<DataRow, int> idFetcher = x => 0;
            Func<DataRow, string> sprintFetcher = x => null;
            Func<DataRow, string> nameFetcher = x => null;

            var visualizer = new VisualizerViewModel();
            if (table.Columns.Contains("Req\r\nID")) idFetcher = x => int.Parse((string)x["Req\r\nID"]);
            if (table.Columns.Contains("Cycle")) sprintFetcher = x => ((string)x["Cycle"]).Replace("Sprint ", "");
            if (table.Columns.Contains("Name")) nameFetcher = x => x["Name"] as string;

            visualizer.Tasks = table.AsEnumerable().Select(x => new TaskViewModel(idFetcher(x), sprintFetcher(x), nameFetcher(x))).ToList();

            return visualizer;
        }
    }
}