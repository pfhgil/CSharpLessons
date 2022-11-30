using System.Diagnostics;

namespace FileExplorer
{
    public static class FileExplorerWindow
    {
        public enum ExplorerMode
        {
            DEFAULT,
            CREATE_FILE,
            CREATE_DIRECTORY
        }

        private static FileExplorerArrow arrow = new FileExplorerArrow();

        private static ExplorerMode mode = ExplorerMode.DEFAULT;

        private static string currentChosenPath = "";

        private static int maxLinesNum = 0;

        private static int cursorPosY = 0;

        private static int infoLinesNum = 3;

        private static bool firstTime = true;

        private static string[] dirsPaths;

        private static string newFilePath = "";

        public static void ClearAll()
        {
            Console.Clear();

            maxLinesNum = 0;
        }

        public static void ClearArrow(int posY)
        {
            Console.SetCursorPosition(0, posY);
            Console.WriteLine("   ");
        }

        private static void Redraw()
        {
            ClearAll();
            DrawFile();

            cursorPosY = 0;
        }
            
        public static void Draw()
        {
            try {
                int last = cursorPosY;

                if (firstTime) {
                    DrawFile();
                    arrow.Draw(infoLinesNum + cursorPosY);
                    firstTime = false;
                }   

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (mode == ExplorerMode.DEFAULT) {
                    switch (keyInfo.Key) {
                        case ConsoleKey.UpArrow:
                            cursorPosY--;
                            break;
                        case ConsoleKey.DownArrow:
                            cursorPosY++;
                            break;
                        case ConsoleKey.Enter:
                            FileAttributes attr = File.GetAttributes(dirsPaths[cursorPosY]);
                            if (attr.HasFlag(FileAttributes.Directory)) {
                                currentChosenPath = dirsPaths[cursorPosY];

                                Redraw();
                            } else {
                                Process proc = new Process();
                                proc.StartInfo.FileName = dirsPaths[cursorPosY];
                                proc.StartInfo.UseShellExecute = true;
                                proc.Start();
                            }
                            break;
                        case ConsoleKey.Escape:
                            if (currentChosenPath != "") {
                                DirectoryInfo parentDir = Directory.GetParent(currentChosenPath);
                                if (parentDir != null) {
                                    currentChosenPath = parentDir.FullName;
                                } else {
                                    currentChosenPath = "";
                                }

                                Redraw();
                            }
                            break;
                        case ConsoleKey.F1:
                            mode = ExplorerMode.CREATE_DIRECTORY;
                            Redraw();
                            break;
                        case ConsoleKey.F2:
                            mode = ExplorerMode.CREATE_FILE;
                            Redraw();
                            break;
                        case ConsoleKey.F3:
                            attr = File.GetAttributes(dirsPaths[cursorPosY]);
                            if (attr.HasFlag(FileAttributes.Directory)) {
                                Directory.Delete(dirsPaths[cursorPosY]);
                            } else {
                                File.Delete(dirsPaths[cursorPosY]);
                            }
                            
                            mode = ExplorerMode.DEFAULT;

                            Redraw();
                            break;
                    }
                }

                cursorPosY = Math.Clamp(cursorPosY, 0, Math.Max(maxLinesNum - 1 - infoLinesNum, 0));

                ClearArrow(infoLinesNum + last);
                arrow.Draw(infoLinesNum + cursorPosY);
            } catch (Exception e) {

            }
        }

        private static void DrawFile()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(
                "\t\tТекущий путь: " + currentChosenPath
                );
            for (int i = 0; i < Console.WindowWidth; i++) {
                Console.Write("-");
            }

            Console.SetCursorPosition(0, 3);
            if (currentChosenPath == "") {
                DriveInfo[] disksInfos = DriveInfo.GetDrives();
                dirsPaths = new string[disksInfos.Length]; 

                for (int i = 0; i < disksInfos.Length; i++) {
                    string diskPath = disksInfos[i].RootDirectory.FullName;
                    WriteFile(disksInfos[i].RootDirectory.FullName, true);

                    dirsPaths[i] = diskPath;
                }

                maxLinesNum += disksInfos.Length + infoLinesNum;
            } else {
                try {
                    dirsPaths = Directory.GetDirectories(currentChosenPath).Concat(Directory.GetFiles(currentChosenPath)).ToArray();

                    for (int i = 0; i < dirsPaths.Length; i++) {
                        WriteFile(dirsPaths[i], false);
                    }

                    maxLinesNum += dirsPaths.Length + infoLinesNum;
                } catch(Exception e) {
                    // нельзя открыть файл получается =)
                }
            }

            if (currentChosenPath != "") {
                Console.SetCursorPosition(10, 2);
                Console.Write("Название");
                Console.SetCursorPosition(40, 2);
                Console.Write("Дата создания");
                Console.SetCursorPosition(70, 2);
                Console.Write("Тип");     
            } else {
                Console.SetCursorPosition(30, 2);
                Console.Write("Свободное место");
            }
            Console.SetCursorPosition(85, 2);
            Console.Write("|");
            Console.SetCursorPosition(85, 3);
            Console.Write("| F1 - Создать папку");
            Console.SetCursorPosition(85, 4);
            Console.Write("| F2 - Создать файл");
            Console.SetCursorPosition(85, 5);
            Console.Write("| F3 - Удалить");
            Console.SetCursorPosition(85, 6);
            Console.Write("|-------------------------");
            if (mode != ExplorerMode.DEFAULT) {
                Console.SetCursorPosition(85, 7);
                Console.Write("| Введите имя папки/файла:");
                Console.SetCursorPosition(85, 8);
                Console.Write("|");
                Console.SetCursorPosition(85, 9);
                Console.Write("| ");
                Console.SetCursorPosition(85, 10);
                Console.Write("|");

                Console.SetCursorPosition(87, 9);
                newFilePath = Console.ReadLine();

                CreateFile();
                mode = ExplorerMode.DEFAULT;
                Redraw();
            }     
        }

        private static void CreateFile()
        {
            string resPath = currentChosenPath + "/" + newFilePath;
            if (mode == ExplorerMode.CREATE_DIRECTORY) {
                if (!Directory.Exists(resPath)) {
                    Directory.CreateDirectory(resPath);
                }
            } else {
                if (!File.Exists(resPath)) {
                    File.Create(resPath).Close();
                }
            }

            newFilePath = "";
        }

        private static void WriteFile(string path, bool isDiskPath)
        {
            if (!isDiskPath) {
                Console.Write("   " + Path.GetFileName(path));
                Console.SetCursorPosition(40, Console.GetCursorPosition().Top);
                Console.Write(Directory.GetCreationTime(path));
                Console.SetCursorPosition(70, Console.GetCursorPosition().Top);
                Console.WriteLine(Path.GetExtension(path));
            } else {
                Console.Write("   " + path);
                DriveInfo driveInf = new DriveInfo(path);
                Console.SetCursorPosition(30, Console.GetCursorPosition().Top);
                if (driveInf.IsReady) {
                    Console.WriteLine("Свободно " + Math.Round(driveInf.TotalFreeSpace / Math.Pow(1024, 3)) + " ГБ из " + Math.Round(driveInf.TotalSize / Math.Pow(1024, 3)) + " ГБ");
                } else {
                    Console.WriteLine("");
                }
            }
        }
    }
}
