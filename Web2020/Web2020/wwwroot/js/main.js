<<<<<<< Updated upstream
﻿function hentAlle() {
    $.get("buss/HentAlle", function () {
        console.log("Test");
    });
=======
﻿

$(function () {
    hentAlleBusser();
});


function hentAlle() {
    $.get("buss/ HentAlle", function (busser)
        skrivUt(busser);
}

function skrivUt(busser) {
    for (let buss of busser) {
        document.write("Kundenavn: " + buss.fornavn + "<br>");
        
    }
>>>>>>> Stashed changes
}