<?php
// Server Login variables
	$server_name = "localhost";
	$server_username = "root";
	$server_password = "";
	$database_name = "nsirpg";
	
// User variables
	$username = $_POST["username"];
	$password = $_POST["password"];
	$email = $_POST["email"];

// Check conneciton
	$conn = new mysqli($server_name, $server_username, $server_password, $database_name);
	if(!$conn)
	{
		die("Connection Failed".mysql_connect_error());
	}
	
// Check if user already exists
	$namecheckqery = "SELECT id FROM users WHERE username = '".$username."';";
	$namecheck = mysqli_query($conn, $namecheckqery);
	if(mysqli_num_rows($namecheck) > 0)
	{
		echo "Username already exists";
		exit();
	}
	
// Check if email already exists
	$emailcheckqery = "SELECT id FROM users WHERE email = '".$email."';";
	$emailcheck = mysqli_query($conn, $emailcheckqery);
	if(mysqli_num_rows($emailcheck) > 0)
	{
		echo "Email already exists";
		exit();
	}
	
// Create user
	$salt = "\$5\$round=5000\$"."supercalifragilisticexpialidocious".$username."\$";
	$hash = crypt($password, $salt);
	
	$insertuserquery = "INSERT INTO users (username, email, hash, salt) VALUES('".$username."','".$email."','".$hash."','".$salt."');";
	mysqli_query($conn, $insertuserquery) or die("Error, insert failed");
	echo "Success";
?>