$(function () {
    console.log("Nei")
    $.get("buss/HentAlleReiser", function (reiser) {
        console.log("Hei")
        let ut = "<table>" +
            "<tr>" +
            "<th>Fra</th><th>Til</th><th>Pris</th><th>Endre</th>" +
            "</tr>";
        console.log("Opprettet")
        for (let enReise of reiser) {
            ut += "<tr>" +
                "<td>" + enReise.reiserFra + "</td>" + "<td>" + enReise.reiserTil + "</td>" + "<td>" + enReise.pris + " kr</td>" +
                "<td> <a class='btn btn-primary' href='endre.html?id=" + kunde.id +"'>Endre</a> </td>"
                "</tr>";
            console.log("JA")
        }
        ut += "</table>"
        $("#alleReiser").html(ut);
    });
});