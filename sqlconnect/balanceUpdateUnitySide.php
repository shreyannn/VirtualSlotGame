<?php
// update_balance.php
$db = new mysqli('localhost', 'root', '', 'slot-final');

if (isset($_POST['userID']) && isset($_POST['newBalance'])) {
    $userID = $db->real_escape_string($_POST['userID']);
    $newBalance = $db->real_escape_string($_POST['newBalance']);

    // Update the balance for the provided username
    $updateQuery = "UPDATE user_balances SET balance = '$newBalance' WHERE user_id = '$userID'";
    
    if ($db->query($updateQuery) === TRUE) {
        echo "Balance updated successfully";
    } else {
        echo "Error updating balance: " . $db->error;
    }
} else {
    echo "Username or new balance not provided.";
}

$db->close();
?>

