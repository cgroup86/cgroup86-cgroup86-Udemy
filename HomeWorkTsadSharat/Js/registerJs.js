$(document).ready(function () {
    $("#email").on("input", function () {
        var pattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        var value = $(this).val();
        var message = "Please match the requested format:  example123@example.com";
        this.setCustomValidity(value.match(pattern) ? "" : message);
    });

    $("#name").on("input", function () {
        var pattern = /^[a-zA-Z\s]*$/;
        var value = $(this).val();
        var message = "Please enter only letters and spaces.";
        this.setCustomValidity(value.match(pattern) ? "" : message);
    });

    $("#registerForm").submit(function (e) {
        e.preventDefault();

        //let api = `https://localhost:${port}/api/Users/Register`;
        let api = 'https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Users/Register';

        const registerData = {
            Id: 0,
            Name: $('#registerForm #name').val(),
            Email: $('#registerForm #email').val(),
            Password: $('#registerForm #password').val(),
            IsAdmin: false,
            IsActive: true,
        };

        ajaxCall("POST", api, JSON.stringify(registerData), registerSuccess, registerError);
    });
});

function registerSuccess(response) {
    Swal.fire({
        icon: 'success',
        title: 'Success!',
        text: 'Registration successful'
    }).then(() => {
        // Automatically log in the user after successful registration
        loginUser($('#registerForm #email').val(), $('#registerForm #password').val());
    });
}

function registerError(err) {
    Swal.fire({
        icon: 'error',
        title: 'Registration Failed',
        text: err.responseJSON.error
    });
}
