/*function lagreBestilling() {
    function validerogLagreBestilling() {
        const fraReiseOK = validFraReise($("#reiseFra").val());
        const tilReiseOK = validTilReise($("#reiseTil#").val());
        const tidspunktOK = validTidspunkt($("#tidspunkt").val());
        const fornavnOK = validFornavn($("#fornavn").val());
        const etternavnOK = validEtternavn($("#etternavn").val());
        const adresseOK = validAdresse($("#adresse").val());
        const telefonnrOK = validTelefonNr($("#telefonnr").val());
        if (fraReiseOK && tilReiseOK && tidspunktOK && fornavnOK && etternavnOK && adresseOK && telefonnrOK) {
            lagreBestilling();
        }
    }
}*/


function lagreBestilling() {
    const buss = {
        reiserFra: $("#reiseFra").val(),
        reiserTil: $("#reiseTil").val(),
        tidspunkt: $("#tidspunkt").val(),
        fornavn: $("#fornavn").val(),
        etternavn: $("#etternavn").val(),
        adresse: $("#adresse").val(),
        telefonnr: $("#telefonnr").val()
    };


    const url = "buss/SettInnData";
    $.post(url, buss, function () {
        window.location.href = "bekreftelse.html";
    });
}