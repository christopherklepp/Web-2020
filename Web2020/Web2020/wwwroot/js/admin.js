$(function () {
    console.log("Nei")
    $.get("buss/HentReiser", function (reiser) {
        console.log("Hei")
        let ut = "<body>"+
            "<div class='container'>" +
            
            "<form id = adminform>" +
            "<table class='table table-bordered'>" +
            "<tr>" +
            "<th>Fra</th><th>Til</th><th>Pris</th><th>Avganger</th><th>Endre</th>" +
            "</tr>";
        console.log("Opprettet")
        for (let enReise of reiser) {
            ut += "<tbody>"+
                "<tr>" +
                "<td>" + enReise.reiserFra + "</td>" + "<td>" + enReise.reiserTil + "</td>" + "<td>" + enReise.pris + " kr</td>" +
                "<td>" + enReise.avganger + "</td><td> <a class='btn btn-primary' href='endre.html?id=" + enReise.rid + "'>Endre</a> </td>"
                "</tr>"+
                "<tbody>"    ;
            console.log(enReise.rid);
        }
        ut += "</table>" +
            "</form>"+
            "</div>"+
            "</body>";

        $("#alleReiser").html(ut);
    });
});