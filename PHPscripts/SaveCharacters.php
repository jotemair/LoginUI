<?php
// Server Login variables
	$server_name = "localhost";
	$server_username = "root";
	$server_password = "";
	$database_name = "nsirpg";
	
// User variables
	$username = $_POST["username"];
	$charNum = (int)$_POST["charNum"];

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
	
// Use user ID to delete existing characters
	$existinginfo = mysqli_fetch_assoc($namecheck);
	$userID = $existinginfo["id"];
	
	$charactersDelQuery = "DELETE FROM characters WHERE userID = '".$userID."';";
	mysqli_query($conn, $charactersDelQuery) or die("Error, deletion failed");

// Add characters
	for ($i = 0; $i < $charNum; $i++)
	{
		$charName = $_POST["charName_".$i];
		$charCode = $_POST["charCode_".$i];
		
		$insertuserquery = "INSERT INTO characters (userID, characterName, code) VALUES('".$userID."','".$charName."','".$charCode."');";
		mysqli_query($conn, $insertuserquery) or die("Error, insert failed");
	}
	
	echo "Success";
?>