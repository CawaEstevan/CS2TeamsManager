document.getElementById("searchJogadores").addEventListener("keyup", function () {
    fetch("/Jogadores/Search?searchTerm=" + this.value)
        .then(res => res.text())
        .then(html => document.getElementById("resultsJogadores").innerHTML = html);
});
