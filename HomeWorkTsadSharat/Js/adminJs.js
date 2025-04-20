//let coursesData = [];
let selectedOption;
let coursesForDataTable = [];
let specCourseForDataTable = null;
let updateDataTable = false;
let uploadedImageUrl = '';
$(document).ready(function () {
    var $ = jQuery.noConflict();
    $('#editFormImageFileBtn').click(function (e) {
        e.preventDefault();
        if ($('#courseImage').val().trim() !== '') {
            if (confirm("Uploading a file will remove the current image link. Are you sure you want to upload a file?")) {
                $('#courseImage').val('');
                $('#courseImageFile').show();
                $('#courseImage').hide();
                $('#editFormImageFileBtn').hide();
                $('#editFormImageLinkBtn').show();
            }
        } else {
            $('#courseImageFile').show();
            $('#courseImage').hide();
            $('#editFormImageFileBtn').hide();
            $('#editFormImageLinkBtn').show();
        }
    });

    $('#editFormImageLinkBtn').click(function (e) {
        e.preventDefault();
        if ($('#courseImageFile').val().trim() !== '') {
            // Ask for confirmation before putting a link if there is an existing file
            if (confirm("Putting a link will remove the current file upload. Are you sure you want to put a link?")) {
                $('#courseImageFile').val('');
                $('#courseImageFile').hide();
                $('#courseImage').show();
                $('#editFormImageFileBtn').show();
                $('#editFormImageLinkBtn').hide();
            }
        } else {
            $('#courseImageFile').hide();
            $('#courseImage').show();
            $('#editFormImageFileBtn').show();
            $('#editFormImageLinkBtn').hide();
        }
    });

    $('#insertFormImageFileBtn').click(function (e) {
        e.preventDefault();
        if ($('#newCourseImage').val().trim() !== '') {
            if (confirm("Uploading a file will remove the current image link. Are you sure you want to upload a file?")) {
                $('#newCourseImage').val('');
                $('#newCourseImageFile').show();
                $('#newCourseImage').hide();
                $('#insertFormImageFileBtn').hide();
                $('#insertFormImageLinkBtn').show();
            }
        } else {
            $('#newCourseImageFile').show();
            $('#newCourseImage').hide();
            $('#insertFormImageFileBtn').hide();
            $('#insertFormImageLinkBtn').show();
        }
    });

    $('#insertFormImageLinkBtn').click(function (e) {
        e.preventDefault();
        if ($('#newCourseImageFile').val().trim() !== '') {
            // Ask for confirmation before putting a link if there is an existing file
            if (confirm("Putting a link will remove the current file upload. Are you sure you want to put a link?")) {
                $('#newCourseImageFile').val('');
                $('#newCourseImageFile').hide();
                $('#newCourseImage').show();
                $('#insertFormImageFileBtn').show();
                $('#insertFormImageLinkBtn').hide();
            }
        } else {
            $('#newCourseImageFile').hide();
            $('#newCourseImage').show();
            $('#insertFormImageFileBtn').show();
            $('#insertFormImageLinkBtn').hide();
        }
    });

    $('#showCoursesButtonForDataTable').click(function () {
        getCoursesToTable();
        $('#pForm').show();
        $(this).hide();
        $('#hideCoursesButtonForDataTable').show();
    });
    $('#hideCoursesButtonForDataTable').click(function () {
        $('#pForm').hide();
        $(this).hide();
        $('#showCoursesButtonForDataTable').show();
    });
    $(document).on('click', '.showMoreCoursesOfThisInstructor', function () {
       getInstructorIdFromThisCourse($(this).data("course-id"));
    });    


    $('#showInsertFormBtn').click(function () {
        $('#newCourseForm').show();
        $(this).hide();
    });

    $('#hideInsertFormBtn').click(function () {
        $('#newCourseForm')[0].reset();

        $('#newCourseForm').hide();
        $('#showInsertFormBtn').show();

        $('#newCourseImageFile').hide();
        $('#newCourseImage').show();
        $('#insertFormImageFileBtn').show();
        $('#insertFormImageLinkBtn').hide();
    });

    $('#showEditFormBtn').click(function () {
        selectedOption = $('#courseSelector').val();
        if (selectedOption) {
            displayCourseDetails(selectedOption);
            $('#editCourseForm').show();
            $(this).hide();
        }
    });

    $('#hideEditFormBtn').click(function () {
        $('#editCourseForm')[0].reset();

        $('#editCourseForm').hide();
        $('#showEditFormBtn').show();

        $('#courseImageFile').hide();
        $('#courseImage').show();
        $('#editFormImageFileBtn').show();
        $('#editFormImageLinkBtn').hide();
    });

    $('#courseSelector').on('input', function () {
        selectedOption = $(this).val();

        if (selectedOption && $('#showEditFormBtn').data('clicked')) {
            displayCourseDetails(selectedOption);
            $('#editCourseForm').show();
            $('#showEditFormBtn').hide();
        } else {
            $('#editCourseForm').hide();
            $('#showEditFormBtn').show();
        }

    });

    // Validation for URL
    $('#courseUrl').on('input', function () {
        const urlRegex = /^https:\/\/(?:www\.)?[\w\-]+(\.[\w\-]+)*(\.[a-zA-Z]{2,})([\/\w\-\.]*)*(\?.*)?$/;
        const value = $(this).val();
        const message = "Invalid URL. It should be in the format: https://www.example.com";
        this.setCustomValidity(urlRegex.test(value) ? "" : message);
    });

    // Validation for Image URL
    $('#courseImage').on('input', function () {
        const imageUrlRegex = /^https:\/\/(?:localhost:\d{1,5}|(?:www\.)?[\w\-]+(\.[\w\-]+)*\.com)(?::\d{1,5})?(\/[\w\-\.]*)*\/([\w\-\.]+)\.(jpg|jpeg|png|gif|webp)$/i;
        const value = $(this).val();
        const message = "Invalid Image URL. It should be in the format: https://example.com/image.jpg";
        this.setCustomValidity(!value || imageUrlRegex.test(value) ? "" : message);
    });

    // Validation for Duration
    $('#courseDuration').on('input', function () {
        const value = $(this).val();
        const message = "Duration must be a number greater than or equal to 0";
        this.setCustomValidity(isFloat(value) && parseFloat(value) >= 0 ? "" : message);
    });


    // Validation for URL
    $('#newCourseUrl').on('input', function () {
        const urlRegex = /^https:\/\/(?:www\.)?[\w\-]+(\.[\w\-]+)*(\.[a-zA-Z]{2,})([\/\w\-\.]*)*(\?.*)?$/;
        const value = $(this).val();
        const message = "Invalid URL. It should be in the format: https://www.example.com";
        this.setCustomValidity(urlRegex.test(value) ? "" : message);
    });

    // Validation for Image URL
    $('#newCourseImage').on('input', function () {
        const imageUrlRegex = /^https:\/\/(?:localhost:\d{1,5}|(?:www\.)?[\w\-]+(\.[\w\-]+)*\.com)(?::\d{1,5})?(\/[\w\-\.]*)*\/([\w\-\.]+)\.(jpg|jpeg|png|gif|webp)$/i;
        const value = $(this).val();
        const message = "Invalid Image URL. It should be in the format: https://example.com/image.jpg";
        this.setCustomValidity(!value || imageUrlRegex.test(value) ? "" : message);
    });

    // Validation for Duration
    $('#newCourseDuration').on('input', function () {
        const value = $(this).val();
        const message = "Duration must be a number greater than or equal to 0";
        this.setCustomValidity(isFloat(value) && parseFloat(value) >= 0 ? "" : message);
    });

    $('#newCourseId').on('input', function () {
        const value = $(this).val();
        const message = "Duration must be a number greater than or equal to 0";
        this.setCustomValidity(parseInt(value) >= 0 ? "" : message);
    });

    $('#newCourseForm').submit(function (e) {
        e.preventDefault();

        var files = $("#newCourseImageFile").get(0).files;

        if (files.length > 0) {

            // Upload the image first
            uploadImageForInsert();
        } else {
            // Directly update the course if no new image is uploaded
            insertNewCourse();
        }
        //insertNewCourse();
    });

    $('#editCourseForm').submit(function (e) {
        e.preventDefault();

        var files = $("#courseImageFile").get(0).files;

        if (files.length > 0) {

            // Upload the image first
            uploadImageForEdit();
        } else {
            // Directly update the course if no new image is uploaded
            updateCourse();
        }
        //updateCourse();
    });

    
    getFromServer();
    insertInstructorsToDataList()
});
//-------------------------------------------------------------------------
//function loadCoursesToServer(data) {
//    console.log("Hi from loadCoursesToServer");

//    //let api = `https://localhost:${port}/api/Courses`;
//    let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Courses`;

//    data.forEach(course => {
//        const courseData = {
//            Id: course.id,
//            Title: course.title,
//            Url: "https://www.udemy.com" + course.url,
//            Rating: course.rating,
//            NumberOfReviews: course.num_reviews,
//            InstructorId: course.instructors_id,
//            ImageReference: course.image,
//            Duration: extractDuration(course.duration),
//            LastUpdate: course.last_update_date,
//        }
//        ajaxCall("POST", api, JSON.stringify(courseData), loadCoursesToServerSCB, loadCoursesToServerECB);
//    })
//    alert("Insrted courses to the data base successfully");
//    insertCoursesToDataList();
//}

//function loadCoursesToServerSCB(stats) {
//    console.log(stats);
//}

//function loadCoursesToServerECB(err) {
//    console.log(err);
//    alert("Failed to insert courses to the data base");
//}

// Gets the numbers from the start of the string (example: 22 total hours)
//function extractDuration(durationStr) {
//    const durationMatch = durationStr.match(/\d+(\.\d+)?/);
//    return parseFloat(durationMatch[0]);
//}

//----------------------------------------------------------------------------------

function insertCoursesToDataList(courses) {
    $('#coursesList').empty();
    $.each(courses, function (index, course) {
        const option = $('<option></option>')
            .val(course.id)
            .text(course.title);
        $('#coursesList').append(option);
    });
}

//------------------------------------------------------------------

function displayCourseDetails(id) {
    id = parseInt(id);
   // let api = `https://localhost:${port}/api/Courses/GetCourseById/${id}`;
    
    let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Courses/GetCourseById/${id}`;
    ajaxCall("GET", api, null, displayCourseDetailsSCB, displayCourseDetailsECB);
}

function displayCourseDetailsSCB(course) {
    console.log(course);
    $('#courseId').val(course.id);
    $('#courseTitle').val(course.title);
    $('#courseUrl').val(course.url);
    $('#courseRating').val(course.rating);
    $('#courseNumberOfReviews').val(course.numberOfReviews);
    $('#courseInstructorId').val(course.instructorId);
    $('#courseImage').val(course.imageReference);
    $('#courseDuration').val(course.duration);
    $('#courseLastUpdate').val(course.lastUpdate);
    $("#courseIsActive").prop('checked', course.isActive);
    initialImageUrl = course.imageReference;
}

function displayCourseDetailsECB(err) {
    alert("Please type the correct course name/Id");
    console.log(err);
}

//-----------------------------------------------------------------------------

// Function to check if a value is a float number
function isFloat(value) {
    return !isNaN(value) && parseFloat(value) == value;
}

//-----------------------------------------------------------------------------------------------------------

function renderCourses(courses) {
    var coursesList = $("#courses-list");
    coursesList.empty();
    $.each(courses, function (index, course) {
        var courseTitle = course.isActive
            ? `<a href="${course.url}">${course.title}</a>`
            : `<a style="color: red;" href="${course.url}">${course.title}</a>`;

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
                </section>
                <button class="showMoreCoursesOfThisInstructor" data-course-id="${course.id}">Show more courses of this instructor</button>

            </div>
        `;
        // Each button course have the id of the course that the button found on him

        coursesList.append(courseBox);

        getInstructorNameById(course.instructorId, function (instructorName) {
            $(`#instructor-${course.id}`).text(instructorName);
        });
    });
}

//----------------------------------------------------------------------------

//    <button id="loadCoursesBTN">Load courses to the server</button>
function getFromServer() {
    console.log("Hi from getFromServer");
   // let api = `https://localhost:${port}/api/Courses`;
    let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Courses`;
    ajaxCall("GET", api, "", getCoursesSCB, getCoursesECB);
}
function getCoursesSCB(coursesList) {
    coursesForDataTable = coursesList;
    console.log(coursesList);
    renderCourses(coursesList);
    insertCoursesToDataList(coursesList);
    if (updateDataTable == true) {
        redrawTable(tbl, coursesList)
    }
}

function getCoursesECB(err) {
    console.log(err);
}

//--------------------------------------------------------------------------------------

function insertInstructorsToDataList() {
   // let api = `https://localhost:${port}/api/Instructors`;
    let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Instructors`;
    ajaxCall("GET", api, "", insertInstructorsToDataListSCB, insertInstructorsToDataListECB);
}
// I assumed that the instructors inside the DataBase
function insertInstructorsToDataListSCB(instructors) {
    $('#instructorsList').empty();
    $.each(instructors, function (index, instructor) {
        const option = $('<option></option>')
            .val(instructor.id)
            .text(instructor.name);
        $('#instructorsList').append(option);
    });
}

function insertInstructorsToDataListECB(err) {
    console.error('Error happend while getting instructors:', error);
}

//---------------------------------------------------------------------------------------------------------------------------------------
function getCoursesToTable() {
    try {
        if (!$.fn.DataTable.isDataTable('#coursesTable')) {
            tbl = $('#coursesTable').DataTable({// change cartaple
                data: coursesForDataTable,
                pageLength: 9,
                columns: [
                    {
                        render: function (data, type, row, meta) {
                            let dataCourse = "data-courseId='" + row.Id + "'";

                            editBtn = "<button type='button' class = 'editBtn btn btn-success' " + dataCourse + "> Edit </button>";
                            return editBtn;
                        }
                    },
                    { data: "id" },
                    { data: "title" },
                    { data: "rating" },
                    { data: "numberOfReviews" },
                    { data: "instructorId" },
                    {
                        data: "imageReference",
                        render: function (data, type, row, meta) {
                            //return data.imageReference;
                            return '<img src="' + data + '" alt="Course Image" style="width: 50px; height: auto;">';
                        }
                    },
                    { data: "duration" },
                    { data: "lastUpdate" },
                    {
                        data: "isActive",
                        render: function (data, type, row, meta) {
                            if (data == true)
                                return '<input type="checkbox" checked disabled="disabled" />';
                            else
                                return '<input type="checkbox" disabled="disabled"/>';
                        }
                    }
                ],
            });
            buttonEvents();

        }        
    }

    catch (err) {
        alert(err);
    }
}
//---------------------------------------------------------------------------------------------------------------------
function buttonEvents() {
    $(document).on("click", ".editBtn", function () {
        markSelected(this);

        // Get row data when the edit button is clicked
        var table = $('#coursesTable').DataTable();
        var row = $(this).closest('tr');
        var rowData = table.row(row).data();

        //console.log(rowData);

        populateFields(rowData);

        $("#editCourseDivForDataTable").show();
        $("#editCourseDivForDataTable :input").prop("disabled", false); // edit mode: enable all controls in the form
    });
    $("#courseFormForDataTable").on("submit", function (event) {
        event.preventDefault();
        onSubmitFunc();
    });
}

//---------------------------------------------------------------------------------------------------------------------
// mark the selected row
function markSelected(btn) {
    $("#coursesTable tr").removeClass("selected"); // remove seleced class from rows that were selected before
    row = (btn.parentNode).parentNode; // button is in TD which is in Row
    row.className = 'selected'; // mark as selected
}

//---------------------------------------------------------------------------------------------------------------------

function populateFields(courseData) {
    specCourseForDataTable = {
        Id: courseData.id,
        Title: courseData.title,
        IsActive: courseData.isActive
    }

    $("#isActiveForDataTable").prop('checked', courseData.isActive);
    $("#titleForDataTable").val(courseData.title);
}

//---------------------------------------------------------------------------------------------------------------------
function onSubmitFunc() {
    specCourseForDataTable.IsActive = $("#isActiveForDataTable").is(":checked");
    specCourseForDataTable.Title = $("#titleForDataTable").val();

    //let api = `https://localhost:${port}/api/Courses/UpdateCourseIsActiveAndTitle/Course Id/${specCourseForDataTable.Id}/Is Active/${specCourseForDataTable.IsActive}/Title/${specCourseForDataTable.Title}`;
    let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Courses/UpdateCourseIsActiveAndTitle/Course Id/${specCourseForDataTable.Id}/Is Active/${specCourseForDataTable.IsActive}/Title/${specCourseForDataTable.Title}`
    ajaxCall("PUT", api, "", updateSuccess, error);
    specCourseForDataTable = null; 
}

function updateSuccess(status) {
    tbl.clear();
    console.log(status)
    //redrawTable(tbl, coursesdata);
    $("#editCourseDivForDataTable").hide();
    updateDataTable = true;
    getFromServer();
    Swal.fire({
        icon: 'success',
        title: 'Course Updated!',
        text: 'The course has been successfully updated.',
    });

    $('#editCourseForm').hide();
    $('#editCourseForm')[0].reset();
    $('#showEditFormBtn').show();
}


// this function is activated in case of a failure
function error(err) {
    Swal.fire({
        icon: 'error',
        title: 'Error',
        text: `Error: ${err}`,
    });
}


//---------------------------------------------------------------------------------------------------------------------

// redraw a datatable with new data
function redrawTable(tbl, data) {
    tbl.clear();
    for (var i = 0; i < data.length; i++) {
        tbl.row.add(data[i]);
    }
    tbl.draw();
    updateDataTable = false;
}


//---------------------------------------------------------------------------------------------------------------

$(document).ready(function () {
    $('#newCourseImageFile').on('change', function () {
        var files = $(this).prop('files');

        if (files && files.length > 0) {
            var isValidFile = validateImageFile(files[0]);

            if (isValidFile) {
                $('#newCourseImage').val('');
            } else {
                $('#newCourseImageFile').val('');
            }
        }
    });

});

//---------------------------------------------------------------------------------------------------------------

function uploadImageForInsert() {
    var data = new FormData();
    var files = $("#newCourseImageFile").get(0).files;

    if (files.length > 0) {
        for (var f = 0; f < files.length; f++) {
            data.append("files", files[f]);
        }
    }
    //let apiUploadImage = 'https://localhost:7123/api/Courses/uploadFile';
    //imageFolder = "https://localhost:7123/images/";
    let apiUploadImage = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Courses/uploadFile`;
    imageFolder = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/images/`

    $.ajax({
        type: "POST",
        url: apiUploadImage,
        contentType: false,
        processData: false,
        data: data,
        success: uploadImageForInsertSCB,
        error: uploadImageForInsertECB
    })
}

function uploadImageForInsertSCB(data) {
    console.log(data);

    if (Array.isArray(data)) {
        $('#newCourseImage').val(imageFolder + data[0]);
    }
    else if (typeof data === 'string') {
        $('#newCourseImage').val(imageFolder + data);
    }
    
    insertNewCourse();
}


function uploadImageForInsertECB(err) {
    alert('Error uploading file:', err);

    $('#newCourseImageFile').val('');
    $('#newCourseImage').val('');

    Swal.fire({
        icon: 'error',
        title: 'Upload Failed',
        text: "Failed to upload image, can't insert the course"
    });
}

//---------------------------------------------------------------------------------------------------------------

function insertNewCourse() {
    //let api = `https://localhost:${port}/api/Courses/InsertNewCourse`;
    let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Courses/InsertNewCourse`;
    let newCourse = {
        Id: parseInt($('#newCourseId').val()),
        Title: $('#newCourseTitle').val(),
        Url: $('#newCourseUrl').val(),
        Rating: parseFloat("0"),
        NumberOfReviews: parseInt("0"),
        InstructorId: parseInt($('#newCourseInstructorId').val()),
        ImageReference: $('#newCourseImage').val(),
        Duration: parseFloat($('#newCourseDuration').val()),
        LastUpdate: "",
        IsActive: $('#newCourseIsActive').prop('checked')
    };
    ajaxCall("POST", api, JSON.stringify(newCourse), insertNewCourseSCB, insertNewCourseECB);
}

function insertNewCourseSCB(stats) {
    $('#newCourseForm')[0].reset();

    Swal.fire({
        icon: 'success',
        title: 'Course Added!',
        text: 'Course got added successfully'
    })

    $('#newCourseForm').hide();
    $('#showInsertFormBtn').show();

    $('#newCourseImageFile').hide();
    $('#newCourseImage').show();
    $('#insertFormImageFileBtn').show();
    $('#insertFormImageLinkBtn').hide();

    updateDataTable = true;
    getFromServer();

    $('#editCourseDivForDataTable').hide();
    $('#editCourseDivForDataTable')[0].reset();
}

function insertNewCourseECB(err) {
    console.log(err);

    // If an image URL is present, delete the image
    if ($('#newCourseImage').val().trim() !== '') {
        deleteImage($('#newCourseImage').val());
    }

    $('#newCourseForm')[0].reset();

    Swal.fire({
        icon: 'error',
        title: 'Adding Course Failed',
        text: 'Failed to add course. Make sure the instructor exists and the course ID is unique.'
    });
}

//---------------------------------------------------------------------------------------------------------------


function validateImageFile(file) {
    var validExtensions = [".jpg", ".jpeg", ".png", ".gif", ".webp"];
    var validMimeTypes = ["image/jpeg", "image/png", "image/gif", "image/webp"];

    var extension = file.name.split('.').pop().toLowerCase();
    var mimeType = file.type;

    if (validExtensions.includes("." + extension) && validMimeTypes.includes(mimeType)) {
        return true;
    } else {
        return false;
    }
}


//---------------------------------------------------------------------------------------------------------------

function deleteImage(imageUrl) {
    var fileName = imageUrl.split('/').pop(); // Extract the file name from the URL
    //let api = `https://localhost:${port}/api/Courses/DeleteImage/File Name/${fileName}`;
    let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Courses/DeleteImage/`;
    api += `File Name/${fileName}`;

    ajaxCall("DELETE", api, null, deleteImageSCB, deleteImageECB);
}
function deleteImageSCB(response) {
    console.log('Image deleted successfully:', response);
}

function deleteImageECB(err) {
    console.error('Error deleting image:', err);
}

//---------------------------------------------------------------------------------------------------------------

var initialImageUrl = '';
$(document).ready(function () {
    $('#courseImageFile').on('change', function () {
        var files = $(this).prop('files');

        if (files && files.length > 0) {
            var isValidFile = validateImageFile(files[0]);

            if (isValidFile) {
                $('#courseImage').val('');
            } else {
                $('#courseImageFile').val('');
            }
        }
    });
});

//---------------------------------------------------------------------------------------------------------------
function uploadImageForEdit() {
    var data = new FormData();
    var files = $("#courseImageFile").get(0).files;

    if (files.length > 0) {
        for (var f = 0; f < files.length; f++) {
            data.append("files", files[f]);
        }
    }
    //let apiUploadImage = 'https://localhost:7123/api/Courses/uploadFile';
   // imageFolder = "https://localhost:7123/images/";
    let apiUploadImage = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Courses/uploadFile`;
    imageFolder = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/images/`
    $.ajax({
        type: "POST",
        url: apiUploadImage,
        contentType: false,
        processData: false,
        data: data,
        success: uploadImageForEditSCB,
        error: uploadImageForEditECB
    })
}

function uploadImageForEditSCB(data) {
    console.log(data);

    if (Array.isArray(data)) {
        $('#courseImage').val(imageFolder + data[0]);
    }
    else if (typeof data === 'string') {
        $('#courseImage').val(imageFolder + data);
    }

    updateCourse();
}


function uploadImageForEditECB(err) {
    alert('Error uploading file:', err);

    $('#courseImageFile').val('');
    $('#courseImage').val('');

    Swal.fire({
        icon: 'error',
        title: 'Upload Failed',
        text: "Failed to upload image, can't insert the course"
    });
}

//---------------------------------------------------------------------------------------------------------------


function updateCourse() {
    let courseId = parseInt($('#courseId').val());
    //let api = `https://localhost:${port}/api/Courses/${courseId}`;
    let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Courses/${courseId}`;

    let updatedCourse = {
        Id: courseId,
        Title: $('#courseTitle').val(),
        Url: $('#courseUrl').val(),
        Rating: parseFloat($('#courseRating').val()),
        NumberOfReviews: parseInt($('#courseNumberOfReviews').val()),
        InstructorId: parseInt($('#courseInstructorId').val()),
        ImageReference: $('#courseImage').val(),
        Duration: parseFloat($('#courseDuration').val()),
        LastUpdate: formatDate(new Date()),
        IsActive: $('#courseIsActive').prop('checked')
    };

    ajaxCall("PUT", api, JSON.stringify(updatedCourse), updateCourseSCB, updateCourseECB);
}

function updateCourseSCB(stats) {
    console.log("Course updated successfully:", stats);

    //check and delete the initial image if the URL was changed:
    if ($('#courseImage').val().trim() !== initialImageUrl && initialImageUrl.startsWith('https://localhost:7123/images/')) {
        deleteImage(initialImageUrl);
    }

    $('#editCourseForm')[0].reset();

    $('#editCourseForm').hide();
    $('#showEditFormBtn').show();

    Swal.fire({
        icon: 'success',
        title: 'Course Updated!',
        text: 'The course has been successfully updated.',
    });

    $('#courseImageFile').hide();
    $('#courseImage').show();
    $('#editFormImageFileBtn').show();
    $('#editFormImageLinkBtn').hide();

    updateDataTable = true;
    getFromServer();
    $('#editCourseDivForDataTable').hide();
    $('#courseFormForDataTable')[0].reset();
}

function updateCourseECB(error) {
    console.error('Error updating course:', error);

    if ($('#courseImage').val().trim() !== '') {
        deleteImage($('#courseImage').val());
    }

    $('#editCourseForm')[0].reset();

    Swal.fire({
        icon: 'error',
        title: 'Update Failed',
        text: 'Failed to update course. Please try again later.',
    });
}

function formatDate(date) {
    let d = new Date(date);
    let day = ('0' + d.getDate()).slice(-2);
    let month = ('0' + (d.getMonth() + 1)).slice(-2);
    let year = d.getFullYear();
    return `${day}/${month}/${year}`;
}