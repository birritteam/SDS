//FAMILIES SCRIPT
$("#familynature").on("change", function () {
    if ($(this).val() == "نازح داخلي") {
        $("#displacementdate").removeClass("hidden");
        $("#lastaddress").removeClass("hidden");
        $("#lastaddress_details").removeClass("hidden");
        
    }
    else {
        $("#displacementdate").addClass("hidden");
        $("#lastaddress").addClass("hidden");
        $("#lastaddress_details").addClass("hidden");
    }
})

$(document).ready(function () {
    $("#familynature").trigger("change")
})