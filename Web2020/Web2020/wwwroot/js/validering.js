// Funksjoner for validering og feilmelding til bruker på klient

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

function validerEpost(epost) {
    var regexp = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    var ok = regexp.test(epost);
    if (!ok) {
        $("#feilEpost").html("Epost må være riktig format: ola@normann.no")
        return false;
    }
    else {
        $("#feilEpost").html("");
        return true;
    }
}

function validerDato() {
    var inputDate = new Date(document.getElementById("tidspunkt").value);
    var date = new Date();
    if (inputDate < date) {
        $("#feilDato").html("Tidspunktet kan ikke være tilbake i tid")
        return false;
    } else {
        $("#feilDato").html("");
        return true;
    }
}



function validerBrukernavn(brukernavn) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{3,15}$/;
    const ok = regexp.test(brukernavn);
    if (!ok) {
        $("#feilBrukernavn").html("Brukernavnet må bestå av kun bokstaver, og være på 3 til 15 tegn");
        return false;
    }
    else {
        $("#feilBrukernavn").html("");
        return true;
    }
}

function validerPassord(passord) {
    const regexp = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$/;
    const ok = regexp.test(passord);
    if (!ok) {
        $("#feilPassord").html("Passordet må bestå av minimum 6 tegnminst en bokstav og et tall");
        return false;
    }
    else {
        $("#feilPassord").html("");
        return true;
    }
}
