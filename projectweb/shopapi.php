<?php
require './connect.php';
$method = $_SERVER['REQUEST_METHOD'];
global $con;

switch ($method) {
    case 'GET':
        $sql = "SELECT * FROM `products`";
        $response = $con->query($sql);
        $products = [];
        while($row = mysqli_fetch_array($response)){
            $products[] = array(
                "id" => $row["id"],
                "category" => $row["category"],
                "name" => $row["name"],
                "description" => $row["description"],
                "code" => $row["code"],
                "price" => $row["price"],
                "image_url" => $row["image_url"]
            );
        } 
        http_response_code(200);
        echo json_encode($products);
        break;
    
        case 'POST':
            $category = $_POST['category'];
            $name = $_POST['name'];
            $desc = $_POST['desc'];
            $code = $_POST['code'];
            $price = $_POST['price'];
            $imageUrl = $_FILES['image'];
            $uploadDir = "uploads/";
            if (!file_exists($uploadDir)) {
                mkdir($uploadDir, 0777, true);
            }
            $filename = uniqid() . "_" . basename($_FILES["image"]["name"]);
            $targetPath = $uploadDir . $filename;
            if (move_uploaded_file($_FILES["image"]["tmp_name"], $targetPath)) {
                $imageUrl = 'http://' . $_SERVER['HTTP_HOST'] . '/' . $targetPath;
                $sql = "INSERT INTO `products`(`id`, `category`, `name`, `description`, `code`, `price`, `image_url`) VALUES (NULL, ?, ?, ?, ?, ?, ?)";
                $stmt = $con->prepare($sql);
                $stmt->bind_param("ssssis", $category, $name, $desc, $code, $price, $imageUrl);
                $stmt->execute();
                header("Location: shop.php");
                die();
            } else {
                echo "Sorry, there was an error uploading your file.";
                echo "Error: " . $_FILES["image"]["error"];
            }
            break;


            case 'DELETE':
                parse_str(file_get_contents("php://input"), $_DELETE);
                if (isset($_DELETE['id'])) {
                    $id = $_DELETE['id'];
                    $sql = "DELETE FROM `products` WHERE `id` = ?";
                    $stmt = $con->prepare($sql);
                    $stmt->bind_param("i", $id);
                    $stmt->execute();
                    $sql_select = "SELECT id FROM products ORDER BY id";
                    $result = $con->query($sql_select);
            
                    if ($result->num_rows > 0) {
                    $new_id = 1;
                    while ($row = $result->fetch_assoc()) {
                        $old_id = $row['id'];
                    
                        if ($old_id != $new_id) {
                            $sql_update = "UPDATE products SET id = $new_id WHERE id = $old_id";
                            if ($con->query($sql_update) !== TRUE) {
                                echo "Error updating record: " . $con->error;
                                break;
                            }
                        }
                        $new_id++;
                    }
                
                    $sql_reset_auto_increment = "ALTER TABLE products AUTO_INCREMENT = $new_id";
                    if ($con->query($sql_reset_auto_increment) === FALSE) {
                        echo "Error resetting auto-increment: " . $con->error;
                    } else {
                        echo "IDs reorganized successfully.";
                    }
                }else {
                        echo "No records found.";
                    }
                    http_response_code(200);
                    echo json_encode(array('message' => 'Product deleted successfully'));
                } else {
                    http_response_code(400);
                    echo json_encode(array('error' => 'Invalid request'));
                }
                break;

        default:
        http_response_code(405);
        echo json_encode(array('error' => 'Unsupported request method'));
        break;
}