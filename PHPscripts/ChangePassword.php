<?php
// Server Login variables
	$server_name = "localhost";
	$server_username = "root";
	$server_password = "";
	$database_name = "nsirpg";
	
// User variables
	$username = $_POST["username_Post"];
	$password = $_POST["password_Post"];

// Check conneciton
	$conn = new mysqli($server_name, $server_username, $server_password, $database_name);
	if(!$conn)
	{
		die("Connection Failed".mysql_connect_error());
	}
	
	$salt = "\$5\$round=5000\$"."supercalifragilisticexpialidocious".$username."\$";
	$hash = crypt($password, $salt);
	
	$updatePasswordQuery = "UPDATE users SET salt = '".$salt."', hash = '".$hash."' WHERE username = '".$username."';";
	$updateResult = mysqli_query($conn, $updatePasswordQuery) or die("Error: Update failed");
	
	if($updateResult)
	{
		echo "Password Changed";
	}
?>