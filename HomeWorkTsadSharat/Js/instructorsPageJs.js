//let port = "7123";

$(document).ready(function () {
    // On click gets the json from that data folder and sends it to the function to insert the instructors data into the DB
    //$('#insertBTN').click(function () {
    //    $.getJSON("../data/Instructor.json", function (data) {
    //        insertInstructorsToDB(data);
    //    });
    //});

    // Calls a function that gets the data from the server and show them on the page
    //$('#getBTN').click(function () {
    //    getAllInstructorsFromDB();
    //});
    $('#GoHomeBtn').click(function () {
        localStorage.removeItem("courses");
        window.location.href = "index.html";
    });

    getFromServer()

    $(document).on('click', '.showMoreCoursesOfThisInstructor', function () {
        getCoursesByInstructorId($(this).data("instructor-id"));
    });
});
//---------------------------------------------------------------------------------
function getFromServer() {
    console.log("Hi from getFromServer");
    //let api = `https://localhost:${port}/api/Instructors`;
    let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Instructors`;
    ajaxCall("GET", api, "", getInstructorsSCB, getInstructorsECB);
}
function getInstructorsSCB(instructors) {
    console.log(instructors);
    renderInstructors(instructors);
}

function getInstructorsECB(err) {
    console.log(err);
}
//---------------------------------------------------------------------------------

// Loads the instructors to page
function renderInstructors(instructors) {
    var instructorsList = $("#instructors-list");
    instructorsList.empty();
    instructors.forEach(function (instructor) {
        var instructorBox = `
        <div class="profile-card-container">
            <div class="profile-card">
                <div class="card-image">
                    <img src="${instructor.image}" alt="${instructor.title}">
                </div>
                <div class="card-details">
                    <button class="showMoreCoursesOfThisInstructor" data-instructor-id="${instructor.id}">Show courses</button>
                    <div class="details-container">
                        <p id="profile-title" class="large-text">Title: ${instructor.title}</p>
                        <p id="profile-display-name" class="italic-text">Display name: ${instructor.name}</p>
                        <p id="profile-job-title" class="green-text">Job title: ${instructor.jobTitle}</p>
                    </div>
                </div>
            </div>
        </div>
    `;
        instructorsList.append(instructorBox);
    });

}
//<p id="profile-id" class="bold-text">Id: ${instructor.id}</p>
//---------------------------------------------------------------------------------

//// Inserts the instructors into the DB
//function insertInstructorsToDB(data) {
//    console.log("hi from insertInstructorsToDB");
    
//    //let api = `https://localhost:${port}/api/Instructors`;
//    let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Instructors`;

//    // Go through a loop to insert the instructors, one everytime 
//    data.forEach(instructor => {
//        const instructorData = {
//            Id: instructor.id,
//            Title: instructor.title,
//            Name: instructor.display_name,
//            Image: instructor.image_100x100,
//            JobTitle: instructor.job_title,
//        };
//        ajaxCall("POST", api, JSON.stringify(instructorData), insertInstructorsToDBSCB, insertInstructorsToDBECB);
//    })
//    alert("Inserted instructors to the data base successfully");
//}

//function insertInstructorsToDBSCB(stats) {
//    console.log(stats);
//}

//function insertInstructorsToDBECB(err) {
//    console.log(err);
//    alert("Failed to insert the instructors to the data base");
//}

//-------------------------------------------------

// Gets the instructors list from the DB and with success calls renderInstructors to show the instructors data on the page
//function getAllInstructorsFromDB() {
//    //let api = `https://localhost:${port}/api/Instructors`;
//    let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Instructors`;

//    ajaxCall("GET", api, "", getAllInstructorsFromDBSCB, getAllInstructorsFromDBECB);

//}

//function getAllInstructorsFromDBSCB(instructorsList) {
//    console.log(instructorsList);
//    renderInstructors(instructorsList);
//}

//function getAllInstructorsFromDBECB(err) {
//    console.log(err);
//}