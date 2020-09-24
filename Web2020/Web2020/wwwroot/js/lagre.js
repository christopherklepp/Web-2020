function requiredInfo() {
    /*  const tidspunktOK = validTidspunkt($("#tidspunkt").val()); */
    console.log($("#fornavn").val());
    const fornavnOK = validerFornavn($("#fornavn").val());
    const etternavnOK = validerEtternavn($("#etternavn").val());
    const adresseOK = validerAdresse($("#adresse").val());
    const telefonnrOK = validerTelefonNr($("#telefonnr").val());
    if (/*tidspunktOK && */ fornavnOK && etternavnOK && adresseOK && telefonnrOK) {

        lagreBestilling();
    }
}


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

$(function () {
    $.get("buss/HentReiser", function (reiser) {
        formaterFraReiser(reiser);
        formaterTilReiser(reiser);
    });
});

function formaterFraReiser(reiser) {
    let ut;
    for (let enReise of reiser) {
        ut += "<option value='" + enReise.reiserFra + "'>" + enReise.reiserFra + "</option>";
    }

    $("#reiseFra").html(ut);
}

function tilReiser() {
    $.get("buss/HentReiser", function (reiser) {
        formaterTilReiser(reiser)
    });
}

function formaterTilReiser(reiser) {
    let reiserFra = $("#reiseFra").val();
    let ut;
    for (let enReise of reiser) {
        if (reiserFra == enReise.reiserFra)
            ut += "<option value='" + enReise.reiserTil + "'>" + enReise.reiserTil + "</option>";
    }

    $("#reiseTil").html(ut);
}