using Microsoft.WindowsAPICodePack.Shell.Interop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Practice11.BudgetDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using static Practice11.BudgetDataSet;

namespace Practice11
{
    public class SerializableRecordsTable
    {
        public RecordsTableAdapter RecordsTableAdapter;
        public RecordsTypesTableAdapter RecordsTypesTableAdapter;

        public List<JArray> Records { get; } = new List<JArray>();
        public List<JArray> RecordsTypes { get; } = new List<JArray>();


        public SerializableRecordsTable() { }

        public SerializableRecordsTable(RecordsTableAdapter recordsTableAdapter, RecordsTypesTableAdapter recordsTypesTableAdapter)
        {
            RecordsTableAdapter = recordsTableAdapter;
            RecordsTypesTableAdapter = recordsTypesTableAdapter;

            foreach(RecordsRow row in RecordsTableAdapter.GetData().Rows)
            {
                Records.Add(JArray.FromObject(row.ItemArray));
            }
    
            foreach (RecordsTypesRow row in RecordsTypesTableAdapter.GetData().Rows)
            {
                RecordsTypes.Add(JArray.FromObject(row.ItemArray));
            }
        }
    }
}
