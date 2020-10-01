//Skriver ut kvitering

$(function () {
    $.get("buss/SisteBestilling", function (buss) {
        let ut = "<div>"
        ut += "<label>Fornavn:  </label> " + buss.fornavn + "</br>" +" <label>Etternavn:  </label> " + buss.etternavn + "</br>"
        ut += " <label>E-post:  </label> " + buss.epost + "</br>"
        ut += " <label>Tidspunkt:  </label> " + buss.tidspunkt + "</br></br>"+" <label>Til:  </label> " + buss.reiserTil+ "</br>"+" <label>Fra:  </label> "+ buss.reiserFra 
        ut += "<br /> <a href='index.html'>Tilbake til forside</a>";
        ut += " </div>";

        $("#bekreftelse").html(ut);
    });
});