// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    $('[data-toggle="tooltip"]').tooltip({
        html: true
    })
})

$("#name-input").focusout(function () {
    let name = $("#name-input").val();

    if (name.length != 0) {
        $('#card-name').text(name);
    }

})

$("image-input").focusout(
    $("select")
    .change(function () {
        let imageUrl = '';
        let categoryname = '';
        $("select option:selected").each(function () {
            imageUrl = $(this).val();
            categoryname = $(this).text();
        });
        $("#card-image").attr('src', imageUrl);
        $('#card-keyword').text(categoryname);
    })
    .trigger("change"));

$("#level-of-complexity").focusout(function () {
    let value = $("#level-of-complexity").val();

    if (value == 1) {
        $("#h1").addClass("level");
        $("#h2").removeClass("level");
        $("#h3").removeClass("level");
        $("#h4").removeClass("level");
        $("#h5").removeClass("level");
    } else if (value == 2) {
        $("#h1").addClass("level");
        $("#h2").addClass("level");
        $("#h3").removeClass("level");
        $("#h4").removeClass("level");
        $("#h5").removeClass("level");
    } else if (value == 3) {
        $("#h1").addClass("level");
        $("#h2").addClass("level");
        $("#h3").addClass("level");
        $("#h4").removeClass("level");
        $("#h5").removeClass("level");
    } else if (value == 4) {
        $("#h1").addClass("level");
        $("#h2").addClass("level");
        $("#h3").addClass("level");
        $("#h4").addClass("level");
        $("#h5").removeClass("level");
    } else if (value == 5) {
        $("#h1").addClass("level");
        $("#h2").addClass("level");
        $("#h3").addClass("level");
        $("#h4").addClass("level");
        $("#h5").addClass("level");
    }
});


//$("#image-input").focusout(function () {
//    let imageUrl = $("#image-input").val();

//    if (imageUrl.length != 0) {
//        $('#card-image').attr('src', imageUrl);
//    }
//})

//$("#keyword-input").focusout(function () {
//    $('#card-keyword').text($("#keyword-input").val());
//})

//$("#attack-input").focusout(function () {
//    let attack = $("#attack-input").val();

//    if (attack.length != 0) {
//        $('#card-attack').text(attack);
//    }
//})

//$("#health-input").focusout(function () {
//    let health = $("#health-input").val();

//    if (health.length != 0) {
//        $('#card-health').text(health);
//    }
//})

//$("#description-input").focusout(function () {
//    $('#card-name').attr('data-original-title', `Description:<br>${$("#description-input").val()}`);
//})
