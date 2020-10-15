$(function () {
    console.log("Nei")
    $.get("buss/HentReiser", function (reiser) {
        console.log("Hei")
        let ut = "<table>" +
            "<tr>" +
            "<th>Fra</th><th>Til</th><th>Pris</th><th>Avganger</th><th>Endre</th>" +
            "</tr>";
        console.log("Opprettet")
        for (let enReise of reiser) {
            ut += "<tr>" +
                "<td>" + enReise.reiserFra + "</td>" + "<td>" + enReise.reiserTil + "</td>" + "<td>" + enReise.pris + " kr</td>" +
                "<td>" + enReise.avganger + "</td><td> <a class='btn btn-primary' href='endre.html?id=" + enReise.rid + "'>Endre</a> </td>"
                "</tr>";
            console.log(enReise.rid);
        }
        ut += "</table>"
        $("#alleReiser").html(ut);
    });
});