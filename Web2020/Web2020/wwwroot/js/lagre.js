//Sjekker at all info er fylt inn
function requiredInfo() {
    const fornavnOK = validerFornavn($("#fornavn").val());
    const etternavnOK = validerEtternavn($("#etternavn").val());
    const epostOK = validerEpost($("#epost").val());
    if (fornavnOK && etternavnOK && epostOK) {

        lagreBestilling();
    }
}

//Lagrer bestillingen og navigerer til side for bekreftelse
function lagreBestilling() {
    let avganger = $("#avganger").val();
    let avgangerSplit = avganger.split(':');
    let dager = avgangerSplit[0];
    let tidspunkt = avgangerSplit[1] + ":" + avgangerSplit[2];

    let priser = $("#priser").val();
    let priserSplit = priser.split(' ');

    const buss = {
        reiserFra: $("#reiseFra").val(),
        reiserTil: $("#reiseTil").val(),
        fornavn: $("#fornavn").val(),
        etternavn: $("#etternavn").val(),
        epost: $("#epost").val(),
        dag: dager,
        tidspunkt: tidspunkt.trim(),
        pris: priserSplit[0]
    };
   

    const url = "buss/SettInnData";

    
    $.post(url, buss, function () {
        window.location.href = "bekreftelse.html";
    })
        .fail(function () {
            $("#feil").html("Feil på server - prøv 'Bestill' igjen senere");
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
        pris();
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
            avgang = enReise.dag + ": " + enReise.tidspunkt;
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
            let avgangSjekk = enReise.dag + ": " + enReise.tidspunkt;
            if (reiserFra == enReise.reiserFra && reiserTil == enReise.reiserTil && avganger == avgangSjekk) {
                pris = enReise.pris + " kr";
                $("#priser").val(pris);
            }
        }
    });
}