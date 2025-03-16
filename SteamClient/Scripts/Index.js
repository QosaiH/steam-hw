let GAMES = [];
$(document).ready(function () {
  getGamesFromServer();
});
//Get the games from the DB
function getGamesFromServer() {
  //const api = "https://localhost:7110/api/Games";
  const api = "https://proj.ruppin.ac.il/igroup15/test2/tar1/api/Games";
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
  const $container = $("#games-container");
  $container.empty();

  // games.forEach to render each game
  GAMES.forEach((game) => {
    const $gameDiv = $(`
            <div class="game">
                <h6>${game.name}</h6>
                <img src="${game.headerImage}" alt="${game.name}"/>
                <h6>Price: $${game.price}</h6>
                <button class="list" data-appid="${game.appID}">ADD TO MY LIST</button>
            </div>
        `);

    // Append the gameDiv to the container
    $container.append($gameDiv);
  });

  // Attach click event to buttons after rendering
  $(".list").on("click", function () {
    if (userId == null) alert("you must be logged in to buy games!");
    else {
      const appId = $(this).data("appid"); // Get the AppID from the button's data attribute
      postToServer(appId);
    }
  });
}

// Add the game to the server
function postToServer(id) {
  gameUser = {
    AppID: id,
    userID: userId,
  };
  const api = "https://proj.ruppin.ac.il/igroup15/test2/tar1/api/GamesUser";
  //const api = "https://localhost:7110/api/GamesUser";
  ajaxCall("POST", api, JSON.stringify(gameUser), postGameSCB, postGameECB);
}

// If added
function postGameSCB(status) {
  if (status == true) alert("game Adde succefully!");
  else alert("Error!! This Game Is Already In youre Library!");
}

// If not added
function postGameECB(err) {
  console.log(err);
}

$(document).on("click", "#EditProfile", function () {
  $("#games-container").hide();
  $("#title").hide();
  showEditForm();
});

$(document).on("click", "#LogOut", function () {
  sessionStorage.clear();
  alert("you have been logged out!");
  location.reload();
});
