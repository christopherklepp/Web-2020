$(function () {
    $.get("buss/ErLoggetInn", function () {

    })
        .fail(function (feil) {
            if (feil.status == 401) {
                window.location.href = "login.html";
            }
        });
});


function lagre() {
    let dag = $("#dag").val();
    let tid = $("#tid").val();
    const reise = {
        reiserFra: $("#fra").val(),
        reiserTil: $("#til").val(),
        pris: $("#pris").val(),
        avganger: dag + ": kl " + tid
    };

    const url = "buss/LagreReise";
    $.post(url, reise, function () {
        window.location.href = "admin.html";
    })
        .fail(function(){
        $("#feil").html("Feil på server - prøv å 'Lagre Reise' igjen senere");
        });
}