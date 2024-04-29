<?php
require_once './auth.php';
session_start();

if(isset($_SESSION['user'])){
    header('Location: index.php');
    die();
}

if($_SERVER['REQUEST_METHOD'] == 'POST'){
    $username = $_POST['username'];
    $password = $_POST['password'];
    
    $auth_result = authenticate($username, $password);
    
    if($auth_result !== false){
        $_SESSION['user'] = $auth_result;
        
        if(isset($_POST['remember']) && $_POST['remember'] == 'on') {
            setcookie('remember_me', $username, time() + (86400 * 30), "/");
        }
        
        if(isset($_SESSION['redirect_url'])){
            header('Location: ' .$_SESSION['redirect_url']);
            die();
        }
        else{
            header('Location: index.php');
            die();
        }
    }
    else{
        $error_message = "Invalid username or password!";
    }
}
?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <script type="module" src="https://unpkg.com/ionicons@7.1.0/dist/ionicons/ionicons.esm.js"></script>
    <script nomodule src="https://unpkg.com/ionicons@7.1.0/dist/ionicons/ionicons.js"></script>
    <link rel="stylesheet" href="./css/login.css">
</head>
<body>
<?php if (isset($error_message)) { ?>
    <p style="color: red;"><?php echo $error_message; ?></p>
<?php } ?>
<div class="wrapper">
    <button class="close-icon" onclick="window.location.href = 'index.php';"><ion-icon name="close"></ion-icon></button> 
        <div class="login">
        <h2>Login</h2>
        <form action="<?php echo $_SERVER['PHP_SELF']?>" method = "POST">
            <div class="input-box">
                <span class="icon">
                    <ion-icon name="person"></ion-icon>
                </span>
                <input type="text" name="username" required>
                <label>Username</label>
            </div>
            <div class="input-box">
                <span class="icon">
                    <ion-icon name="lock-closed"></ion-icon>
                </span>
                <input type="password" name="password" required>
                <label>Password</label>
            </div>
            <div class="remember-forgot">
                <label><input type="checkbox" require>Remember me</label>
                <a href="forget.php">Forgot password?</a>
            </div>
            <button type="submit" name="login" class="btn">Login</button>
                <div class="login-register">
                    <p>Don't have an account?<a href="signup.php" class="register-link">Register</a></p>
                </div>
        </form>
        </div>
</div>
</body>
</html>
