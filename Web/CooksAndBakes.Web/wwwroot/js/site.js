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

document.querySelectorAll(".customize-file-upload-button").forEach(function (button) {
    const hiddenInput = button.parentElement.querySelector(".file-input");
    const label = button.parentElement.querySelector(".file-upload-label");
    const defaultLabelText = 'No file(s) selected.';

    label.textContent = defaultLabelText;
    label.title = defaultLabelText;

    button.addEventListener('click', function () {
        hiddenInput.click();
    });

    hiddenInput.addEventListener('change', function () {
        const fileNameList = Array.from(hiddenInput.files).map(function (file) {
            return file.name;
        });

        label.textContent = fileNameList.join(', ') || defaultLabelText;
        label.title = label.textContent;
    });
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
