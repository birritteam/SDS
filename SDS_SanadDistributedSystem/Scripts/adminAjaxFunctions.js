function registerSuccess()
{
    alert("تمت إضافة المستخدم");
}

function registerFailure() {
    alert("حدث خطأ ما");
}


function createServiceSuccessed()
{
    alert("تمت إضافة الخدمة");
}

function createServiceFailed() {
    alert("حدث خطأ ما");
}



function FillServices() {
    var selectedId = $('#idcase_FK').val();
    alert("sssssssssssss" + selectedId);
    $.ajax({
        url: '/sds/services/FillServices',
        type: "GET",
        dataType: "JSON",
        data: { 'selectedId': selectedId },
        success: function (model) {
            $("#RolesID").html("");
            $.each(model, function (i, AspNetRole) {
                $("#RolesID").append(
                $('<option></option>').val(AspNetRole.Id).html(AspNetRole.Name));
            });
        },
        error: function (xhr, status, error) {
            // check status && error
            alert("Whooaaa! Something went wrong..")
        }
    });
}
