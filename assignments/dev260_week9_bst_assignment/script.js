document.addEventListener("DOMContentLoaded", function (event) {
  document.getElementById("welcome-aboard").disabled = true;
  document.getElementById("welcome-back").disabled = true;
)}
function formSubmissionEvent(){
  let username = document.getElementById("username").value;
  let passcode = document.getElementById("passcode").value;
  let town = document.getElementById("town").value;
  let state = document.getElementById("state").value;
}
function welcomeNewUser(){
  document.getElementById("hello").disabled = true;
  document.getElementById("welcome-aboard").disabled = false;

  l
}
function welcomePastUser(){
  document.getElementById("hello").disabled = true;
  document.getElementByID("welcome-back").disabled = false;
  let username = document.getElementById("username").value;
  let passcode = document.getElementById("passcode").value;
}
function storeAway(){
  localStorage.setItem("username", username);
  localStorage.setItem("passcode", passcode);
  localStorage.setItem("town", town);
  localStorage.setItem("state", state);
}
function isThisYou(){
  if(username == username.localStorage && password == password.localStorage){
    break;
  else if(username != username.localStorage || password != password.localStorage){
    alert("Invalid username or password. Please try again.");
  }
  }
}


