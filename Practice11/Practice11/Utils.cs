using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Practice11
{
    public class Utils
    {
        public static readonly Regex russianRegex = new Regex("^[а-яА-Я]+$");

        public static void SerializeData<T>(string fileName, T data)
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
