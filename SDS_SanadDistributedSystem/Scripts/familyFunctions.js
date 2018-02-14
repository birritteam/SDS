//FAMILIES SCRIPT
$("#familynature").on("change", function () {
    if ($(this).val() == "نازح داخلي") {
        $("#displacementdate").removeClass("hidden");
        $("#lastaddress").removeClass("hidden");
        
    }
    else {
        $("#displacementdate").addClass("hidden");
        $("#lastaddress").addClass("hidden");
    }
})

$(document).ready(function () {
    $("#familynature").trigger("change")
})