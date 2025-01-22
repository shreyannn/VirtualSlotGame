<?php
// login.php
session_start();
$db = new mysqli('localhost', 'root', '', 'slot-final');   

if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $username = $_POST['username'];
    $password = $_POST['password'];

    // Prepared statement to retrieve the user's id and password
    // $stmt = $db->prepare("SELECT id, password FROM users WHERE name = ?");
    $stmt = $db->prepare("SELECT id, password, banned FROM users WHERE name = ?");
    $stmt->bind_param("s", $username);  // "s" is for string
    $stmt->execute();
    $result = $stmt->get_result();

    if ($result->num_rows == 1) {
        $user = $result->fetch_assoc();
        
        // Verify the password
        if (password_verify($password, $user['password'])) {
            $_SESSION['user_id'] = $user['id'];

        //    Retrieve spin count
           $stmt3 = $db->prepare("SELECT spin_count FROM player_activities WHERE user_id = ? ORDER BY id DESC LIMIT 1");
            $stmt3->bind_param("i", $user['id']); 
            $stmt3->execute();
            $result3 = $stmt3->get_result();

            // Check if any data exists
            if ($result3->num_rows > 0) {
                $spinCount = $result3->fetch_assoc()['spin_count'];
            } else {
                $spinCount = 0; // Default to 0 if no data exists
            }

            // Retrieve balance for the logged-in user
            $stmt2 = $db->prepare("SELECT balance FROM user_balances WHERE user_id = ?");
            $stmt2->bind_param("i", $user['id']);  // "i" is for integer
            $stmt2->execute();
            $result2 = $stmt2->get_result();

            if ($result2->num_rows == 1) {
                $balance = $result2->fetch_assoc();
                echo "Login successful! | ID: " . $user['id'] . " | Account Status: " . $user['banned'] .   " | Your SpinCount: "  . $spinCount . " | Your balance: " . $balance['balance'] ; 
            }
            else {
                echo "Error retrieving balance.";
            }
        } else {
            echo "Invalid username or password";
        }
    } else {
        echo "Invalid username or password";
    }
}

$db->close();
?>


