$(function () {
    $.get("buss/SisteBestilling", function (buss) {
        let ut = "<table> <tr>"
        ut += "<td>" + buss.fornavn + "</td>" + "<td>" + buss.etternavn + "</td>" + "<td>" + buss.adresse + "</td>" + "<td>" + buss.telefonnr + "</td>";
        ut += "<td>" + buss.tidspunkt + "</td>" + "<td>" + buss.reiserTil + "</td>" + "<td>" + buss.reiserFra + "</td>"
        ut += "</tr> </table>";

        $("#bekreftelse").html(ut);
    });
});