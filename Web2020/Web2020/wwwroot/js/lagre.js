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
    console.log(buss.reiserFra);
    console.log(buss.reiserTil);
    console.log(buss.tidspunkt);
    console.log(buss.fornavn);
    console.log(buss.etternavn);
    console.log(buss.adresse);
    console.log(buss.telefonnr);

    const url = "buss/SettInnData";
    $.post(url, buss, function () {
        window.location.href = "bekreftelse.html";
    });
}