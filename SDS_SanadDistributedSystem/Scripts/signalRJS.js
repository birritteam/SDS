$(function () {
    // Click on notification icon
    $('span.noti').click(function (e) {

        e.stopPropagation();
        $('.noti-content').show();
        var count = 0;
        count = parseInt($('span.count').html()) || 0;
        //only load notifcation if not alerady loaded

        //if (count > 0) {
            updateNotification();
        //}
        $('span.count', this).html('&nbsp;')

    });
    // hide notifications		



    $('html').click(function (e) {

        if (!$(e.target).hasClass('noti-content'))
            $('.noti-content').hide();

    });
    // update notification
    function updateNotification() {
        $("#notiContent").empty();
        $("#notiContent").append($('<li> ... جاري التحميل </li>'));

        $.ajax({
            type: 'GET',
            url: '/sds/referalpersons/GetNotificationsReferal',
            success: function (response) {
                //alert("dasdad")
                $('#notiContent').empty();
                if (response.length == 0) {
                    $("#notiContent").append($('<li> لا يوجد إحالات جديدة لك</li>'));

                }
                $.each(response, function (index, value) {
                    //var contactid = value.Contactid;/sds/referalpersons/Edit?idreferalperson=30&idperson=12345C3&idcase=4
                    $('#notiContent').append($("<a class='col-md-12 list-group-item' href='/sds/referalpersons/Edit?idreferalperson=" + value.idreferalperson + "&idperson=" + value.idperson_FK + "&idcase=" + value.idcase_FK + "' target='_blank'>" +
                                                 "<div class='col-md-2'> <i class='fa fa-newspaper-o fa-lg' aria-hidden='true'></i></div> " +
                                               "<div class='col-md-10'> <p style='font-size:12px'> تمت إحالة " + value.personname + " إلى خدمة " + value.serviceType + " من قبل " + value.senderuserrole + " </p>  <small style='float: left; font-size:10px;'> <i class='fa fa-home fa-lg' aria-hidden='true'></i> " + value.center + " - <i class='fa fa-calendar' aria-hidden='true'></i> " + value.referaldate.toString() + "</small> </div>" +
                                               "</div></a> "));
                });
            },

            error: function (error) {
                console.log(error);
            }
        })
    }

    // update notifcation count
    function updateNotificationCount() {
        var count = 0;
        count = parseInt($('span.count').html()) || 0;
        count++;
        $('span.count').html(count);
        $.playSound("http://www.noiseaddicts.com/samples_1w72b820/3724.mp3");

    }
    var notificationHub = $.connection.notificationHub;
    $.connection.hub.start().done(function () {
        console.log('Notification hub stareted' + $.connection.hub.id)
       
    });

    //signalr method for push server message to client
    notificationHub.client.notify = function (message, username) {
        var loginusername = $('#loginusername').html();
        if (message && message.toLowerCase() == "added" && username == loginusername) {
            updateNotificationCount();
            toastr.success('لقد وصلتك إحالة جديدة .. من فضلك راجع الإشعارات');
        }

    }
})