namespace FileExplorer
{
    public class FileExplorerArrow
    {
        public void Clear(int posY) 
        {
            Console.SetCursorPosition(0, posY);
            Console.WriteLine("   ");
        }

        public void Draw(int posY)
        {
            Console.SetCursorPosition(0, posY);
            Console.Write("-> ");
        }
    }
}
