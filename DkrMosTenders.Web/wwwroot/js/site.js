// Write your JavaScript code.

$(".btn-adv-search").bind("click", function (e) {
    e.preventDefault();
    $(this).closest(".search-block").children(".adv-search-block").slideToggle();
    $(this).toggleClass("active");
    $(this).toggleClass("dropup");
})

$('#search').submit(function () {
    $.ajax({
        url: 'Tenders',
        type: 'GET',
        success: function (tenders) {

        }
    })
})