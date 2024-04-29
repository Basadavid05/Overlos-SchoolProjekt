<?php
session_start();
$_SESSION['redirect_url'] = $_SERVER['REQUEST_URI'];

$page = isset($page) ? $page : "home";

if(isset($_GET['logout']) && $_GET['logout'] == true) {
    session_destroy();
    header("Location: index.php");
    die();
}
?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <script type="module" src="https://unpkg.com/ionicons@7.1.0/dist/ionicons/ionicons.esm.js"></script>
    <script nomodule src="https://unpkg.com/ionicons@7.1.0/dist/ionicons/ionicons.js"></script>
    <link rel="stylesheet" href="./css/navbar.css">
    <title><?php echo $title ?></title>
</head>
<body>
    <div class="nava">
        <button class="btn" id="menu"><ion-icon name="menu-outline"></ion-icon></button>
        <p>Overlos</p>
        <?php if($page != 'home') { ?>
            <ul class="lista">
                <li><a href="index.php" class="<?php if($page == 'home') echo 'active';?>">Home</a></li>
                <li><a href="game.php"class="<?php if($page == 'overlos') echo 'active';?>">Game</a></li>
                <li><a href="shop.php"class="<?php if($page == 'shop') echo 'active';?>">Shop</a></li>
            </ul>
        <?php } ?>
        <?php if($page == 'home' || $page == 'overlos' || $page == 'shop' || $page == 'us' || $page == 'sett') { ?>
            <?php if(isset($_SESSION['user'])) { ?>              
                <div id="profile">
                    <button class="btn"><ion-icon name="person"></ion-icon></button>
                    <div id="profile-dropdown">
                        <span><?php echo $_SESSION['user']['username']; ?></span>
                        <a href="sett.php">Settings</a>
                        <a href="?logout=true">Logout</a>
                    </div>
                </div>
            <?php } else { ?>
                <div class="login-regist">
                   <button id="login" onclick="window.location.href = 'login.php';">Login</button>
                    <button id="regist" onclick="window.location.href = 'signup.php';">Sign up</button>
                </div>
            <?php } ?>
        <?php } ?>
    </div>
    <?php if($page == 'home') { ?>
    <style>
        #menu {
            display: none;
        }
    </style>
    <?php } ?>
    <script>
        let bar = document.querySelector('#menu');
        let lista = document.querySelector('.lista');

        bar.addEventListener('click', () => {
            if (lista.style.display === 'flex') {
                lista.style.display = 'none';
            } else {
                lista.style.display = 'flex';
            }
        });
    </script>
</body>
</html>