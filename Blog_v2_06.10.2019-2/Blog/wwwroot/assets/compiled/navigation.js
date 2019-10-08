// * TODO: Меню в смартфоне
function toggleMenu() {
    $("#menu").toggleClass("w3-show");
}
// * TODO: Аккордеон в меню в смартфоне
//function showMenu(id) {
//    $(`#${id}`).toggleClass("w3-show");
//}
function showMenu(id, caretId) {
    $(`#${id}`).toggleClass('w3-show');
    console.log(caretId);
    var cl = $(`#${caretId}`).hasClass('fa-caret-down');
    console.log('Show menu wors');
    if (cl) {
        $(`#${caretId}`).removeClass("fa-caret-down");
        $(`#${caretId}`).addClass("fa-caret-up");
    }
    if (!cl) {
        $(`#${caretId}`).removeClass("fa-caret-up");
        $(`#${caretId}`).addClass("fa-caret-down");
    }
}
//# sourceMappingURL=navigation.js.map
