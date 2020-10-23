//Funksjon for at en admin kan logge inn
function login() {

    const brukernavnOK = validerBrukernavn($("#brukernavn").val());
    const passordOK = validerPassord($("#passord").val());

    if (brukernavnOK && passordOK) {
        const bruker = {
            brukernavn: $("#brukernavn").val(),
            passord: $("#passord").val()
        }
        $.post("Buss/Login", bruker, function (OK) {
            if (OK) {
                window.location.href = 'admin.html';
            }
            else {
                $("#feil").html("Feil brukernavn eller passord er oppgitt");
            }
        })
        .fail(function () {
                $("#feil").html("Funksjonen for å logge inn fungerer ikke for øyeblikket, vennligst prøv igjen senere");
            });
    }
}
