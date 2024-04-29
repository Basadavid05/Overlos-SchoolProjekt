<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <script type="module" src="https://unpkg.com/ionicons@7.1.0/dist/ionicons/ionicons.esm.js"></script>
    <script nomodule src="https://unpkg.com/ionicons@7.1.0/dist/ionicons/ionicons.js"></script>
    <link rel="stylesheet" href="./css/shop.css">
</head>
<body>
    <header><?php
        $title = "Shop";
        $page = "shop";
        include './navbar.php';
        ?></header>
    <h1>Soul Shop</h1>
    <div class="container">
        <button class="menubtn" id="shopmenu"><ion-icon name="menu-outline"></ion-icon></button>
        <div class="menu">
            <input id="search_bar" type="text" name="search" placeholder="Search...">
            <ion-icon class="search_icon" name="search-sharp"></ion-icon>
                    
            <ul class="menulist">
                <li id="show-all-products"><a href="#">All</a></li>
                <label>Game:</label>
                <li class="butt"><a href="#">DLC</a></li>
                <li class="butt"><a href="#">Content</a></li>
            </ul>
        </div>
        <div id="product-container"></div>
        <div class="cadt">
            <button id="showcart"><ion-icon id="cart" name="cart-outline"></ion-icon></button>
            <div class="cartAmount">0</div>
            <div id="cart-container"></div>
            <p id="total"></p>
            <button href="#" id="checkout">Checkout</button>
        </div>
    </div>
    <div id="buy">
        <button id="close-icon"><ion-icon name="close"></ion-icon></button> 
                <h3>Pay form</h3>
                <form action="bankapi.php" method="POST">

                    <label class="lab">First name:</label>
                    <input type="text" class="bank" id="first_name" name="first_name" ><br>

                    <label class="lab">Last name:</label>
                    <input type="text" class="bank" id="last_name" name="last_name" ><br>

                    <label class="lab">Email:</label>
                    <input type="email" class="bank" id="eamil" name="email" ><br>

                    <label class="lab">Birthdate:</label>
                    <input type="date" class="bank" id="birth_date" name="birth_date" ><br>

                    <label class="lab">Address:</label>
                    <input type="text" class="bank" id="address" name="address" ><br>

                    <label class="lab">Bank name:</label>
                    <input type="text" class="bank" id="bank_name" name="bank_name" ><br>

                    <label class="lab">Account number:</label>
                    <input type="text" class="bank" id="account_number" name="account_number" ><br>

                    <button id="buy-form" type="submit">Buy</button>
                </form>
    </div>
    <p id="codes"></p>
    <div id="addd">
        <?php if(isset($_SESSION['user']['username']) && $_SESSION['user']['username'] === "admin") { ?>
                <form action="./shopapi.php" method="POST" enctype="multipart/form-data">
                    <input class="inp" type="text" name="category" placeholder="Category..." >
                    <input class="inp" type="text" name="name" placeholder="Name..." >
                    <input class="inp" type="text" name="desc" placeholder="Description..." >
                    <input class="inp" type="text" name="code" placeholder="Code..." >
                    <input class="inp" type="number" name="price" placeholder="Price..." >
                    <input class="inpfile" type="file" name="image" >
                    <input id="sub" type="submit" name="submit">
                </form> 
            <?php } ?>
    </div>
    <footer>
        <p>Copyright &copy; 2024 Game name, Designed by Suba Dávid, Basa Dávid</p>
    </footer>
    <script src="shop.js"></script>
</body>
</html>

