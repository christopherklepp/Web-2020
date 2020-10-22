$(function () {
    $.get("buss/ErLoggetInn", function () {

    })
        .fail(function (feil) {
            if (feil.status == 401) {
                window.location.href = "login.html";
            }
        });

    let dato = new Date();
    let tid = dato.getHours() + ":" + dato.getMinutes();
    $("#tid").val(tid);
});

function nyReiseValid() {
   
    const fraOK = validerFra($("#fra").val());
    const tilOK = validerTil($("#til").val());
    const prisOK = validerPris($("#pris").val());
    if (fraOK && tilOK && prisOK) {

        lagre();
    }
}


function lagre() {
    const reise = {
        reiserFra: $("#fra").val(),
        reiserTil: $("#til").val(),
        pris: $("#pris").val(),
        dag: $("#dag").val(),
        tidspunkt: $("#tid").val()
    };

    const url = "buss/LagreReise";
    $.post(url, reise, function () {
        window.location.href = "admin.html";
    })
        .fail(function(){
        $("#feil").html("Feil på server - prøv å 'Lagre Reise' igjen senere");
        });
}