function referalClosedSuccessfully(data){
    toastr.success("تم إغلاق الحالة " + data.idperson);
    //toastr.info(data.idperson)
    $("#closeReferal"+data.idperson).addClass("hidden");
    $("#openReferal" + data.idperson).removeClass("hidden");
    $("#serviceState" + data.idperson).html(data.servicestate);
    $("#serviceState" + data.idperson).addClass("text-danger");
    $("#" + data.idperson + "row").addClass("grey lighten-3 grey-text");
}

function referalOpenedSuccessfully(data) {
    toastr.success( "تم إعادة فتح الحالة " + data.idperson);
    //toastr.info(data.idperson)
    $("#openReferal" + data.idperson).addClass("hidden");
    $("#closeReferal" + data.idperson).removeClass("hidden");
    $("#serviceState" + data.idperson).html(data.servicestate);
    $("#serviceState" + data.idperson).removeClass("text-danger");
    $("#" + data.idperson + "row").removeClass("grey lighten-3 grey-text");
}

function referalFailedToOpen() {
    toastr.error("فشلت عملية فتح الحالة");
}

function referalFailedToClose() {
    toastr.error("فشلت عملية إغلاق الحالة");
}