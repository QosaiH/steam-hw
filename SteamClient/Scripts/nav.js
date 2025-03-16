// Call the function to create the navbar
const userId = sessionStorage.getItem("userId");
if (userId == 16) createAdminNavbar();
else if (userId != null) createUserNavbar();
else createGuestNavbar();

function createUserNavbar() {
  const navbarHTML = `
          <nav id="userNav" class="navbar sticky-top navbar-expand-lg navbar-dark bg-dark"> 
              <a class="navbar-brand" href="#"><img src="https://cdn.icon-icons.com/icons2/3053/PNG/512/steam_macos_bigsur_icon_189699.png" width="30" height="30" class="d-inline-block align-top" alt="">  
  Steam</a>
              <a class="navbar-brand">Hello ${sessionStorage.getItem(
                "userName"
              )}!</a>
              <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                  <span class="navbar-toggler-icon"></span>
              </button>
              <div class="collapse navbar-collapse" id="navbarNav">
                  <ul class="navbar-nav ml-auto">
                      <li class="nav-item">
                          <a class="nav-link" href="index.html"><button type="button" class="btn btn-success">Home</button></a>
                      </li>
                      <li class="nav-item">
                          <a class="nav-link" href="mygames.html"><button type="button" class="btn btn-success">My Games</button></a>
                      </li>
                      <li class="nav-item">
                          <a class="nav-link" href="Bonus.html"><button type="button" class="btn btn-success">Bonus</button></a>
                      </li>                                         
                      <li class="nav-item">
                          <a class="nav-link"><button class="btn btn-warning" id="EditProfile">Edit Profile</button></a>
                      </li>
                      <li class="nav-item">
                          <a class="nav-link"><button class="btn btn-danger" id="LogOut" >Log Out</button></a>
                      </li>
                  </ul>
              </div>
          </nav>
      `;
  // Append the navbar to the body or a specific element
  document.body.insertAdjacentHTML("afterbegin", navbarHTML);
}

function createAdminNavbar() {
  const navbarHTML = `
          <nav id="adminNav" class="navbar sticky-top navbar-expand-lg navbar-dark bg-dark"> 
              <a class="navbar-brand" href="#"><img src="https://cdn.icon-icons.com/icons2/3053/PNG/512/steam_macos_bigsur_icon_189699.png" width="30" height="30" class="d-inline-block align-top" alt="">  
  Steam</a>
              <a class="navbar-brand">Hello ${sessionStorage.getItem(
                "userName"
              )}!</a>
              <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                  <span class="navbar-toggler-icon"></span>
              </button>
              <div class="collapse navbar-collapse" id="navbarNav">
                  <ul class="navbar-nav ml-auto">
                      <li class="nav-item">
                          <a class="nav-link" href="index.html"><button type="button" class="btn btn-success">Home</button></a>
                      </li>
                      <li class="nav-item">
                          <a class="nav-link" href="mygames.html"><button type="button" class="btn btn-success">My Games</button></a>
                      </li>    
                      <li class="nav-item">
                          <a class="nav-link" href="Bonus.html"><button type="button" class="btn btn-success">Bonus</button></a>
                      </li>                                     
                      <li class="nav-item">
                          <a class="nav-link" href="adminPage.html"><button class="btn btn-warning" id="AdminPage">Admin Page</button></a>
                      </li>
                      <li class="nav-item">
                          <a class="nav-link"><button class="btn btn-danger" id="LogOut" >Log Out</button></a>
                      </li>
                  </ul>
              </div>
          </nav>
      `;
  // Append the navbar to the body or a specific element
  document.body.insertAdjacentHTML("afterbegin", navbarHTML);
}

function createGuestNavbar() {
  const navbarHTML = `
    <nav id="guestNav" class="navbar sticky-top navbar-expand-lg navbar-dark bg-dark">
    <a class="navbar-brand" href="#">
  <img src="https://cdn.icon-icons.com/icons2/3053/PNG/512/steam_macos_bigsur_icon_189699.png" width="30" height="30" class="d-inline-block align-top" alt="">
  </a>         
              <a class="navbar-brand" href="#">Steam</a>
              <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                  <span class="navbar-toggler-icon"></span>
              </button>
              <div class="collapse navbar-collapse" id="navbarNav">
                  <ul class="navbar-nav ml-auto">
                      <li class="nav-item">
                          <a class="nav-link" href="index.html"><button type="button" class="btn btn-success">Home</button></a>
                      </li>
                      <li class="nav-item">
                          <a class="nav-link" href="mygames.html"><button type="button" class="btn btn-success">My Games</button></a>
                      </li>  
                      <li class="nav-item">
                          <a class="nav-link" href="Bonus.html"><button type="button" class="btn btn-success">Bonus</button></a>
                      </li>  
                      <li class="nav-item">
                          <a class="nav-link active" href="login.html"><button id="Login" class="btn btn-primary">Log In</button></a>
                      </li>
                  </ul>
              </div>
          </nav>
      `;
  // Append the navbar to the body or a specific element
  document.body.insertAdjacentHTML("afterbegin", navbarHTML);
}
