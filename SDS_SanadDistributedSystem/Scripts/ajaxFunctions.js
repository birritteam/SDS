function SuccessLoadForPerson(data) {

    $("#personCreationForm").children($("input[type='text'], input[type='datetime'], input[type='number']").val(""));
    $("#personCreationForm").children($("input[type='text'], input[type='datetime'] , input[type='number']").text(""));
    $("#idperson").val(data.idperson);
    $("#idfamily_FK").val(data.idfamily);

    alert("Person: " + data + " Added Successfully!!");

    toastr.success("نجاح", 'نجاح العملية');
    
    $("#familymembers").append("<tr><td>" + data.fname + "</td></tr>");

}

function FailureLoadForPerson(data) {
    alert("Adding Person with ID: " + data + " Failed!!");
    toastr.error("نجاح", 'نجاح العملية');
}