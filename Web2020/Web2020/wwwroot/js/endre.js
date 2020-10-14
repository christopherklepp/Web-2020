$(function () {
    const id = window.location.search.substring();
    console.log(id);
    const url = "buss/HentEnReise" + id;
    if (id == "") {
        window.location.href = "admin.html";
    }
    $.get(url, function (reise) {
        $("#id").val(reise.rid);
        $("#fra").val(reise.reiserFra);
        $("#til").val(reise.reiserTil);
        $("#pris").val(reise.pris);
    });
});

function endreReise() {
    const Reise = {
        Rid: $("#id").val(),
        reiserFra: $("#fra").val(),
        reiserTil: $("#til").val(),
        pris: $("#pris").val()
    };
    $.post("buss/Endre", Reise, function () {
        console.log("Endret");
        window.location.href = "admin.html";
    });
}
