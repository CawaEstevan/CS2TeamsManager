document.getElementById("searchBox").addEventListener("keyup", function () {
    fetch("/Times/Search?searchTerm=" + this.value)
        .then(res => res.text())
        .then(html => document.getElementById("results").innerHTML = html);
});
