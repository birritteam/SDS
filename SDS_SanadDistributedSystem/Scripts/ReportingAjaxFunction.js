function SuccessGetReport(data) {

    var rows1 = "";
    var rows2 = "";
    var rows3 = "";
    var header;

    if (data.name == "كل المشروع")
        header = "الأعداد في الجداول التالية خاصة ب" + data.name;
    else
        header = "الأعداد في الجداول التالية خاصة بمركز " + data.name;



    for (var i = 0; i < data.servicerport.length; i++) {
        rows1 += "<tr><td>" + data.servicerport[i].type + "</td>" +
             "<td>" + data.servicerport[i].servicename + "</td>" +
             "<td>" + data.servicerport[i].count + "</td>" +
             "<td>" + data.servicerport[i].malecount + "</td>" +
             "<td>" + data.servicerport[i].femalecount + "</td>" +
             "<td>" + data.servicerport[i].lesseighteenmalecount + "</td>" +
             "<td>" + data.servicerport[i].lesseighteenfemalecount + "</td>" +
             "<td>" + data.servicerport[i].betweenmalecount + "</td>" +
             "<td>" + data.servicerport[i].betweenfemalecount + "</td>" +
             "<td>" + data.servicerport[i].heighersixtymalecount + "</td>" +
             "<td>" + data.servicerport[i].heighersixtyfemalecount + "</td>" +
             "<td>" + data.servicerport[i].internaldisplacemenmalecount + "</td>" +
             "<td>" + data.servicerport[i].internaldisplacemenfemalecount + "</td>" +
             "<td>" + data.servicerport[i].hostcommunirtmalecount + "</td>" +
             "<td>" + data.servicerport[i].hostcommunirtfemalecount + "</td>" +
             "<td>" + data.servicerport[i].internaldisplacedreturneemalecount + "</td>" +
             "<td>" + data.servicerport[i].internaldisplacedreturnefeemalecount + "</td>" +
             "<td>" + data.servicerport[i].refugeereturningtosyriamalecount + "</td>" +
             "<td>" + data.servicerport[i].refugeereturningtosyriafemalecount + "</td>" +
             "<td>" + data.servicerport[i].refugeewantedmalecount + "</td>" +
             "<td>" + data.servicerport[i].refugeewantedfemalecount + "</td>" +
             "<td>" + data.servicerport[i].inprogressstatemalecount + "</td>" +
             "<td>" + data.servicerport[i].inprogressstatefemalecount + "</td>" +
             "<td>" + data.servicerport[i].closedstatemalecount + "</td>" +
             "<td>" + data.servicerport[i].closedstatefemalecount + "</td>" +
             "</tr>";
    }

    if ( data.weaknesreport != null )
        for (var i = 0; i < data.weaknesreport.length; i++) {
            rows1 += "<tr><td>" + data.weaknesreport[i].type + "</td>" +
             "<td>" + data.weaknesreport[i].servicename + "</td>" +
            "<td>" + data.weaknesreport[i].count + "</td>" +
            "<td>" + data.weaknesreport[i].malecount + "</td>" +
            "<td>" + data.weaknesreport[i].femalecount + "</td>" +
            "<td>" + data.weaknesreport[i].lesseighteenmalecount + "</td>" +
            "<td>" + data.weaknesreport[i].lesseighteenfemalecount + "</td>" +
            "<td>" + data.weaknesreport[i].betweenmalecount + "</td>" +
            "<td>" + data.weaknesreport[i].betweenfemalecount + "</td>" +
            "<td>" + data.weaknesreport[i].heighersixtymalecount + "</td>" +
            "<td>" + data.weaknesreport[i].heighersixtyfemalecount + "</td>" +
            "<td>" + data.weaknesreport[i].internaldisplacemenmalecount + "</td>" +
            "<td>" + data.weaknesreport[i].internaldisplacemenfemalecount + "</td>" +
            "<td>" + data.weaknesreport[i].hostcommunirtmalecount + "</td>" +
            "<td>" + data.weaknesreport[i].hostcommunirtfemalecount + "</td>" +
            "<td>" + data.weaknesreport[i].internaldisplacedreturneemalecount + "</td>" +
            "<td>" + data.weaknesreport[i].internaldisplacedreturnefeemalecount + "</td>" +
            "<td>" + data.weaknesreport[i].refugeereturningtosyriamalecount + "</td>" +
            "<td>" + data.weaknesreport[i].refugeereturningtosyriafemalecount + "</td>" +
            "<td>" + data.weaknesreport[i].refugeewantedmalecount + "</td>" +
            "<td>" + data.weaknesreport[i].refugeewantedfemalecount + "</td>" +
            "<td>" + data.weaknesreport[i].inprogressstatemalecount + "</td>" +
            "<td>" + data.weaknesreport[i].inprogressstatefemalecount + "</td>" +
            "<td>" + data.weaknesreport[i].closedstatemalecount + "</td>" +
            "<td>" + data.weaknesreport[i].closedstatefemalecount + "</td>" +
            "</tr>";
        }
  
    if ( data.threewaysreport != null )
        for (var i = 0; i < data.threewaysreport.length; i++) {
            rows2 += "<tr><td>" + data.threewaysreport[i].waysknow + "</td>" + "<td>" + data.threewaysreport[i].count + "</td></tr>";
        }
    if ( data.threeaddresreport != null )
    for (var i = 0; i < data.threeaddresreport.length; i++) {
        rows3 += "<tr><td>" + data.threeaddresreport[i].addressname + "</td>" + "<td>" + data.threeaddresreport[i].count + " </td></tr>";
    }

    $("#report").slideUp(1000);
    $("#report-header").text("");
    $("#service-report tr:not('.header-table')").remove();
    $("#ways-report tr:not('.header-table')").remove();
    $("#address-report tr:not('.header-table')").remove();

    $("#report-header").text(header);
    $("#service-report").append(rows1);
    $("#ways-report").append(rows2);
    $("#address-report").append(rows3);

    $("#report").slideDown(1000);
    //break;


    //break;



    //}

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

//$("#exporttoexcel").click(function () {
//    $("#All").table2excel({
//        filename: "Your_File_Name.xls"
//    });
//})
//;

$(function () {
    $("#exporttoexcel").click(function () {
        var d = new Date();

        var month = d.getMonth() + 1;
        var day = d.getDate();

        var output = d.getFullYear() + '/' +
            (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day;


        $("#service-report").table2excel({
            filename: "report" + output + ".xls"
        });
    });


});