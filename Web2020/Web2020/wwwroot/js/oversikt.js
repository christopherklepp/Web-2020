$(function () {
    console.log("ja");
    $.get("buss/HentAlle", function (bestilling) {
        console.log("hei");
        formaterOversikt(bestilling)
    });
});

function formaterOversikt(bestilling) {
    console.log("hallo");
    let ut = "<table>" +
        "<tr><th>Reiser fra</th><th>Reiser til</th><th>Tidspunkt</th><th>Fornavn</th><th>Etternavn</th>" +
        "<th>Adresse</th><th>Telefonnr</th></tr>";
    for (let enBestilling of bestilling) {
        console.log(enBestilling.reiserFra)
        ut += "<tr>" +
                "<td>" + enBestilling.reiserFra + "</td>" +
                "<td>" + enBestilling.reiserTil + "</td>" +
                "<td>" + enBestilling.tidspunkt + "</td>" +
                "<td>" + enBestilling.fornavn + "</td>" +
                "<td>" + enBestilling.etternavn + "</td>" +
                "<td>" + enBestilling.adresse + "</td>" +
                "<td>" + enBestilling.telefonnr + "</td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#tabellUt").html(ut);
}