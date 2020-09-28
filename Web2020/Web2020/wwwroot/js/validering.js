/* Mangler valideringsfunksjon for tidspunkt */

function validerFornavn(fornavn) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,20}$/;
    const ok = regexp.test(fornavn);
    if (!ok) {
        $("#feilFornavn").html("Fornavn må bestå av kun bokstaver og være 2 til 20 tegn");
        return false;
    }
    else {
        $("#feilFornavn").html("");
        return true;
    }
}

function validerEtternavn(etternavn) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,20}$/;
    const ok = regexp.test(etternavn);
    if (!ok) {
        $("#feilEtternavn").html("Etternavnet må bestå av kun bokstaver og være på 2 til 20 bokstaver");
        return false;
    }
    else {
        $("#feilEtternavn").html("");
        return true;
    }
}

function validerAdresse(adresse) {
    var regexp = /^[0-9a-zA-ZæøåÆØÅ\ \.\-]{2,50}$/;
    var ok = regexp.test(adresse);
    if (!ok) {
        $("#feilAdresse").html("Adressen kan bestå av tall og bokstaver, fra 2 til 50 tegn");
        return false;
    }
    else {
        $("#feilAdresse").html("");
        return true;
    }
}

function validerTelefonNr(telefonnr) {
    var regexp = /^[0-9a-zA-ZæøåÆØÅ\ \.\-]{2,50}$/;
    var ok = regexp.test(telefonnr);
    if (!ok) {
        $("#feilTelefonnr").html("Telefonnummer skal bestå av tall mellom 7 og 14 tegn")
        return false;
    }
    else {
        $("feilTelefonnr").html("");
        return true;
    }
}

function validerTidspunkt(tidspunkt) {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }

    today = yyyy + '-' + mm + '-' + dd;
    document.getElementById("tidspunkt").setAttribute("min", today);


}



