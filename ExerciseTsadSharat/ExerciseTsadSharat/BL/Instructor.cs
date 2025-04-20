namespace ExerciseTsadSharat.BL
{
    public class Instructor
    {
        int id;
        string title;
        string name;
        string image;
        string jobTitle;

        //public static List<Instructor> instructorsList = new List<Instructor>();

        public Instructor() { }

        public Instructor(int id, string title, string name, string image, string jobTitle)
        {
            Id = id;
            Title = title;
            Name = name;
            Image = image;
            JobTitle = jobTitle;
        }

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Name { get => name; set => name = value; }
        public string Image { get => image; set => image = value; }
        public string JobTitle { get => jobTitle; set => jobTitle = value; }

        public int Insert()
        {
            DBservices dBservices = new DBservices();
            return dBservices.insertInstructor(this);
        }

        public List<Instructor> Read()
        {
            DBservices dBservices = new DBservices();
            return dBservices.readInstructors();
        }

        public static Instructor GetInstructorById(int id)
        {
            DBservices dBservices = new DBservices();
            return dBservices.readSpecInstructorFromInstructors(id);
        }
    }
}
