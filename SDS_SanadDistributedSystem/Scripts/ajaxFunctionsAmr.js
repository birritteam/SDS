function FillServices() {
    var caseId = $('#idcase_FK').val();
    alert("sssssssssssss" + caseId);
    $.ajax({
        url: '/referalpersons/FillServices',
        type: "GET",
        dataType: "JSON",
        data: { 'caseId': caseId },
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
            alert("Whooaaa! Something went wrong..")
        }
    });
}

function FillRecivers()
{

    var serviceId = $('#services').val();
    alert("serviceId =" + serviceId);
    $.ajax({
        url: '/referalpersons/FillRecivers',
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
            alert("Whooaaa! Something went wrong..")
        }
    });



}

function FillRecivers_EDIT() {

    var serviceId = $('#idservice_FK').val();
    alert("serviceId =" + serviceId);
    $.ajax({
        url: '/referalpersons/FillRecivers',
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
            alert("Whooaaa! Something went wrong..")
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
    alert(id);
    $.ajax({
        url: '/referalpersons/deleteRow',
        type: "GET",
        dataType: "JSON",
        data: { 'selectedId': id },
        success:
function (data) {
    alert("successFillTable.............")
    $("#myTable tbody").empty();
    $.each(data, function (i, referalperson) {
        $("#myTable").append(
        $('<tr><td>' + (i + 1) + '</td><td>' + referalperson.idcase_FK +
            '</td><td>' + referalperson.idservice_FK + '</td><td>' +
            referalperson.senderevalution + '</td><td> <input type="submit" id=' + (i + 1) + ' value="delete" class="btn btn-default" onclick="deleterow(this.id)" /></td></tr>'));
    });
},

        error: function (xhr, status, error) {
            // check status && error
            alert("Whooaaa! Something went wrong..")
        }
    });
}


function successFillTable(data) {
    alert("successFillTable.............")
    $("#myTable tbody").empty();
    $.each(data, function (i, referalperson) {
        $("#myTable").append(
        $('<tr><td>' + (i + 1) + '</td><td>' + referalperson.idcase_FK +
            '</td><td>' + referalperson.idservice_FK + '</td><td>' +
            referalperson.senderevalution + '</td><td> <input type="submit" id=' + (i + 1) + ' value="delete" class="btn btn-default" onclick="deleterow(this.id)" /></td></tr>'));
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

    //var idcenter_FK = document.getElementById("idcenter_FK").value;
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
                    item.referalReciver_FK + '</td><td> <input type="submit" id=' + (i + 1) + ' value="delete" class="btn btn-default" onclick="deleterowNew(this.id)" /></td></tr>'));
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
            item.senderevalution + '</td><td> <input type="submit" id=' + (i + 1) + ' value="delete" class="btn btn-default" onclick="deleterowNew(this.id)" /></td></tr>'));
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
        url: '/referalpersons/sendReferals',
        type: "POST",
        dataType: "JSON",
        data: referals,
        success:
function (data) {
    if (data == "Success")
    {
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
    alert("Failure FailureSendReferals.............")
}


function OnFailureFillTable() {
    alert("Failure FillTable.............")

}

function FillReferalStateDropdown() {
    var servicestate_text = document.getElementById("servicestate").options[document.getElementById("servicestate").selectedIndex].text;

    var referalstate_text = document.getElementById("referalstate").options[document.getElementById("servicestate").selectedIndex].text;



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

function searchByName()
{
    var name = document.getElementById("personname").value;
    var idcase = document.getElementById("idcase_FK").value;

    if (name != "")
    {
        $.ajax({
            url: '/referalpersons/searchReferalByName',
            type: "GET",
            dataType: "JSON",
            data: { 'name': name,  'idcase': idcase },
            success: function (data) {
                $('#referalbysearch').empty()
                $.each(data, function (i, referal) {
                    $("#referalbysearch").append(
                    $('<tr></tr>')
                    .append('<td>' + referal.name + '</td>')
                    .append('<td>' + referal.submittingdate + '</td>')

                    .append('<td>' + referal.referaldate + '</td>')

                    .append('<td>' + referal.type + '</td>')

                    .append('<td>' + referal.servicestartdate + '</td>')

                    .append('<td>' + referal.serviceenddate + '</td>')

                    .append('<td>' + referal.senderevalution + '</td>')
                         .append('<td>' + referal.recieverevalution + '</td>')
                         .append('<td>' + referal.outreachnote + '</td>')

                                   .append('<td>'+
                                      '<a href="/referalpersons/Edit?idreferalperson=' + referal.idreferalperson + '&amp;idperson=' + referal.idperson +
                                      '&amp;idcase=' + referal.idcase + '">تعديل</a>' +
                                        '<a length="0" href="/referalpersons/personReferalByCaseManager/' + referal.idreferalperson + '?idcase=' + referal.idcase
                                        + '">إحالة جديدة</a>' +
                                      '</td>')
                                      

                    );
                  
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
    var idcase = document.getElementById("idcase_FK").value;

    if ( from != "" && to != "") {
        $.ajax({
            url: '/referalpersons/searchReferalByDate',
            type: "GET",
            dataType: "JSON",
            data: {  'from': from, 'to': to, 'idcase': idcase },
            success: function (data) {
                $('#referalbysearch').empty()
                $.each(data, function (i, referal) {
                    $("#referalbysearch").append(
                    $('<tr></tr>')
                    .append('<td>' + referal.name + '</td>')
                    .append('<td>' + referal.submittingdate + '</td>')

                    .append('<td>' + referal.referaldate + '</td>')

                    .append('<td>' + referal.type + '</td>')

                    .append('<td>' + referal.servicestartdate + '</td>')

                    .append('<td>' + referal.serviceenddate + '</td>')

                    .append('<td>' + referal.senderevalution + '</td>')
                         .append('<td>' + referal.recieverevalution + '</td>')
                         .append('<td>' + referal.outreachnote + '</td>')

                                   .append('<td>' +
                                      '<a href="/referalpersons/Edit?idreferalperson=' + referal.idreferalperson + '&amp;idperson=' + referal.idperson +
                                      '&amp;idcase=' + referal.idcase + '">تعديل</a>' +
                                        '<a length="0" href="/referalpersons/personReferalByCaseManager/' + referal.idreferalperson + '?idcase=' + referal.idcase
                                        + '">إحالة جديدة</a>' +
                                      '</td>')


                    );

                });

            },
            error: function (xhr, status, error) {
                // check status && error
                toastr.error("فشلة عملية البحث ")
            }
        });



    }


}


