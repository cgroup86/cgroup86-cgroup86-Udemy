//const port = "7123";
let filteredCourses = []; // Is made to make sure when deleting a course the filtered data stay on the screen
let deletedCourseId; // Is made to store the deleted course Id so we can remove it from the filtered list
$(document).ready(function () {
    // Calls getFromServer when the page is loaded to show all the courses in the DB
    getFromServer();
    $('#GoHomeBtn').click(function () {
        localStorage.removeItem("courses");
        window.location.href = "index.html";
    });

    // On click on search button by duration calls GetByDurationRangeFromServer
    $("#minMaxDurBTN").click(GetByDurationRangeFromServer);

    // On click on search button by duration calls GetByRatingRangeFromServer
    $("#minMaxRateBTN").click(GetByRatingRangeFromServer);

    // Calls RemoveCourseByIdFromServer to remove a specific course from the data base while providing its id
    $(document).on('click', '.remove-course-btn', function () {
        RemoveCourseByIdFromServer($(this).data("course-id"));
    });

    // Used to reset the page and show all the courses in the DB after searching for a specific courses
    $("#resetPage").click(getFromServer);

    $(document).on('click', '.showMoreCoursesOfThisInstructor', function () {
        getInstructorIdFromThisCourse($(this).data("course-id"));
    });
});
//----------------------------------------------------------

// Did this to load the courses when the page is opened
function getFromServer() {
    console.log("Hi from getFromServer");
    const user = JSON.parse(sessionStorage.getItem('userData'));
    //let api = `https://localhost:${port}/api/Users/GetMyCourses`;
    let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Users/GetMyCourses`;
    api += `?id=${user.userId}`;
    ajaxCall("GET", api, "", getCoursesSCB, getCoursesECB);
}
function getCoursesSCB(coursesList) {
    console.log(coursesList);
    filteredCourses = coursesList;
    renderCourses(coursesList);
}

function getCoursesECB(err) {
    console.log(err);
}

//----------------------------------------------------------

// Did this function to load the courses from the Data base/ Json
function renderCourses(courses) {
    var coursesList = $("#courses-list");
    coursesList.empty();
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
                    <button class="remove-course-btn" data-course-id="${course.id}">Remove course</button>
                </section>
                <button class="showMoreCoursesOfThisInstructor" data-course-id="${course.id}">Show more courses of this instructor</button>
            </div>
        `;

        coursesList.append(courseBox);

        // Fetch and update the instructor name
        getInstructorNameById(course.instructorId, function (instructorName) {
            $(`#instructor-${course.id}`).text(instructorName);
        });
    });
}


//----------------------------------------------------------

// Did this to search courses in a duration range
function GetByDurationRangeFromServer() {
    console.log("In duration range from server");
    //let api = `https://localhost:${port}/api/Users/SearchByDuration`;
    let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Users/SearchByDuration`;

    let s = {
            minDuration: $("#minDurTB").val(),
            maxDuration: $("#maxDurTB").val()
    }

    // Calls the function to made sure that the input is in the right range and format
    if (!isValidDurationRange(s.minDuration, s.maxDuration)) {
        alert("Make sure to enter valid numbers. Minimum duration must be less or equal to maximum duration and both must be greater than -1.");
    }
    else {
        const user = JSON.parse(sessionStorage.getItem('userData'));
        api += `?minDuration=${s.minDuration}&maxDuration=${s.maxDuration}&id=${user.userId}`;

        ajaxCall("GET", api, "", getCoursesDurationSCB, getCoursesDurationECB);
    }
}

function getCoursesDurationSCB(coursesList) {
    console.log(coursesList);
    filteredCourses = coursesList;
    renderCourses(coursesList);
}

function getCoursesDurationECB(err) {
    console.log(err);
}

//----------------------------------------------------------

// Did this to search courses in a rating range
function GetByRatingRangeFromServer() {
    console.log("In rating range from server");
    //let api = `https://localhost:${port}/api/Users/searchByRating/`;
    let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Users/searchByRating/`;

    let s = {
        minRating: $("#minRateTB").val(),
        maxRating: $("#maxRateTB").val()
    }

    // Calls the function to made sure that the input is in the right range and format
    if (!isValidRatingRange(s.minRating, s.maxRating))
    {
        alert("Make sure to enter valid numbers. Minimum rating must be less or equal to the maximum rating and both must be between 0 and 5.");
    }
    else {
        const user = JSON.parse(sessionStorage.getItem('userData'));
        api += `Minimum rating/${s.minRating}/Maximum rating/${s.maxRating}/Id/${user.userId}`;

        ajaxCall("GET", api, "", getCoursesRateSCB, getCoursesRateECB);
    }
    
}

function getCoursesRateSCB(coursesList) {
    console.log(coursesList);
    filteredCourses = coursesList;
    renderCourses(coursesList);
}

function getCoursesRateECB(err) {
    console.log(err);
}

//----------------------------------------------------------

// Did this to remove a course from the Data base
function RemoveCourseByIdFromServer(courseId) {
    console.log("In removing course from server");
    console.log(courseId);
    deletedCourseId = courseId;
    const user = JSON.parse(sessionStorage.getItem('userData'));
    //let api = `https://localhost:${port}/api/Users/DeleteCourseById/CourseId/${courseId}/Id/${user.userId}`;
    let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Users/DeleteCourseById/CourseId/${courseId}/Id/${user.userId}`;
    ajaxCall("DELETE", api, "", removeCourseWithIdSCB, removeCourseWithIdECB);
}

function removeCourseWithIdSCB(stats) {
    console.log("hi");
    console.log(stats);
    console.log("hi");
    filteredCourses = filteredCourses.filter(course => course.id !== deletedCourseId);
    renderCourses(filteredCourses)
}

function removeCourseWithIdECB(err) {
    console.log(err);
}

//-----------------------------------------------------------------

// I did this to check if the numbers are valid and to avoid errors
// check if min > max / max > min
function isValidDurationRange(min, max) {
    if (isNaN(min) || isNaN(max)) {
        return false;
    }
    if (parseFloat(min) <= -1 || parseFloat(max) <= -1) {
        return false;
    }

    min = parseFloat(min);
    max = parseFloat(max);

    return min <= max; 
}

// I did this to check if the numbers are valid and to avoid errors
// check if min > max / max > min
function isValidRatingRange(min, max) {
    if (isNaN(min) || isNaN(max)) {
        return false;
    }
    if (parseFloat(min) < 0 || parseFloat(max) < 0 || parseFloat(min) > 5 || parseFloat(max) > 5) {
        return false;
    }

    min = parseFloat(min);
    max = parseFloat(max);

    return min <= max; 
}