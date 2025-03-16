$("#loadingIndicator").hide();
let favorite = []; // Array to store favorite games (assuming API returns an array)
let priced = [];
let ranked = [];
const $container = $("#my-games-container");
let debounceTimeout; // Timeout variable for debouncing
//
$(document).ready(function () {
  if (userId == null) alert("you have to login to see buyed games!");
  else getGamesFromServer();
});
// Load the game list from the server

function getGamesFromServer() {
  const api = `https://proj.ruppin.ac.il/igroup15/test2/tar1/api/GamesUser/${userId}`;
  //const api = `https://localhost:7110/api/GamesUser/${userId}`;
  ajaxCall("GET", api, "", getGamesFromServerSCB, getGamesFromServerECB);
}

// Update favorite games with fetched data
function getGamesFromServerSCB(data) {
  favorite = data;
  renderMyGames(favorite);
}

// Error alert if the data not loaded
function getGamesFromServerECB(err) {
  console.error("Error fetching games:", err);
}

// Render the favorite games
function renderMyGames(favorite) {
  $container.empty();
  // games.forEach to render each game
  favorite.forEach((game) => {
    const api = `https://proj.ruppin.ac.il/igroup15/test2/tar1/api/Games/${game.appId}`;
    //const api = `https://localhost:7110/api/Games/${game.appId}`;
    ajaxCall("GET", api, "", getGamesSCB, getGamesECB);
  });
}
// Update favorite games with fetched data
function getGamesSCB(data) {
  game = data;
  const $gameDiv = $(`
      <div class="game">
          <h6>${game.name}</h6>
          <img src="${game.headerImage}" alt="${game.name}"> 
          <h6>Price: $${game.price}</h6>
          <h6>Rank: ${game.scoreRank}</h6>
          <button onclick="deleteGame(${game.appID})">DELETE</button>
          <div>
          <textarea type="text" id="userInput" placeholder="add comment"></textarea>
          <button onclick="analyzeSentiment()">Comment</button>
          </div>
      </div>
  `);
  $container.append($gameDiv); // Append the gameDiv to the container
}
// Error alert if the data not loaded
function getGamesECB(err) {
  console.error("Error fetching games:", err);
}

// Use "DELETE" for delete request
function deleteGame(appID) {
  gameUser = {
    AppID: appID,
    userID: userId,
  };
  const api = `https://proj.ruppin.ac.il/igroup15/test2/tar1/api/GamesUser/delete`;
  //const api = `https://localhost:7110/api/GamesUser/delete`;
  ajaxCall(
    "DELETE",
    api,
    JSON.stringify(gameUser),
    deleteGameSCB,
    deleteGameECB
  );
}

// If deleted
function deleteGameSCB(status) {
  getFromServer(); // Refresh the game list after successful deletion
  alert("Game Deleted Succefully!");
  getGamesFromServer();
}

// Error alert if not deleted
function deleteGameECB(err) {
  console.error(err);
}

// Price input event listener
$("#price").on("input", function () {
  clearTimeout(debounceTimeout); // Clear any previous timeout
  debounceTimeout = setTimeout(() => {
    $("#Rank").val(""); // Clear the rank input
    const inputValue = parseFloat($(this).val());
    if (!isNaN(inputValue)) {
      SearchByPrice(inputValue);
    } else {
      renderMyGames(favorite); // Reset to favorite games if the input is invalid
    }
  }, 300); // 300ms debounce delay
});

function SearchByPrice(price) {
  data = {
    Price: price,
    id: userId,
  };
  const api = `https://proj.ruppin.ac.il/igroup15/test2/tar1/api/Games/SearchByPrice`;
  //const api = `https://localhost:7110/api/Games/SearchByPrice?price=${price}&id=${userId}`;
  ajaxCall("GET", api, data, SearchByPriceSuccess, SearchByPriceECB);
}

// If search is successful
function SearchByPriceSuccess(data) {
  priced = data; // Update favorite with the search results
  renderFilterdGames(priced); // Render the filtered games
}

// Error alert if search fails
function SearchByPriceECB(err) {
  console.error(err);
}

// Rank input event listener
$("#Rank").on("input", function () {
  clearTimeout(debounceTimeout); // Clear any previous timeout
  debounceTimeout = setTimeout(() => {
    $("#price").val(""); // Clear the price input
    const inputValue = parseInt($(this).val(), 10);
    if (!isNaN(inputValue)) {
      SearchByRank(inputValue);
    } else {
      renderMyGames(favorite); // Reset to favorite games if the input is invalid
    }
  }, 300); // 300ms debounce delay
});

function SearchByRank(rank) {
  data = {
    Rank: rank,
    id: userId,
  };
  const api = `https://proj.ruppin.ac.il/igroup15/test2/tar1/api/Games/searchByRank/rank/${rank}`;
  //const api = `https://localhost:7110/api/Games/searchByRank/rank/${rank}?id=${userId}`;
  ajaxCall("GET", api, data, SearchByRankSuccess, SearchByRankECB);
}

// If search is successful
function SearchByRankSuccess(data) {
  ranked = data; // Update favorite with the search results
  renderFilterdGames(ranked); // Render the filtered games
}

// Error alert if search fails
function SearchByRankECB(err) {
  console.error(err);
}

// Render the favorite games
function renderFilterdGames(list) {
  const $container = $("#my-games-container");
  $container.empty();

  // games.forEach to render each game
  list.forEach((game) => {
    const $gameDiv = $(`
          <div class="game">
              <h6>${game.name}</h6>
              <img src="${game.headerImage}" alt="${game.name}"> 
              <h6>Price: $${game.price.toFixed(2)}</h6>
              <h6>Rank: ${game.scoreRank}</h6>
              <button onclick="deleteGame(${game.appID})">DELETE</button>
S          </div>
      `);
    $container.append($gameDiv); // Append the gameDiv to the container
  });
}

$(document).on("click", "#EditProfile", function () {
  $("#my-games-container").hide();
  $("#textbox").hide();
  $("#title").hide();

  showEditForm();
});

$(document).on("click", "#LogOut", function () {
  sessionStorage.clear();
  alert("you have been logged out!");
  window.location.href = "index.html";
});

async function analyzeSentiment() {
  const userInput = $("#userInput").val();
  const result = $("#result");

  result.html("Analyzing sentiment...");

  try {
    const response = await fetch(
      "https://api-inference.huggingface.co/models/distilbert-base-uncased-finetuned-sst-2-english",
      {
        headers: {
          Authorization: "mycode",
        },
        method: "POST",
        body: JSON.stringify({ inputs: userInput }),
      }
    );

    if (!response.ok) {
      throw new Error("Error analyzing sentiment");
    }

    const data = await response.json();
    const sentiment = data[0][0].label;
    const score = (data[0][0].score * 100).toFixed(2);

    result.html(`Sentiment: <strong>${sentiment}</strong> (${score}%)`);
  } catch (error) {
    result.html(`Error: Unable to analyze sentiment. Please try again later.`);
  }
}
