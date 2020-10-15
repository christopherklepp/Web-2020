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
    });
}