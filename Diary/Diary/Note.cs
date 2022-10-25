namespace Diary
{
    public class Note
    {
        public DateTime date = DateTime.Now;
        public string name = "";
        public string description = "";

        public Note() { }
        public Note(Note note)
        {
            set(note);
        }

        public void reset()
        {
            name = "";
            description = "";
        }

        public void set(Note note)
        {
            date = note.date;
            name = note.name;
            description = note.description;
        }
    }
}
