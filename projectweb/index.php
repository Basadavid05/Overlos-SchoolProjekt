<!DOCTYPE html>
<html>
    <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <link rel="stylesheet" href="css/index.css">
    </head>
    <body>
        <?php
        //echo 'REQUEST METHOD: ' . $_SERVER["REQUEST_METHOD"] .'</br>';
        //echo 'REQUEST URI: ' . $_SERVER["REQUEST_URI"] .'</br>';
        //echo 'REQUEST Query_String: ' . $_SERVER["QUERY_STRING"] .'</br>';
        $title = 'Home';
        $page = 'home';
        include './navbar.php';
        ?>
        <div class="slider">
         <div class="list">
            <div class="item active">
                <img src="./css/image/Kép2.png">
                <div class="content">
                    <h2 id="gamtit">Overlos</h2>
                    <p>
                        You can read about the game and download it.
                    </p>
                    <a href="game.php">Learn more...</a>
                </div>
            </div>
            <div class="item">
                <img src="./css/image/shop.jpg">
                <div class="content">
                    <h2>Shop</h2>
                    <p>
                       Here you can find cool merch and more content for the game.
                    </p>
                    <a href="shop.php">Learn more...</a>
                </div>
            </div>
        </div>
        <!-- button arrows -->
        <div class="arrows">
            <button id="prev"><</button>
            <button id="next">></button>
        </div>
        <!-- thumbnail -->
        <div class="thumbnail">
            <div class="item active">
                <img src="./css/image/Kép2.png">
                <div class="content">
                    Game
                </div>
            </div>
            <div class="item">
                <img src="./css/image/shop.jpg">
                <div class="content">
                    Shop
                </div>
            </div>
        </div>
    </div>
    <script src="index.js"></script>
    </body>
</html>
