export function FilterSearch() {
    // Declare variables
    var input, filter, ul, li, a, i, txtValue;
    input = document.getElementById('myInput');
    filter = input.value.toUpperCase();
    ul = document.getElementById("myUL");
    li = ul.getElementsByTagName('li');

    // Loop through all list items, and hide those who don't match the search query
    for (i = 0; i < li.length; i++) {
        a = li[i].getElementsByTagName("a")[0];
        txtValue = a.textContent || a.innerText;
        li[i].style.display = (txtValue.toUpperCase().indexOf(filter) > -1) ? "" : "none";
    }
}
export function AddNameToList(name) {
    var ul = document.getElementById("myUL");
    var li = document.createElement("li");
    var a = document.createElement("a");
    a.textContent = name;
    li.appendChild(a);
    ul.appendChild(li);

 }
