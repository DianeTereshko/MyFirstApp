import "../node_modules/jquery/dist/jquery";
import "../node_modules/@types/jquery/index";


document.addEventListener("DOMContentLoaded", function () {
    // * TODO: Инициация поиска в PC по клику
    $("#LinkToSearch").on("click", null, null, function () {
        $("#myform").submit();
    });

    // * TODO: Инициация поиска в PC по нажатию enter
    $("#search").on("keyup", null, null, function (event: JQueryEventObject) {
        if (event.keyCode === 13) {
            $("#myform").submit();
        }
    });

    // * TODO: Инициация поиска в смартфоне по нажатию Enter
    $("#search2").on("keyup", null, null, function (event: JQueryEventObject) {
        if (event.keyCode === 13) {
            $("#myform").submit();
        }
    });
});

