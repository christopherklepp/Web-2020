function requiredInfo() {
    //const tidspunktOK = validerDato($("#tidspunkt").val());
    const fornavnOK = validerFornavn($("#fornavn").val());
    const etternavnOK = validerEtternavn($("#etternavn").val());
    const epostOK = validerEpost($("#epost").val());
    //let tid = $("#tidspunkt").val();
    if (/*tidspunktOK && */fornavnOK && etternavnOK && epostOK) {

        lagreBestilling();
    }
}


function lagreBestilling() {
    const buss = {
        reiserFra: $("#reiseFra").val(),
        reiserTil: $("#reiseTil").val(),
        //tidspunkt: $("#tidspunkt").val(),
        avganger: $("#avganger").val(),
        fornavn: $("#fornavn").val(),
        etternavn: $("#etternavn").val(),
        epost: $("#epost").val()
    };

    const url = "buss/SettInnData";

    
    $.post(url, buss, function () {
        window.location.href = "bekreftelse.html";
    })
        .fail(function () {
            $("#feil").html("Feil i registrering av bestilling. Prøv igjen. Husk å fylle ut og velg alle feltene");
        });
        
   
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
        formaterAvganger(reiser);
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
        formaterTilReiser(reiser);
        formaterAvganger(reiser);
    });
}

function formaterTilReiser(reiser) {
    let reiserFra = $("#reiseFra").val();
    let ut;
    for (let enReise of reiser) {
        if (reiserFra == enReise.reiserFra) { 
                ut += "<option value='" + enReise.reiserTil + "'>" + enReise.reiserTil + "</option>";    
        }
    }
    $("#reiseTil").html(ut);
}
function avgang() {
    $.get("buss/HentReiser", function (reiser) {
        formaterAvganger(reiser);
        pris();
    });
}

function formaterAvganger(reiser) {
    
    let reiserFra = $("#reiseFra").val();
    let reiserTil = $("#reiseTil").val();
    let avgang;
    let ut = "";
    for (let enReise of reiser) {
        if (reiserFra == enReise.reiserFra && reiserTil == enReise.reiserTil) {
            avgang = enReise.avganger;
            ut += "<option>" + avgang + "</option>";
        }
    }
    $("#avganger").html(ut);
}

function pris() {
    $.get("buss/HentReiser", function (reiser) {
        let reiserFra = $("#reiseFra").val();
        let reiserTil = $("#reiseTil").val();
        let avganger = $("#avganger").val();
        let pris;
        for (let enReise of reiser) {
            if (reiserFra == enReise.reiserFra && reiserTil == enReise.reiserTil && avganger == enReise.avganger) {
                pris = enReise.pris + " kr";
                $("#pris").html(pris);
            }
        }
    });
}