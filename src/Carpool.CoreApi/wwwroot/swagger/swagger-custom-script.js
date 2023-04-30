(function () {
    window.addEventListener("load", function () {
        setTimeout(function () {
            var logo = document.getElementsByClassName("link");
            logo[0].children[0].alt = "Implemeting Swagger for Infobip";
            logo[0].children[0].src = "/swagger/infobit-logo.jpg";
        });
    });
})();