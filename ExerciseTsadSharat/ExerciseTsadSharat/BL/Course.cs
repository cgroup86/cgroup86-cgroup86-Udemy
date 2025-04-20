using System.Security;

namespace ExerciseTsadSharat.BL
{
    public class Course
    {
        int id;
        string title;
        string url;
        float rating;
        int numberOfReviews;
        int instructorId;
        string imageReference;
        float duration;
        string lastUpdate;
        bool isActive;
        public Course() { }

        public Course(int id, string title, string url, float rating, int numberOfReviews, int instructorId, string imageReference, int duration, string lastUpdate, bool isActive)
        {
            Id = id;
            Title = title;
            Url = url;
            Rating = rating;
            NumberOfReviews = numberOfReviews;
            InstructorId = instructorId;
            ImageReference = imageReference;
            Duration = duration;
            LastUpdate = lastUpdate;
            IsActive = isActive;   
        }

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Url { get => url; set => url = value; }
        public float Rating { get => rating; set => rating = value; }
        public int NumberOfReviews { get => numberOfReviews; set => numberOfReviews = value; }
        public int InstructorId { get => instructorId; set => instructorId = value; }
        public string ImageReference { get => imageReference; set => imageReference = value; }
        public float Duration { get => duration; set => duration = value; }
        public string LastUpdate { get => lastUpdate; set => lastUpdate = value; }
        public bool IsActive { get=> isActive; set => isActive = value; }


        public int Insert()
        {
            DBservices dBservices = new DBservices();
            return dBservices.InsertCourseToCourses(this);
        }

        public List<Course> Read()
        {
            DBservices dBservices = new DBservices();
            return dBservices.readCourses();
        }

        public static void UpdateCourse(Course updatedCourse)
        {
            DBservices dBservices = new DBservices();
            dBservices.UpdateCourse(updatedCourse);
        }

        public static Course GetCourseById(int id)
        {
            DBservices dBservices = new DBservices();
            return dBservices.readSpecCourseFromCourses(id);
        }

        public static bool CheckInstructorExists(int instructorId)
        {
            DBservices dbs = new DBservices();
            return dbs.CheckInstructorExists(instructorId);
        }

        public static List<Course> GetCoursesByInstructorId(int instructorId)
        {
            try
            {
                DBservices dBservices = new DBservices();
                return dBservices.GetCoursesByInstructorId(instructorId);
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void changeIsActiveAndTitle(int courseId,bool isActive,string title)
        {
            
            try
            {
                DBservices dbs = new DBservices();
                dbs.changeIsActiveAttributeAndTitle(courseId, isActive, title);
            }
            catch (ArgumentException ex) {
                throw ex;
            }
        }

        public object GetTop5CoursesByUsersRegistered()
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetTop5CoursesCourses();
        }
    }
}

