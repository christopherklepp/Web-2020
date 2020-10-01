function requiredInfo() {
    const tidspunktOK = validerDato($("#tidspunkt").val());
    const fornavnOK = validerFornavn($("#fornavn").val());
    const etternavnOK = validerEtternavn($("#etternavn").val());
    const epostOK = validerEpost($("#epost").val());
    let tid = $("#tidspunkt").val();
    console.log(tid);
    if (tidspunktOK && fornavnOK && etternavnOK && epostOK) {

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
        epost: $("#epost").val()
    };

    const url = "buss/SettInnData";
   
    $.post(url, buss, function (OK) {
        if (OK == false) {

            $("#feil").html("Feil i registrering av bestilling. Prøv igjen. Husk å fylle ut og velg alle feltene");
        } else {
            window.location.href = "bekreftelse.html";
        }
    });
}


$(function () {
    $.get("buss/HentReiser", function (reiser) {
        formaterFraReiser(reiser);
        formaterTilReiser(reiser);
        pris();
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
        pris();
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
function pris() {
    $.get("buss/HentReiser", function (reiser) {
        let reiserFra = $("#reiseFra").val()
        let reiserTil = $("#reiseTil").val()
        let pris;
        for (let enReise of reiser) {
            if (reiserFra == enReise.reiserFra && reiserTil == enReise.reiserTil) {
                pris = enReise.pris + " kr";
                $("#pris").html(pris);
            }
        }
    });
}