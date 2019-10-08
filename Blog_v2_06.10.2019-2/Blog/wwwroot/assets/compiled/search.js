document.addEventListener("DOMContentLoaded", function () {
    // * TODO: Инициация поиска в PC по клику
    $("#LinkToSearch").on("click", null, null, function () {
        $("#myform").submit();
    });
    // * TODO: Инициация поиска в PC по нажатию enter
    $("#search").on("keyup", null, null, function (event) {
        if (event.keyCode === 13) {
            $("#myform").submit();
        }
    });
    // * TODO: Инициация поиска в смартфоне по нажатию Enter
    $("#search2").on("keyup", null, null, function (event) {
        if (event.keyCode === 13) {
            $("#myform").submit();
        }
    });
});
//# sourceMappingURL=search.js.map
