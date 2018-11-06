
$(document).ready(function () {
    var table = $('.All').DataTable({
        dom: "<'row'<'col-md-6'f><'col-md-3'l><'col-md-3'B>>" +
             "<'row'<'col-md-12'th>>" +
               "<'row'<'col-md-12'tr>>" +
             "<'row'<'col-md-7'p><'col-md-5'i>>",

        buttons: [{
            extend: 'print',
            exportOptions: {
                columns: ':visible'
            },
            text: "طباعة <i class='fa fa-print'></i>",
            key: {
                key: "p",
                altkey: true
            },
        },
        {
            extend: 'copy',
            text: "نسخ <i class='fa fa fa-copy'></i>",
            key: {
                key: "c",
                altkey: true
            }
        }, {
            extend: 'excel',
            exportOptions: {
                columns: ':visible'
            },
            text: "تصدير إلى Excel <i class='fa fa fa-file-excel-o'></i>",
            key: {
                key: "e",
                altkey: true
            }
        }
        //{
        //    extend: 'pdf',
        //    text: "تصدير إلى PDF <i class='fa fa fa-file-pdf-o'></i>"
        //}
        ],
        "scrollX": false,
      

        "lengthMenu": [[25, 50, 75, 100, -1], [25,50, 75, 100, "الكل"]],
       // "columnDefs": [
       ////{ "targets": [0], "visible": false }
       // ],
        language: {

            "decimal": "",
            "emptyTable": "لا يوجد أي بيانات لعرضها ضمن الجدول",
            "info": "إظهار _START_ إلى _END_ من أصل _TOTAL_ مُدخلات",
            "infoEmpty": "إظهار 0 إلى 0 من أصل 0 مُدخلات",
            "infoFiltered": "(تم البحث في   _MAX_  عناصر )",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "إظهار _MENU_ سجل",
            "loadingRecords": "... جارٍ التحميل",
            "processing": "... جارٍ المعالجة",
            "search": " ابحث: ",
            "zeroRecords": "لا يوجد أي تطابق من السجلات الموجودة",
            "paginate": {
                "first": "الأول",
                "last": "الأخير",
                "next": "التالي",
                "previous": "السابق"
            },
      //      "columnDefs": [
      //{ "width": "10px", "targets": 0 },
      //{ "width": "40px", "targets": 1 },
      //{ "width": "100px", "targets": 2 },
      //{ "width": "70px", "targets": 3 },
      //{ "width": "120px", "targets": 4 },
      //{ "width": "440px", "targets": 5 },
      //   { "width": "120px", "targets": 6 }, { "width": "100px", "targets": 7 }, { "width": "100px", "targets": 8 }, { "width": "100px", "targets": 9 },
            //      ],
            //      "columnDefs": [
            //{ "width": "120px", "targets": 10 },
            //      ],
            select: {

                rows: {
                    _: "  %d أسطر تم تحديدها",
                    0: "  انقر لتحديد سطر",
                    1: "  سطر واحد محدد",
                }
            },

            //"url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Arabic.json",

        },

        select: false
    });


    var table = $('.ReportTable').DataTable({
        dom: "<'row'<'col-md-6'f><'col-md-3'l><'col-md-3'B>>" +
             "<'row'<'col-md-12'th>>" +
               "<'row'<'col-md-12'tr>>" +
             "<'row'<'col-md-7'p><'col-md-5'i>>",

        buttons: [{
            extend: 'print',
            exportOptions: {
                columns: ':visible'
            },
            text: "طباعة <i class='fa fa-print'></i>",
            key: {
                key: "p",
                altkey: true
            },
        },
        {
            extend: 'copy',
            text: "نسخ <i class='fa fa fa-copy'></i>",
            key: {
                key: "c",
                altkey: true
            }
        }, {
            extend: 'excel',
            exportOptions: {
                columns: ':visible'
            },
            text: "تصدير إلى Excel <i class='fa fa fa-file-excel-o'></i>",
            key: {
                key: "e",
                altkey: true
            }
        }
        //{
        //    extend: 'pdf',
        //    text: "تصدير إلى PDF <i class='fa fa fa-file-pdf-o'></i>"
        //}
        ],
        "scrollX": false,



        "lengthMenu": [[25,50, 75, 100, -1], [25,50, 75, 100, "الكل"]],
        "columnDefs": [
       //{ "targets": [0], "visible": false }
        ],
        language: {

            "decimal": "",
            "emptyTable": "لا يوجد أي بيانات لعرضها ضمن الجدول",
            "info": "إظهار _START_ إلى _END_ من أصل _TOTAL_ مُدخلات",
            "infoEmpty": "إظهار 0 إلى 0 من أصل 0 مُدخلات",
            "infoFiltered": "(تم البحث في   _MAX_  عناصر )",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "إظهار _MENU_ سجل",
            "loadingRecords": "... جارٍ التحميل",
            "processing": "... جارٍ المعالجة",
            "search": " ابحث: ",
            "zeroRecords": "لا يوجد أي تطابق من السجلات الموجودة",
            "paginate": {
                "first": "الأول",
                "last": "الأخير",
                "next": "التالي",
                "previous": "السابق"
            },

            select: {

                rows: {
                    _: "  %d أسطر تم تحديدها",
                    0: "  انقر لتحديد سطر",
                    1: "  سطر واحد محدد",
                }
            },

            //"url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Arabic.json",

        },

        select: true
    });

 

    //$("th").addClass('dt-head-nowrap');

    //var counter = 0;

    //// links for All datatables
    //var linkE = $(".linkE").attr("href");
    //var linkD = $(".linkD").attr("href");
    //var linkR = $(".linkR").attr("href");

    ////links for index Cource 
    //var EmpAttendCource = $(".EmdAttendCource").attr("href");

    //$("#All tbody").on('click', 'tr', function () {
    //    $(".afterhead").remove();
    //    var idx = table.row(this).index();
    //    ID = table.cell(idx, 0).data();
    //    StringToView = table.cell(idx, 1).data();
    //    $(".head").after("<div class='col-sm-12 text-center afterhead' style='margin:15px'>"
    //                    + "<span class='alert alert-info'> لقد قمت بتحديد السجل : " + StringToView + "</span></div>")
    //    // links for All datatables
    //    $(".linkE").attr("href", linkE.replace("Rid", ID))
    //    $(".linkD").attr("href", linkD.replace("Rid", ID))
    //    $(".linkR").attr("href", linkR.replace("Rid", ID))

    //    //links for index Cource 
    //    $(".EmdAttendCource").attr("href", EmpAttendCource.replace("Rid", ID));
    //    counter++;
    //});


    //$(".linkE, .linkD, .linkR, .EmdAttendCource").click(function (e) {
    //    if (counter == 0) {
    //        e.preventDefault();
    //        $.notify({
    //            // options
    //            icon: 'glyphicon glyphicon-warning-sign',
    //            title: '',
    //            message: "يجب اختيار سجل من الجدول",
    //            target: '_blank'
    //        }, {
    //            // settings
    //            element: 'body',
    //            position: null,
    //            type: "danger",
    //            allow_dismiss: true,
    //            newest_on_top: false,
    //            showProgressbar: false,
    //            placement: {
    //                from: "top",
    //                align: "center"
    //            },
    //            offset: 20,
    //            spacing: 10,
    //            z_index: 1031,
    //            delay: 5000,
    //            timer: 1000,
    //            url_target: '_blank',
    //            mouse_over: null,
    //            animate: {
    //                enter: 'animated fadeInDown',
    //                exit: 'animated fadeOutUp'
    //            },
    //            onShow: null,
    //            onShown: null,
    //            onClose: null,
    //            onClosed: null,
    //            icon_type: 'class',
    //            template: '<div data-notify="container" class="col-xs-12 col-sm-6 text-center alert alert-{0}" role="alert">' +
    //                '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
    //                '<h4><span data-notify="icon"></span> ' +
    //                '<span data-notify="title">{1}</span> ' +
    //                '<span data-notify="message">{2}</span></h4>' +
    //                '<div class="progress" data-notify="progressbar">' +
    //                    '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
    //                '</div>' +
    //                '<a href="{3}" target="{4}" data-notify="url"></a>' +
    //            '</div>'
    //        });
    //    }
    //})
});

$(document).ready(function () {
    var table = $('.All2').DataTable({
        dom: "<'row'<'col-md-6'f><'col-md-3'l><'col-md-3'B>>" +
             "<'row'<'col-md-12'th>>" +
               "<'row'<'col-md-12'tr>>" +
             "<'row'<'col-md-7'p><'col-md-5'i>>",

        buttons: [{
            extend: 'print',
            exportOptions: {
                columns: ':visible'
            },
            text: "طباعة <i class='fa fa-print'></i>",
            key: {
                key: "p",
                altkey: true
            },
        },
        {
            extend: 'copy',
            text: "نسخ <i class='fa fa fa-copy'></i>",
            key: {
                key: "c",
                altkey: true
            }
        }, {
            extend: 'excel',
            exportOptions: {
                columns: ':visible'
            },
            text: "تصدير إلى Excel <i class='fa fa fa-file-excel-o'></i>",
            key: {
                key: "e",
                altkey: true
            }
        }
        //{
        //    extend: 'pdf',
        //    text: "تصدير إلى PDF <i class='fa fa fa-file-pdf-o'></i>"
        //}
        ],
        "sScrollX": "100%",



        "lengthMenu": [[25, 50, 75, 100, -1], [25, 50, 75, 100, "الكل"]],
        // "columnDefs": [
        ////{ "targets": [0], "visible": false }
        // ],
        language: {

            "decimal": "",
            "emptyTable": "لا يوجد أي بيانات لعرضها ضمن الجدول",
            "info": "إظهار _START_ إلى _END_ من أصل _TOTAL_ مُدخلات",
            "infoEmpty": "إظهار 0 إلى 0 من أصل 0 مُدخلات",
            "infoFiltered": "(تم البحث في   _MAX_  عناصر )",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "إظهار _MENU_ سجل",
            "loadingRecords": "... جارٍ التحميل",
            "processing": "... جارٍ المعالجة",
            "search": " ابحث: ",
            "zeroRecords": "لا يوجد أي تطابق من السجلات الموجودة",
            "paginate": {
                "first": "الأول",
                "last": "الأخير",
                "next": "التالي",
                "previous": "السابق"
            },
            //      "columnDefs": [
            //{ "width": "10px", "targets": 0 },
            //{ "width": "40px", "targets": 1 },
            //{ "width": "100px", "targets": 2 },
            //{ "width": "70px", "targets": 3 },
            //{ "width": "120px", "targets": 4 },
            //{ "width": "440px", "targets": 5 },
            //   { "width": "120px", "targets": 6 }, { "width": "100px", "targets": 7 }, { "width": "100px", "targets": 8 }, { "width": "100px", "targets": 9 },
            //      ],
            //      "columnDefs": [
            //{ "width": "120px", "targets": 10 },
            //      ],
            select: {

                rows: {
                    _: "  %d أسطر تم تحديدها",
                    0: "  انقر لتحديد سطر",
                    1: "  سطر واحد محدد",
                }
            },

            //"url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Arabic.json",

        },

        select: false
    });

});
