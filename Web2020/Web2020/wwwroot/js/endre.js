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
        $("#dag").val(reise.dag);
        $("#tid").val(reise.tidspunkt);
    })
        .fail(function (feil) {
            if (feil.status == 401) {
                window.location.href = "login.html";
            }
        });
});

function endreReiseValid() {
    const fraOK = validerFra($("#fra").val());
    const tilOK = validerTil($("#til").val());
    const prisOK = validerPris($("#pris").val());
    if (fraOK && tilOK && prisOK) {

        endreReise();
    }
}

function endreReise() {
    const Reise = {
        Rid: $("#id").val(),
        reiserFra: $("#fra").val(),
        reiserTil: $("#til").val(),
        pris: $("#pris").val(),
        dag: $("#dag").val(),
        tidspunkt: $("#tid").val()
    };
    $.post("buss/Endre", Reise, function () {
        console.log("Endret");
        window.location.href = "admin.html";
    })
    .fail(function () {
        $("#feil").html("Feil på server - prøv 'Endre' igjen senere");
    });
}
