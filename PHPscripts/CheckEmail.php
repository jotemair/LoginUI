<?php
// Server Login variables
	$server_name = "localhost";
	$server_username = "root";
	$server_password = "";
	$database_name = "nsirpg";
	
// User variables
	$email = $_POST["email_Post"];

// Check conneciton
	$conn = new mysqli($server_name, $server_username, $server_password, $database_name);
	if(!$conn)
	{
		die("Connection Failed".mysql_connect_error());
	}
	
	$usernamecheckquery = "SELECT username FROM users WHERE email = '".$email."';";
	$usernamecheck = mysqli_query($conn, $usernamecheckquery);
	if(mysqli_num_rows($usernamecheck) != 1)
	{
		echo "Email does not exists";
		exit();
	}
	
	$existinginfo = mysqli_fetch_assoc($usernamecheck);
	$username = $existinginfo["username"];
	
	echo $username;
	exit();
?>