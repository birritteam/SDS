﻿$(function () {
    // Click on notification icon
    $('span.noti').click(function (e) {

        e.stopPropagation();
        $('.noti-content').show();
        var count = 0;
        count = parseInt($('span.count').html()) || 0;
        //only load notifcation if not alerady loaded

        if (count > 0) {
            updateNotification();
        }
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
                alert("dasdad")
                $('#notiContent').empty();
                if (response.length == 0) {
                    $("#notiContent").append($('<li> لا يوجد أي إشعارات</li>'));

                }
                $.each(response, function (index, value) {
                    //var contactid = value.Contactid;
                    $('#notiContent').append($("<li> <a href='/Contacts/Details/" + value.personname + "' target='_blank'> New contact: '" + value.serviceType + "' ('" + value.serviceType + "') </a> </li>"));
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

    }
    var notificationHub = $.connection.notificationHub;
    $.connection.hub.start().done(function () {
        console.log('Notification hub stareted')
    });

    //signalr method for push server message to client
    notificationHub.client.notify = function (message, username) {
        var loginusername = $('#loginusername').html();
        if (message && message.toLowerCase() == "added" && username == loginusername) {
            updateNotificationCount();
        }

    }
})