$(function () {
    const id = window.location.search.substring();
    console.log(id);
    const url = "buss/HentEnReise" + id;
    $.get(url, function (reise) {
        $("#fra").val(reise.reiserFra);
        $("#til").val(reise.reiserTil);
        $("#pris").val(reise.pris);
    });
});
