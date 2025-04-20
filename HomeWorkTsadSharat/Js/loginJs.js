$(document).ready(function () {
    $("#email").on("input", function () {
        var pattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        var value = $(this).val();
        var message = "Please match the requested format:  example123@example.com";
        this.setCustomValidity(value.match(pattern) ? "" : message);
    });

    $("#loginForm").submit(function (e) {
        e.preventDefault();
        loginUser($('#loginForm #email').val(), $('#loginForm #password').val());
    });
});
