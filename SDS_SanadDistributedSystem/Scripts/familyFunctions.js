//FAMILIES SCRIPT
$("#familynature").on("change", function () {
    if ($(this).val() == "فرد من المجتمع المضيف") {
        $("#displacementdate").addClass("hidden");
        $("#lastaddress").addClass("hidden");
    }
    else {
        $("#displacementdate").removeClass("hidden");
        $("#lastaddress").removeClass("hidden");
    }
})

$(document).ready(function () {
    $("#familynature").trigger("change")
})