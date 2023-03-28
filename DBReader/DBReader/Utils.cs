using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DBReader
{
    public class Utils
    {
        public static readonly Regex namesAllowedRegex = new Regex("^[a-zA-Zа-яА-Я]*[-{1}]?[a-zA-Zа-яА-Я]*$");
        public static readonly Regex loginRegex = new Regex("^[a-zA-Z]?[a-zA-Z0-9]*[_{1}]?[a-zA-Z0-9]*$");
        public static readonly Regex numbersRegex = new Regex("^[0-9]+$");

        public static void FillComboBoxFromDataGrid(ComboBox comboBox, DataGrid dataGrid, string columnName)
        {
            comboBox.Items.Clear();

            foreach (DataRowView row in dataGrid.Items)
            {
                comboBox.Items.Add(row[columnName]);
            }
        }

        public static int FindIndexInComboBox(ComboBox comboBox, DataRowView dataRowView, string strToCompare)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i] as string == strToCompare)
                {
                    return i;
                }
            }

            return 0;
        }

        public static void DisableDataGridEditing(DataGrid dataGrid)
        {
            dataGrid.BeginningEdit += (sender, e) =>
            {
                e.Cancel = true;
            };
        }

        public static void SerializeData(string fileName, object data)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog() { IsFolderPicker = true };
            var showRes = dialog.ShowDialog();

            if (showRes == CommonFileDialogResult.Ok)
            {
                File.WriteAllText(dialog.FileName + System.IO.Path.DirectorySeparatorChar + fileName + ".json", JsonConvert.SerializeObject(data, Formatting.Indented));
            }
        }
    }
}
