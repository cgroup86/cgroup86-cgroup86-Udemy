//const port = "7123";
let courses;

$(document).ready(function () {
    $('#GoHomeBtn').click(function () {
        localStorage.removeItem("courses");
        window.location.href = "index.html";
    });

    courses = JSON.parse(localStorage.getItem("courses"));
    if (courses) {
        console.log(courses);
        renderCoursesByInstructorList(courses);
        localStorage.removeItem("courses");
    } else {
        console.log("No courses found in local storage.");
    }
});

//------------------------------------------------------------------------------------

function getCoursesByInstructorId(id) {
    id = parseInt(id);
    //let api = `https://localhost:${port}/api/Courses/GetCoursesByInstructorId/${id}`;
    let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Courses/GetCoursesByInstructorId/${id}`;
    ajaxCall("GET", api, null, getCoursesByInstructorIdSCB, getCoursesByInstructorIdECB);
}

function getCoursesByInstructorIdSCB(listOfCourses) {
    console.log(listOfCourses);
    localStorage.setItem("courses", JSON.stringify(listOfCourses));
    window.location.href = "instructorCourses.html";
}

function getCoursesByInstructorIdECB(err) {
    alert("Please type the correct Id");
    console.log(err);
}
//------------------------------------------------------------------------------------
function renderCoursesByInstructorList(courses) {
    var coursesList = $("#courses-list-by-instrutor-id");
    coursesList.empty();
    let counter = 0;
    $.each(courses, function (index, course) {
        var courseTitle = course.isActive
            ? `<a href="${course.url}">${course.title}</a>`
            : `<a class="inactive-course-title" style="color: red;">${course.title}</a>`;

        var courseBox = `
            <div class="course-card${course.isActive ? '' : ' inactive'}" data-id="${course.id}">
                <section class="upper-section">
                    <img src="${course.imageReference}" alt="">
                    <div>
                        <p><strong>Last update</strong> <br><span>${course.lastUpdate}</span></p>
                        <p><strong>Duration</strong> <br><span>${course.duration} total hours</span></p>
                        <p><strong>Instructor Name:</strong><br><span id="instructor-${course.id}">Loading...</span></p>
                    </div>
                    <h3>
                         ${courseTitle}
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
                    ${course.isActive ?
                          `<button onclick="addCourseToServerFromAnyWhere(${counter})">Add course</button>`
                        : `<span class="inactive-notice">This course is not active</span>`
                    }
                </section>
            </div>
        `;
        // Each button course have the id of the course that the button found on him

        coursesList.append(courseBox);

        getInstructorNameById(course.instructorId, function (instructorName) {
            $(`#instructor-${course.id}`).text(instructorName);
        });

        counter++
    });
}

//------------------------------------------------------------------------------------
function getInstructorIdFromThisCourse(id) {
    id = parseInt(id);
    //let api = `https://localhost:${port}/api/Courses/GetCourseById/${id}`;
    let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Courses/GetCourseById/${id}`;
    ajaxCall("GET", api, null, getInstructorIdFromThisCourseSCB, getInstructorIdFromThisCourseECB);
}

function getInstructorIdFromThisCourseSCB(course) {
    getCoursesByInstructorId(course.instructorId);
}

function getInstructorIdFromThisCourseECB(err) {
    console.log(err);
}


//--------------------------------------------------------------------

// Adds a course to the DB
function addCourseToServerFromAnyWhere(index) {
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
        // Putting the data into object from json type
        const courseData = {
            Id: courses[index].id,
            Title: courses[index].title,
            Url: courses[index].url,
            Rating: courses[index].rating,
            NumberOfReviews: courses[index].numberOfReviews,
            InstructorId: courses[index].instructorId,
            ImageReference: courses[index].imageReference,
            Duration: courses[index].duration,
            LastUpdate: courses[index].lastUpdate,
        };
        ajaxCall("POST", api, JSON.stringify(courseData), addCourseToServerFromAnyWhereSCB, addCourseToServerFromAnyWhereECB);
    }

}

function addCourseToServerFromAnyWhereSCB(status) {
    console.log(status);
    if (status == true) { alert("Course has been added successfully"); }
    else { alert("The course already exists"); }
}

function addCourseToServerFromAnyWhereECB(err) {
    console.log(err);
}

//--------------------------------------------------------------------

function getInstructorNameById(id, callback) {
    id = parseInt(id);
    //let api = `https://localhost:${port}/api/Instructors/GetInstructorById/${id}`;
    let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Instructors/GetInstructorById/${id}`;

    ajaxCall("GET", api, null, function (instructor) {
        callback(instructor.name);
    }, function (err) {
        console.log(err);
    });
}
//--------------------------------------------------------------------
