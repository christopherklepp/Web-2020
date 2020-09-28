$(function () {
    $.get("buss/SisteBestilling", function (buss) {
        let ut = "<div>"
        ut += "<label>Fornavn:  </label> " + buss.fornavn + "</br>" +" <label>Etternavn:  </label> " + buss.etternavn + "</br>"
        ut += " <label>Adresse:  </label> " + buss.adresse + "</br>" + " <label>Telefonnr:  </label> " + buss.telefonnr + "</br>"
        ut += " <label>Tidspunkt:  </label> " + buss.tidspunkt + "</br></br>"+" <label>Til:  </label> " + buss.reiserTil+ "</br>"+" <label>Fra:  </label> "+ buss.reiserFra 
        ut += "<br /> <a href='index.html'>Tilbake til forside</a>";
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