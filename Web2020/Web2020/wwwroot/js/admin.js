$(function () {
    $.get("buss/HentReiserAdmin", function (reiser) {
        let ut = "<body>"+
            "<div class='container'>" +
            
            "<form id = adminform>" +
            "<table class='table table-bordered'>" +
            "<tr>" +
            "<th>Fra</th><th>Til</th><th>Pris</th><th>Avganger</th><th>Endre</th>" +
            "</tr>";
        for (let enReise of reiser) {
            ut += "<tbody>"+
                "<tr>" +
                "<td>" + enReise.reiserFra + "</td>" + "<td>" + enReise.reiserTil + "</td>" + "<td>" + enReise.pris + " kr</td>" +
                "<td>" + enReise.dag + ": kl " + enReise.tidspunkt + "</td><td> <a class='btn btn-primary' href='endre.html?id=" + enReise.rid + "'>Endre</a> </td>" +
                "<td> <button onclick='slett(" + enReise.rid + ")'>Slett</button></td>"
                "</tr>"+
                "<tbody>";
        }
        ut += "</table>" +
            "</form>"+
            "</div>"+
            "</body>";

        $("#alleReiser").html(ut);
    })
        .fail(function (feil) {
            if (feil.status == 401) {
                window.location.href = "login.html";
            }
        });
});

function slett(id) {
    let url = "buss/SlettReise?id=" + id; 
    $.get(url, function() {
         window.location.href = "admin.html";
    })
    .fail(function () {
        $("#feil").html("Feil på server - prøv 'Slett' igjen senere");
    });
}

function loggUt() {
    let url = "buss/LoggUt";
    $.get(url, function () {
        window.location.href = "index.html";
    });
}