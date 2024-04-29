<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <script type="module" src="https://unpkg.com/ionicons@7.1.0/dist/ionicons/ionicons.esm.js"></script>
    <script nomodule src="https://unpkg.com/ionicons@7.1.0/dist/ionicons/ionicons.js"></script>
    <link rel="stylesheet" href="./css/regist.css">
</head>
<body>
<div class="wrapper">
    <button class="close-icon" onclick="window.location.href = 'index.php';"><ion-icon name="close"></ion-icon></button> 
    <div class="register">
        <h2>Registration</h2>
        <form action="./regauth.php" method = "POST">
            <div class="input-box">
                <span class="icon">
                    <ion-icon name="person"></ion-icon>
                </span>
                <input type="text" name="username" required>
                <label>Username</label>
            </div>
            <div class="input-box">
               <span class="icon">
                    <ion-icon name="mail"></ion-icon>
                </span>
               <input type="email" name="email" required>
                <label>Email</label>
            </div>
           <div class="input-box">
                <span class="icon">
                    <ion-icon name="lock-closed"></ion-icon>
                </span>
                <input type="password" name="password" required>
                <label>Password</label>
            </div>
            <div class="remember-forgot">
                <label><input type="checkbox" required>I agree to the terms & conditions</label>
            </div>
            <button type="submit" name="regist" class="btn">Register</button>
            <div class="login-register">
                <p>Already have an account?<a href="login.php" class="login-link">Login</a></p>
            </div>
            </form>
    </div>    
</div>
</body>
</html>






