<?php
$db = new mysqli('localhost', 'root', '', 'user_auth');


if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $username = $db->real_escape_string($_POST['username']);
    $password = password_hash($_POST['password'], PASSWORD_DEFAULT);

    $result = $db->query("SELECT id FROM users WHERE username = '$username'");
    if ($result->num_rows == 0) {
        $db->query("INSERT INTO users (username, password) VALUES ('$username', '$password')");
        echo "Registration successful";
    } else {
        echo "Username already exists";
    }
}
$db->close();