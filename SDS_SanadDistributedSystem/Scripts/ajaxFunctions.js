function SuccessLoadForPerson(data) {

    $("#personCreationForm").children($("input[type='text'], input[type='datetime'], input[type='number']").val(""));
    $("#personCreationForm").children($("input[type='text'], input[type='datetime'] , input[type='number']").text(""));
    $("#idperson").val(data.idfamily);
    $("#idfamily_FK").val(data.idfamily);

    //alert("Person: " + data + " Added Successfully!!");

    toastr.success("نجاح", 'نجاح العملية');

    $("#familymembers").append(
        "<tr><td>" + data.idperson + "</td><td>" +
        data.fname + "</td><td>" + data.lname + "</td><td>" +
        data.fathername + "</td><td>" + data.mothername + "</td><td>" +
        data.age + "</td><td>" + data.gender + "</td><td>" +

        '<a href="/people/Edit/'+
         data.idfamily + '"> تعديل </a>|<a href="/people/Details/'+
        data.idfamily + '"> تفاصيل </a>|<a href="/people/Delete/'+
        data.idfamily + '"> حذف </a></td></tr>');
}

function FailureLoadForPerson(data) {
    //alert("Adding Person with ID: " + data + " Failed!!");
    toastr.error("فشل", 'فشل العملية');
}

var idFamily;
var type;

$(document).ready(function () {
    idFamily = $("#idperson").val();
    $('#idfamily_FK').val(idFamily);
});

$(function () {
    var addresses = [
      "ActionScript",
      "AppleScript",
      "Asp",
      "BASIC",
      "C",
      "C++",
      "Clojure",
      "COBOL",
      "ColdFusion",
      "Erlang",
      "Fortran",
      "Groovy",
      "Haskell",
      "Java",
      "JavaScript",
      "Lisp",
      "Perl",
      "PHP",
      "Python",
      "Ruby",
      "Scala",
      "Scheme"
    ];
    $("#currentaddress").autocomplete({
        source: addresses
    });
});


$("input[type=radio][name=position]").on("change", function () {

    type = $(this).val();

    switch ($(this).prop('id')) {
        case "D":
            $("#childOrder").removeClass("hidden");
            $("#wifeOrder").addClass("hidden");
            $("#أنثى").prop("checked", true).trigger("click");
            break;
        case "S":
            $("#childOrder").removeClass("hidden");
            $("#wifeOrder").addClass("hidden");
            $("#ذكر").prop("checked", true).trigger("click");
            break;
        case "W":
            $("#wifeOrder").removeClass("hidden");
            $("#childOrder").addClass("hidden");
            $("#أنثى").prop("checked", true).trigger("click");
            break;
        case "H":
            $("#wifeOrder").addClass("hidden");
            $("#childOrder").addClass("hidden");
            $("#ذكر").prop("checked", true).trigger("click");
            break;
        default:
            $("#wifeOrder").addClass("hidden");
            $("#childOrder").addClass("hidden");
    }

    $("#idperson").val(idFamily + type);
});

$("#childOrder").on("change", function () {

    var childOrder = $("#childOrder").val();
    $("#idperson").val(idFamily + type + childOrder);

});

$("#wifeOrder").on("change", function () {
    var wifeOrder = $("#wifeOrder").val();
    $("#idperson").val(idFamily + type + wifeOrder);
});
