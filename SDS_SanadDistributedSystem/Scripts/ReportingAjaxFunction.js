function SuccessGetReport(data) {
    var rows = "";
    var header = "";
    switch(data.submitname)
    {
        case "إعداد التقرير الشهري":
            for (var i = 0; i < data.resultus.length; i++) {
                rows += "<tr><td>" + data.resultus[i].servicename + "</td>" +
                     "<td>" + data.resultus[i].count + "</td>" +
                     "<td>" + data.resultus[i].malecount + "</td>" +
                     "<td>" + data.resultus[i].femalecount + "</td>" +
                     "<td>" + data.resultus[i].lesseighteenmalecount + "</td>" +
                     "<td>" + data.resultus[i].lesseighteenfemalecount + "</td>" +
                     "<td>" + data.resultus[i].betweenmalecount + "</td>" +
                     "<td>" + data.resultus[i].betweenfemalecount + "</td>" +
                     "<td>" + data.resultus[i].heighersixtymalecount + "</td>" +
                     "<td>" + data.resultus[i].heighersixtyfemalecount + "</td>" +
                     "<td>" + data.resultus[i].internaldisplacemenmalecount + "</td>" +
                     "<td>" + data.resultus[i].internaldisplacemenfemalecount + "</td>" +
                     "<td>" + data.resultus[i].hostcommunirtmalecount + "</td>" +
                     "<td>" + data.resultus[i].hostcommunirtfemalecount + "</td>" +
                     "<td>" + data.resultus[i].internaldisplacedreturneemalecount + "</td>" +
                     "<td>" + data.resultus[i].internaldisplacedreturnefeemalecount + "</td>" +
                     "<td>" + data.resultus[i].refugeereturningtosyriamalecount + "</td>" +
                     "<td>" + data.resultus[i].refugeereturningtosyriafemalecount + "</td>" +
                     "<td>" + data.resultus[i].refugeewantedmalecount + "</td>" +
                     "<td>" + data.resultus[i].refugeewantedfemalecount + "</td>" +
                     "<td>" + data.resultus[i].inprogressstatemalecount + "</td>" +
                     "<td>" + data.resultus[i].inprogressstatefemalecount + "</td>" +
                     "<td>" + data.resultus[i].closedstatemalecount + "</td>" +
                     "<td>" + data.resultus[i].closedstatefemalecount + "</td>" +
                     "</tr>";
            }

            $("#month-report table tbody").append(rows);
            $("#month-report").fadeIn();
        case "عدد المسجلين الجدد":
            header = "<tr>" +
                            "<th> طبيعة العائلة </th>" +
                            "<th> الجنس </th>" +
                            "<th> العمر </th>" +
                            "<th> العدد</th>" +
                        "<tr>";
            for (var i = 0; i < data.resultus.length; i++) {
                rows += "<tr><td>" + data.resultus[i].familynature + "</td>" + "<td>" + data.resultus[i].gender + "</td>" + "<td>" + data.resultus[i].age + "</td>" + "<td>" + data.resultus[i].count + " </td></tr>";
            }
            break;
        case "أكثر ثلاث عناوين مكررة":
            header = "<tr>" +
                            "<th> العنوان </th>" +
                            "<th> العدد </th>" +
                        "<tr>";
            for (var i = 0; i < data.resultus.length; i++) {
                rows += "<tr><td>" + data.resultus[i].addressname + "</td>" + "<td>" + data.resultus[i].count + "</tr>";
            }
            break;
        case "أكثر ثلاث طرق للتعرف  على المركز المجتمعي":
            header = "<tr>" +
                            "<th> طريقة التعرف </th>" +
                            "<th> العدد </th>" +
                      "<tr>";
            for (var i = 0; i < data.resultus.length; i++) {
                rows += "<tr><td>" + data.resultus[i].waysknow + "</td>" + "<td>" + data.resultus[i].count + "</tr>";
            }
            break;

    }
    
    //$("#resultreport").remove();
    //$("<div class='col-md-6' id='resultreport'>" +
    //            "<table id='All' class='display table table-bordered All' cellspacing='0'>" +
    //                "<thead>" +
    //                        header +
    //                "</thead>" +
    //                "<tbody>" +
    //                        rows +
    //                "</tbody>" +
    //            "</table>" +
    //        "</div>"
    //    ).insertAfter("#ReportForm");
}

function FaildGetReport() {
    toastr.error("حدث خطأ ما", "خطأ")
}

function BeginGetReport() {
    $("#loading").append("<i class='fa fa-spinner fa-7x fa-spin' id='load-icon'></i>");
}

function CompleteGetReport() {
    $("#load-icon").remove();
}