<?php
// Server Login variables
	$server_name = "localhost";
	$server_username = "root";
	$server_password = "";
	$database_name = "nsirpg";
	
// User variables
	$username = $_POST["username"];
	$password = $_POST["password"];

// Check conneciton
	$conn = new mysqli($server_name, $server_username, $server_password, $database_name);
	if(!$conn)
	{
		die("Connection Failed".mysql_connect_error());
	}

// Check if user exists
	$namecheckqery = "SELECT username, salt, hash FROM users WHERE username = '".$username."';";
	$namecheck = mysqli_query($conn, $namecheckqery);
	if(mysqli_num_rows($namecheck) != 1)
	{
		echo "Username does not exists";
		exit();
	}
	
// Check if password is correct
	$existinginfo = mysqli_fetch_assoc($namecheck);
	$salt = $existinginfo["salt"];
	$hash = $existinginfo["hash"];
	
	$hashcheck = crypt($password, $salt);
	if ($hashcheck != $hash)
	{
		echo "Incorrect password";
		exit();
	}
	else
	{
		echo "Success";
		exit();
	}
?>