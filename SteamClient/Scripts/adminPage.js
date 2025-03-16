const toggleSwitch = $("#toggleSwitch");
let GAMES = [];
let USERS = [];
let UsersCounter = 1;
let Gamescounter = 1;
const $next = $("#Next");
const $Previous = $("#Previous");
const $entries = $("#entries");
const toggleState = sessionStorage.getItem("toggleState");

//doc is ready
$(document).ready(function () {
  if (toggleState === "true") {
    toggleSwitch.prop("checked", true);
    $(".users-container").hide();
    $(".games-container").show();
    getGamesFromServer();
  } else {
    toggleSwitch.prop("checked", false);
    $(".games-container").hide();
    $(".users-container").show();
    getUsersFromServer();
  }
});

//check the toggle
toggleSwitch.on("change", () => {
  if (toggleSwitch.is(":checked")) {
    $(".users-container").hide();
    $(".games-container").show();
    sessionStorage.setItem("toggleState", "true"); // Save state to session storage
    Gamescounter = 1;
    getGamesFromServer();
  } else {
    $(".games-container").hide();
    $(".users-container").show();
    sessionStorage.setItem("toggleState", "false"); // Save state to session storage
    UsersCounter = 1;
    getUsersFromServer();
  }
});

//load the games list
function getGamesFromServer() {
  //const api = "https://localhost:7110/api/Games/gamesInfo";
  const api =
    "https://proj.ruppin.ac.il/igroup15/test2/tar1/api/Games/gamesInfo";
  ajaxCall("GET", api, "", getGamesSCB, getGamesECB);
}
function getGamesSCB(data) {
  GAMES = data;
  renderGames(GAMES);
}

// Error alert if the data not loaded
function getGamesECB(err) {
  console.error("Error fetching games:", err);
}
// Render the games
function renderGames(GAMES) {
  const $container = $("#games");
  $container.empty();

  const entriesPerPage = parseInt($entries.val()); // Get the selected number of entries per page
  const startIndex = (Gamescounter - 1) * entriesPerPage;
  const endIndex = startIndex + entriesPerPage;

  const paginatedGames = GAMES.slice(startIndex, endIndex);

  paginatedGames.forEach((game) => {
    const $gameDiv = $(`
      <tr>
          <td>${game.id}</td>
          <td>${game.title}</td>
          <td>${game.downloads}</td>
          <td>${game.totalRevenue}</td>
      </tr>
    `);

    $container.append($gameDiv);
  });

  // Update pagination info
  $("#page-info").text(
    `Showing ${Math.min(startIndex + 1, GAMES.length)} to ${Math.min(
      endIndex,
      GAMES.length
    )} of ${GAMES.length} entries`
  );
  $("#counter").text(`${Gamescounter}`);
}

function getUsersFromServer() {
  //const api = `https://localhost:7110/api/Users/usersInfo`;
  const api =
    "https://proj.ruppin.ac.il/igroup15/test2/tar1/api/Users/usersInfo";
  ajaxCall("GET", api, "", getUsersSCB, getUsersECB);
}
function getUsersSCB(data) {
  USERS = data;
  renderUsers(USERS);
}

// Error alert if the data not loaded
function getUsersECB(err) {
  console.error("Error fetching users:", err);
}

// Render the users list
function renderUsers(USERS) {
  const $containerUser = $("#users");
  $containerUser.empty();
  let $userDiv = ``;
  const entriesPerPage = parseInt($entries.val()); // Get the selected number of entries per page
  const startIndex = (UsersCounter - 1) * entriesPerPage;
  const endIndex = startIndex + entriesPerPage;
  const paginatedUsers = USERS.slice(startIndex, endIndex);
  // games.forEach to render each game
  paginatedUsers.forEach((user) => {
    if (user.isActive == 1) {
      $userDiv = $(`
                          <tr>
                              <td>${user.id}</td>
                              <td>${user.name}</td>
                              <td>${user.boughtGames}</td>
                              <td>${user.totalSpent}</td>
                              <td><input type="checkbox" class="Active" data-userId="${user.id}" checked></td>
                          </tr>
            `);
    } else {
      $userDiv = $(`
            <tr>
                <td>${user.id}</td>
                <td>${user.name}</td>
                <td>${user.boughtGames}</td>
                <td>${user.totalSpent}</td>
                <td><input type="checkbox" class="notActive" data-userId="${user.id}"></td>
            </tr>
`);
    }
    // Append the Users to the container
    $containerUser.append($userDiv);
  });

  $("#page-info").text(
    `Showing ${Math.min(startIndex + 1, USERS.length)} to ${Math.min(
      endIndex,
      USERS.length
    )} of ${USERS.length} entries`
  );
  $("#counter").text(`${UsersCounter}`);
}

// Attach click event to checkBoxs after rendering

$(document).on("click", ".Active", function () {
  let userID = $(this).attr("data-userId"); // Get the userID from the checkBOX data attribute
  putToServer(userID, false);
});

$(document).on("click", ".notActive", function () {
  let userID = $(this).attr("data-userId"); // Get the userID from the checkBOX data attribute
  putToServer(userID, true);
});

function putToServer(id, state) {
  info = {
    userID: parseInt(id),
    isActive: state,
  };
  const api = `https://proj.ruppin.ac.il/igroup15/test2/tar1/api/Users/${info.userID}/${info.isActive}`;
  //const api = `https://localhost:7110/api/Users/${info.userID}/${info.isActive}`;
  ajaxCall("PUT", api, JSON.stringify(info), putToServerSCB, putToServerECB);
}

// If added
function putToServerSCB(status) {
  console.log("Server response:", status); // Log the server response
  alert("status changed succefully!");
}

// If not added
function putToServerECB(err) {
  console.log(err);
}

$(document).on("click", "#LogOut", function () {
  sessionStorage.clear();
  alert("You have been logged out!");
  window.location.href = "index.html";
});

$next.on("click", function () {
  if (toggleSwitch.is(":checked")) {
    const entriesPerPage = parseInt($("#entries").val());
    if (Gamescounter * entriesPerPage < GAMES.length) {
      Gamescounter++;
      renderGames(GAMES);
    }
  } else {
    const entriesPerPage = parseInt($("#entries").val());
    if (UsersCounter * entriesPerPage < USERS.length) {
      UsersCounter++;
      renderUsers(USERS);
    }
  }
});

$Previous.on("click", function () {
  if (toggleSwitch.is(":checked")) {
    if (Gamescounter > 1) {
      Gamescounter--;
      renderGames(GAMES);
    }
  } else {
    if (UsersCounter > 1) {
      UsersCounter--;
      renderUsers(USERS);
    }
  }
});

$entries.on("change", function () {
  if (toggleSwitch.is(":checked")) {
    Gamescounter = 1; // Reset to the first page
    renderGames(GAMES);
  } else {
    UsersCounter = 1; // Reset to the first page
    renderUsers(USERS);
  }
});

$("#dataTables_filter").on("input", function () {
  if (toggleSwitch.is(":checked")) {
    const searchValue = $(this).val().toLowerCase();
    const filteredGames = GAMES.filter((game) =>
      game.title.toLowerCase().includes(searchValue)
    );
    renderGames(filteredGames);
  } else {
    const searchValue = $(this).val().toLowerCase();
    const filteredUsers = USERS.filter((user) =>
      user.name.toLowerCase().includes(searchValue)
    );
    renderUsers(filteredUsers);
  }
});
