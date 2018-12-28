var gameRunning = false;
var squareSize = 20;
var fieldSize = 500;
var squares = [];
var emptyActionName = "death";
var fillActionName = "life";

document.addEventListener("DOMContentLoaded", function (event) {
    var game = document.getElementById("field");
    game.width = game.height = fieldSize;
    var gameContext = game.getContext('2d');
    gameContext.strokeStyle = "black";
    gameContext.lineWidth = 2;


    var drawing = function () {
        for (var x = 0; x < fieldSize * 2; x += squareSize) {
            squares[Math.floor(x / squareSize)] = [];
            for (var y = 0; y < fieldSize * 2; y += squareSize) {
                gameContext.fillStyle = "white";
                gameContext.fillRect(x, y, squareSize, squareSize);
                gameContext.strokeRect(x, y, squareSize, squareSize);
                squares[Math.floor(x / squareSize)][Math.floor(y / squareSize)] = false;
            }
        }
    };

    drawing();


    var doAction = function (x, y, actionName) {
        x = Math.floor(x / squareSize);
        y = Math.floor(y / squareSize);
        squares[x][y] = actionName === fillActionName;
        gameContext.fillStyle = actionName !== fillActionName ? "white" : "black";
        gameContext.fillRect(x * squareSize, y * squareSize, squareSize, squareSize);
        if (actionName === emptyActionName)
            gameContext.strokeRect(x * squareSize, y * squareSize, squareSize, squareSize);

    }

    var getSqare = function (x, y) {
        return squares[Math.floor(x / squareSize)][Math.floor(y / squareSize)];
    }

    game.onclick = function (event) {
        if (!gameRunning) {
            if (getSqare(event.offsetX, event.offsetY)) {
                doAction(event.offsetX, event.offsetY, emptyActionName);
            } else {
                doAction(event.offsetX, event.offsetY, fillActionName);
            }
        }
    }

    var checkNeighbours = function (x, y) {
        var neighboursCount = 0;
        var checkX = [-1, 0, 1];
        var checkY = [-1, 0, 1];
        if (x == 0) checkX = [0, 1];
        else if (x == squares.lenght - 1) checkX = [0, -1]
        if (y == 0) checkY = [0, 1];
        else if (y == squares[x].lenght - 1) checkY = [0, -1];
        for (var i = 0; i < checkX.lenght; i++) {
            for (var j = 0; j < checkY.lenght; j++) {
                if (checkX[i] != 0 || checkY[j] != 0)
                    if (squares[x + checkX[i]][y + checkY[j]])
                        neighboursCount++;
            }
        }
        if (squares[x][y] && (neighboursCount > 3 || neighboursCount < 2))
            return { "x": x * squareSize, "y": y * squareSize, actionName: emptyActionName };
        else if (!squares[x][y] && neighboursCount == 3)
            return { "x": x * squareSize, "y": y * squareSize, actionName: fillActionName };
    }

    var performNextStep = function () {
        var squareStates = [];
        for (var x = 0; x < squares.length; x++) {
            for (var y = 0; y < squares[x].length; y++) {
                var state = checkNeighbours(x, y);
                if (state)
                    squareStates.push(state);
            }
        }
        for (var i = 0; i < squareStates.length; i++) {
            doAction(squareStates[i].x, squareStates[i].y, squareStates[i].actionName);
        }
        if (gameRunning)
            setTimeout(performNextStep, 100);
    }

    document.getElementById("startButton").onclick = function () {
        gameRunning = true;
        performNextStep();
    };

    document.getElementById("runOneStepButton").onclick = performNextStep;

    document.getElementById("stopButton").onclick = function () {
        gameRunning = false;
    };

    document.getElementById("clearButton").onclick = function () {
        gameRunning = false;
        drawing();
    }

});

