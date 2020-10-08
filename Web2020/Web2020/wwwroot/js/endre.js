$(function () {
    const id = window.location.search.substring(1);
    const url = "buss/HentEnReise?" + id;
    $.get(url, function (reise) {
        $("fra").val(reise.reiserfra);
        $("til").val(reise.reiserTil);
        $("pris").val(reise.pris);
    });
});