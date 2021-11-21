//Inspired from https://www.w3schools.com/howto/howto_js_tabs.asp

function loadForm(event, FormName) {
    var i, tabcontent, taboptions;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    taboptions = document.getElementsByClassName("taboptions");
    for (i = 0; i < taboptions.length; i++) {
        taboptions[i].className = taboptions[i].className.replace(" active", "");
    }
    document.getElementById(FormName).style.display = "block";
    event.currentTarget.className += " active";

}
