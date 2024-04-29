<?php
require './connect.php';
function authenticate($username, $password){
    global $con;
    $sql = "SELECT * FROM `users` WHERE username = ? ";
    $stmt = $con->prepare($sql);
    $stmt->bind_param("s", $username);
    $stmt->execute();
    
    $result = $stmt->get_result();
    if($result->num_rows > 0){
        $user = $result->fetch_assoc();
        if(password_verify($password, $user['password'])){
            return $user;
        }
    }
    $stmt->close();
    $con->close();
    
    return false;
}
