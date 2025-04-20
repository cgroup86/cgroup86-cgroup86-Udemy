let coursesData = [];
//let port = "7123";

let topFiveCourses=[];
$(document).ready(function () {

    // Gets the json data and store it into an array
    // Calls renderCourses(data) to Show the courses on the page when its opened
    //$.getJSON("../data/course.json", function (data) {
    //    coursesData = data;
    //    renderCourses(data);
    //});

    getTopFiveCourses();
    // Calls getFromServer when the page is loaded to show all the courses in the DB
    getFromServer();
 
    $(document).on('click', '.showMoreCoursesOfThisInstructor', function () {
        getInstructorIdFromThisCourse($(this).data("course-id"));
    });

    $('#loginPageBTN').click(function () {
        const user = JSON.parse(sessionStorage.getItem('userData'));
        if (!user || !user.isLoggedIn) {
            window.location.href = "login.html";
        }
        else {
            alert("You already logged in");
        }

    });

    $('#logoutBTN').click(function () {
        const user = JSON.parse(sessionStorage.getItem('userData'));
        if (user && user.isLoggedIn) {
            sessionStorage.removeItem('userData');
            alert("Logged out successfully");
        }
        else {
            alert("You can't logout without being logged in");
        }
    });

    $('#adminPageBTN').click(function () {
        const user = JSON.parse(sessionStorage.getItem('userData'));
        if (!user || !user.isAdmin) {
            alert("You need to be an admin to go to this page");
        }
        else {
            window.location.href = "admin.html";
        }

    });

    // On click opens MyCoursses.html in another page in the browser
    $('#coursesPageBTN').click(function () {
        const user = JSON.parse(sessionStorage.getItem('userData'));
        if (!user || !user.isLoggedIn) {
            alert("You need to log in to see the courses you added");
            window.location.href = "login.html";
        }
        else {
            window.location.href = "MyCourses.html";
        }       
    });

    // On click opens instructorPage.html in another page in the browser
    $('#instructorsPageBTN').click(function () {
        window.location.href = "instructorsPage.html";
    });
});
//------------------------------------------------------------

function getFromServer() {
    console.log("Hi from getFromServer");
    //let api = `https://localhost:${port}/api/Courses`;
    let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Courses`;
    ajaxCall("GET", api, "", getCoursesSCB, getCoursesECB);
}
function getCoursesSCB(coursesList) {
    console.log(coursesList);
    coursesData = coursesList;
    renderCourses(coursesList);
}

function getCoursesECB(err) {
    console.log(err);
}

//------------------------------------------------------------

// Did this function to load the courses from the Data base/ Json
//function renderCourses(courses) {
//    // Gets the div id from html page
//    var coursesList = $("#courses-list");
//    let counter = 0;

//    $.each(courses, function (index, course) {
//        var courseBox = `
//                <div class="course-card" data-id="${course.id}">
//                    <section class="upper-section">
//                        <img src="${course.imageReference}" alt="">
//                        <div>
//                            <p><strong>Last update</strong> <br><span>${course.lastUpdate}</span></p>
//                            <p><strong>Duration</strong> <br><span>${course.duration} total hours</span></p>
//                            <p><strong>Instructor Id:</strong><br><span>${}</span></p>
//                        </div>
//                        <h3>
//                            <a href="${course.url}">${course.title}</a>
//                        </h3>
//                    </section>
//                    <section class="down-section">
//                        <div class="rating">
//                            <p>Rating <br>
//                                <span class="stars">
//                                    <i class="fa-solid fa-star" style="color: yellow;"></i>
//                                </span>
//                                ${course.rating.toFixed(2)}
//                            </p>
//                        </div>
//                        <p class="reviews-number">
//                            Number of reviews <br><span>${course.numberOfReviews}</span>
//                        </p>
//                        <button onclick="addCourseToServer(${counter})">Add course</button>
//                    </section>
//                    <button class="showMoreCoursesOfThisInstructor" data-course-id="${course.id}">Show more courses of this instructor</button>
//                </div>
//            `;
//        coursesList.append(courseBox);
//    });
//}

//--------------------------------------------------------------------
function renderCourses(courses) {
    var coursesList = $("#courses-list");
    let counter = 0;
    $.each(courses, function (index, course) {
        if (course.isActive) {
            var courseBox = `
            <div class="course-card" data-id="${course.id}">
                <section class="upper-section">
                    <img src="${course.imageReference}" alt="">
                    <div>
                        <p><strong>Last update</strong> <br><span>${course.lastUpdate}</span></p>
                        <p><strong>Duration</strong> <br><span>${course.duration} total hours</span></p>
                        <p><strong>Instructor Name:</strong><br><span id="instructor-${course.id}">Loading...</span></p>
                    </div>
                    <h3>
                        <a href="${course.url}">${course.title}</a>
                    </h3>
                </section>
                <section class="down-section">
                    <div class="rating">
                        <p>Rating <br>
                            <span class="stars">
                                <i class="fa-solid fa-star" style="color: yellow;"></i>
                            </span>
                            ${course.rating.toFixed(2)}
                        </p>
                    </div>
                    <p class="reviews-number">
                        Number of reviews <br><span>${course.numberOfReviews}</span>
                    </p>
                    <button onclick="addCourseToServer(${counter}, 1)">Add course</button>
                </section>
                <button class="showMoreCoursesOfThisInstructor" data-course-id="${course.id}">Show more courses of this instructor</button>
            </div>
        `;
            coursesList.append(courseBox);

            // Fetch and update the instructor name
            getInstructorNameById(course.instructorId, function (instructorName) {
                $(`#instructor-${course.id}`).text(instructorName);
            });
        }
        // Increment the counter for the next course
        counter++;
    });
}

//--------------------------------------------------------------------

// Adds a course to the DB
function addCourseToServer(index, x) {
    const user = JSON.parse(sessionStorage.getItem('userData'));
    if (!user || !user.isLoggedIn) {
        alert("You need to log in to add a course");
        window.location.href = "login.html";
    }
    else {
        console.log("hi from addCourseToServer");

        //let api = `https://localhost:${port}/api/Users/PostMyCourses`;
        let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Users/PostMyCourses`;
        api += `?id= ${user.userId}`;

        let course;
        if (x == 1) {
            course = coursesData[index];
        }
        else if (x == 2) {
            course = topFiveCourses[index];
        }

        // Putting the data into object from json type
        console.log(course);
        console.log(index);
        const courseData = {
            Id: course.id,
            Title: course.title,
            Url: course.url,
            Rating: course.rating,
            NumberOfReviews: course.numberOfReviews,
            InstructorId: course.instructorId,
            ImageReference: course.imageReference,
            Duration: course.duration,
            LastUpdate: course.lastUpdate,
        };
        ajaxCall("POST", api, JSON.stringify(courseData), addCoursesSCB, addCoursesECB);
    }
    
}

// Gets the numbers from the start of the string (example: 22 total hours)
//function extractDuration(durationStr) {
//    const durationMatch = durationStr.match(/\d+(\.\d+)?/);
//    return parseFloat(durationMatch[0]);
//}

function addCoursesSCB(status) {
    console.log(status);
    if (status == true) { alert("Course has been added successfully"); }
    else { alert("The course already exists"); }
}

function addCoursesECB(err) {
    console.log(err);
}
//-----------------------------------------------------------------------------------------------------------

//function getInstructorNameById(id, callback) {
//    id = parseInt(id);
//    let api = `https://localhost:${port}/api/Instructors/GetInstructorById/${id}`;
//    //let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Courses/GetInstructorById/${id}`;

//    ajaxCall("GET", api, null, function (instructor) {
//        callback(instructor.name);
//    }, function (err) {
//        console.log(err);
//        callback("Unknown Instructor"); // Fallback in case of error
//    });
//}



function getTopFiveCourses() {
    console.log("Hi from getFromServer");
    //let api = `https://localhost:${port}/api/Courses/GetTop5Courses`;
    let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Courses/GetTop5Courses`;
    ajaxCall("GET", api, "", getTopFiveCoursesSCB, getTopFiveCoursesECB);
}
function getTopFiveCoursesSCB(top5Courses) {
    console.log(top5Courses);
    renderTopFiveCourses(top5Courses);
}

function getTopFiveCoursesECB(err) {
    console.log(err);
}




function renderTopFiveCourses(courses) {
    var coursesList = $("#top-5-courses");
    let counter = 0;
    $.each(courses, function (index, course) {
        var courseBox = `
            <div class="course-card" data-id="${course.id}">
                <section class="upper-section">
                    <img id="imageReference-${course.id}" src="" alt="Loading...">
                    <div>
                        <p><strong>Last update</strong> <br><span id="lastUpdate-${course.id}">Loading...</span></p>
                        <p><strong>Duration</strong> <br><span id="duration-${course.id}">Loading...</span></p>
                        <p><strong>Instructor Name:</strong><br><span id="instructor1-${course.id}">Loading...</span></p>
                    </div>
                    <h3>
                        <a id="url-${course.id}" href="">${course.courseName}</a>
                    </h3>
                    <p><strong>Number of users:</strong> <br> ${course.numberOfUsers}</p>
                </section>
                <section class="down-section">
                    <div class="rating">
                        <p>Rating <br>
                            <span class="stars">
                                <i class="fa-solid fa-star" style="color: yellow;"></i>
                            </span>
                            ${course.rating.toFixed(2)}
                        </p>
                    </div>
                    <p class="reviews-number">
                        Number of reviews <br><span id="numberOfReviews-${course.id}">Loading...</span>
                    </p>
                    <button onclick="addCourseToServer(${counter}, 2)">Add course</button>
                </section>
                <button class="showMoreCoursesOfThisInstructor" data-course-id="${course.id}">Show more courses of this instructor</button>
            </div>
        `;
        coursesList.append(courseBox);

        // Fetch and update the course attributes
        GetCourseById(course.id, function (specCourse) {
            $(`#duration-${course.id}`).text(specCourse.duration+" total hours");
            $(`#numberOfReviews-${course.id}`).text(specCourse.numberOfReviews);
            $(`#lastUpdate-${course.id}`).text(specCourse.lastUpdate);
            $(`#imageReference-${course.id}`).attr('src', course.imageReference);
            $(`#imageReference-${course.id}`).attr('src', specCourse.imageReference);
            $(`#url-${course.id}`).attr('href', specCourse.url).text(specCourse.courseName);
            getInstructorNameById(specCourse.instructorId, function (instructorName) {
                $(`#instructor1-${course.id}`).text(instructorName);
            });

            topFiveCourses.splice(counter, 0, specCourse);

        });
        // Fetch and update the instructor name
        //getInstructorNameById(course.instructorId, function (instructorName) {
        //    $(`#instructor-${course.id}`).text(instructorName);
        //});

        // Increment the counter for the next course
        counter++;
    });
}

//--------------------------------------------------------------------

function GetCourseById(id, callback) {
    id = parseInt(id);
    //let api = `https://localhost:${port}/api/Courses/GetCourseById/${id}`;
    
    let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Courses/GetCourseById/${id}`;

    ajaxCall("GET", api, null, function (course) {
        callback(course);
    }, function (err) {
        console.log(err);
    });
}
//"id": 175,
//    "courseName": "Complete UiPath RPA Developer Course: Build 7 Robots",
//        "rating": 4.597488,
//            "numberOfUsers": 4