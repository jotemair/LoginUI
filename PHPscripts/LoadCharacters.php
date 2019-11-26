<?php
// Server Login variables
	$server_name = "localhost";
	$server_username = "root";
	$server_password = "";
	$database_name = "nsirpg";
	
// User variables
	$username = $_POST["username"];

// Check conneciton
	$conn = new mysqli($server_name, $server_username, $server_password, $database_name);
	if(!$conn)
	{
		die("Connection Failed".mysql_connect_error());
	}

// Check if user exists
	$namecheckqery = "SELECT id FROM users WHERE username = '".$username."';";
	$namecheck = mysqli_query($conn, $namecheckqery);
	if(mysqli_num_rows($namecheck) != 1)
	{
		echo "Username does not exists";
		exit();
	}
	
// Use user ID to get characters
	$existinginfo = mysqli_fetch_assoc($namecheck);
	$userID = $existinginfo["id"];
	
	$charResults = "";
	$charactersGetQuery = "SELECT characterName, code FROM characters WHERE userID = '".$userID."';";
	$charactersGet = mysqli_query($conn, $charactersGetQuery);
	while($row = mysqli_fetch_assoc($charactersGet))
	{
	   $charResults .= $row['characterName']."|";
	   $charResults .= $row['code']."|";
	}
	
	echo $charResults;
?>