namespace Editor
{
    public class Rectangle
    {
        public string name = "Прямоугольник";
        public int x, y, width, height;

        public Rectangle() { }

        public Rectangle(Rectangle rectangle)
        {
            Set(rectangle);
        }

        public void Reset()
        {
            name = "Прямоугольник";
            x = y = width = height = 0;
        }

        public void Set(Rectangle rectangle)
        {
            name = rectangle.name;

            x = rectangle.x;
            y = rectangle.y;
            width = rectangle.width;
            height = rectangle.height;
        }

        public override string ToString()
        {
            return name + "\n" + x + "\n" + y + "\n" + width + "\n" + height + "\n";
        }

        public string[] ToStringArray()
        {
            return new string[] {
                name,
                x.ToString(),
                y.ToString(),
                width.ToString(),
                height.ToString()
            };
        }

        public static Rectangle Parse(string str)
        {
            Rectangle tmp = new Rectangle();
            try {
                string[] parameters = str.Split('\n');
         
                tmp.name = parameters[0];

                int.TryParse(parameters[1], out tmp.x);
                int.TryParse(parameters[2], out tmp.y);
                int.TryParse(parameters[3], out tmp.width);
                int.TryParse(parameters[4], out tmp.height);
            } catch(Exception e) { }

            return tmp;
        }
    }
}
