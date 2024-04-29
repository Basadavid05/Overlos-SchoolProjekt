<?php
require './connect.php';

if($_SERVER['REQUEST_METHOD'] == 'POST'){
    $username = $_POST['username'];
    $email = $_POST['email'];
    
    $user_sql = "SELECT * FROM `users` WHERE username LIKE ? ";
    $user_stmt = $con->prepare($user_sql);
    $user_stmt->bind_param("s", $username);
    $user_stmt->execute();
    $user_result = $user_stmt->get_result();
    
    $email_sql = "SELECT * FROM `users` WHERE email LIKE ? ";
    $email_stmt = $con->prepare($email_sql);
    $email_stmt->bind_param("s", $email);
    $email_stmt->execute();     
    $email_result = $email_stmt->get_result();
    
    if($user_result->num_rows > 0){
        echo "ERROR: Username is already taken. Please choose another username.";
        die();
    }elseif($email_result->num_rows > 0){
        echo "ERROR: Email is already taken. Please choose another email.";
        die();
    }else{
         $password = password_hash($_POST["password"], PASSWORD_DEFAULT);
         $sql = "INSERT INTO `users`(`id`, `username`, `email`, `password`) VALUES (NULL, ?, ?, ?)";
         $stmt = $con->prepare($sql);
         $stmt->bind_param("sss", $username, $email, $password);
         $stmt->execute();
         header("Location: login.php");
         die();
    }
     $_POST["username"] = "";
     $_POST["email"] = "";
     $_POST["password"] = "";
     $user_stmt->close();
     $email_stmt->close();
     $con->close();
}

