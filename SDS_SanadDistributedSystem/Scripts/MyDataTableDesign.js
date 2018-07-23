
$(document).ready(function () {
    var table = $('.MyDataTable').DataTable(
        {
            //"lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "الكل"]],

            "lengthMenu": [[5, 10, 25, 50, 100,200], [5, 10, 25, 50,100,200]],
            deferRender: true,
            scrollY: 260,
            scrollCollapse: true,
            scroller: true,

            "dom":
                "<'row'<'col-sm-12 col-md-6'l><'col-sm-12 col-md-6'f>>" +
                "<'row'<'col-sm-12'tr>>" +
                "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>"
            ,
            //"order": [[3, "desc"]],

            columnDefs: [{
                targets: [-1],
                "orderable": false,
                "searchable": false,
                //"width": "250px"
            }],


            "language": {

                "decimal": "",
                "emptyTable": "لا يوجد أي بيانات لعرضها ضمن الجدول",
                "info": "إظهار _START_ إلى _END_ من أصل _TOTAL_ سجل",
                "infoEmpty": "إظهار 0 إلى 0 من أصل 0 سجل",
                "infoFiltered": "(تم البحث في   _MAX_  عناصر )",
                "infoPostFix": "",
                "thousands": ",",
                "lengthMenu": "إظهار _MENU_ سجل",
                "loadingRecords": "... جارٍ التحميل",
                "processing": "... جارٍ المعالجة",
                "search": "بحث   ",
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


            },

            select: true
        });

});