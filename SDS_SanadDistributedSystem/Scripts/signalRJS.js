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
            url: '/referalpersons/GetNotificationsReferal',
            success: function (response) {
                //alert("dasdad")
                $('#notiContent').empty();
                if (response.data.length == 0) {
                    $("#notiContent").append($('<li> لا يوجد إحالات جديدة لك</li>'));

                }
                
                    //var contactid = value.Contactid;/sds/referalpersons/Edit?idreferalperson=30&idperson=12345C3&idcase=4
                    //Edit?idreferalperson=" + value.idreferalperson + "&idperson=" + value.idperson_FK + "&idcase=" + value.idcase_FK + 

                    if (response.validate == "outreach volunteer")
                        for (var i = 0; i < response.data.length; i++) {
                            $('#notiContent').append($("<a class='col-md-12 list-group-item' href='/sds/referalpersons/IndexOutReach' target='_Self'>" +
                                                "<div class='col-md-2'> <i class='fa fa-newspaper-o fa-lg' aria-hidden='true'></i></div> " +
                                              "<div class='col-md-10'> <p style='font-size:12px'> تمت إضافة   " + response.data[i].personname + " إلى جدول البيانات الغير مستكملة من قبل " + response.data[i].senderuserrole + " </p>  <small style='float: left; font-size:10px;'> <i class='fa fa-home fa-lg' aria-hidden='true'></i> " + response.data[i].center + " - <i class='fa fa-calendar' aria-hidden='true'></i> " + response.data[i].referaldate.toString() + "</small> </div>" +
                                              "</div></a> "));

                        }
                        
                    else
                        for (var i = 0; i < response.data.length; i++) {
                            $('#notiContent').append($("<a class='col-md-12 list-group-item' href='/sds/referalpersons/index' target='_Self'>" +
                                                     "<div class='col-md-2'> <i class='fa fa-newspaper-o fa-lg' aria-hidden='true'></i></div> " +
                                                   "<div class='col-md-10'> <p style='font-size:12px'> تمت إحالة " + response.data[i].personname + " إلى خدمة " + response.data[i].serviceType + " من قبل " + response.data[i].senderuserrole + " </p>  <small style='float: left; font-size:10px;'> <i class='fa fa-home fa-lg' aria-hidden='true'></i> " + response.data[i].center + " - <i class='fa fa-calendar' aria-hidden='true'></i> " + response.data[i].referaldate.toString() + "</small> </div>" +
                                                   "</div></a> "));
                        }

                       
               
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
        // $.playSound("http://www.noiseaddicts.com/samples_1w72b820/3724.mp3");
         $.playSound("/Sound/slow-spring-board.mp3");

    }

    /*
    
     مشكلة إرسال الإشعارات انحلت من خلال 
     https://stackoverflow.com/questions/14072223/signalr-js-client-methods-not-invoked
    */
    //signalr method for push server message to client

    var notificationHub = $.connection.notificationHub;
    notificationHub.client.notify = function (message, username) {
        //var loginusername = $('#loginusername').html();
        if (message && message.toLowerCase() == "added") {
            updateNotificationCount();
            toastr.success('لقد وصلتك إحالة جديدة .. من فضلك راجع الإشعارات');
        }
        else if (message && message.toLowerCase() == "added outreach") {
            updateNotificationCount();
            toastr.success('لقد وصلتك إحالة ببيانات غير مستكملة .. من فضلك راجع الإشعارات');
        }

    }


    $.connection.hub.start().done(function () {
        console.log('Notification hub stareted ' + $.connection.hub.id)

    });


})