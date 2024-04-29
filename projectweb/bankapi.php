<?php
session_start();
require './connect.php';
$method = $_SERVER['REQUEST_METHOD'];
global $con;

$userId = $_SESSION['user']['id'];

switch($method){
    case 'POST':
        $userId = $_SESSION['user']['id'];
            
                $sql = "SELECT * FROM `personal` WHERE `id` = ?";
                $stmt = $con->prepare($sql);
                $stmt->bind_param("i", $userId);
                $stmt->execute();
                $result = $stmt->get_result();
            
                if ($result->num_rows > 0) {  
                    http_response_code(200);
                } else {
                    $first_name = $_POST['first_name'];
                    $last_name = $_POST['last_name'];
                    $birth_date = $_POST['birth_date'];
                    $address = $_POST['address'];
                    $bank_name = $_POST['bank_name'];
                    $account_number = $_POST['account_number'];
            
                    $insert_sql = "INSERT INTO `personal` (`id`, `first_name`, `last_name`, `birth_date`, `home_address`, `bank_name`, `account_number`) VALUES (?, ?, ?, ?, ?, ?, ?)";
                    $insert_stmt = $con->prepare($insert_sql);
                    $insert_stmt->bind_param("isssssi", $userId, $first_name, $last_name, $birth_date, $address, $bank_name, $account_number);
                    
                    if ($insert_stmt->execute()) {
                        http_response_code(200);
                        echo json_encode(array('message' => 'Data inserted successfully'));
                    } else {
                        http_response_code(500);
                        echo json_encode(array('error' => 'Failed to insert data'));
                    }
                }
                header("Location: shop.php");
                die();
                
                break;
}

?>