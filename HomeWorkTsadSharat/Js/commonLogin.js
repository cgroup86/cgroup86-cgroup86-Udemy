// Made this page so we don't use the login page twice
// When user register it logs him on automaticlly

//let port = "7123";

function loginUser(email, password) {
    //let api = `https://localhost:${port}/api/Users/Login`;
    let api = `https://proj.ruppin.ac.il/cgroup86/test2/tar1/api/Users/Login`;

    const loginData = {
        Id: 0,
        Name: "0",
        Email: email,
        Password: password,
        IsAdmin: false,
        IsActive: true,
    };

    ajaxCall("POST", api, JSON.stringify(loginData), loginSuccess, loginError);
}

function loginSuccess(response) {
    //const userId = response.id.toString(); 
    let admin = false;
    if (response.id == 0) {
        admin = true;
    }
    const userData = {
        userId: response.id,
        isAdmin: admin,
        isLoggedIn: true,
    };

    sessionStorage.setItem('userData', JSON.stringify(userData));

    Swal.fire({
        icon: 'success',
        title: 'Success!',
        text: 'Login successful'
    }).then(() => {
        // Redirect to home page after successful login
        window.location.href = "index.html";
    });
}

function loginError(err) {
    Swal.fire({
        icon: 'error',
        title: 'Login Failed',
        text: err.responseJSON.error
    });
}
