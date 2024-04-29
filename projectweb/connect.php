<?php
$con = new mysqli("localhost", "root", "", "gamedata");
if($con->errno){
    die("Connection failed:" .$con->errno);
}
$con->set_charset('utf8');
