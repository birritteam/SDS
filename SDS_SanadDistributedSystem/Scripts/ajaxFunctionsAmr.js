function FillServices() {
    var selectedId = $('#idcase_FK').val();
    alert("sssssssssssss" + selectedId);
    $.ajax({
        url: '/referalpersons/FillServices',
        type: "GET",
        dataType: "JSON",
        data: { 'selectedId': selectedId },
        success: function (model) {
            $("#idservice_FK").html("");
            $.each(model, function (i, service) {
                $("#idservice_FK").append(
                $('<option></option>').val(service.idservice).html(service.name));
            });
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
    var idperson_FK = document.getElementById("idperson_FK").value;
    var idperson_FK_text = document.getElementById("idperson_FK").options[document.getElementById("idperson_FK").selectedIndex].text;

    var idcase_FK = document.getElementById("idcase_FK").value;
    var idcase_FK_text = document.getElementById("idcase_FK").options[document.getElementById("idcase_FK").selectedIndex].text;

    var selectedId = $('#idcase_FK').val();
    if (selectedId == 1) {
        var idservice_FK = document.getElementById("cmEducation").value;
        var idservice_FK_text = document.getElementById("cmEducation").options[document.getElementById("cmEducation").selectedIndex].text;
    }
    else if (selectedId == 2) {
        var idservice_FK = document.getElementById("cmProfessional").value;
        var idservice_FK_text = document.getElementById("cmProfessional").options[document.getElementById("cmProfessional").selectedIndex].text;
    }
    else if (selectedId == 3) {
        var idservice_FK = document.getElementById("cmChildProtection").value;
        var idservice_FK_text = document.getElementById("cmChildProtection").options[document.getElementById("cmChildProtection").selectedIndex].text;
    }
    else if (selectedId == 4) {
        var idservice_FK = document.getElementById("cmPsychologicalSupport1").value;
        var idservice_FK_text = document.getElementById("cmPsychologicalSupport1").options[document.getElementById("cmPsychologicalSupport1").selectedIndex].text;

    }
    else if (selectedId == 5) {
        var idservice_FK = document.getElementById("cmPsychologicalSupport2").value;
        var idservice_FK_text = document.getElementById("cmPsychologicalSupport2").options[document.getElementById("cmPsychologicalSupport2").selectedIndex].text;
    }
    else if (selectedId == 6) {
        var idservice_FK = document.getElementById("cmPsychologicalSupport3").value;
        var idservice_FK_text = document.getElementById("cmPsychologicalSupport3").options[document.getElementById("cmPsychologicalSupport3").selectedIndex].text;


    }
    else if (selectedId == 7) {
        var idservice_FK = document.getElementById("cmDayCare").value;
        var idservice_FK_text = document.getElementById("cmDayCare").options[document.getElementById("cmDayCare").selectedIndex].text;
    }
    else if (selectedId == 8) {
        var idservice_FK = document.getElementById("cmHomeCare").value;
        var idservice_FK_text = document.getElementById("cmHomeCare").options[document.getElementById("cmHomeCare").selectedIndex].text;


    }
    else if (selectedId == 9) {
        var idservice_FK = document.getElementById("cmSGBV").value;
        var idservice_FK_text = document.getElementById("cmSGBV").options[document.getElementById("cmSGBV").selectedIndex].text;
    }
    else if (selectedId == 10) {
        var idservice_FK = document.getElementById("cmSmallProjects").value;
        var idservice_FK_text = document.getElementById("cmSmallProjects").options[document.getElementById("cmSmallProjects").selectedIndex].text;
    }
    else if (selectedId == 11) {
        var idservice_FK = document.getElementById("cmIOutReachTeam").value;
        var idservice_FK_text = document.getElementById("cmIOutReachTeam").options[document.getElementById("cmIOutReachTeam").selectedIndex].text;
    }
    else if (selectedId == 12) {
        var idservice_FK = document.getElementById("cmInkindAssistance").value;
        var idservice_FK_text = document.getElementById("cmInkindAssistance").options[document.getElementById("cmInkindAssistance").selectedIndex].text;
    }
    //----------------------------------------
    var submittingdate = document.getElementById("submittingdate").value;

    //var referalstate = document.getElementById("referalstate").value;
    //var referalstate_text = document.getElementById("referalstate").options[document.getElementById("referalstate").selectedIndex].text;

    var referaldate;

    //var servicestate = document.getElementById("servicestate").value;
    //var servicestate_text = document.getElementById("servicestate").options[document.getElementById("servicestate").selectedIndex].text;

    var servicestartdate;

    var serviceenddate;

    var referalsender_FK = document.getElementById("referalsender_FK").value;
    var referalsender_FK_text = document.getElementById("referalsender_FK").options[document.getElementById("referalsender_FK").selectedIndex].text;

    var senderevalution = document.getElementById("senderevalution").value;

    var recieverevalution;

    var idcenter_FK = document.getElementById("idcenter_FK").value;
    var idcenter_FK_text = document.getElementById("idcenter_FK").options[document.getElementById("idcenter_FK").selectedIndex].text;

    var outreachnote = document.getElementById("outreachnote").value;
    var obj = {
        "idperson_FK": idperson_FK,
        "idcase_FK": idcase_FK, "idservice_FK": idservice_FK, "submittingdate": submittingdate, 
         "referalsender_FK": referalsender_FK, "senderevalution": senderevalution, "idcenter_FK": idcenter_FK, "outreachnote": outreachnote
    };

    var obj_text = {
        "idperson_FK": idperson_FK_text,
        "idcase_FK": idcase_FK_text, "idservice_FK": idservice_FK_text, "submittingdate": submittingdate,
         "referalsender_FK": referalsender_FK_text, "senderevalution": senderevalution, "idcenter_FK": idcenter_FK_text, "outreachnote": outreachnote
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
    if (!checkexist && !checkundefined) {
        personReferal.push(obj);
        personReferal_text.push(obj_text);
        alert("successFillTableNew............." + obj.idcase_FK)
        $("#myTable tbody").empty();
        personReferal_text.forEach(function (item, i, array) {
            //console.log(item, i);
            $("#myTable").append(
            $('<tr><td>' + (i + 1) + '</td><td>' + item.idcase_FK +
                '</td><td>' + item.idservice_FK + '</td><td>' +
                item.senderevalution + '</td><td> <input type="submit" id=' + (i + 1) + ' value="delete" class="btn btn-default" onclick="deleterowNew(this.id)" /></td></tr>'));
        });
        // BindItemTable();
        alert("successFillTableNew........end .")
    }
    else {
        if (checkexist)
            alert("already exist.....")
        else
            alert(" لا يمكن")
    }
}

function deleterowNew(index) {
    alert(index);
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
        contentType: 'application/json; charset=utf-8',
        url: '/referalpersons/sendReferals',
        type: "POST",
        dataType: "JSON",
        data: referals,
        success:
function (data) {
    alert(data)
    $("#myTable tbody").empty();
    personReferal = [];
    personReferal_text = [];
},

        error: function (xhr, status, error) {
            // check status && error
            alert("fail send referals............")
        }
    });



}
function OnFailureSendReferals() {
    alert("Failure FailureSendReferals.............")
}


function OnFailureFillTable() {
    alert("Failure FillTable.............")

}


