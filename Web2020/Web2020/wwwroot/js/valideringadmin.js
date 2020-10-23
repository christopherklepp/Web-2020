//Funksjoner for validering og feilmeldinger på admin sider
function validerFra(fra) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,20}$/;
    const ok = regexp.test(fra);
    if (!ok) {
        $("#feilFra").html("Fra destinasjon må bestå av kun bokstaver og være 2 til 20 tegn");
        return false;
    }
    else {
        $("#feilFra").html("");
        return true;
    }
}

function validerTil(til) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,20}$/;
    const ok = regexp.test(til);
    if (!ok) {
        $("#feilTil").html("Til destinasjon må bestå av kun bokstaver og være 2 til 20 tegn");
        return false;
    }
    else {
        $("#feilTil").html("");
        return true;
    }
}

function validerPris(pris) {
    const regexp = /^[0-9]{1,4}$/;
    const ok = regexp.test(pris);
    if (!ok) {
        $("#feilPris").html("Pris må være 1-4 siffer");
        return false;
    }
    else {
        $("#feilPris").html("");
        return true;
    }
}

