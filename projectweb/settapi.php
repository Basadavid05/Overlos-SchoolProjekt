<?php
session_start();
require './connect.php';
global $con;

$userId = $_SESSION['user']['id'];
$userNa = $_SESSION['user']['username'];

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    if (isset($_POST['action'])) {
        switch ($_POST['action']) {

            case 'update-profile':
                $newUsername = $_POST['username'] ?? null;
                $newEmail = $_POST['email'] ?? null;
            
                if ($newUsername) {
                    $sql = "SELECT `username` FROM `users` WHERE username = ?";
                    $stmt = $con->prepare($sql);
                    $stmt->bind_param("s", $newUsername);
                    $stmt->execute();
            
                    $result = $stmt->get_result();
                    if ($result->num_rows > 0) {
                        echo "ERROR: Username is already taken. Please choose another username.";
                        die();
                    } else {
                        $sql = "UPDATE users SET username=? WHERE id=?";
                        $stmt = $con->prepare($sql);
                        $stmt->bind_param("si", $newUsername, $userId);
                        $stmt->execute();
                        $_SESSION['user']['username'] = $newUsername;
                    }
                    $stmt->close();
                }
            
                if ($newEmail) {
                    $sql = "SELECT `email` FROM `users` WHERE email = ?";
                    $stmt = $con->prepare($sql);
                    $stmt->bind_param("s", $newEmail);
                    $stmt->execute();
            
                    $result = $stmt->get_result();
                    if ($result->num_rows > 0) {
                        echo "ERROR: Email is already taken. Please choose another email.";
                        die();
                    } else {
                        $sql = "UPDATE users SET email=? WHERE id=?";
                        $stmt = $con->prepare($sql);
                        $stmt->bind_param("si", $newEmail, $userId);
                        $stmt->execute();
                    }
                    $stmt->close();
                }
                header("Location: sett.php");
                exit();
                break;

            case 'change-password':
                $newPassword = $_POST['new-password'] ?? null;
                $confirmPassword = $_POST['confirm-password'] ?? null;
                
                if ($newPassword !== $confirmPassword) {
                    echo "Passwords do not match.";
                    exit();
                }

                $hashedPassword = password_hash($newPassword, PASSWORD_DEFAULT);
                
                $sql = "UPDATE users SET `password`=? WHERE id=?";
                $stmt = $con->prepare($sql);
                $stmt->bind_param("si", $hashedPassword, $userId);
                
                if ($stmt->execute() === TRUE) {
                    header("Location: sett.php");
                    exit();
                } else {
                    echo "Error: " . $sql . "<br>" . $stmt->error;
                }
                $stmt->close();
                break;
                
            case 'delete-account':
                //a többi táblából is törölje a dolgokat
                $delete_account = $_POST['delete-account'] ?? null;
                if ($delete_account == $use) {
                $sql = "DELETE FROM `users` WHERE id = ?";
                $stmt = $con->prepare($sql);
                $stmt->bind_param("i", $userId);
                if ($stmt->execute()) {

                    $sql_select = "SELECT id FROM users ORDER BY id";
                    $result = $con->query($sql_select);
                        
                    if ($result->num_rows > 0) {
                        $new_id = 1;
                        while ($row = $result->fetch_assoc()) {
                            $old_id = $row['id'];
                                
                            if ($old_id != $new_id) {
                            $sql_update = "UPDATE users SET id = $new_id WHERE id = $old_id";
                                if ($con->query($sql_update) !== TRUE) {
                                    echo "Error updating record: " . $con->error;
                                    break;
                                }
                            }
                                $new_id++;
                        }
                            
                        $sql_reset_auto_increment = "ALTER TABLE users AUTO_INCREMENT = $new_id";
                        if ($con->query($sql_reset_auto_increment) === FALSE) {
                        echo "Error resetting auto-increment: " . $con->error;
                        } else {
                            echo "IDs reorganized successfully.";
                        }
                        }
                        else {
                            echo "No records found.";
                        }
                            echo "Your account has been deleted successfully.";
                            session_destroy();
                            header("Location: index.php");
                            exit();
                        } else {
                            echo "Error deleting your account. Please try again later.";
                        }
                        $stmt->close();
                    } else {
                        echo "Invalid username. Please try again.";
                    }
                    break;
                    
            default:
            http_response_code(405);
            echo json_encode(array('error' => 'Unsupported request method'));
            break;

        }
    } else {
        // No action specified, handle error
        echo "No action specified.";
    }
} else {
    // Invalid request method, handle error
    echo "Invalid request method.";
}
?>


