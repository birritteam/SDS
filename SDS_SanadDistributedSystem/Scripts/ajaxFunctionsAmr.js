function FillServices() {
    try {
        var caseId = $('#idcase_FK').val();
        var centerId = $('#centers').val();
        if (caseId != null) {        //toastr.error("sssssssssssss" + caseId);{
            $.ajax({
                url: '/sds/referalpersons/FillServices',
                type: "GET",
                dataType: "JSON",
                data: { 'caseId': caseId, 'centerId': centerId },
                success: function (data) {
                    $('#services').empty()
                    $.each(data, function (i, service) {
                        $("#services").append(
                        $('<option selected="selected"></option>').val(service.idservice).html(service.name));
                        $("#referalReciver_FK").empty();
                        $.each(service.recivers, function (i, reciver) {
                            $("#referalReciver_FK").append(
                            $('<option></option>').val(reciver.Id).html(reciver.UserName));
                        });
                    });

                    $('#services').selectpicker('refresh');
                    $('#referalReciver_FK').selectpicker('refresh');
                },
                error: function (xhr, status, error) {
                    // check status && error
                    toastr.error("Whooaaa! Something went wrong.. FillServices")
                }
            });
        }
    }
    catch (err) {

    }

}

function FillRecivers() {

    var serviceId = $('#services').val();
    // alert("serviceId =" + serviceId);
    $.ajax({
        url: '/sds/referalpersons/FillRecivers',
        type: "GET",
        dataType: "JSON",
        data: { 'serviceId': serviceId },
        success: function (recivers) {

            $("#referalReciver_FK").empty();
            $.each(recivers, function (i, reciver) {
                $("#referalReciver_FK").append(
                $('<option></option>').val(reciver.Id).html(reciver.UserName));
            });
            $('#referalReciver_FK').selectpicker('refresh');

        },
        error: function (xhr, status, error) {
            // check status && error
            toastr.error("Whooaaa! Something went wrong..FillRecivers")
        }
    });



}

//function FillServicesByCenter() {

//    try {
//        var caseId = $('#idcase_FK').val();
//        var centerId = $('#centers').val();
//        if (caseId != null) {        //toastr.error("sssssssssssss" + caseId);{
//            $.ajax({
//                url: ' /sds/referalpersons/FillServicesByCenter',
//                type: "GET",
//                dataType: "JSON",
//                data: { 'caseId': caseId, 'centerId': centerId },
//                success: function (data) {
//                    $('#services').empty()
//                    $.each(data, function (i, service) {
//                        $("#services").append(
//                        $('<option selected="selected"></option>').val(service.idservice).html(service.name));
//                        $("#referalReciver_FK").empty();
//                        $.each(service.recivers, function (i, reciver) {
//                            $("#referalReciver_FK").append(
//                            $('<option></option>').val(reciver.Id).html(reciver.UserName));
//                        });
//                    });

//                    $('#services').selectpicker('refresh');
//                    $('#referalReciver_FK').selectpicker('refresh');
//                },
//                error: function (xhr, status, error) {
//                    // check status && error
//                    toastr.error("Whooaaa! Something went wrong.. FillServices")
//                }
//            });
//        }
//    }
//    catch (err) {

//    }


//}

function FillRecivers_EDIT() {

    var serviceId = $('#idservice_FK').val();
    //alert("serviceId =" + serviceId);
    $.ajax({
        url: ' /sds/referalpersons/FillRecivers',
        type: "GET",
        dataType: "JSON",
        data: { 'serviceId': serviceId },
        success: function (recivers) {

            $("#referalReciver_FK").empty();
            $.each(recivers, function (i, reciver) {
                $("#referalReciver_FK").append(
                $('<option></option>').val(reciver.Id).html(reciver.UserName));
            });
            $('#referalReciver_FK').selectpicker('refresh');

        },
        error: function (xhr, status, error) {
            // check status && error
            toastr.error("Whooaaa! Something went wrong..FillRecivers_EDIT")
        }
    });



}

function FillServicesNew() {

    // var idcase_FK_text = document.getElementById("idcase_FK").options[document.getElementById("idcase_FK").selectedIndex].text;
    var selectedId = $('#idcase_FK').val();
    if (selectedId == 1) {
        $("button[data-id='cmEducation']").show();
        $("button[data-id='cmProfessional']").hide();
        $("button[data-id='cmChildProtection']").hide();
        $("button[data-id='cmPsychologicalSupport1']").hide();
        $("button[data-id='cmPsychologicalSupport2']").hide();
        $("button[data-id='cmPsychologicalSupport3']").hide();
        $("button[data-id='cmDayCare']").hide();
        $("button[data-id='cmHomeCare']").hide();
        $("button[data-id='cmSGBV']").hide();
        $("button[data-id='cmSmallProjects']").hide();
        $("button[data-id='cmIOutReachTeam']").hide();
        $("button[data-id='cmInkindAssistance']").hide();

        $("#cmEducation").show(); $("#cmProfessional").hide();
        $("#cmChildProtection").hide(); $("#cmPsychologicalSupport1").hide();
        $("#cmPsychologicalSupport2").hide(); $("#cmPsychologicalSupport3").hide();
        $("#cmDayCare").hide(); $("#cmHomeCare").hide();
        $("#cmSGBV").hide(); $("#cmSmallProjects").hide();
        $("#cmIOutReachTeam").hide(); $("#cmInkindAssistance").hide();
    }
    else if (selectedId == 2) {
        $("button[data-id='cmEducation']").hide();
        $("button[data-id='cmProfessional']").show();
        $("button[data-id='cmChildProtection']").hide();
        $("button[data-id='cmPsychologicalSupport1']").hide();
        $("button[data-id='cmPsychologicalSupport2']").hide();
        $("button[data-id='cmPsychologicalSupport3']").hide();
        $("button[data-id='cmDayCare']").hide();
        $("button[data-id='cmHomeCare']").hide();
        $("button[data-id='cmSGBV']").hide();
        $("button[data-id='cmSmallProjects']").hide();
        $("button[data-id='cmIOutReachTeam']").hide();
        $("button[data-id='cmInkindAssistance']").hide();

        $("#cmEducation").hide(); $("#cmProfessional").show();
        $("#cmChildProtection").hide(); $("#cmPsychologicalSupport1").hide();
        $("#cmPsychologicalSupport2").hide(); $("#cmPsychologicalSupport3").hide();
        $("#cmDayCare").hide(); $("#cmHomeCare").hide();
        $("#cmSGBV").hide(); $("#cmSmallProjects").hide();
        $("#cmIOutReachTeam").hide(); $("#cmInkindAssistance").hide();
    }
    else if (selectedId == 3) {
        $("button[data-id='cmEducation']").hide();
        $("button[data-id='cmProfessional']").hide();
        $("button[data-id='cmChildProtection']").show();
        $("button[data-id='cmPsychologicalSupport1']").hide();
        $("button[data-id='cmPsychologicalSupport2']").hide();
        $("button[data-id='cmPsychologicalSupport3']").hide();
        $("button[data-id='cmDayCare']").hide();
        $("button[data-id='cmHomeCare']").hide();
        $("button[data-id='cmSGBV']").hide();
        $("button[data-id='cmSmallProjects']").hide();
        $("button[data-id='cmIOutReachTeam']").hide();
        $("button[data-id='cmInkindAssistance']").hide();

        $("#cmEducation").hide(); $("#cmProfessional").hide();
        $("#cmChildProtection").show(); $("#cmPsychologicalSupport1").hide();
        $("#cmPsychologicalSupport2").hide(); $("#cmPsychologicalSupport3").hide();
        $("#cmDayCare").hide(); $("#cmHomeCare").hide();
        $("#cmSGBV").hide(); $("#cmSmallProjects").hide();
        $("#cmIOutReachTeam").hide(); $("#cmInkindAssistance").hide();
    }
    else if (selectedId == 4) {
        $("button[data-id='cmEducation']").hide();
        $("button[data-id='cmProfessional']").hide();
        $("button[data-id='cmChildProtection']").hide();
        $("button[data-id='cmPsychologicalSupport1']").show();
        $("button[data-id='cmPsychologicalSupport2']").hide();
        $("button[data-id='cmPsychologicalSupport3']").hide();
        $("button[data-id='cmDayCare']").hide();
        $("button[data-id='cmHomeCare']").hide();
        $("button[data-id='cmSGBV']").hide();
        $("button[data-id='cmSmallProjects']").hide();
        $("button[data-id='cmIOutReachTeam']").hide();
        $("button[data-id='cmInkindAssistance']").hide();

        $("#cmEducation").hide(); $("#cmProfessional").hide();
        $("#cmChildProtection").hide(); $("#cmPsychologicalSupport1").show();
        $("#cmPsychologicalSupport2").hide(); $("#cmPsychologicalSupport3").hide();
        $("#cmDayCare").hide(); $("#cmHomeCare").hide();
        $("#cmSGBV").hide(); $("#cmSmallProjects").hide();
        $("#cmIOutReachTeam").hide(); $("#cmInkindAssistance").hide();
    }
    else if (selectedId == 5) {
        $("button[data-id='cmEducation']").hide();
        $("button[data-id='cmProfessional']").hide();
        $("button[data-id='cmChildProtection']").hide();
        $("button[data-id='cmPsychologicalSupport1']").hide();
        $("button[data-id='cmPsychologicalSupport2']").show();
        $("button[data-id='cmPsychologicalSupport3']").hide();
        $("button[data-id='cmDayCare']").hide();
        $("button[data-id='cmHomeCare']").hide();
        $("button[data-id='cmSGBV']").hide();
        $("button[data-id='cmSmallProjects']").hide();
        $("button[data-id='cmIOutReachTeam']").hide();
        $("button[data-id='cmInkindAssistance']").hide();

        $("#cmEducation").hide(); $("#cmProfessional").hide();
        $("#cmChildProtection").hide(); $("#cmPsychologicalSupport1").hide();
        $("#cmPsychologicalSupport2").show(); $("#cmPsychologicalSupport3").hide();
        $("#cmDayCare").hide(); $("#cmHomeCare").hide();
        $("#cmSGBV").hide(); $("#cmSmallProjects").hide();
        $("#cmIOutReachTeam").hide(); $("#cmInkindAssistance").hide();
    }
    else if (selectedId == 6) {
        $("button[data-id='cmEducation']").hide();
        $("button[data-id='cmProfessional']").hide();
        $("button[data-id='cmChildProtection']").hide();
        $("button[data-id='cmPsychologicalSupport1']").hide();
        $("button[data-id='cmPsychologicalSupport2']").hide();
        $("button[data-id='cmPsychologicalSupport3']").show();
        $("button[data-id='cmDayCare']").hide();
        $("button[data-id='cmHomeCare']").hide();
        $("button[data-id='cmSGBV']").hide();
        $("button[data-id='cmSmallProjects']").hide();
        $("button[data-id='cmIOutReachTeam']").hide();
        $("button[data-id='cmInkindAssistance']").hide();

        $("#cmEducation").hide(); $("#cmProfessional").hide();
        $("#cmChildProtection").hide(); $("#cmPsychologicalSupport1").hide();
        $("#cmPsychologicalSupport2").hide(); $("#cmPsychologicalSupport3").show();
        $("#cmDayCare").hide(); $("#cmHomeCare").hide();
        $("#cmSGBV").hide(); $("#cmSmallProjects").hide();
        $("#cmIOutReachTeam").hide(); $("#cmInkindAssistance").hide();
    }
    else if (selectedId == 7) {
        $("button[data-id='cmEducation']").hide();
        $("button[data-id='cmProfessional']").hide();
        $("button[data-id='cmChildProtection']").hide();
        $("button[data-id='cmPsychologicalSupport1']").hide();
        $("button[data-id='cmPsychologicalSupport2']").hide();
        $("button[data-id='cmPsychologicalSupport3']").hide();
        $("button[data-id='cmDayCare']").show();
        $("button[data-id='cmHomeCare']").hide();
        $("button[data-id='cmSGBV']").hide();
        $("button[data-id='cmSmallProjects']").hide();
        $("button[data-id='cmIOutReachTeam']").hide();
        $("button[data-id='cmInkindAssistance']").hide();

        $("#cmEducation").hide(); $("#cmProfessional").hide();
        $("#cmChildProtection").hide(); $("#cmPsychologicalSupport1").hide();
        $("#cmPsychologicalSupport2").hide(); $("#cmPsychologicalSupport3").hide();
        $("#cmDayCare").show(); $("#cmHomeCare").hide();
        $("#cmSGBV").hide(); $("#cmSmallProjects").hide();
        $("#cmIOutReachTeam").hide(); $("#cmInkindAssistance").hide();
    }
    else if (selectedId == 8) {
        $("button[data-id='cmEducation']").hide();
        $("button[data-id='cmProfessional']").hide();
        $("button[data-id='cmChildProtection']").hide();
        $("button[data-id='cmPsychologicalSupport1']").hide();
        $("button[data-id='cmPsychologicalSupport2']").hide();
        $("button[data-id='cmPsychologicalSupport3']").hide();
        $("button[data-id='cmDayCare']").hide();
        $("button[data-id='cmHomeCare']").show();
        $("button[data-id='cmSGBV']").hide();
        $("button[data-id='cmSmallProjects']").hide();
        $("button[data-id='cmIOutReachTeam']").hide();
        $("button[data-id='cmInkindAssistance']").hide();

        $("#cmEducation").hide(); $("#cmProfessional").hide();
        $("#cmChildProtection").hide(); $("#cmPsychologicalSupport1").hide();
        $("#cmPsychologicalSupport2").hide(); $("#cmPsychologicalSupport3").hide();
        $("#cmDayCare").hide(); $("#cmHomeCare").show();
        $("#cmSGBV").hide(); $("#cmSmallProjects").hide();
        $("#cmIOutReachTeam").hide(); $("#cmInkindAssistance").hide();
    }
    else if (selectedId == 9) {
        $("button[data-id='cmEducation']").hide();
        $("button[data-id='cmProfessional']").hide();
        $("button[data-id='cmChildProtection']").hide();
        $("button[data-id='cmPsychologicalSupport1']").hide();
        $("button[data-id='cmPsychologicalSupport2']").hide();
        $("button[data-id='cmPsychologicalSupport3']").hide();
        $("button[data-id='cmDayCare']").hide();
        $("button[data-id='cmHomeCare']").hide();
        $("button[data-id='cmSGBV']").show();
        $("button[data-id='cmSmallProjects']").hide();
        $("button[data-id='cmIOutReachTeam']").hide();
        $("button[data-id='cmInkindAssistance']").hide();

        $("#cmEducation").hide(); $("#cmProfessional").hide();
        $("#cmChildProtection").hide(); $("#cmPsychologicalSupport1").hide();
        $("#cmPsychologicalSupport2").hide(); $("#cmPsychologicalSupport3").hide();
        $("#cmDayCare").hide(); $("#cmHomeCare").hide();
        $("#cmSGBV").show(); $("#cmSmallProjects").hide();
        $("#cmIOutReachTeam").hide(); $("#cmInkindAssistance").hide();
    }
    else if (selectedId == 10) {
        $("button[data-id='cmEducation']").hide();
        $("button[data-id='cmProfessional']").hide();
        $("button[data-id='cmChildProtection']").hide();
        $("button[data-id='cmPsychologicalSupport1']").hide();
        $("button[data-id='cmPsychologicalSupport2']").hide();
        $("button[data-id='cmPsychologicalSupport3']").hide();
        $("button[data-id='cmDayCare']").hide();
        $("button[data-id='cmHomeCare']").hide();
        $("button[data-id='cmSGBV']").hide();
        $("button[data-id='cmSmallProjects']").show();
        $("button[data-id='cmIOutReachTeam']").hide();
        $("button[data-id='cmInkindAssistance']").hide();

        $("#cmEducation").hide(); $("#cmProfessional").hide();
        $("#cmChildProtection").hide(); $("#cmPsychologicalSupport1").hide();
        $("#cmPsychologicalSupport2").hide(); $("#cmPsychologicalSupport3").hide();
        $("#cmDayCare").hide(); $("#cmHomeCare").hide();
        $("#cmSGBV").hide(); $("#cmSmallProjects").show();
        $("#cmIOutReachTeam").hide(); $("#cmInkindAssistance").hide();
    }
    else if (selectedId == 11) {
        $("button[data-id='cmEducation']").hide();
        $("button[data-id='cmProfessional']").hide();
        $("button[data-id='cmChildProtection']").hide();
        $("button[data-id='cmPsychologicalSupport1']").hide();
        $("button[data-id='cmPsychologicalSupport2']").hide();
        $("button[data-id='cmPsychologicalSupport3']").hide();
        $("button[data-id='cmDayCare']").hide();
        $("button[data-id='cmHomeCare']").hide();
        $("button[data-id='cmSGBV']").hide();
        $("button[data-id='cmSmallProjects']").hide();
        $("button[data-id='cmIOutReachTeam']").show();
        $("button[data-id='cmInkindAssistance']").hide();

        $("#cmEducation").hide(); $("#cmProfessional").hide();
        $("#cmChildProtection").hide(); $("#cmPsychologicalSupport1").hide();
        $("#cmPsychologicalSupport2").hide(); $("#cmPsychologicalSupport3").hide();
        $("#cmDayCare").hide(); $("#cmHomeCare").hide();
        $("#cmSGBV").hide(); $("#cmSmallProjects").hide();
        $("#cmIOutReachTeam").show(); $("#cmInkindAssistance").hide();
    }
    else if (selectedId == 12) {
        $("button[data-id='cmEducation']").hide();
        $("button[data-id='cmProfessional']").hide();
        $("button[data-id='cmChildProtection']").hide();
        $("button[data-id='cmPsychologicalSupport1']").hide();
        $("button[data-id='cmPsychologicalSupport2']").hide();
        $("button[data-id='cmPsychologicalSupport3']").hide();
        $("button[data-id='cmDayCare']").hide();
        $("button[data-id='cmHomeCare']").hide();
        $("button[data-id='cmSGBV']").hide();
        $("button[data-id='cmSmallProjects']").hide();
        $("button[data-id='cmIOutReachTeam']").hide();
        $("button[data-id='cmInkindAssistance']").show();

        $("#cmEducation").hide(); $("#cmProfessional").hide();
        $("#cmChildProtection").hide(); $("#cmPsychologicalSupport1").hide();
        $("#cmPsychologicalSupport2").hide(); $("#cmPsychologicalSupport3").hide();
        $("#cmDayCare").hide(); $("#cmHomeCare").hide();
        $("#cmSGBV").hide(); $("#cmSmallProjects").hide();
        $("#cmIOutReachTeam").hide(); $("#cmInkindAssistance").show();
    }

}

function BindItemTable() {
    var myTable = $("#myTable").DataTable({
        "deferRender": true,
        "paging": true,
        "lengthChange": false,
        "searching": false,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "sDom": 'lfrtip'
    });
}
function deleterow(id) {
    //  alert(id);
    $.ajax({
        url: ' /sds/referalpersons/deleteRow',
        type: "GET",
        dataType: "JSON",
        data: { 'selectedId': id },
        success:
function (data) {
    //  alert("successFillTable.............")
    $("#myTable tbody").empty();
    $.each(data, function (i, referalperson) {
        $("#myTable").append(
        $('<tr><td>' + (i + 1) + '</td><td>' + referalperson.idcase_FK +
            '</td><td>' + referalperson.idservice_FK + '</td><td>' +
            referalperson.senderevalution + '</td><td> <input type="submit" id=' + (i + 1) + ' value="حذف" class="btn btn-default" onclick="deleterow(this.id)" /></td></tr>'));
    });
},

        error: function (xhr, status, error) {
            // check status && error
            toastr.error("حدث خظأ.تأكد من الاتصال.....")
        }
    });
}


function successFillTable(data) {
    //alert("successFillTable.............")
    $("#myTable tbody").empty();
    $.each(data, function (i, referalperson) {
        $("#myTable").append(
        $('<tr><td>' + (i + 1) + '</td><td>' + referalperson.idcase_FK +
            '</td><td>' + referalperson.idservice_FK + '</td><td>' +
            referalperson.senderevalution + '</td><td> <input type="submit" id=' + (i + 1) + ' value="حذف" class="btn btn-default" onclick="deleterow(this.id)" /></td></tr>'));
    });
}

var personReferal = [];
var personReferal_text = [];

function successFillTableNew() {
    //alert("successFillTableNew.............")
    //$("#SchedulingProfile_Id").val();
    // var idperson_FK = $("#idperson_FK").val();

    referalReciver_FK

    var idperson_FK = document.getElementById("idperson_FK").value;

    var idperson_FK_text = idperson_FK;
    //var idperson_FK_text = document.getElementById("idperson_FK").options[document.getElementById("idperson_FK").selectedIndex].text;

    var idcase_FK = document.getElementById("idcase_FK").value;
    var idcase_FK_text = document.getElementById("idcase_FK").options[document.getElementById("idcase_FK").selectedIndex].text;

    var idservice_FK = document.getElementById("services").value;
    var idservice_FK_text = document.getElementById("services").options[document.getElementById("services").selectedIndex].text;

    //var selectedId = $('#idcase_FK').val();
    //if (selectedId == 1) {
    //    var idservice_FK = document.getElementById("cmEducation").value;
    //    var idservice_FK_text = document.getElementById("cmEducation").options[document.getElementById("cmEducation").selectedIndex].text;
    //}
    //else if (selectedId == 2) {
    //    var idservice_FK = document.getElementById("cmProfessional").value;
    //    var idservice_FK_text = document.getElementById("cmProfessional").options[document.getElementById("cmProfessional").selectedIndex].text;
    //}
    //else if (selectedId == 3) {
    //    var idservice_FK = document.getElementById("cmChildProtection").value;
    //    var idservice_FK_text = document.getElementById("cmChildProtection").options[document.getElementById("cmChildProtection").selectedIndex].text;
    //}
    //else if (selectedId == 4) {
    //    var idservice_FK = document.getElementById("cmPsychologicalSupport1").value;
    //    var idservice_FK_text = document.getElementById("cmPsychologicalSupport1").options[document.getElementById("cmPsychologicalSupport1").selectedIndex].text;

    //}
    //else if (selectedId == 5) {
    //    var idservice_FK = document.getElementById("cmPsychologicalSupport2").value;
    //    var idservice_FK_text = document.getElementById("cmPsychologicalSupport2").options[document.getElementById("cmPsychologicalSupport2").selectedIndex].text;
    //}
    //else if (selectedId == 6) {
    //    var idservice_FK = document.getElementById("cmPsychologicalSupport3").value;
    //    var idservice_FK_text = document.getElementById("cmPsychologicalSupport3").options[document.getElementById("cmPsychologicalSupport3").selectedIndex].text;


    //}
    //else if (selectedId == 7) {
    //    var idservice_FK = document.getElementById("cmDayCare").value;
    //    var idservice_FK_text = document.getElementById("cmDayCare").options[document.getElementById("cmDayCare").selectedIndex].text;
    //}
    //else if (selectedId == 8) {
    //    var idservice_FK = document.getElementById("cmHomeCare").value;
    //    var idservice_FK_text = document.getElementById("cmHomeCare").options[document.getElementById("cmHomeCare").selectedIndex].text;


    //}
    //else if (selectedId == 9) {
    //    var idservice_FK = document.getElementById("cmSGBV").value;
    //    var idservice_FK_text = document.getElementById("cmSGBV").options[document.getElementById("cmSGBV").selectedIndex].text;
    //}
    //else if (selectedId == 10) {
    //    var idservice_FK = document.getElementById("cmSmallProjects").value;
    //    var idservice_FK_text = document.getElementById("cmSmallProjects").options[document.getElementById("cmSmallProjects").selectedIndex].text;
    //}
    //else if (selectedId == 11) {
    //    var idservice_FK = document.getElementById("cmIOutReachTeam").value;
    //    var idservice_FK_text = document.getElementById("cmIOutReachTeam").options[document.getElementById("cmIOutReachTeam").selectedIndex].text;
    //}
    //else if (selectedId == 12) {
    //    var idservice_FK = document.getElementById("cmInkindAssistance").value;
    //    var idservice_FK_text = document.getElementById("cmInkindAssistance").options[document.getElementById("cmInkindAssistance").selectedIndex].text;
    //}
    //----------------------------------------
    //var submittingdate = document.getElementById("submittingdate").value;

    //var referalstate = document.getElementById("referalstate").value;
    //var referalstate_text = document.getElementById("referalstate").options[document.getElementById("referalstate").selectedIndex].text;

    var referaldate;

    //var servicestate = document.getElementById("servicestate").value;
    //var servicestate_text = document.getElementById("servicestate").options[document.getElementById("servicestate").selectedIndex].text;

    var servicestartdate;

    var serviceenddate;

    //var referalsender_FK = document.getElementById("referalsender_FK").value;
    //var referalsender_FK_text = document.getElementById("referalsender_FK").options[document.getElementById("referalsender_FK").selectedIndex].text;

    var referalReciver_FK = document.getElementById("referalReciver_FK").value;
    var referalReciver_FK_text = document.getElementById("referalReciver_FK").options[document.getElementById("referalReciver_FK").selectedIndex].text;

    var senderevalution = document.getElementById("senderevalution").value;

    var recieverevalution;

    //var idcenter_FK = document.getElementById("idcenr_FK").value;
    //var idcenter_FK_text = document.getElementById("idcenter_FK").options[document.getElementById("idcenter_FK").selectedIndex].text;

    var outreachnote = document.getElementById("outreachnote").value;
    //<<<<<انتبه ارسل الايميل
    var obj = {
        "idperson_FK": idperson_FK,
        "idcase_FK": idcase_FK, "idservice_FK": idservice_FK,

        "senderevalution": senderevalution + "", "outreachnote": outreachnote + "", "referalreciever_FK": referalReciver_FK + ""
    };

    var obj_text = {
        "idperson_FK": idperson_FK_text,
        "idcase_FK": idcase_FK_text, "idservice_FK": idservice_FK_text,

        "senderevalution": senderevalution, "outreachnote": outreachnote, "referalreciever_FK": referalReciver_FK_text
    };

    var checkexist = false;
    var checkundefined = false;


    personReferal_text.forEach(function (item, i, array) {
        if (item.idcase_FK == idcase_FK_text && item.idservice_FK == idservice_FK_text)
            checkexist = true;
        if (item.idcase_FK == idcase_FK_text && item.idservice_FK == "غير محدد" && idservice_FK_text != "غير محدد")
            checkundefined = true;
        if (item.idcase_FK == idcase_FK_text && item.idservice_FK != "غير محدد" && idservice_FK_text == "غير محدد")
            checkundefined = true;

    });
    if (idservice_FK_text != "لايوجد خدمات مفّعلة") {

        if (!checkexist && !checkundefined) {
            personReferal.push(obj);
            personReferal_text.push(obj_text);
            // alert("successFillTableNew............." + obj.idcase_FK)
            $("#myTable tbody").empty();
            personReferal_text.forEach(function (item, i, array) {
                //console.log(item, i);
                $("#myTable").append(
                $('<tr><td>' + (i + 1) + '</td><td>' + item.idcase_FK +
                    '</td><td>' + item.idservice_FK + '</td><td>' +
                    item.senderevalution + '</td><td>' +
                    item.referalreciever_FK + '</td><td> <input type="submit" id=' + (i + 1) + ' value="حذف" class="btn btn-danger btn-rounded p-1" onclick="deleterowNew(this.id)"/></td></tr>'));
            });
            // BindItemTable();
            toastr.success('تم إضافة إحالة للجدول');
            // alert("successFillTableNew........end .")
        }
        else {
            if (checkexist)
                toastr.warning('تمت إضافة هذه الخدمة مسبقا للجدول.');
                //alert("already exist.....")

            else
                toastr.warning(" لا يمكن إضافة هذه الخدمة")
        }
    }
    else {
        toastr.error(" لا يوجد خدمات مفعلة في هذا القسم")
    }

}

function deleterowNew(index) {
    toastr.info("حذف من الجدول");
    personReferal.splice(index - 1, 1);
    personReferal_text.splice(index - 1, 1);
    $("#myTable tbody").empty();
    $("#myTable tbody").empty();
    personReferal_text.forEach(function (item, i, array) {
        //console.log(item, i);
        $("#myTable").append(
        $('<tr><td>' + (i + 1) + '</td><td>' + item.idcase_FK +
             '</td><td>' + item.idservice_FK + '</td><td>' +
                    item.senderevalution + '</td><td>' +
                    item.referalreciever_FK + '</td><td> <input type="submit" id=' + (i + 1) + ' value="حذف" class="btn btn-default" onclick="deleterowNew(this.id)" /></td></tr>'));
    });
    // BindItemTable();
}




function successSendReferals() {
    //var result = [];
    //personReferal.forEach(function (item, i, array) {
    //var list = JSON.stringify(personReferal);

    var referals = JSON.stringify({ 'referals': personReferal });;
    //    result.push(data);
    //});

    $.ajax({
        // contentType: 'application/json; charset=utf-8',
        contentType: 'application/json',
        url: ' /sds/referalpersons/sendReferals',
        type: "POST",
        dataType: "JSON",
        data: referals,
        success:
function (data) {
    if (data == "Success") {
        toastr.success('تم الحفظ بنجاح');
        $("#myTable tbody").empty();
        personReferal = [];
        personReferal_text = [];
    }
    else
        toastr.error('حدث خطأ أثناء الحفظ');

},

        error: function (xhr, status, error) {
            // check status && error
            toastr.error("فشلة عملية الحفظ ")
        }
    });



}
function OnFailureSendReferals() {
    toastr.error("Failure FailureSendReferals.............")
}


function OnFailureFillTable() {
    toastr.error("Failure FillTable.............")

}

function FillReferalStateDropdown() {

    var ddl = document.getElementById("<%=servicestate.ClientID%>");

    var ddl2 = document.getElementById("<%=referalstate.ClientID%>");



    if (ddl != null) {
        var servicestate_text = ddl.options[ddl.selectedIndex].value;
        var referalstate_text = ddl2.options[ddl2.selectedIndex].value;

        if (servicestate_text == "Pending") {

            $('#referalstate')
        .empty()
        .append('<option selected="selected" value="Pending">Pending</option>')
             .append('<option selected="selected" value="Approved">Approved</option>')
             .append('<option selected="selected" value="Rejected">Rejected</option>')

             .append('<option selected="selected" value="OutReach">OutReach</option>')
             .append('<option selected="selected" value="External">External</option>')
            ;



            $('select#referalstate option')
       .each(function () { this.selected = (this.text == referalstate_text); });

            $('#referalstate').selectpicker('refresh');

            //    $('.selectpicker #referalstate')
            //.find('option')
            //.remove()
            //.end()
            //.append('<option value="Pending">Pending</option>')
            //.val('Pending')
            //    ;
            //  $("#referalstate").append(
            //$('<option></option>').val("Pending").html("Pending"),
            //$('<option></option>').val("Approved").html("Approved"),
            //$('<option></option>').val("Rejected").html("Rejected"),
            //$('<option></option>').val("OutReach").html("OutReach"),
            //$('<option></option>').val("External").html("External"));

        }
            // if (servicestate_text == "In prgress") else if (servicestate_text == "Closed")
        else {

            $('#referalstate')
     .empty()
          .append('<option selected="selected" value="Approved">Approved</option>')
          .append('<option selected="selected" value="OutReach">OutReach</option>')
            ;
            $('select#referalstate option')
        .each(function () { this.selected = (this.text == "Approved"); });

            $('#referalstate').selectpicker('refresh');

        }

    }


}

function searchByName() {
    var name = document.getElementById("personname").value;
    var idrole = document.getElementById("role_id").value;

    if (name != "") {
        $.ajax({
            url: ' /sds/referalpersons/searchReferalByName',
            type: "GET",
            dataType: "JSON",
            data: { 'name': name, 'idrole': idrole },
            success: function (data) {
                $('#All7').DataTable().clear().draw();
                $.each(data, function (i, referal) {
                    $("#All7").DataTable().row.add(
                    $('<tr></tr>')
                    .append('<td>' + referal.name + '</td>')
                    .append('<td>' + referal.submittingdate + '</td>')

                    .append('<td>' + referal.referaldate + '</td>')

                    .append('<td>' + referal.type + '</td>')

                    .append('<td>' + referal.servicestartdate + '</td>')

                    .append('<td>' + referal.serviceenddate + '</td>')

                    .append('<td>' + referal.referalsender_FK + '</td>')

                    .append('<td>' + referal.senderevalution + '</td>')

                    .append('<td>' + referal.recieverevalution + '</td>')

                     .append('<td>' + referal.outreachnote + '</td>')

                                    .append('<td>' +
                                  '<a href=" /sds/referalpersons/Edit?idreferalperson=' + referal.idreferalperson + '&amp;idperson=' + referal.idperson +
                                  '&amp;idcase=' + referal.idcase + '">تعديل</a>' +
                                    '<a length="0" href=" /sds/referalpersons/personReferalByCaseManager/' + referal.idperson + '?idcase=' + referal.idcase
                                    + '">إحالة جديدة</a>' +
                                  '</td>')


                    ).draw().node();

                });

            },
            error: function (xhr, status, error) {
                // check status && error
                toastr.error("فشلة عملية البحث ")
            }
        });



    }


}
function searcByDate() {
    var from = document.getElementById("datepickerfrom").value;
    var to = document.getElementById("datepickerto").value;
    var idrole = document.getElementById("role_id").value;

    if (from != "" && to != "") {
        $.ajax({
            url: ' /sds/referalpersons/searchReferalByDate',
            type: "GET",
            dataType: "JSON",
            data: { 'from': from, 'to': to, 'idrole': idrole },
            success: function (data) {
                $('#All7').DataTable().clear().draw();
                $.each(data, function (i, referal) {
                    $("#All7").DataTable().row.add(
                    $('<tr></tr>')
                    .append('<td>' + referal.name + '</td>')
                    .append('<td>' + referal.submittingdate + '</td>')

                    .append('<td>' + referal.referaldate + '</td>')

                    .append('<td>' + referal.type + '</td>')

                    .append('<td>' + referal.servicestartdate + '</td>')

                    .append('<td>' + referal.serviceenddate + '</td>')

                     .append('<td>' + referal.referalsender_FK + '</td>')

                    .append('<td>' + referal.senderevalution + '</td>')

                    .append('<td>' + referal.recieverevalution + '</td>')

                    .append('<td>' + referal.outreachnote + '</td>')

                                  .append('<td>' +
                                  '<a href=" /sds/referalpersons/Edit?idreferalperson=' + referal.idreferalperson + '&amp;idperson=' + referal.idperson +
                                  '&amp;idcase=' + referal.idcase + '">تعديل</a>' +
                                    '<a length="0" href=" /sds/referalpersons/personReferalByCaseManager/' + referal.idperson + '?idcase=' + referal.idcase
                                    + '">إحالة جديدة</a>' +
                                  '</td>')

                    ).draw().node();

                });

            },
            error: function (xhr, status, error) {
                // check status && error
                toastr.error("فشلة عملية البحث ")
            }
        });



    }


}

function searchByNameCo() {
    var name = document.getElementById("personname").value;
    var idcase = document.getElementById("idcase").value;

    if (name != "") {
        $.ajax({
            url: ' /sds/referalpersons/searchReferalByNameCO',
            type: "GET",
            dataType: "JSON",
            data: { 'name': name, 'idcase': idcase },
            success: function (data) {
                $('#All7').DataTable().clear().draw();
                $.each(data, function (i, referal) {
                    $("#All7").DataTable().row.add(
                    $('<tr></tr>')
                    .append('<td>' + referal.name + '</td>')
                    .append('<td>' + referal.submittingdate + '</td>')

                    .append('<td>' + referal.referaldate + '</td>')

                    .append('<td>' + referal.type + '</td>')

                    .append('<td>' + referal.servicestartdate + '</td>')

                    .append('<td>' + referal.serviceenddate + '</td>')

                    .append('<td>' + referal.referalsender_FK + '</td>')

                    .append('<td>' + referal.senderevalution + '</td>')

                    .append('<td>' + referal.recieverevalution + '</td>')

                    .append('<td>' + referal.outreachnote + '</td>')

                          ).draw().node();

                });

            },
            error: function (xhr, status, error) {
                // check status && error
                toastr.error("فشلة عملية البحث ")
            }
        });



    }


}

function searcByDateCo() {
    var from = document.getElementById("datepickerfrom").value;
    var to = document.getElementById("datepickerto").value;
    var idcase = document.getElementById("idcase").value;

    if (from != "" && to != "") {
        $.ajax({
            url: ' /sds/referalpersons/searchReferalByDateCO',
            type: "GET",
            dataType: "JSON",
            data: { 'from': from, 'to': to, 'idcase': idcase },
            success: function (data) {
                $('#All7').DataTable().clear().draw();
                $.each(data, function (i, referal) {
                    $("#All7").DataTable().row.add(
                    $('<tr></tr>')
                    .append('<td>' + referal.name + '</td>')
                    .append('<td>' + referal.submittingdate + '</td>')

                    .append('<td>' + referal.referaldate + '</td>')

                    .append('<td>' + referal.type + '</td>')

                    .append('<td>' + referal.servicestartdate + '</td>')

                    .append('<td>' + referal.serviceenddate + '</td>')

                    .append('<td>' + referal.referalsender_FK + '</td>')

                     .append('<td>' + referal.senderevalution + '</td>')

                        .append('<td>' + referal.recieverevalution + '</td>')

                        .append('<td>' + referal.outreachnote + '</td>')


                    ).draw().node();

                });

            },
            error: function (xhr, status, error) {
                // check status && error
                toastr.error("فشلة عملية البحث ")
            }
        });



    }


}

function successClose() {

    toastr.success("تمت إغلاق الإحالة");
}

function OnFailureClose() {
    toastr.error("فشلة عملية الإغلاق ")
}

function OnCloseSuccess() {

    toastr.success("تمت إغلاق الإحالة");
}

function OnCloseFailure() {
    toastr.error("فشلة عملية الإغلاق ")
}







function refereshtables() {

    var table = $('#All').DataTable();
    var table2 = $('#All2').DataTable();
    var table3 = $('#All3').DataTable();
    var table4 = $('#All4').DataTable();
    var table5 = $('#All5').DataTable();
    var table6 = $('#All6').DataTable();
    var table7 = $('#All7').DataTable();
    var table8 = $('#All8').DataTable();
    var table9 = $('#All9').DataTable();


    table.draw();
    table2.draw();
    table3.draw();
    table4.draw();
    table5.draw();
    table6.draw();
    table7.draw();
    table8.draw();
    table9.draw();

}

//-------------------------------------------------------------------------------pendding
//function CloseReferalFormIndex() {
$(document).ready(function () {
    //$(".PendingReReferal").on("click", function () {//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    $('body').on('click', '.PendingReReferal', function () {


        //to remvoe current Row in this table
        var tr = $(this).closest('tr');
        var ctable = $(this).closest('table').DataTable();
        var row = ctable.row(tr);
        //var rowNode = row.node();


        //to prevent multiple clicks on button
        //$(this).find('button[type="button"]').attr('disabled', 'disabled');
        //$(this).off('click');
        showPleaseWait();
        //// Get the button
        //var btn = $(this).find('input[type="button"]');

        //// Save and remove the onclick handler
        //var clickHandler = btn[0].onclick;
        //btn[0].onclick = false;

        var idreferalperson = $(this).siblings('.idreferalperson').val();
        var idcase = $(this).siblings('.idcase').val();
        var idperson = $(this).siblings('.idperson').val();
        var table = $('#All').DataTable();//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        $.ajax({

            url: ' /sds/referalpersons/PendingReReferal',//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            type: "Post",
            dataType: "JSON",
            data: { 'idreferalperson': idreferalperson, 'idperson': idperson, 'idcase': idcase },
            success: function (data) {

                $.each(data, function (i, referal) {
                    table.row.add(
                    $('<tr></tr>')
                    .append('<td>' + referal.name + '</td>')
                    .append('<td>' + referal.submittingdate + '</td>')

                    .append('<td>' + referal.referaldate + '</td>')

                    .append('<td>' + referal.servicetype + '</td>')

                    .append('<td>' + referal.servicestartdate + '</td>')

                    .append('<td>' + referal.serviceenddate + '</td>')

                    .append('<td>' + referal.referalsender_FK + '</td>')

                    .append('<td>' + referal.senderevalution + '</td>')

                        .append('<td>' + referal.recieverevalution + '</td>')

                        .append('<td>' + referal.outreachnote + '</td>')

                                   .append('<td>' +
                                   '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">' +
                                   '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">' +
                                   '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">' +
                                   '<button type="button" class="fa fa-edit btn gre btn-success btn-rounded p-1 btn-details waves-effect waves-light" title="تعديل"></button>' +
                                   '<a class=" fa fa-info-circle btn ora btn-warning btn-rounded p-1 waves-effect waves-light" href=" /people/Details/' + referal.idperson + '" title=" تفاصيل"> </a>' +
                                   '<a class=" fa fa-plus-square btn pur btn-purple btn-rounded p-1 waves-effect waves-light" href=" /sds/referalpersons/personReferalByCaseManager/' + referal.idperson + '?idcase=' + referal.idcase + '" title=" إحالة جديدة"> </a>' +
                                     '</td>')


                                      //Pending 1
                                      .append('<td>' +
                                      '<ul class="collapsible collapsible-accordion">'
                                           + '<li>'
                                                + '<a class="collapsible-header waves-effect arrow-r"><i></i>  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; تعديل الاحالة  <i class="fa fa-angle-down rotate-icon"></i></a>'
                                                + '<div class="collapsible-body">'
                                                   + ' <ul>'
                                                       + '<li>'
                                                            + '<div class="form-group">' +




                                       //PendingApproved 2
                                        '<div class="col-md-3">' +
                                      '<form action=" /sds/referalpersons/Index" method="post">'
                                     + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                     + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                     + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                     + '<button type="button" value="" title="قبول" class=" fa fa-check-circle icon-large btn btn-danger  PendingApproved waves-effect waves-light"></button> </form>'
                                       + '</div>' +

                                       //PendingOutReach 3
                                        '<div class="col-md-3">' +
                                      '<form action=" /sds/referalpersons/Index" method="post">'
                                     + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                     + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                     + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                     + '<button type="button" value="" title="قبول-وصول" class=" fa fa-check-circle icon-large btn btn-danger  PendingOutReach waves-effect waves-light"> وصول </button> </form>'
                                       + '</div>' +

                                       //PendingRejected 4
                                       '<div class="col-md-3">' +
                                      '<form action=" /sds/referalpersons/Index" method="post">'
                                     + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                     + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                     + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                     + '<button type="button" value="" title="رفض" class=" fa fa-eject icon-large btn btn-danger PendingRejected  waves-effect waves-light"></button></form>'
                                       + '</div>' +

                                       //PendingExternal 5
                                       '<div class="col-md-3">' +
                                      '<form action=" /sds/referalpersons/Index" method="post">'
                                     + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                     + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                     + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                     + ' <button type="button" value="" title="خارجي" class=" fa fa-external-link icon-large btn btn-danger PendingExternal  waves-effect waves-light"></button> </form>'
                                      + '</div>' +

                                       //ApprovedInprgress 6
                                        '<div class="col-md-3">' +
                                      '<form action=" /sds/referalpersons/Index" method="post">'
                                     + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                     + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                     + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                     + '<button type="button" value="" title="قيد المتابعة" class=" fa fa-spinner icon-large btn btn-danger  ApprovedInprgress waves-effect waves-light"></button> </form>'
                                      + '</div>' +
                                       //OutReachInprgress 7
                                       '<div class="col-md-3">' +
                                      '<form action=" /sds/referalpersons/Index" method="post">'
                                     + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                     + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                     + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                     + '<button type="button" value="" title="قيد المتابعة-وصول" class=" fa fa-spinner icon-large btn btn-danger  OutReachInprgress waves-effect waves-light"> وصول </button> </form>'
                                      + '</div>' +
                                       //closed 8
                                      '<div class="col-md-3">' +
                                      '<form action=" /sds/referalpersons/Index" method="post">'
                                     + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                     + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                     + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                     + '<button type="button" value="" title="إغلاق" class=" fa fa-expeditedssl icon-large btn btn-danger closed waves-effect waves-light"></button> </form>'
                                      + '</div>' +

                                       //OutReachClosed 9
                                       '<div class="col-md-3">' +
                                      '<form action=" /sds/referalpersons/Index" method="post">'
                                     + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                     + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                     + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                     + '<button type="button" value="" title="إغلاق-وصول" class=" fa fa-expeditedssl icon-large btn btn-danger OutReachClosed waves-effect waves-light"> وصول </button> </form>'
                                     + '</div>'

                                       + '</div>'
                                        + '</li>'
                                         + '</ul>'
                                          + '</div>'
                                           + '</li>'
                                           + '</ul>'
                                      + '</td>')


                    ).draw().node();

                });
                $('.collapsible').collapsible({ refresh: true });
                row.remove().draw();

                toastr.success("تمت تعليق حالة الإحالة");
                bindAction();//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                // refereshtables();
                // $(this).on('click');
                hidePleaseWait();
            },
            error: function (xhr, status, error) {
                // check status && error
                toastr.error("فشلة عملية ");
                //  $(".PendingReReferal").disabled = false;//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                hidePleaseWait();
            }
        });



    });
});

//------------------------------------------------------------------------------------------ Pending Approved
//function CloseReferalFormIndex() {
$(document).ready(function () {
    // $(".PendingApproved").on("click", function () {//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    $('body').on('click', '.PendingApproved', function () {
        //to remvoe current Row in this table
        var tr = $(this).closest('tr');
        var ctable = $(this).closest('table').DataTable();
        var row = ctable.row(tr);
        //var rowNode = row.node();


        //to prevent multiple clicks on button
        //$(this).find('button[type="button"]').attr('disabled', 'disabled');
        //$(this).off('click');
        showPleaseWait();
        //// Get the button
        //var btn = $(this).find('input[type="button"]');

        //// Save and remove the onclick handler
        //var clickHandler = btn[0].onclick;
        //btn[0].onclick = false;

        var idreferalperson = $(this).siblings('.idreferalperson').val();
        var idcase = $(this).siblings('.idcase').val();
        var idperson = $(this).siblings('.idperson').val();
        var table = $('#All2').DataTable();//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        $.ajax({
            // url: ' /sds/referalpersons/CloseReferal',
            url: ' /sds/referalpersons/PendingApprovedReReferal',//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            type: "Post",
            dataType: "JSON",
            data: { 'idreferalperson': idreferalperson, 'idperson': idperson, 'idcase': idcase },
            success: function (data) {

                $.each(data, function (i, referal) {
                    table.row.add(
                    $('<tr></tr>')
                    .append('<td>' + referal.name + '</td>')
                    .append('<td>' + referal.submittingdate + '</td>')

                    .append('<td>' + referal.referaldate + '</td>')

                    .append('<td>' + referal.servicetype + '</td>')

                    .append('<td>' + referal.servicestartdate + '</td>')

                    .append('<td>' + referal.serviceenddate + '</td>')

                    .append('<td>' + referal.referalsender_FK + '</td>')

                    .append('<td>' + referal.senderevalution + '</td>')

                        .append('<td>' + referal.recieverevalution + '</td>')

                        .append('<td>' + referal.outreachnote + '</td>')
                       

                                  .append('<td>' +
                                   '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">' +
                                   '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">' +
                                   '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">' +
                                   '<button type="button" class="fa fa-edit btn gre btn-success btn-rounded p-1 btn-details waves-effect waves-light" title="تعديل"></button>' +
                                   '<a class=" fa fa-info-circle btn ora btn-warning btn-rounded p-1 waves-effect waves-light" href=" /people/Details/' + referal.idperson + '" title=" تفاصيل"> </a>' +
                                   '<a class=" fa fa-plus-square btn pur btn-purple btn-rounded p-1 waves-effect waves-light" href=" /sds/referalpersons/personReferalByCaseManager/' + referal.idperson + '?idcase=' + referal.idcase + '" title=" إحالة جديدة"> </a>' +
                                     '</td>')

                                  //Pending 1
                                  .append('<td>' +
                                  '<ul class="collapsible collapsible-accordion">'
                                       + '<li>'
                                            + '<a class="collapsible-header waves-effect arrow-r"><i></i>  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; تعديل الاحالة  <i class="fa fa-angle-down rotate-icon"></i></a>'
                                            + '<div class="collapsible-body">'
                                               + ' <ul>'
                                                   + '<li>'
                                                        + '<div class="form-group">' +



                                 // '<div class="col-md-3">' +
                                 // '<form action=" /sds/referalpersons/Index" method="post">'
                                 //+ '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 //+ '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 //+ '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 //+ '<button type="button" value="" title="معلقة" class=" fa fa-tasks icon-large btn btn-danger  PendingReReferal waves-effect waves-light"></button> </form>'
                                 // + '</div>' +



                                   //PendingRejected 4
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="رفض" class=" fa fa-eject icon-large btn btn-danger PendingRejected  waves-effect waves-light"></button></form>'
                                   + '</div>' +

                                   //PendingExternal 5
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + ' <button type="button" value="" title="خارجي" class=" fa fa-external-link icon-large btn btn-danger PendingExternal  waves-effect waves-light"></button> </form>'
                                  + '</div>' +

                                   //ApprovedInprgress 6
                                    '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قيد المتابعة" class=" fa fa-spinner icon-large btn btn-danger  ApprovedInprgress waves-effect waves-light"></button> </form>'
                                  + '</div>' +
                                   //OutReachInprgress 7
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قيد المتابعة-وصول" class=" fa fa-spinner icon-large btn btn-danger  OutReachInprgress waves-effect waves-light"> وصول </button> </form>'
                                  + '</div>' +
                                   //closed 8
                                  '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="إغلاق" class=" fa fa-expeditedssl icon-large btn btn-danger closed waves-effect waves-light"></button> </form>'
                                  + '</div>' +

                                   //OutReachClosed 9
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="إغلاق-وصول" class=" fa fa-expeditedssl icon-large btn btn-danger OutReachClosed waves-effect waves-light"> وصول </button> </form>'
                                 + '</div>'

                                   + '</div>'
                                    + '</li>'
                                     + '</ul>'
                                      + '</div>'
                                       + '</li>'
                                       + '</ul>'
                                  + '</td>')
                                    .append('<td>' + "لا" + '</td>')


                    ).draw().node();

                });
                $('.collapsible').collapsible({ refresh: true });
                row.remove().draw();

                toastr.success("تمت قبول الإحالة");
                bindAction();//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                // refereshtables();
                // $(this).on('click');
                hidePleaseWait();
            },
            error: function (xhr, status, error) {
                // check status && error
                toastr.error("فشلة العملية  ");
                //$(".PendingApproved").disabled = false;//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                hidePleaseWait();
            }
        });



    });
});

//------------------------------------------------------------------------------------------Pending OutReach
//function CloseReferalFormIndex() {
$(document).ready(function () {
    //   $(".PendingOutReach").on("click", function () {//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    $('body').on('click', '.PendingOutReach', function () {
        //to remvoe current Row in this table
        var tr = $(this).closest('tr');
        var ctable = $(this).closest('table').DataTable();
        var row = ctable.row(tr);
        //var rowNode = row.node();


        //to prevent multiple clicks on button
        //$(this).find('button[type="button"]').attr('disabled', 'disabled');
        //$(this).off('click');
        showPleaseWait();
        //// Get the button
        //var btn = $(this).find('input[type="button"]');

        //// Save and remove the onclick handler
        //var clickHandler = btn[0].onclick;
        //btn[0].onclick = false;

        var idreferalperson = $(this).siblings('.idreferalperson').val();
        var idcase = $(this).siblings('.idcase').val();
        var idperson = $(this).siblings('.idperson').val();
        var table = $('#All2').DataTable();//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        $.ajax({
            // url: ' /sds/referalpersons/CloseReferal',
            url: ' /sds/referalpersons/PendingOutReachReferal',//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            type: "Post",
            dataType: "JSON",
            data: { 'idreferalperson': idreferalperson, 'idperson': idperson, 'idcase': idcase },
            success: function (data) {

                $.each(data, function (i, referal) {
                    table.row.add(
                    $('<tr></tr>')
                    .append('<td>' + referal.name + '</td>')
                    .append('<td>' + referal.submittingdate + '</td>')

                    .append('<td>' + referal.referaldate + '</td>')

                    .append('<td>' + referal.servicetype + '</td>')

                    .append('<td>' + referal.servicestartdate + '</td>')

                    .append('<td>' + referal.serviceenddate + '</td>')

                    .append('<td>' + referal.referalsender_FK + '</td>')

                    .append('<td>' + referal.senderevalution + '</td>')

                        .append('<td>' + referal.recieverevalution + '</td>')

                        .append('<td>' + referal.outreachnote + '</td>')
                       

                                  .append('<td>' +
                                   '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">' +
                                   '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">' +
                                   '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">' +
                                   '<button type="button" class="fa fa-edit btn gre btn-success btn-rounded p-1 btn-details waves-effect waves-light" title="تعديل"></button>' +
                                   '<a class=" fa fa-info-circle btn ora btn-warning btn-rounded p-1 waves-effect waves-light" href=" /people/Details/' + referal.idperson + '" title=" تفاصيل"> </a>' +
                                   '<a class=" fa fa-plus-square btn pur btn-purple btn-rounded p-1 waves-effect waves-light" href=" /sds/referalpersons/personReferalByCaseManager/' + referal.idperson + '?idcase=' + referal.idcase + '" title=" إحالة جديدة"> </a>' +
                                     '</td>')

                                //Pending 1
                                  .append('<td>' +
                                  '<ul class="collapsible collapsible-accordion">'
                                       + '<li>'
                                            + '<a class="collapsible-header waves-effect arrow-r"><i></i>  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; تعديل الاحالة  <i class="fa fa-angle-down rotate-icon"></i></a>'
                                            + '<div class="collapsible-body">'
                                               + ' <ul>'
                                                   + '<li>'
                                                        + '<div class="form-group">' +



                                 // '<div class="col-md-3">' +
                                 // '<form action=" /sds/referalpersons/Index" method="post">'
                                 //+ '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 //+ '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 //+ '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 //+ '<button type="button" value="" title="معلقة" class=" fa fa-tasks icon-large btn btn-danger  PendingReReferal waves-effect waves-light"></button> </form>'
                                 // + '</div>' +


                                   //PendingRejected 4
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="رفض" class=" fa fa-eject icon-large btn btn-danger PendingRejected  waves-effect waves-light"></button></form>'
                                   + '</div>' +

                                   //PendingExternal 5
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + ' <button type="button" value="" title="خارجي" class=" fa fa-external-link icon-large btn btn-danger PendingExternal  waves-effect waves-light"></button> </form>'
                                  + '</div>' +

                                   //ApprovedInprgress 6
                                    '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قيد المتابعة" class=" fa fa-spinner icon-large btn btn-danger  ApprovedInprgress waves-effect waves-light"></button> </form>'
                                  + '</div>' +
                                   //OutReachInprgress 7
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قيد المتابعة-وصول" class=" fa fa-spinner icon-large btn btn-danger  OutReachInprgress waves-effect waves-light"> وصول </button> </form>'
                                  + '</div>' +
                                   //closed 8
                                  '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="إغلاق" class=" fa fa-expeditedssl icon-large btn btn-danger closed waves-effect waves-light"></button> </form>'
                                  + '</div>' +

                                   //OutReachClosed 9
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="إغلاق-وصول" class=" fa fa-expeditedssl icon-large btn btn-danger OutReachClosed waves-effect waves-light"> وصول </button> </form>'
                                 + '</div>'

                                   + '</div>'
                                    + '</li>'
                                     + '</ul>'
                                      + '</div>'
                                       + '</li>'
                                       + '</ul>'
                                  + '</td>')

                                    .append('<td>' + "نعم" + '</td>')
                    ).draw().node();

                });
                $('.collapsible').collapsible({ refresh: true });
                row.remove().draw();

                toastr.success("تمت قبول الإحالة-وصول");
                bindAction();//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                // refereshtables();
                //  $(this).on('click');
                hidePleaseWait();
            },
            error: function (xhr, status, error) {
                // check status && error
                toastr.error("فشلة العملية  ");
                // $(".PendingOutReach").disabled = false;//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                hidePleaseWait();
            }
        });



    });
});
//------------------------------------------------------------------------------------------Pending Rejected
//function CloseReferalFormIndex() {
$(document).ready(function () {
    //$(".PendingRejected").on("click", function () {//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    $('body').on('click', '.PendingRejected', function () {
        //to remvoe current Row in this table
        var tr = $(this).closest('tr');
        var ctable = $(this).closest('table').DataTable();
        var row = ctable.row(tr);
        //var rowNode = row.node();


        //to prevent multiple clicks on button
        //$(this).find('button[type="button"]').attr('disabled', 'disabled');
        //$(this).off('click');
        showPleaseWait();

        //// Get the button
        //var btn = $(this).find('input[type="button"]');

        //// Save and remove the onclick handler
        //var clickHandler = btn[0].onclick;
        //btn[0].onclick = false;

        var idreferalperson = $(this).siblings('.idreferalperson').val();
        var idcase = $(this).siblings('.idcase').val();
        var idperson = $(this).siblings('.idperson').val();
        var table = $('#All3').DataTable();//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        $.ajax({
            // url: ' /sds/referalpersons/CloseReferal',
            url: ' /sds/referalpersons/PendingRejectedReferal',//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            type: "Post",
            dataType: "JSON",
            data: { 'idreferalperson': idreferalperson, 'idperson': idperson, 'idcase': idcase },
            success: function (data) {

                $.each(data, function (i, referal) {
                    table.row.add(
                    $('<tr></tr>')
                    .append('<td>' + referal.name + '</td>')
                    .append('<td>' + referal.submittingdate + '</td>')

                    .append('<td>' + referal.referaldate + '</td>')

                   .append('<td>' + referal.servicetype + '</td>')

                    .append('<td>' + referal.servicestartdate + '</td>')

                    .append('<td>' + referal.serviceenddate + '</td>')

                    .append('<td>' + referal.referalsender_FK + '</td>')

                    .append('<td>' + referal.senderevalution + '</td>')

                        .append('<td>' + referal.recieverevalution + '</td>')

                        .append('<td>' + referal.outreachnote + '</td>')

                               .append('<td>' +
                                   '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">' +
                                   '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">' +
                                   '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">' +
                                   '<button type="button" class="fa fa-edit btn gre btn-success btn-rounded p-1 btn-details waves-effect waves-light" title="تعديل"></button>' +
                                   '<a class=" fa fa-info-circle btn ora btn-warning btn-rounded p-1 waves-effect waves-light" href=" /people/Details/' + referal.idperson + '" title=" تفاصيل"> </a>' +
                                   '<a class=" fa fa-plus-square btn pur btn-purple btn-rounded p-1 waves-effect waves-light" href=" /sds/referalpersons/personReferalByCaseManager/' + referal.idperson + '?idcase=' + referal.idcase + '" title=" إحالة جديدة"> </a>' +
                                     '</td>')

                         //Pending 1
                                  .append('<td>' +
                                  '<ul class="collapsible collapsible-accordion">'
                                       + '<li>'
                                            + '<a class="collapsible-header waves-effect arrow-r"><i></i>  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; تعديل الاحالة  <i class="fa fa-angle-down rotate-icon"></i></a>'
                                            + '<div class="collapsible-body">'
                                               + ' <ul>'
                                                   + '<li>'
                                                        + '<div class="form-group">' +



                                 // '<div class="col-md-3">' +
                                 // '<form action=" /sds/referalpersons/Index" method="post">'
                                 //+ '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 //+ '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 //+ '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 //+ '<button type="button" value="" title="معلقة" class=" fa fa-tasks icon-large btn btn-danger  PendingReReferal waves-effect waves-light"></button> </form>'
                                 // + '</div>' +

                                   //PendingApproved 2
                                    '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قبول" class=" fa fa-check-circle icon-large btn btn-danger  PendingApproved waves-effect waves-light"></button> </form>'
                                   + '</div>' +

                                   //PendingOutReach 3
                                    '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قبول-وصول" class=" fa fa-check-circle icon-large btn btn-danger  PendingOutReach waves-effect waves-light"> وصول </button> </form>'
                                   + '</div>' +

                                //PendingExternal 5
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + ' <button type="button" value="" title="خارجي" class=" fa fa-external-link icon-large btn btn-danger PendingExternal  waves-effect waves-light"></button> </form>'
                                  + '</div>' +

                                   //ApprovedInprgress 6
                                    '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قيد المتابعة" class=" fa fa-spinner icon-large btn btn-danger  ApprovedInprgress waves-effect waves-light"></button> </form>'
                                  + '</div>' +
                                   //OutReachInprgress 7
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قيد المتابعة-وصول" class=" fa fa-spinner icon-large btn btn-danger  OutReachInprgress waves-effect waves-light"> وصول </button> </form>'
                                  + '</div>' +
                                   //closed 8
                                  '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="إغلاق" class=" fa fa-expeditedssl icon-large btn btn-danger closed waves-effect waves-light"></button> </form>'
                                  + '</div>' +

                                   //OutReachClosed 9
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="إغلاق-وصول" class=" fa fa-expeditedssl icon-large btn btn-danger OutReachClosed waves-effect waves-light"> وصول </button> </form>'
                                 + '</div>'

                                   + '</div>'
                                    + '</li>'
                                     + '</ul>'
                                      + '</div>'
                                       + '</li>'
                                       + '</ul>'
                                  + '</td>')


                    ).draw().node();

                });
                $('.collapsible').collapsible({ refresh: true });
                row.remove().draw();

                toastr.success("تمت رفض الإحالة");
                bindAction();//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                // refereshtables();
                //$(this).on('click');
                hidePleaseWait();
            },
            error: function (xhr, status, error) {
                // check status && error
                toastr.error("فشلة العملية  ");
                // $(".PendingRejected").disabled = false;//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                hidePleaseWait();
            }
        });



    });
});

//------------------------------------------------------------------------------------------Pending External
//function CloseReferalFormIndex() {
$(document).ready(function () {
    //   $(".PendingExternal").on("click", function () {//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    $('body').on('click', '.PendingExternal', function () {
        //to remvoe current Row in this table
        var tr = $(this).closest('tr');
        var ctable = $(this).closest('table').DataTable();
        var row = ctable.row(tr);
        //var rowNode = row.node();


        //to prevent multiple clicks on button
        //$(this).find('button[type="button"]').attr('disabled', 'disabled');
        //$(this).off('click');
        showPleaseWait();

        //// Get the button
        //var btn = $(this).find('input[type="button"]');

        //// Save and remove the onclick handler
        //var clickHandler = btn[0].onclick;
        //btn[0].onclick = false;

        var idreferalperson = $(this).siblings('.idreferalperson').val();
        var idcase = $(this).siblings('.idcase').val();
        var idperson = $(this).siblings('.idperson').val();
        var table = $('#All6').DataTable();//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        $.ajax({
            // url: ' /sds/referalpersons/CloseReferal',
            url: ' /sds/referalpersons/PendingExternalReferal',//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            type: "Post",
            dataType: "JSON",
            data: { 'idreferalperson': idreferalperson, 'idperson': idperson, 'idcase': idcase },
            success: function (data) {

                $.each(data, function (i, referal) {
                    table.row.add(
                    $('<tr></tr>')
                    .append('<td>' + referal.name + '</td>')
                    .append('<td>' + referal.submittingdate + '</td>')

                    .append('<td>' + referal.referaldate + '</td>')

                    .append('<td>' + referal.servicetype + '</td>')

                    .append('<td>' + referal.servicestartdate + '</td>')

                    .append('<td>' + referal.serviceenddate + '</td>')

                    .append('<td>' + referal.referalsender_FK + '</td>')

                    .append('<td>' + referal.senderevalution + '</td>')

                        .append('<td>' + referal.recieverevalution + '</td>')

                        .append('<td>' + referal.outreachnote + '</td>')

                                    .append('<td>' +
                                   '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">' +
                                   '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">' +
                                   '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">' +
                                   '<button type="button" class="fa fa-edit btn gre btn-success btn-rounded p-1 btn-details waves-effect waves-light" title="تعديل"></button>' +
                                   '<a class=" fa fa-info-circle btn ora btn-warning btn-rounded p-1 waves-effect waves-light" href=" /people/Details/' + referal.idperson + '" title=" تفاصيل"> </a>' +
                                   '<a class=" fa fa-plus-square btn pur btn-purple btn-rounded p-1 waves-effect waves-light" href=" /sds/referalpersons/personReferalByCaseManager/' + referal.idperson + '?idcase=' + referal.idcase + '" title=" إحالة جديدة"> </a>' +
                                     '</td>')


                                   //Pending 1
                                  .append('<td>' +
                                  '<ul class="collapsible collapsible-accordion">'
                                       + '<li>'
                                            + '<a class="collapsible-header waves-effect arrow-r"><i></i>  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; تعديل الاحالة  <i class="fa fa-angle-down rotate-icon"></i></a>'
                                            + '<div class="collapsible-body">'
                                               + ' <ul>'
                                                   + '<li>'
                                                        + '<div class="form-group">' +



                                 // '<div class="col-md-3">' +
                                 // '<form action=" /sds/referalpersons/Index" method="post">'
                                 //+ '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 //+ '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 //+ '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 //+ '<button type="button" value="" title="معلقة" class=" fa fa-tasks icon-large btn btn-danger  PendingReReferal waves-effect waves-light"></button> </form>'
                                 // + '</div>' +

                                   //PendingApproved 2
                                    '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قبول" class=" fa fa-check-circle icon-large btn btn-danger  PendingApproved waves-effect waves-light"></button> </form>'
                                   + '</div>' +

                                   //PendingOutReach 3
                                    '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قبول-وصول" class=" fa fa-check-circle icon-large btn btn-danger  PendingOutReach waves-effect waves-light"> وصول </button> </form>'
                                   + '</div>' +

                                   //PendingRejected 4
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="رفض" class=" fa fa-eject icon-large btn btn-danger PendingRejected  waves-effect waves-light"></button></form>'
                                   + '</div>' +


                                   //ApprovedInprgress 6
                                    '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قيد المتابعة" class=" fa fa-spinner icon-large btn btn-danger  ApprovedInprgress waves-effect waves-light"></button> </form>'
                                  + '</div>' +
                                   //OutReachInprgress 7
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قيد المتابعة-وصول" class=" fa fa-spinner icon-large btn btn-danger  OutReachInprgress waves-effect waves-light"> وصول </button> </form>'
                                  + '</div>' +
                                   //closed 8
                                  '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="إغلاق" class=" fa fa-expeditedssl icon-large btn btn-danger closed waves-effect waves-light"></button> </form>'
                                  + '</div>' +

                                   //OutReachClosed 9
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="إغلاق-وصول" class=" fa fa-expeditedssl icon-large btn btn-danger OutReachClosed waves-effect waves-light"> وصول </button> </form>'
                                 + '</div>'

                                  + '</div>'
                                    + '</li>'
                                     + '</ul>'
                                      + '</div>'
                                       + '</li>'
                                       + '</ul>'
                                  + '</td>')




                    ).draw().node();

                });
                $('.collapsible').collapsible({ refresh: true });
                row.remove().draw();

                toastr.success("الإحالة خارجية");
                bindAction();//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                // refereshtables();
                //  $(this).on('click');
                hidePleaseWait();
            },
            error: function (xhr, status, error) {
                // check status && error
                toastr.error("فشلة العملية  ");
                // $(".PendingExternal").disabled = false;//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                hidePleaseWait();
            }
        });



    });
});

//------------------------------------------------------------------------------------------Approved Inprgress
//function CloseReferalFormIndex() {
$(document).ready(function () {
    //$(".ApprovedInprgress").on("click", function () {//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    $('body').on('click', '.ApprovedInprgress', function () {
        //to remvoe current Row in this table
        var tr = $(this).closest('tr');
        var ctable = $(this).closest('table').DataTable();
        var row = ctable.row(tr);
        //var rowNode = row.node();


        //to prevent multiple clicks on button
        //$(this).find('button[type="button"]').attr('disabled', 'disabled');
        //$(this).off('click');
        showPleaseWait();

        //// Get the button
        //var btn = $(this).find('input[type="button"]');

        //// Save and remove the onclick handler
        //var clickHandler = btn[0].onclick;
        //btn[0].onclick = false;

        var idreferalperson = $(this).siblings('.idreferalperson').val();
        var idcase = $(this).siblings('.idcase').val();
        var idperson = $(this).siblings('.idperson').val();
        var table = $('#All4').DataTable();//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        $.ajax({
            // url: ' /sds/referalpersons/CloseReferal',
            url: ' /sds/referalpersons/ApprovedInprgressReferal',//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            type: "Post",
            dataType: "JSON",
            data: { 'idreferalperson': idreferalperson, 'idperson': idperson, 'idcase': idcase },
            success: function (data) {

                $.each(data, function (i, referal) {
                    table.row.add(
                    $('<tr></tr>')
                    .append('<td>' + referal.name + '</td>')
                    .append('<td>' + referal.submittingdate + '</td>')

                    .append('<td>' + referal.referaldate + '</td>')

                    .append('<td>' + referal.servicetype + '</td>')

                    .append('<td>' + referal.servicestartdate + '</td>')

                    .append('<td>' + referal.serviceenddate + '</td>')

                    .append('<td>' + referal.referalsender_FK + '</td>')

                    .append('<td>' + referal.senderevalution + '</td>')

                        .append('<td>' + referal.recieverevalution + '</td>')

                        .append('<td>' + referal.outreachnote + '</td>')
                       
                                      .append('<td>' +
                                   '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">' +
                                   '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">' +
                                   '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">' +
                                   '<button type="button" class="fa fa-edit btn gre btn-success btn-rounded p-1 btn-details waves-effect waves-light" title="تعديل"></button>' +
                                   '<a class=" fa fa-info-circle btn ora btn-warning btn-rounded p-1 waves-effect waves-light" href=" /people/Details/' + referal.idperson + '" title=" تفاصيل"> </a>' +
                                   '<a class=" fa fa-plus-square btn pur btn-purple btn-rounded p-1 waves-effect waves-light" href=" /sds/referalpersons/personReferalByCaseManager/' + referal.idperson + '?idcase=' + referal.idcase + '" title=" إحالة جديدة"> </a>' +
                                     '</td>')

                          //Pending 1
                                  .append('<td>' +
                                  '<ul class="collapsible collapsible-accordion">'
                                       + '<li>'
                                            + '<a class="collapsible-header waves-effect arrow-r"><i></i>  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; تعديل الاحالة  <i class="fa fa-angle-down rotate-icon"></i></a>'
                                            + '<div class="collapsible-body">'
                                               + ' <ul>'
                                                   + '<li>'
                                                        + '<div class="form-group">' +



                                 // '<div class="col-md-3">' +
                                 // '<form action=" /sds/referalpersons/Index" method="post">'
                                 //+ '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 //+ '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 //+ '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 //+ '<button type="button" value="" title="معلقة" class=" fa fa-tasks icon-large btn btn-danger  PendingReReferal waves-effect waves-light"></button> </form>'
                                 // + '</div>' +

                                   //PendingApproved 2
                                    '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قبول" class=" fa fa-check-circle icon-large btn btn-danger  PendingApproved waves-effect waves-light"></button> </form>'
                                   + '</div>' +

                                   //PendingOutReach 3
                                    '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قبول-وصول" class=" fa fa-check-circle icon-large btn btn-danger  PendingOutReach waves-effect waves-light"> وصول </button> </form>'
                                   + '</div>' +

                                   //PendingRejected 4
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="رفض" class=" fa fa-eject icon-large btn btn-danger PendingRejected  waves-effect waves-light"></button></form>'
                                   + '</div>' +

                                   //PendingExternal 5
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + ' <button type="button" value="" title="خارجي" class=" fa fa-external-link icon-large btn btn-danger PendingExternal  waves-effect waves-light"></button> </form>'
                                  + '</div>' +


                                   //closed 8
                                  '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="إغلاق" class=" fa fa-expeditedssl icon-large btn btn-danger closed waves-effect waves-light"></button> </form>'
                                  + '</div>' +

                                   //OutReachClosed 9
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="إغلاق-وصول" class=" fa fa-expeditedssl icon-large btn btn-danger OutReachClosed waves-effect waves-light"> وصول </button> </form>'
                                 + '</div>'

                                + '</div>'
                                    + '</li>'
                                     + '</ul>'
                                      + '</div>'
                                       + '</li>'
                                       + '</ul>'
                                  + '</td>')
                                   .append('<td>' + "لا" + '</td>')

                    ).draw().node();

                });
                $('.collapsible').collapsible({ refresh: true });
                row.remove().draw();

                toastr.success("الإحالة قيد المتابعة");
                bindAction();//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                // refereshtables();
                // $(this).on('click');
                hidePleaseWait();
            },
            error: function (xhr, status, error) {
                // check status && error
                toastr.error("فشلة العملية  ");
                //$(".ApprovedInprgress").disabled = false;//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                hidePleaseWait();
            }
        });



    });
});

//------------------------------------------------------------------------------------------OutReach Inprgress
//function CloseReferalFormIndex() {
$(document).ready(function () {
    //$(".OutReachInprgress").on("click", function () {//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    $('body').on('click', '.OutReachInprgress', function () {
        //to remvoe current Row in this table
        var tr = $(this).closest('tr');
        var ctable = $(this).closest('table').DataTable();
        var row = ctable.row(tr);
        //var rowNode = row.node();


        //to prevent multiple clicks on button
        //$(this).find('button[type="button"]').attr('disabled', 'disabled');
        //$(this).off('click');
        showPleaseWait();

        //// Get the button
        //var btn = $(this).find('input[type="button"]');

        //// Save and remove the onclick handler
        //var clickHandler = btn[0].onclick;
        //btn[0].onclick = false;

        var idreferalperson = $(this).siblings('.idreferalperson').val();
        var idcase = $(this).siblings('.idcase').val();
        var idperson = $(this).siblings('.idperson').val();
        var table = $('#All4').DataTable();//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        $.ajax({
            // url: ' /sds/referalpersons/CloseReferal',
            url: ' /sds/referalpersons/OutReachInprgressReferal',//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            type: "Post",
            dataType: "JSON",
            data: { 'idreferalperson': idreferalperson, 'idperson': idperson, 'idcase': idcase },
            success: function (data) {

                $.each(data, function (i, referal) {
                    table.row.add(
                    $('<tr></tr>')
                    .append('<td>' + referal.name + '</td>')
                    .append('<td>' + referal.submittingdate + '</td>')

                    .append('<td>' + referal.referaldate + '</td>')

                   .append('<td>' + referal.servicetype + '</td>')

                    .append('<td>' + referal.servicestartdate + '</td>')

                    .append('<td>' + referal.serviceenddate + '</td>')

                    .append('<td>' + referal.referalsender_FK + '</td>')

                    .append('<td>' + referal.senderevalution + '</td>')

                        .append('<td>' + referal.recieverevalution + '</td>')

                        .append('<td>' + referal.outreachnote + '</td>')
                       

                                  .append('<td>' +
                                   '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">' +
                                   '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">' +
                                   '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">' +
                                   '<button type="button" class="fa fa-edit btn gre btn-success btn-rounded p-1 btn-details waves-effect waves-light" title="تعديل"></button>' +
                                   '<a class=" fa fa-info-circle btn ora btn-warning btn-rounded p-1 waves-effect waves-light" href=" /people/Details/' + referal.idperson + '" title=" تفاصيل"> </a>' +
                                   '<a class=" fa fa-plus-square btn pur btn-purple btn-rounded p-1 waves-effect waves-light" href=" /sds/referalpersons/personReferalByCaseManager/' + referal.idperson + '?idcase=' + referal.idcase + '" title=" إحالة جديدة"> </a>' +
                                     '</td>')


                                  //Pending 1
                                  .append('<td>' +
                                  '<ul class="collapsible collapsible-accordion">'
                                       + '<li>'
                                            + '<a class="collapsible-header waves-effect arrow-r"><i></i>  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; تعديل الاحالة  <i class="fa fa-angle-down rotate-icon"></i></a>'
                                            + '<div class="collapsible-body">'
                                               + ' <ul>'
                                                   + '<li>'
                                                        + '<div class="form-group">' +



                                 // '<div class="col-md-3">' +
                                 // '<form action=" /sds/referalpersons/Index" method="post">'
                                 //+ '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 //+ '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 //+ '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 //+ '<button type="button" value="" title="معلقة" class=" fa fa-tasks icon-large btn btn-danger  PendingReReferal waves-effect waves-light"></button> </form>'
                                 // + '</div>' +

                                   //PendingApproved 2
                                    '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قبول" class=" fa fa-check-circle icon-large btn btn-danger  PendingApproved waves-effect waves-light"></button> </form>'
                                   + '</div>' +

                                   //PendingOutReach 3
                                    '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قبول-وصول" class=" fa fa-check-circle icon-large btn btn-danger  PendingOutReach waves-effect waves-light"> وصول </button> </form>'
                                   + '</div>' +

                                   //PendingRejected 4
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="رفض" class=" fa fa-eject icon-large btn btn-danger PendingRejected  waves-effect waves-light"></button></form>'
                                   + '</div>' +

                                   //PendingExternal 5
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + ' <button type="button" value="" title="خارجي" class=" fa fa-external-link icon-large btn btn-danger PendingExternal  waves-effect waves-light"></button> </form>'
                                  + '</div>' +


                                   //closed 8
                                  '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="إغلاق" class=" fa fa-expeditedssl icon-large btn btn-danger closed waves-effect waves-light"></button> </form>'
                                  + '</div>' +

                                   //OutReachClosed 9
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="إغلاق-وصول" class=" fa fa-expeditedssl icon-large btn btn-danger OutReachClosed waves-effect waves-light"> وصول </button> </form>'
                                 + '</div>'

                                  + '</div>'
                                    + '</li>'
                                     + '</ul>'
                                      + '</div>'
                                       + '</li>'
                                       + '</ul>'
                                  + '</td>')
                                   .append('<td>' + "نعم" + '</td>')

                    ).draw().node();

                });
                $('.collapsible').collapsible({ refresh: true });
                row.remove().draw();

                toastr.success("الإحالة قيد المتابعة-وصول");
                bindAction();//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                // refereshtables();
                // $(this).on('click');
                hidePleaseWait();
            },
            error: function (xhr, status, error) {
                // check status && error
                toastr.error("فشلة العملية  ");
                // $(".OutReachInprgress").disabled = false;//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                hidePleaseWait();
            }
        });



    });
});
//-------------------------------------------------------------------------------close
//function CloseReferalFormIndex() {
$(document).ready(function () {
    //$(".closed").on("click", function () {
    $('body').on('click', '.closed', function () {
        //to remvoe current Row in this table
        var tr = $(this).closest('tr');
        var ctable = $(this).closest('table').DataTable();
        var row = ctable.row(tr);
        //var rowNode = row.node();


        //to prevent multiple clicks on button
        //$(this).find('button[type="button"]').attr('disabled', 'disabled');
        //$(this).off('click');
        showPleaseWait();

        //// Get the button
        //var btn = $(this).find('input[type="button"]');

        //// Save and remove the onclick handler
        //var clickHandler = btn[0].onclick;
        //btn[0].onclick = false;

        var idreferalperson = $(this).siblings('.idreferalperson').val();
        var idcase = $(this).siblings('.idcase').val();
        var idperson = $(this).siblings('.idperson').val();
        var table = $('#All5').DataTable();

        $.ajax({
            // url: ' /sds/referalpersons/CloseReferal',
            url: ' /sds/referalpersons/CloseReferal',
            type: "Post",
            dataType: "JSON",
            data: { 'idreferalperson': idreferalperson, 'idperson': idperson, 'idcase': idcase },
            success: function (data) {

                $.each(data, function (i, referal) {
                    table.row.add(
                    $('<tr></tr>')
                    .append('<td>' + referal.name + '</td>')
                    .append('<td>' + referal.submittingdate + '</td>')

                    .append('<td>' + referal.referaldate + '</td>')

                    .append('<td>' + referal.servicetype + '</td>')

                    .append('<td>' + referal.servicestartdate + '</td>')

                    .append('<td>' + referal.serviceenddate + '</td>')

                    .append('<td>' + referal.referalsender_FK + '</td>')

                    .append('<td>' + referal.senderevalution + '</td>')

                        .append('<td>' + referal.recieverevalution + '</td>')

                        .append('<td>' + referal.outreachnote + '</td>')
                        

                                  .append('<td>' +
                                   '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">' +
                                   '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">' +
                                   '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">' +
                                   '<button type="button" class="fa fa-edit btn gre btn-success btn-rounded p-1 btn-details waves-effect waves-light" title="تعديل"></button>' +
                                   '<a class=" fa fa-info-circle btn ora btn-warning btn-rounded p-1 waves-effect waves-light" href=" /people/Details/' + referal.idperson + '" title=" تفاصيل"> </a>' +
                                   '<a class=" fa fa-plus-square btn pur btn-purple btn-rounded p-1 waves-effect waves-light" href=" /sds/referalpersons/personReferalByCaseManager/' + referal.idperson + '?idcase=' + referal.idcase + '" title=" إحالة جديدة"> </a>' +
                                     '</td>')


                              //Pending 1
                                  .append('<td>' +
                                  '<ul class="collapsible collapsible-accordion">'
                                       + '<li>'
                                            + '<a class="collapsible-header waves-effect arrow-r"><i></i>  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; تعديل الاحالة  <i class="fa fa-angle-down rotate-icon"></i></a>'
                                            + '<div class="collapsible-body">'
                                               + ' <ul>'
                                                   + '<li>'
                                                        + '<div class="form-group">' +



                                 // '<div class="col-md-3">' +
                                 // '<form action=" /sds/referalpersons/Index" method="post">'
                                 //+ '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 //+ '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 //+ '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 //+ '<button type="button" value="" title="معلقة" class=" fa fa-tasks icon-large btn btn-danger  PendingReReferal waves-effect waves-light"></button> </form>'
                                 // + '</div>' +

                                   //PendingApproved 2
                                    '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قبول" class=" fa fa-check-circle icon-large btn btn-danger  PendingApproved waves-effect waves-light"></button> </form>'
                                   + '</div>' +

                                   //PendingOutReach 3
                                    '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قبول-وصول" class=" fa fa-check-circle icon-large btn btn-danger  PendingOutReach waves-effect waves-light"> وصول </button> </form>'
                                   + '</div>' +

                                   //PendingRejected 4
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="رفض" class=" fa fa-eject icon-large btn btn-danger PendingRejected  waves-effect waves-light"></button></form>'
                                   + '</div>' +

                                   //PendingExternal 5
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + ' <button type="button" value="" title="خارجي" class=" fa fa-external-link icon-large btn btn-danger PendingExternal  waves-effect waves-light"></button> </form>'
                                  + '</div>' +

                                   //ApprovedInprgress 6
                                    '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قيد المتابعة" class=" fa fa-spinner icon-large btn btn-danger  ApprovedInprgress waves-effect waves-light"></button> </form>'
                                  + '</div>' +
                                   //OutReachInprgress 7
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قيد المتابعة-وصول" class=" fa fa-spinner icon-large btn btn-danger  OutReachInprgress waves-effect waves-light"> وصول </button> </form>'
                                  + '</div>'


                                  + '</div>'
                                    + '</li>'
                                     + '</ul>'
                                      + '</div>'
                                       + '</li>'
                                       + '</ul>'
                                  + '</td>')

                                  .append('<td>' + "لا" + '</td>')

                    ).draw().node();

                });
                $('.collapsible').collapsible({ refresh: true });
                row.remove().draw();

                toastr.success("تمت إغلاق الإحالة");
                bindAction();
                // refereshtables();
                // $(this).on('click');
                hidePleaseWait();
            },
            error: function (xhr, status, error) {
                // check status && error
                toastr.error("فشلة عملية الإغلاق ");
                //$(".closed").disabled = false;
                hidePleaseWait();
            }
        });



    });
});

//------------------------------------------------------------------------------------------OutReach Closed
//function CloseReferalFormIndex() {
$(document).ready(function () {
    //$(".OutReachClosed").on("click", function () {//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    $('body').on('click', '.OutReachClosed', function () {
        //to remvoe current Row in this table
        var tr = $(this).closest('tr');
        var ctable = $(this).closest('table').DataTable();
        var row = ctable.row(tr);
        //var rowNode = row.node();


        //to prevent multiple clicks on button
        //$(this).find('button[type="button"]').attr('disabled', 'disabled');
        //$(this).off('click');
        showPleaseWait();

        //// Get the button
        //var btn = $(this).find('input[type="button"]');

        //// Save and remove the onclick handler
        //var clickHandler = btn[0].onclick;
        //btn[0].onclick = false;

        var idreferalperson = $(this).siblings('.idreferalperson').val();
        var idcase = $(this).siblings('.idcase').val();
        var idperson = $(this).siblings('.idperson').val();
        var table = $('#All5').DataTable();//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        $.ajax({
            // url: ' /sds/referalpersons/CloseReferal',
            url: ' /sds/referalpersons/OutReachClosedReReferal',//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            type: "Post",
            dataType: "JSON",
            data: { 'idreferalperson': idreferalperson, 'idperson': idperson, 'idcase': idcase },
            success: function (data) {

                $.each(data, function (i, referal) {
                    table.row.add(
                    $('<tr></tr>')
                    .append('<td>' + referal.name + '</td>')
                    .append('<td>' + referal.submittingdate + '</td>')

                    .append('<td>' + referal.referaldate + '</td>')

                   .append('<td>' + referal.servicetype + '</td>')

                    .append('<td>' + referal.servicestartdate + '</td>')

                    .append('<td>' + referal.serviceenddate + '</td>')

                    .append('<td>' + referal.referalsender_FK + '</td>')

                    .append('<td>' + referal.senderevalution + '</td>')

                        .append('<td>' + referal.recieverevalution + '</td>')

                        .append('<td>' + referal.outreachnote + '</td>')
                        

                                 .append('<td>' +
                                   '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">' +
                                   '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">' +
                                   '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">' +
                                   '<button type="button" class="fa fa-edit btn gre btn-success btn-rounded p-1 btn-details waves-effect waves-light" title="تعديل"></button>' +
                                   '<a class=" fa fa-info-circle btn ora btn-warning btn-rounded p-1 waves-effect waves-light" href=" /people/Details/' + referal.idperson + '" title=" تفاصيل"> </a>' +
                                   '<a class=" fa fa-plus-square btn pur btn-purple btn-rounded p-1 waves-effect waves-light" href=" /sds/referalpersons/personReferalByCaseManager/' + referal.idperson + '?idcase=' + referal.idcase + '" title=" إحالة جديدة"> </a>' +
                                     '</td>')


                                   //Pending 1
                                  .append('<td>' +
                                  '<ul class="collapsible collapsible-accordion">'
                                       + '<li>'
                                            + '<a class="collapsible-header waves-effect arrow-r"><i></i>  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; تعديل الاحالة  <i class="fa fa-angle-down rotate-icon"></i></a>'
                                            + '<div class="collapsible-body">'
                                               + ' <ul>'
                                                   + '<li>'
                                                        + '<div class="form-group">' +



                                 // '<div class="col-md-3">' +
                                 // '<form action=" /sds/referalpersons/Index" method="post">'
                                 //+ '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 //+ '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 //+ '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 //+ '<button type="button" value="" title="معلقة" class=" fa fa-tasks icon-large btn btn-danger  PendingReReferal waves-effect waves-light"></button> </form>'
                                 // + '</div>' +

                                   //PendingApproved 2
                                    '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قبول" class=" fa fa-check-circle icon-large btn btn-danger  PendingApproved waves-effect waves-light"></button> </form>'
                                   + '</div>' +

                                   //PendingOutReach 3
                                    '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قبول-وصول" class=" fa fa-check-circle icon-large btn btn-danger  PendingOutReach waves-effect waves-light"> وصول </button> </form>'
                                   + '</div>' +

                                   //PendingRejected 4
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="رفض" class=" fa fa-eject icon-large btn btn-danger PendingRejected  waves-effect waves-light"></button></form>'
                                   + '</div>' +

                                   //PendingExternal 5
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + ' <button type="button" value="" title="خارجي" class=" fa fa-external-link icon-large btn btn-danger PendingExternal  waves-effect waves-light"></button> </form>'
                                  + '</div>' +

                                   //ApprovedInprgress 6
                                    '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قيد المتابعة" class=" fa fa-spinner icon-large btn btn-danger  ApprovedInprgress waves-effect waves-light"></button> </form>'
                                  + '</div>' +
                                   //OutReachInprgress 7
                                   '<div class="col-md-3">' +
                                  '<form action=" /sds/referalpersons/Index" method="post">'
                                 + '<input type="hidden" class="idreferalperson" name="idreferalperson" value="' + referal.idreferalperson + '">'
                                 + '<input type="hidden" class="idcase" name="idcase" value="' + referal.idcase + '">'
                                 + '<input type="hidden" class="idperson" name="idperson" value="' + referal.idperson + '">'
                                 + '<button type="button" value="" title="قيد المتابعة-وصول" class=" fa fa-spinner icon-large btn btn-danger  OutReachInprgress waves-effect waves-light"> وصول </button> </form>'
                                  + '</div>' +


                                   +'</div>'
                                    + '</li>'
                                     + '</ul>'
                                      + '</div>'
                                       + '</li>'
                                       + '</ul>'
                                  + '</td>')

                                  .append('<td>' + "نعم" + '</td>')



                    ).draw().node();

                });
                $('.collapsible').collapsible({ refresh: true });
                row.remove().draw();

                toastr.success("تمت إغلاق الإحالة-وصول");
                bindAction();//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                // refereshtables();
                //  $(this).on('click');
                hidePleaseWait();
            },
            error: function (xhr, status, error) {
                // check status && error
                toastr.error("فشلة عملية الإغلاق ");
                //$(".OutReachClosed").disabled = false;//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                hidePleaseWait();
            }
        });



    });
});
///00000000000000000000000000000000000099999999999999999999999990000000000000000009999999999999999990000000000000000000000000000999999999

function bindAction() {//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<



}
//-------------------------------------------------------------------------------------------------------

/**
 * Displays overlay with "Please wait" text. Based on bootstrap modal. Contains animated progress bar.
 */
function showPleaseWait() {
    var modalLoading = '<div class="modal" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false role="dialog">\
        <div class="modal-dialog">\
            <div class="modal-content">\
                <div class="modal-header">\
                    <h4 class="modal-title">جاري عملية الحفظ ....</h4>\
                </div>\
                <div class="modal-body">\
                    <div class="progress">\
                      <div class="progress-bar progress-bar-success progress-bar-striped active" role="progressbar"\
                      aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width:100%; height: 40px">\
                      </div>\
                    </div>\
                </div>\
            </div>\
        </div>\
    </div>';
    $(document.body).append(modalLoading);
    $("#pleaseWaitDialog").modal("show");
}

/**
 * Hides "Please wait" overlay. See function showPleaseWait().
 */
function hidePleaseWait() {
    $("#pleaseWaitDialog").modal("hide");
}

var notes_cell;
var evaluation_cell;
var table_edit;

$('body').on('click', '.btn-details', function () {
    $("#dlg-details").modal("show");

    var tr = $(this).closest('tr');
    var ctable = $(this).closest('table').DataTable();
    var row = ctable.row(tr);

    var idreferalperson = $(this).siblings('.idreferalperson').val();
    var idcase = $(this).siblings('.idcase').val();
    var idperson = $(this).siblings('.idperson').val();
    table_edit = ctable;
    notes_cell = $(this).parent().prev('td');
    evaluation_cell = $(this).parent().prev('td').prev('td');
    var notes = $(this).parent().prev('td').text().trim();
    var evaluation = $(this).parent().prev('td').prev('td').text().trim();

    document.getElementById("evaluation").value = evaluation;
    document.getElementById("notes").value = notes;

    document.getElementById("idreferalpersonselected").value = idreferalperson;
    document.getElementById("idcaseselected").value = idcase;
    document.getElementById("idpersonselected").value = idperson;


    





});

$('body').on('click', '#modalclose', function () {

    $("#dlg-details").modal("hide");

});

$('body').on('click', '#modalsave', function () {


    var recieverevalution= document.getElementById("evaluation").value ;
   var  outreachnote = document.getElementById("notes").value ;

    var idreferalperson= document.getElementById("idreferalpersonselected").value  ;
    var idcase =document.getElementById("idcaseselected").value  ;
    var idperson=  document.getElementById("idpersonselected").value  ;


    $.ajax({
        // url: ' /sds/referalpersons/CloseReferal',
        url: ' /sds/referalpersons/editreferalrow',//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        type: "Post",
        dataType: "JSON",
        data: { 'idreferalperson': idreferalperson, 'idperson': idperson, 'idcase': idcase, 'recieverevalution': recieverevalution, 'outreachnote': outreachnote },
        success: function (data) {

            if (data == "sucsses") {
                             
                table_edit.cell(notes_cell).data(outreachnote).draw();
                table_edit.cell(evaluation_cell).data(recieverevalution).draw();

                toastr.success("تم تعديل الإحالة بنجاح");
                $("#dlg-details").modal("hide");
               
            }

           


        },
        error: function (xhr, status, error) {
            // check status && error
            toastr.error("فشلة العملية-يرجى التاكد من الاتصال" );
            // $(".OutReachInprgress").disabled = false;//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        }
    });

});

