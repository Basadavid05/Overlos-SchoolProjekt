<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <script type="module" src="https://unpkg.com/ionicons@7.1.0/dist/ionicons/ionicons.esm.js"></script>
    <script nomodule src="https://unpkg.com/ionicons@7.1.0/dist/ionicons/ionicons.js"></script>
    <link rel="stylesheet" href="./css/forget.css">
</head>
<body>
<div id="wrapper">
    <button class="close-icon" onclick="window.location.href = 'login.php';"><ion-icon name="close"></ion-icon></button>
    <div class="forget">
    <h2>Password Settings</h2>
    <form action="<?php echo $_SERVER['PHP_SELF']; ?>" method="POST">
        <div class="input-pass">   
            <input type="text" id="username" name="username" required><br>
            <label>Username:</label><br>
        </div>

        <div class="input-pass">
            <input type="password" id="newpassword" name="new-password" required><br>
            <label>New Password:</label><br>
        </div>
        
        <div class="input-pass">
            <input type="password" id="confirmpassword" name="confirm-password" required><br>
            <label>Confirm New Password:</label><br>
        </div>
        <button class="changepass" type="submit">Change Password</button>
    </form>
    </div>
</div>
</body>
</html>

<?php
require './connect.php';
global $con;

if(isset($_SERVER['REQUEST_METHOD']) && $_SERVER['REQUEST_METHOD'] === 'POST'){
    $username = $_POST['username'] ?? '';
    $newPassword = $_POST['newpassword'] ?? '';
    $confirmPassword = $_POST['confirmpassword'] ?? '';
                
    if ($newPassword !== $confirmPassword) {
        echo "Passwords do not match.";
        exit();
    }

    $hashedPassword = password_hash($newPassword, PASSWORD_DEFAULT);
                
    $sql = "UPDATE users SET `password`=? WHERE username =?";
    $stmt = $con->prepare($sql);
    $stmt->bind_param("ss", $hashedPassword, $username);
    if($stmt->execute()){
        header("Location: login.php");
        exit();
    }else{
        echo '<script>console.log("here")</script>';
    }
    $stmt->close();
}
?>

