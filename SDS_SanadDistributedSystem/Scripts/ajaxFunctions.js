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


//PEOPLE SCRIPTS



function SuccessLoadForPerson(data) {

    $("input[type='text'], input[type='datetime']").val("");
    $("input[type='text'], input[type='datetime']").text("");
    $("input[type='number']").val(1);
    $("input[type='number']").text(1);

    $("#idperson").val(data.idfamily);
    $("#idfamily_FK").val(data.idfamily);

    toastr.success("نجاح", 'نجاح العملية');

    $("#familymembers").append(
        "<tr><td>" + data.idperson + "</td><td>" +
        data.fname + "</td><td>" + data.lname + "</td><td>" +
        data.fathername + "</td><td>" + data.mothername + "</td><td>" +
        data.age + "</td><td>" + data.gender + "</td><td>" +

        '<a href="/people/Edit/' +
         data.idperson + '"> تعديل </a>|<a href="/people/Details/' +
        data.idperson + '"> تفاصيل </a>' +
        //'|<a href="/people/Delete/' + data.idfamily + '"> حذف </a></td>' +
        '|<a href="/referalpersons/personReferal/' + data.idperson + '"> إحالة </a></td>' +

        '</tr>');
}

function FailureLoadForPerson(data) {
    //alert("Adding Person with ID: " + data + " Failed!!");
    toastr.error("فشل", 'فشل العملية');
}

var idFamily;
var type;

$(document).ready(function () {
    //$('#birthday').datepicker({
    //        dateFormat: "dd/mm/yy",
    //        showStatus: true,
    //        showWeeks: true,
    //        currentText: 'Now',
    //        autoSize: true,
    //        gotoCurrent: true,
    //        showAnim: 'blind',
    //        highlightWeek: true
    //    });
    $.validator.unobtrusive.parse("#ParentDiv > form");
    idFamily = $("#idperson").val();
    $('#idfamily_FK').val(idFamily);
    $("#ذكر").prop("checked", true).trigger("click");
    $("#idperson").val(idFamily + "H").trigger("focusout");

    //$(function () {

    //    $('#birthday').datepicker({
    //        dateFormat: 'dd/mm/yy'
    //    });

    //});
});

//$(function () {
//    //var addresses = [
//    //  "ActionScript",
//    //  "AppleScript",
//    //  "Asp",
//    //  "BASIC",
//    //  "C",
//    //  "C++",
//    //  "Clojure",
//    //  "COBOL",
//    //  "ColdFusion",
//    //  "Erlang",
//    //  "Fortran",
//    //  "Groovy",
//    //  "Haskell",
//    //  "Java",
//    //  "JavaScript",
//    //  "Lisp",
//    //  "Perl",
//    //  "PHP",
//    //  "Python",
//    //  "Ruby",
//    //  "Scala",
//    //  "Scheme"
//    //];
//    //$("#currentaddress").autocomplete({
//    //    source: addresses
//    //});
//});


// generate person's id based on his/her familyid and his position in the family

$("input[type=radio][name=position]").on("change", function () {

    type = $(this).val();

    switch ($(this).prop('id')) {
        case "D":
            $("#childOrder").removeClass("hidden");
            $("#wifeOrder").addClass("hidden");
            $("#أنثى").prop("checked", true).trigger("click");
            $("#idperson").val(idFamily + type + $("#childOrder").val()).trigger("focusout");
            break;
        case "S":
            $("#childOrder").removeClass("hidden");
            $("#wifeOrder").addClass("hidden");
            $("#ذكر").prop("checked", true).trigger("click");
            $("#idperson").val(idFamily + type + $("#childOrder").val()).trigger("focusout");
            break;
        case "W":
            $("#wifeOrder").removeClass("hidden");
            $("#childOrder").addClass("hidden");
            $("#أنثى").prop("checked", true).trigger("click");
            $("#idperson").val(idFamily + type + $("#wifeOrder").val()).trigger("focusout");
            break;
        case "H":
            $("#wifeOrder").addClass("hidden");
            $("#childOrder").addClass("hidden");
            $("#ذكر").prop("checked", true).trigger("click");
            $("#idperson").val(idFamily + type).trigger("focusout");
            break;
        default:
            $("#wifeOrder").addClass("hidden");
            $("#childOrder").addClass("hidden");
    }


});

$("#childOrder").on("change", function () {

    var childOrder = $("#childOrder").val();
    $("#idperson").val(idFamily + type + childOrder).trigger("focusout");;

});

$("#wifeOrder").on("change", function () {
    var wifeOrder = $("#wifeOrder").val();
    $("#idperson").val(idFamily + type + wifeOrder).trigger("focusout");;
});
