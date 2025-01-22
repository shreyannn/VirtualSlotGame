<?php
// Database connection settings
$servername = "localhost"; // Replace with your database server
$username = "root";        // Replace with your database username
$password = "";            // Replace with your database password
$dbname = "slot-final"; // Replace with your database name

// Create a connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

// Get POST data
$user_id = $_POST['user_id'];
$old_balance = $_POST['old_balance'];
$new_balance = $_POST['new_balance'];
$spin_count = $_POST['spin_count'];
$bet = $_POST['bet'];
$win = $_POST['win'];
$RTP = $_POST['RTP'];

// Validate data (basic example)
// if (empty($user_id) || empty($old_balance) || empty($new_balance) || empty($spin_count) || empty($bet) || empty($win) || empty($RTP)) {
//     echo json_encode(['success' => false, 'message' => 'All fields are required.']);
//     exit;
// }

if (!isset($user_id) || !isset($old_balance) || !isset($new_balance) || 
    !isset($spin_count) || !isset($bet) || !isset($win) || !isset($RTP)) {
    echo json_encode(['success' => false, 'message' => 'All fields are required.']);
    exit;
}

// Prepare and execute SQL query
$sql = "INSERT INTO player_activities (user_id, old_balance, new_balance, spin_count, bet, win, RTP, created_at) 
        VALUES ('$user_id', '$old_balance', '$new_balance', '$spin_count', '$bet', '$win', '$RTP', NOW())";

if ($conn->query($sql) === TRUE) {
    echo json_encode(['success' => true, 'message' => 'Player activity recorded successfully.']);
} else {
    echo json_encode(['success' => false, 'message' => 'Error: ' . $conn->error]);
}

// Close the connection
$conn->close();
?>
