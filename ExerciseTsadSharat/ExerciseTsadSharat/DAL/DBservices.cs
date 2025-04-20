using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
//using RuppinProj.Models;
using ExerciseTsadSharat.BL;
using System.Data.Common;

/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{

    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }


    //--------------------------------------------------------------------------------------------------
    // This method update a student to the student table 
    //--------------------------------------------------------------------------------------------------
    //public int Update(Student student)
    //{

    //    SqlConnection con;
    //    SqlCommand cmd;

    //    try
    //    {
    //        con = connect("myProjDB"); // create the connection
    //    }
    //    catch (Exception ex)
    //    {
    //        // write to log
    //        throw (ex);
    //    }

    //    cmd = CreateCommandWithStoredProcedure("spUpdateStudent1", con,student);             // create the command

    //    try
    //    {
    //        int numEffected = cmd.ExecuteNonQuery(); // execute the command
    //        return numEffected;
    //    }
    //    catch (Exception ex)
    //    {
    //        // write to log
    //        throw (ex);
    //    }

    //    finally
    //    {
    //        if (con != null)
    //        {
    //            // close the db connection
    //            con.Close();
    //        }
    //    }

    //}



    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    //private SqlCommand CreateCommandWithStoredProcedure(String spName, SqlConnection con, Student student)
    //{

    //    SqlCommand cmd = new SqlCommand(); // create the command object

    //    cmd.Connection = con;              // assign the connection to the command object

    //    cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

    //    cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

    //    cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

    //    cmd.Parameters.AddWithValue("@id", student.Id);

    //    cmd.Parameters.AddWithValue("@name", student.Name);

    //    cmd.Parameters.AddWithValue("@age", student.Age);


    //    return cmd;
    //}


    //--------------------------------------------------------------------------------------------------
    // This method insert instructor to the isntructor table 
    //--------------------------------------------------------------------------------------------------
    public int insertInstructor(Instructor instructor)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureInsertInstructor("SP_InsertInstructor", con, instructor);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureInsertInstructor(String spName, SqlConnection con, Instructor instructor)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@title", instructor.Title);
        cmd.Parameters.AddWithValue("@name", instructor.Name);
        cmd.Parameters.AddWithValue("@image", instructor.Image);
        cmd.Parameters.AddWithValue("@jobTitle", instructor.JobTitle);

        return cmd;
    }



    //--------------------------------------------------------------------------------------------------
    // This method Get instructors from the isntructor table 
    //--------------------------------------------------------------------------------------------------
    public List<Instructor> readInstructors()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureReadInstructors("SP_ReadInstructors", con); // create the command

        List<Instructor> instructors = new List<Instructor>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Instructor instructor = new Instructor
                {
                    Id = Convert.ToInt32(dataReader["id"]),
                    Title = dataReader["title"].ToString(),
                    Name = dataReader["name"].ToString(),
                    Image = dataReader["image"].ToString(),
                    JobTitle = dataReader["jobTitle"].ToString()
                };
                instructors.Add(instructor);
            }
            return instructors;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureReadInstructors(String spName, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;
    }

    //--------------------------------------------------------------------------------------------------
    // This method update a course in the Courses table 
    //--------------------------------------------------------------------------------------------------
    public int UpdateCourse(Course course)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureUpdateCourse("SP_updateCourse", con, course);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureUpdateCourse(String spName, SqlConnection con, Course course)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@CourseId", course.Id);
        cmd.Parameters.AddWithValue("@Title", course.Title);
        cmd.Parameters.AddWithValue("@Url", course.Url);
        cmd.Parameters.AddWithValue("@Rating", course.Rating);
        cmd.Parameters.AddWithValue("@NumberOfReviews", course.NumberOfReviews);
        cmd.Parameters.AddWithValue("@InstructorId", course.InstructorId);
        cmd.Parameters.AddWithValue("@ImageReference", course.ImageReference);
        cmd.Parameters.AddWithValue("@Duration", course.Duration);
        cmd.Parameters.AddWithValue("@LastUpdate", course.LastUpdate);
        cmd.Parameters.AddWithValue("@IsActive", course.IsActive);

        return cmd;
    }





    //---------------------------------------------------------------------------------
    // this method to read the courses from DB
    //---------------------------------------------------------------------------------
    public List<Course> readCourses()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureReadCourse("SP_readCourses", con); // create the command

        List<Course> courses = new List<Course>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Course course = new Course
                {
                    Id = Convert.ToInt32(dataReader["id"]),
                    Title = dataReader["title"].ToString(),
                    Url = dataReader["url"].ToString(),
                    Rating = float.Parse(dataReader["rating"].ToString()),
                    NumberOfReviews = Convert.ToInt32(dataReader["numberOfReviews"]),
                    InstructorId = Convert.ToInt32(dataReader["instructorId"]),
                    ImageReference = dataReader["imageReference"].ToString(),
                    Duration = float.Parse(dataReader["duration"].ToString()),
                    LastUpdate = dataReader["lastUpdate"].ToString(),
                    IsActive = bool.Parse( dataReader["isActive"].ToString())
                };
                courses.Add(course);
            }
            return courses;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }



    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureReadCourse(String spName, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;
    }


    //---------------------------------------------------------------------------------
    // this method to read a course from DB from Courses Table
    //---------------------------------------------------------------------------------
    public Course readSpecCourseFromCourses(int id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureReadSpecCourseFromCourses("SP_GetCourseByIdFromCourses", con, id); // create the command


        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            if  (dataReader.Read() && Convert.ToInt32(dataReader["Result"]) != 0)
            {
                return new Course
                {
                    Id = Convert.ToInt32(dataReader["id"]),
                    Title = dataReader["title"].ToString(),
                    Url = dataReader["url"].ToString(),
                    Rating = float.Parse(dataReader["rating"].ToString()),
                    NumberOfReviews = Convert.ToInt32(dataReader["numberOfReviews"]),
                    InstructorId = Convert.ToInt32(dataReader["instructorId"]),
                    ImageReference = dataReader["imageReference"].ToString(),
                    Duration = float.Parse(dataReader["duration"].ToString()),
                    LastUpdate = dataReader["lastUpdate"].ToString(),
                    IsActive = bool.Parse(dataReader["isActive"].ToString())
                };
            }
            else
            {
                throw new KeyNotFoundException("Course wasn't found");
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureReadSpecCourseFromCourses(String spName, SqlConnection con, int id)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@id", id);

        return cmd;
    }


    //--------------------------------------------------------------------------------------------------
    // This method insert course to the Courses table 
    //--------------------------------------------------------------------------------------------------
    public int InsertCourseToCourses(Course course)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureInsertCourseToCourses("SP_insertToCourses", con, course);   // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureInsertCourseToCourses(String spName, SqlConnection con, Course course)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text


        cmd.Parameters.AddWithValue("@Title", course.Title);
        cmd.Parameters.AddWithValue("@Url", course.Url);
        cmd.Parameters.AddWithValue("@Rating", course.Rating);
        cmd.Parameters.AddWithValue("@NumberOfReviews", course.NumberOfReviews);
        cmd.Parameters.AddWithValue("@InstructorId", course.InstructorId);
        cmd.Parameters.AddWithValue("@ImageReference", course.ImageReference);
        cmd.Parameters.AddWithValue("@Duration", course.Duration);
        cmd.Parameters.AddWithValue("@LastUpdate", course.LastUpdate);
        cmd.Parameters.AddWithValue("@IsActive", course.IsActive);
        return cmd;
    }





    //--------------------------------------------------------------------------------------------------
    // This method get the courses By their duration from Courses table 
    //--------------------------------------------------------------------------------------------------
    public List<Course> GetCoursesByDurationRange(float minDuration, float maxDuration, int userId)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureGetCoursesByDurationRange("SP_GetCoursesByDurationRange", con, minDuration, maxDuration, userId);   // create the command

        List<Course> courses = new List<Course>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Course course = new Course
                {
                    Id = Convert.ToInt32(dataReader["id"]),
                    Title = dataReader["title"].ToString(),
                    Url = dataReader["url"].ToString(),
                    Rating = float.Parse(dataReader["rating"].ToString()),
                    NumberOfReviews = Convert.ToInt32(dataReader["numberOfReviews"]),
                    InstructorId = Convert.ToInt32(dataReader["instructorId"]),
                    ImageReference = dataReader["imageReference"].ToString(),
                    Duration = float.Parse(dataReader["duration"].ToString()),
                    LastUpdate = dataReader["lastUpdate"].ToString(),
                    IsActive = bool.Parse(dataReader["isActive"].ToString())
                };
                courses.Add(course);
            }
            return courses;
        }


        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureGetCoursesByDurationRange(String spName, SqlConnection con, float minDuration, float maxDuration, int userId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text


        cmd.Parameters.AddWithValue("@MinDuration", minDuration);
        cmd.Parameters.AddWithValue("@MaxDuration", maxDuration);
        cmd.Parameters.AddWithValue("@UserId", userId);

        return cmd;
    }






    //--------------------------------------------------------------------------------------------------
    // This method get the courses By their rating from Courses table 
    //--------------------------------------------------------------------------------------------------
    public List<Course> GetCoursesByRatingRange(float minRating, float maxRating, int userId)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureGetCoursesByRatingRange("SP_GetCoursesByRatingRange", con, minRating, maxRating, userId);   // create the command

        List<Course> courses = new List<Course>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Course course = new Course
                {
                    Id = Convert.ToInt32(dataReader["id"]),
                    Title = dataReader["title"].ToString(),
                    Url = dataReader["url"].ToString(),
                    Rating = float.Parse(dataReader["rating"].ToString()),
                    NumberOfReviews = Convert.ToInt32(dataReader["numberOfReviews"]),
                    InstructorId = Convert.ToInt32(dataReader["instructorId"]),
                    ImageReference = dataReader["imageReference"].ToString(),
                    Duration = float.Parse(dataReader["duration"].ToString()),
                    LastUpdate = dataReader["lastUpdate"].ToString(),
                    IsActive = bool.Parse(dataReader["isActive"].ToString())
                };
                courses.Add(course);
            }
            return courses;
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureGetCoursesByRatingRange(String spName, SqlConnection con, float minRating, float maxRating, int userId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@UserId", userId);
        cmd.Parameters.AddWithValue("@MinRating", minRating);
        cmd.Parameters.AddWithValue("@MaxRating", maxRating);

        return cmd;
    }



    //--------------------------------------------------------------------------------------------------
    // This method Delete Course from User Courses table 
    //--------------------------------------------------------------------------------------------------
    public void DeleteCourseFromUserCourses(int courseId, int userId)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureDeleteCourseFromUserCourses("SP_DeleteCourseFromUseCourse", con, courseId, userId);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dataReader.Read())
            {
                int result = Convert.ToInt32(dataReader["Result"]);

                if (result == 0)
                {
                    throw new ArgumentException("There is no such course");
                }

            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureDeleteCourseFromUserCourses(String spName, SqlConnection con, int courseId, int userId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@CourseId", courseId);
        cmd.Parameters.AddWithValue("@UserId", userId);

        return cmd;
    }



    //--------------------------------------------------------------------------------------------------
    // This method logs the user in
    //--------------------------------------------------------------------------------------------------
    public bool UserLogin(User user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureLoginUsers("SP_LoginUsers", con, user);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            if (dataReader.Read() && Convert.ToInt32(dataReader["UserId"]) >= 0)
            {
                user.Id = Convert.ToInt32(dataReader["UserId"]);
                user.Name = dataReader["name"].ToString();
                user.IsAdmin = Convert.ToBoolean(dataReader["isAdmin"]);
                user.IsActive = Convert.ToBoolean(dataReader["isActive"]);
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureLoginUsers(String spName, SqlConnection con, User user)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@Email", user.Email);
        cmd.Parameters.AddWithValue("@Password", user.Password);

        return cmd;
    }



    //--------------------------------------------------------------------------------------------------
    // This method register a new user
    //--------------------------------------------------------------------------------------------------
    public void UserRegister(User user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureUserRegister("UsersRejester", con, user);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dataReader.Read())
            {
                int result = Convert.ToInt32(dataReader["Result"]);

                if (result == 0)
                {
                    throw new ArgumentException("User with this email already exists");
                }
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureUserRegister(String spName, SqlConnection con, User user)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@name", user.Name);
        cmd.Parameters.AddWithValue("@email", user.Email);
        cmd.Parameters.AddWithValue("@password", user.Password);

        return cmd;
    }


    //--------------------------------------------------------------------------------------------------
    // This method get courses for a specific user
    //--------------------------------------------------------------------------------------------------
    public List<Course> GetCoursesFromUserCourse(int id)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureGetCoursesByRatingRange("SP_readCoursesFromUserCourse", con, id);   // create the command

        List<Course> courses = new List<Course>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Course course = new Course
                {
                    Id = Convert.ToInt32(dataReader["id"]),
                    Title = dataReader["title"].ToString(),
                    Url = dataReader["url"].ToString(),
                    Rating = float.Parse(dataReader["rating"].ToString()),
                    NumberOfReviews = Convert.ToInt32(dataReader["numberOfReviews"]),
                    InstructorId = Convert.ToInt32(dataReader["instructorId"]),
                    ImageReference = dataReader["imageReference"].ToString(),
                    Duration = float.Parse(dataReader["duration"].ToString()),
                    LastUpdate = dataReader["lastUpdate"].ToString(),
                    IsActive = bool.Parse(dataReader["isActive"].ToString())
                };
                courses.Add(course);
            }
            return courses;
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureGetCoursesByRatingRange(String spName, SqlConnection con, int id)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@UserId", id);

        return cmd;
    }


    //---------------------------------------------------------------------------------
    // this method to read the Users from DB table
    //---------------------------------------------------------------------------------
    public List<User> readUsers()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureReadUsers("SP_ReadUser", con); // create the command

        List<User> users = new List<User>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                User user = new User()
                {
                    Id = Convert.ToInt32(dataReader["id"]),
                    Name = dataReader["name"].ToString(),
                    Email = dataReader["email"].ToString(),
                    Password = dataReader["password"].ToString(),
                    IsAdmin = Convert.ToBoolean(dataReader["isAdmin"]),
                    IsActive = Convert.ToBoolean(dataReader["isActive"])
                };
                users.Add(user);
            }
            return users;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureReadUsers(String spName, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;
    }

    //--------------------------------------------------------------------------------------------------
    // This method insert course to the Courses table 
    //--------------------------------------------------------------------------------------------------
    public void InsertCourseToUserCourse(int courseId, int useId)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureInsertCourseToUserCourse("SP_InsertToUserCourse", con, courseId, useId);   // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dataReader.Read())
            {
                int result = Convert.ToInt32(dataReader["Result"]);

                if (result == 0)
                {
                    throw new ArgumentException();
                }

            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureInsertCourseToUserCourse(String spName, SqlConnection con, int courseId, int userId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text


        cmd.Parameters.AddWithValue("@UserId", userId);
        cmd.Parameters.AddWithValue("@CourseId", courseId);

        return cmd;
    }



    //--------------------------------------------------------------------------------------------------
    // This method insert course to the Courses table 
    //--------------------------------------------------------------------------------------------------
    public bool CheckInstructorExists(int instructorId)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureCheckInstructorExists("SP_CheckInstructorExists", con, instructorId);   // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dataReader.Read() && Convert.ToInt32(dataReader["Result"]) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureCheckInstructorExists(String spName, SqlConnection con, int instructorId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text


        cmd.Parameters.AddWithValue("@InstructorId", instructorId);

        return cmd;
    }


    //--------------------------------------------------------------------------------------------------
    // This method get the courses By their duration from Courses table 
    //--------------------------------------------------------------------------------------------------
    public List<Course> GetCoursesByInstructorId(int instructorId)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureGetCoursesByInstructorId("SP_GetCoursesByInstructorId", con, instructorId);   // create the command

        List<Course> courses = new List<Course>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                int result = Convert.ToInt32(dataReader["Result"]);
                if (result == 0)
                {
                    throw new ArgumentNullException("There is no instructor with this id");
                }
                else if (result == 1)
                {
                    Course course = new Course
                    {
                        Id = Convert.ToInt32(dataReader["id"]),
                        Title = dataReader["title"].ToString(),
                        Url = dataReader["url"].ToString(),
                        Rating = float.Parse(dataReader["rating"].ToString()),
                        NumberOfReviews = Convert.ToInt32(dataReader["numberOfReviews"]),
                        InstructorId = Convert.ToInt32(dataReader["instructorId"]),
                        ImageReference = dataReader["imageReference"].ToString(),
                        Duration = float.Parse(dataReader["duration"].ToString()),
                        LastUpdate = dataReader["lastUpdate"].ToString(),
                        IsActive = bool.Parse(dataReader["isActive"].ToString())
                    };
                    courses.Add(course);
                }
            }
            return courses;
        }


        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureGetCoursesByInstructorId(String spName, SqlConnection con, int instructorId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text


        cmd.Parameters.AddWithValue("@instructorId", instructorId);

        return cmd;
    }





    //---------------------------------------------------------------------------------
    // this method to read a instructors from DB from instructors Table
    //---------------------------------------------------------------------------------
    public Instructor readSpecInstructorFromInstructors(int id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureReadSpecInstructorFromInstructors("SP_GetInstructorById", con, id); // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            if (dataReader.Read() && Convert.ToInt32(dataReader["Result"]) != 0)
            {
                return new Instructor
                {
                    Id = Convert.ToInt32(dataReader["id"]),
                    Title = dataReader["title"].ToString(),
                    Name = dataReader["name"].ToString(),
                    Image = dataReader["image"].ToString(),
                    JobTitle = dataReader["jobTitle"].ToString()
                };
            }
            else
            {
                throw new KeyNotFoundException("Instructor wasn't found");
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureReadSpecInstructorFromInstructors(String spName, SqlConnection con, int id)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@id", id);

        return cmd;
    }





    //--------------------------------------------------------------------------------------------------
    // This method is changing the attribute of the variable isActive
    //--------------------------------------------------------------------------------------------------
    public void changeIsActiveAttributeAndTitle(int courseId,bool isActiveNewValue,string title)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureChangeIsActiveAttributeAndTitle("SP_UpdateCourseIsActiveAndTitle", con, courseId,isActiveNewValue,title);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dataReader.Read())
            {
                int result = Convert.ToInt32(dataReader["Result"]);

                if (result == 0)
                {
                    throw new ArgumentException("No course found with the specified CourseId.");
                }

            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureChangeIsActiveAttributeAndTitle(String spName, SqlConnection con, int courseId, bool isActiveNewValue,string title)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@CourseId", courseId);
        cmd.Parameters.AddWithValue("@IsActive", isActiveNewValue);
        cmd.Parameters.AddWithValue("@CourseName", title);
        return cmd;
    }







    //--------------------------------------------------------------------------------------------------
    // This method get Top 5 Courses By number of users registered
    //--------------------------------------------------------------------------------------------------
    public List<object> GetTop5CoursesCourses()
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureGetTop5CoursesCourses("SP_GetTop5Corses", con);   // create the command

        //  List<Course> courses = new List<Course>();
        List<Object> listObjs = new List<Object>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                //Course course = new Course
                listObjs.Add(new
                {
                    id = Convert.ToInt32(dataReader["Id"]),
                    courseName = dataReader["CourseName"].ToString(),
                    rating = float.Parse(dataReader["CourseRating"].ToString()),
                    numberOfUsers = Convert.ToInt32(dataReader["NumberOfUsers"])

                });
              //  course.Add(course);
            }
            return listObjs;
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureGetTop5CoursesCourses(String spName, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text


        return cmd;
    }

}
