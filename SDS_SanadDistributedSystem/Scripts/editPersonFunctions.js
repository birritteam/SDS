var idFamily;
var type;

$(document).ready(function () {


    idFamily = $("#family_book_number").val();
    var family_order = $("#family_order_id").val();
    
    if (family_order.includes("H")) {
        $("input[type=radio][name=position][id=H]").trigger("click").trigger("change");
    } else if (family_order.includes("W")) {
        $("#wifeOrder").val(family_order.substr(family_order.length - 1))
        $("input[type=radio][name=position][id=W]").trigger("click").trigger("change");
    } else if (family_order.includes("C")) {
        if ($("#gender").val() == "ذكر") {
            $("#childOrder").val(family_order.split("C").pop()); 
            $("input[type=radio][name=position][id=S]").trigger("click").trigger("change");
        } else if ($("#gender").val() == "أنثى") {
            $("#childOrder").val(family_order.split("C").pop());
            $("input[type=radio][name=position][id=D]").trigger("click").trigger("change");

        }
    }

   // $("#family_order_id").trigger("focus");

    //$("#ذكر").prop("checked", true).trigger("click");
    //$("#family_order_id").val(idFamily + "H").trigger("change");
    //$("#family_order_id").trigger("focus");
 
});


// generate person's id based on his/her familyid and his position in the family

$("input[type=radio][name=position]").on("change", function () {

    type = $(this).val();

    switch ($(this).prop('id')) {
        case "D":
            $("#childOrder").removeClass("hidden");
            $("#wifeOrder").addClass("hidden");
            $("#أنثى").prop("checked", true).trigger("click");
            $("#family_order_id").val(idFamily + type + $("#childOrder").val()).trigger("focusout");
            break;
        case "S":
            $("#childOrder").removeClass("hidden");
            $("#wifeOrder").addClass("hidden");
            $("#ذكر").prop("checked", true).trigger("click");
            $("#family_order_id").val(idFamily + type + $("#childOrder").val()).trigger("focusout");
            break;
        case "W":
            $("#wifeOrder").removeClass("hidden");
            $("#childOrder").addClass("hidden");
            $("#أنثى").prop("checked", true).trigger("click");
            $("#family_order_id").val(idFamily + type + $("#wifeOrder").val()).trigger("focusout");
            break;
        case "H":
            $("#wifeOrder").addClass("hidden");
            $("#childOrder").addClass("hidden");
            $("#ذكر").prop("checked", true).trigger("click");
            $("#family_order_id").val(idFamily + type).trigger("focusout");
            break;
        default:
            $("#wifeOrder").addClass("hidden");
            $("#childOrder").addClass("hidden");
    }


});

$("#childOrder").on("change", function () {

    var childOrder = $("#childOrder").val();
    $("#family_order_id").val(idFamily + type + childOrder).trigger("focusout");;

});

$("#wifeOrder").on("change", function () {
    var wifeOrder = $("#wifeOrder").val();
    $("#family_order_id").val(idFamily + type + wifeOrder).trigger("focusout");;
});