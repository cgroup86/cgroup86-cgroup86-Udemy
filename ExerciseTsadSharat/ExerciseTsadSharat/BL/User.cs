using System.Diagnostics.Eventing.Reader;

namespace ExerciseTsadSharat.BL
{
    public class User
    {
        int id;
        string name;
        string email;
        string password;
        bool isAdmin;
        bool isActive;
/*
        static List<Course> myCourses = new List<Course>();
        static List<User> usersList = new List<User>();*/

      // static int userId = 1; // To give id for each users, each time new user made it increases with 1
        public User(int id, string name, string email, string password, bool isAdmin, bool isActive)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            IsAdmin = isAdmin;
            IsActive = isActive;
        }

        public User() { }

        // We used static User() so the admin user is added automatically before everything
        //static User()
        //{
        //    usersList.Add(new User(0, "admin", "admin@admin.com", "admin", true, true));
        //}

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }
        public bool IsActive { get => isActive; set => isActive = value; }

        public List<User> Read()
        {
            DBservices dBservices = new DBservices();
            return dBservices.readUsers();
        }

        // function to login into the user
        public bool Login()
        {

            DBservices dBservices = new DBservices();
            bool success = dBservices.UserLogin(this);
            if (!success)
            {
                throw new ArgumentException("Invalid email or password");
            }
            return true;
        }

        // function to register into the user
        public bool Register()
        {
            try
            {
                DBservices dBservices = new DBservices();
                dBservices.UserRegister(this);
                return true;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }

        public bool InsertCourse(Course course, int id)
        {
            try
            {
                DBservices dBservices = new DBservices();
                dBservices.InsertCourseToUserCourse(course.Id, id);
                return true;
            }
            catch (ArgumentException ex)
            {
                return false;
            }
        }

        public List<Course> ReadMyCourses(int id)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetCoursesFromUserCourse(id);
        }

        public List<Course> GetByDurationRange(float minDuration, float maxDuration,int id)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetCoursesByDurationRange(minDuration, maxDuration, id);
        }

        public List<Course> GetByRatingRange(float minRating, float maxRating, int userId)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetCoursesByRatingRange(minRating, maxRating, userId);
        }

        public void DeleteById(int courseId,int userId)
        {
            try
            {
                DBservices dBservices = new DBservices();
                dBservices.DeleteCourseFromUserCourses(courseId, userId);
               
            }
            catch(ArgumentException ex) {
                throw ex;
            }
        }
    }
}
