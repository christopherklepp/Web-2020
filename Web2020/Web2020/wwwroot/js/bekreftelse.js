//Skriver ut kvittering
$(function () {
    $.get("buss/SisteBestilling", function (buss) {
        let ut = "<div>"
        ut += "<label>Fornavn:  </label> " + buss.fornavn + "</br>" +" <label>Etternavn:  </label> " + buss.etternavn + "</br>"
        ut += " <label>E-post:  </label> " + buss.epost + "</br>"
        ut += " <label>Avgang:  </label> " + buss.dag + ": kl " + buss.tidspunkt + "</br></br>" + "<label>Fra:  </label> " + buss.reiserFra + " <br> <label>Til:  </label> " + buss.reiserTil + "</br>" + " <label > Pris:   </label > " + buss.pris + " kr</br > "
        ut += "<br /> <a class='btn btn-outline-success' href='index.html'>Tilbake til forside</a>";
        ut += " </div>";

        $("#bekreftelse").html(ut);
    });
});