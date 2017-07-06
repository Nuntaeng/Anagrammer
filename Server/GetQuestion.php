<?php

	$connection = mysqli_connect("mysql.hostinger.kr", "u245160108_words", "4SbFSiLAoF");
	$param = json_decode(file_get_contents('php://input'), true);
	mysqli_select_db($connection, "u245160108_words");


	mysqli_query($connection, "set session character_set_connection=utf8;");
	mysqli_query($connection, "set session character_set_results=utf8;");
	mysqli_query($connection, "set session character_set_client=utf8;");
	$result = mysqli_query($connection, "SELECT WORD, HURIGANA, MEANING FROM WORDS WHERE LEVEL = ".$param['level']." AND NO = ".$param['no']);

	print $param['level']."  ".$param['no'];

	if(isset($result))
	{
		do
		{
			while($row = mysql_fetch_array($result))
			{
				echo $row[0];
			}

		}while(mysql_next_result($result));
	}

	mysqli_close($connection);
?>