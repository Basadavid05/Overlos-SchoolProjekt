<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="./css/game.css">
</head>
<body>
    <header><?php
        $title = "Overlos";
        $page = "overlos";
        include './navbar.php';?></header>
    <main>
    <h1 id="game">Overlos</h1>
    <p id="bev">When we first embarked on this project, we didn't know what exactly we would like to do.
    But as time went on, we discovered we would like to make an RPG, Soulslite.
    Though it's my third map in the game, I looked for stunning textures to create beautiful scenery, and I believe this to be the greatest.
    Where players could explore a vast map and amazing landscapes while searching for secret objects and buildings at different times of the day.
    And they can engage in thrilling battles against formidable enemies.
    Although our game is still in development and not perfect, we have tried our best to create a playable game that you can play for hours.
    However several of our plans were canceled by a tight schedule. So, in the future, we plan to include new equipment and gear, like weapons and armor, and elaborate structures and scenery in the initial update.
    I hope you will enjoy our game.
    </p>
    <a id="downgame" href="./image/img1.png" download="img1.png">Download Game</a>
    <div id="game_story">    
    <h3>Story:</h3>
    <p>As I slowly open my eyes, I am greeted by a breathtaking sight.
    I am no longer in my familiar bedroom, but in a strange and magical world. 
    The sky is a vibrant shade of blue, with clouds floating lazily across it. 
    Trees of all shapes and sizes stand tall, their leaves a myriad of colors ranging from deep greens to bright reds. 
    And surrounding me are creatures I could have never imagined in my wildest dreams.
    As I sit up, I take in my surroundings. 
    I am in a small clearing, surrounded by a forest of towering trees.
    In the distance I see houses on a plateau. 
    But my attention is drawn to the creatures around me. 
    There are huge scorpions, dinosaurus walking across the meadow.
    In the woods i see a beautiful white tiger lieing on the gound.
    I stand up, my feet sinking into the soft grass beneath me. 
    I take a deep breath, the air filled with a sweet fragrance that I can't quite place. 
    As I start to explore, i walked to the sea and i saw a huge shark jumping out of water.
    When i reached the houses i found wierd creatures, they are a humanoid pigs, but after the huge giganotosaurus i'm not suprised.
    As I take in the beauty of this world, I can't help but feel a sense of wonder and excitement. 
    I am in a world of magic and fantasy, surrounded by creatures I could have only dreamed of. 
    And as I continue to explore, I can't wait to see what other wonders this world has in store for me.
    </p>
    </div>
    <div class="pic">
        <h3 id="h">Pictures:</h3>
    <div class="slider">
         <div class="list">
            <div class="item active">
                <img src="./css/image/map1.jpg">
            </div>
            <div class="item">
                <img src="./css/image/kép3.png">
            </div>
            <div class="item">
                <img src="./css/image/shark.jfif">
            </div>
            <div class="item">
                <img src="./css/image/basilisc.jpeg">
            </div>
            <div class="item">
                <img src="./css/image/giagno.jpg">
            </div>
            <div class="item">
                <img src="./css/image/scorpion.jpg">
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
                <img src="./css/image/map1.jpg">
            </div>
            <div class="item">
                <img src="./css/image/kép3.png">
            </div>
            <div class="item">
                <img src="./css/image/shark.jfif">
            </div>
            <div class="item">
                <img src="./css/image/basilisc.jpeg">
            </div>
            <div class="item">
                <img src="./css/image/giagno.jpg">
            </div>
            <div class="item">
                <img src="./css/image/scorpion.jpg">
            </div>
        </div>
    </div>
    <script src="index.js"></script>
    </div>
</main>
<footer><p>Copyright &copy; 2024 Game name, Designed by Suba Dávid, Basa Dávid</p></footer>
</body>
</html>