$(function () {
    $.get("buss/SisteBestilling", function (buss) {
        let ut = "<div>"
        ut +=  "<label>Fornavn:  </label>" + buss.fornavn  + "<label>Etternavn:  </label>" + buss.etternavn + "</br>"
        ut +=  "<label>Adresse:  </label>" + buss.adresse + "<label>Telefonnr:  </label>" + buss.telefonnr + "</br>"
        ut += "<label>Tidspunkt:  </label>" + buss.tidspunkt + "<label>Til:  </label>" + buss.reiserTil+ "<label>Fra:  </label>"+ buss.reiserFra 
        ut += " </div>";

        $("#bekreftelse").html(ut);
    });
});

/*$(function () {
    $.get("buss/SisteBestilling", function (buss) {
        let ut = "<table> <tr>"
        ut += "<td>" + "<label>Fornavn</label>" + buss.fornavn + "</td>" + "<td>" + "<label>Etternavn</label>" + buss.etternavn + "</td></br>"
        ut += "<td>" + "<label>Adresse</label>" + buss.adresse + "</td>" + "<td>" + "<label>Telefonnr</label>" + buss.telefonnr + "</td></br>"
        ut += "<td>" + "<label>Tidspunkt</label>" + buss.tidspunkt + "</td>" + "<td>" + "<label>Fra</label>" + buss.reiserTil + "</td>" + "<td>" + buss.reiserFra + "</td>"
        ut += "</tr> </table>";

        $("#bekreftelse").html(ut);
    });
});
*/