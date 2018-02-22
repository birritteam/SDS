function SuccessGetReport(data) {
    var rows = "";
    var header = "";
    switch(data.submitname)
    {
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
    
    $("#resultreport").remove();
    $("<div class='col-md-6' id='resultreport'>" +
                "<table id='All' class='display table table-bordered All' cellspacing='0'>" +
                    "<thead>" +
                            header +
                    "</thead>" +
                    "<tbody>" +
                            rows +
                    "</tbody>" +
                "</table>" +
            "</div>"
        ).insertAfter("#ReportForm");
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