let users = [];
let loginData = [];
$(document).ready(function () {
  getFromServer();
});

function getFromServer() {
  const api = "https://proj.ruppin.ac.il/igroup15/test2/tar1/api/Users";
  //const api = "https://localhost:7110/api/Users";
  ajaxCall("GET", api, "", getUsersSCB, getUsersECB);
}

// Update users with fetched data
function getUsersSCB(data) {
  users = data;
}

// Error alert if the data not loaded
function getUsersECB(err) {
  console.error("Error fetching users:", err);
  alert("An error occurred while fetching users. Please try again later.");
}

$("#usersForm").on("submit", function (e) {
  e.preventDefault(); // Prevent the default form submission (page refresh)
  const email = $("#email").val();
  const password = $("#password").val();

  // Perform validations
  if (!validateEmail(email)) {
    alert("Please enter a valid email address.");
    return; // Stop the submission if validation fails
  }
  login(email, password);
});

function login(email, password) {
  loginData = {
    email: email,
    password: password,
    id: 1,
    name: "a",
  };
  const api = "https://proj.ruppin.ac.il/igroup15/test2/tar1/api/Users/Login";
  // const api = "https://localhost:7110/api/Users/Login";
  ajaxCall("POST", api, JSON.stringify(loginData), loginSCB, loginECB);
}

function loginSCB(data) {
  let isActive = data;
  if (isActive == 2) {
    alert(
      "you was blocked from the site for more informations you have to email the support team."
    );
  } else if (isActive == 1) {
    alert("Logged in successfully!");
    users.forEach((user) => {
      if (loginData.email == user.email) LoginUser = user;
    });
    sessionStorage.setItem("userId", JSON.stringify(LoginUser.id));
    sessionStorage.setItem("userName", JSON.stringify(LoginUser.name));
    sessionStorage.setItem("userEmail", JSON.stringify(LoginUser.email));
    window.location.href = "index.html";
  } else {
    alert("user not fount, You must register before try to login!");
  }
}
// Error alert if login fails
function loginECB(err) {
  alert("Invalid email or password. Please try again.");
}

$("#registerLink").on("click", function (e) {
  e.preventDefault(); // Prevent the default link behavior
  $("#my-container").hide(); // Hide the login form
  showRegisterForm(); // Show the registration form
});

function showRegisterForm() {
  registerFormHtml = `
        <form id="registerForm">
            <div class="input-group">
                <label class="label" for="registername">NAME</label>
                <input type="text" id="registername" class="input" required>
            </div>
            <div class="input-group">
                <label class="label" for="registerEmail">EMAIL</label>
                <input type="email" id="registerEmail" class="input" autocomplete="current-password" required>
            </div>
            <div class="input-group">
                <label class="label" for="registerPassword">PASSWORD</label>
                <input type="password" id="registerPassword" class="input" autocomplete="current-password" required>
            </div>
            <button id="registerButton" type="submit" class="submit-button">Register</button>
            <button id="backToLogin" type="button" class="submit-button" onclick="BacktoLogin()">Back</button>
        </form>
    `;
  $("#my-container2").html(registerFormHtml).show();

  // Handle registration form submission
  $("#registerForm").on("submit", function (e) {
    e.preventDefault(); // Prevent the default form submission (page refresh)
    const id = 1;
    const name = $("#registername").val();
    const email = $("#registerEmail").val();
    const password = $("#registerPassword").val();

    // Perform validations
    if (!validateName(name)) {
      alert(
        "Please enter a valid name (at least 2 characters long and no numbers)."
      );
      return; // Stop the submission if validation fails
    }

    if (!validateEmail(email)) {
      alert("Please enter a valid email address.");
      return; // Stop the submission if validation fails
    }

    if (!validatePassword(password)) {
      alert(
        "Password must be at least 8 characters long including just upper case letters and numbers ."
      );
      return; // Stop the submission if validation fails
    }

    const Newuser = {
      id: id,
      name: name,
      email: email,
      password: password,
    };

    if (!validateID(Newuser)) {
      return; // Stop the submission if validation fails
    }
  });
}
function showEditForm() {
  registerFormHtml = `
  <h1 id="title">Profile Edit</h1>
        <form id="EditForm">
            <div class="input-group">
                <label class="label" for="editName">NAME</label>
                <input type="text" id="editName" class="input" required value=${sessionStorage.getItem(
                  "userName"
                )}>
            </div>
            <div class="input-group">
                <label class="label" for="editEmail">EMAIL</label>
                <input type="email" id="editEmail" class="input" autocomplete="current-password" value=${sessionStorage.getItem(
                  "userEmail"
                )} required>
            </div>
            <div class="input-group">
                <label class="label" for="editPassword">PASSWORD</label>
                <input type="password" id="editPassword" class="input" autocomplete="current-password" required>
            </div>
            <button id="Updateuser" type="submit" class="submit-button">Confirm</button>
            <button id="backToLogin" type="button" class="submit-button" onclick="BacktoIndex()">Back</button>
        </form>
    `;
  $("#my-container2").html(registerFormHtml).show();

  // Handle registration form submission
  $("#EditForm").on("submit", function (e) {
    e.preventDefault(); // Prevent the default form submission (page refresh)
    const editId = sessionStorage.getItem("userId");
    const editName = $("#editName").val();
    const editEmail = $("#editEmail").val();
    const editPassword = $("#editPassword").val();

    // Perform validations
    if (!validateName(editName)) {
      alert(
        "Please enter a valid name (at least 2 characters long and no numbers)."
      );
      return; // Stop the submission if validation fails
    }

    if (!validateEmail(editEmail)) {
      alert("Please enter a valid email address.");
      return; // Stop the submission if validation fails
    }

    if (!validatePassword(editPassword)) {
      alert(
        "Password must be at least 8 characters long including just upper case letters and numbers ."
      );
      return; // Stop the submission if validation fails
    }

    const Newwuser = {
      id: editId,
      name: editName,
      email: editEmail,
      password: editPassword,
    };

    if (!validateID(Newwuser)) {
      return; // Stop the submission if validation fails
    }
  });
}

function BacktoLogin() {
  $("#my-container2").hide();
  $("#my-container").show();
}

function validateEmail(email) {
  // Simple email validation regex
  const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return re.test(email);
}

function validatePassword(password) {
  // Password validation (at least 8 characters)
  const re = /^[A-Z1-9\s]{8,}$/; // Regex to allow only letters and spaces
  return re.test(password) && password.length >= 8;
}

function validateName(name) {
  // Name validation (at least 2 characters long and no numbers)
  const re = /^[A-Za-z\s]{2,}$/; // Regex to allow only letters and spaces
  return re.test(name);
}

function validateID(Newuser) {
  if (sessionStorage.getItem("userId") == null)
    return postUserToServer(Newuser);
  else return putUserToServer(Newuser);
}

function postUserToServer(Newuser) {
  const api =
    "https://proj.ruppin.ac.il/igroup15/test2/tar1/api/Users/Register";
  //const api = "https://localhost:7110/api/Users/Register";
  ajaxCall("POST", api, JSON.stringify(Newuser), postUserSCB, postUserECB);
}

function postUserSCB(status) {
  alert("User  registered successfully!");
  const email = $("#registerEmail").val();
  const password = $("#registerPassword").val();
  window.location.href = "index.html"; // continue to gamess
  sessionStorage.setItem("userName", JSON.stringify($("#registername").val()));
  sessionStorage.setItem(
    "userEmail",
    JSON.stringify($("#registerEmail").val())
  );
  id = users.length + 2;
  sessionStorage.setItem("userId", JSON.stringify(id));
}

function postUserECB(err) {
  if (err.status === 409) {
    // Conflict, e.g., user ID already exists
    alert("User  ID already exists. Please use a different ID.");
  } else {
    alert("An error occurred while registering the user. Please try again.");
  }
}
function putUserToServer(Newuser) {
  const api = `https://proj.ruppin.ac.il/igroup15/test2/tar1/api/Users/${Newuser.id}`;
  //const api = `https://localhost:7110/api/Users/${Newuser.id}`;
  ajaxCall("PUT", api, JSON.stringify(Newuser), putUserSCB, putUserECB);
}

function putUserSCB(status) {
  alert("User  Profile Updated successfully!");
  const email = $("#editEmail").val();
  const password = $("#editPassword").val();
  window.location.href = "index.html"; // continue to gamess
  sessionStorage.setItem("userName", JSON.stringify($("#editName").val()));
  sessionStorage.setItem("userEmail", JSON.stringify($("#editEmail").val()));
  location.reload(); // continue to games
}

function putUserECB(err) {
  alert("An error occurred while updating profile. Please try again.");
}

function BacktoIndex() {
  window.location.href = "index.html";
}
