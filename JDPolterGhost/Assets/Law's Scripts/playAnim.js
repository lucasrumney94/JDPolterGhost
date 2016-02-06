#pragma strict

function Start () {

}

function Update () {
     if (Input.GetKey (KeyCode.W)){
         GetComponent.<Animation>().Play("frightened");
     }
 
 }