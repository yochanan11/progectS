function ShowNewUser() {
    document.getElementById("connect").style.display = "none";
    document.getElementById("registery").style.display = "";
    document.getElementById("bConnect").style.display = "";
    document.getElementById("bShowNewUser").style.display = "none";
}
function ShowConnect () {
    document.getElementById("connect").style.display = "";
    document.getElementById("registery").style.display = "none";
    document.getElementById("bShowNewUser").style.display = "";
    document.getElementById("bConnect").style.display = "none";
}

function Search(input) {
    var url = "/home/SearchCategoryOrFood/?name" + input.value;
    window.location.href = url;
}
function ShowPassword() {
    document.getElementById("ShowPassword").style.display = "none";
    document.getElementById("Password").style.display = "";
}
function AddIndices() {
    document.getElementById("AddIndices").style = "";
}
function NoAddIndices() {
    document.getElementById("AddIndices").style = "none";
}