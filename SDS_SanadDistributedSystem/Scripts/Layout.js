// woo initialize
new WOW().init();
// SideNav init
$(".button-collapse").sideNav();


// Custom scrollbar init
//var el = document.querySelector('.custom-scrollbar');
//Ps.initialize(el);


// Date Time Picker Init
$('.datepicker').pickadate();


// select Init
$(document).ready(function () {
    //$('.mdb-select').material_select();

    $('select').prop('class', 'selectpicker');
 //   $('select').attr('data-live-search', true);
    $('.selectpicker').selecxxtpicker({
        style: 'btn-info',
        size: 4
    }

 );

});


// alert optios Init
toastr.options = {
    "closeButton": true, // true/false
    "debug": true, // true/false
    "newestOnTop": false, // true/false
    "progressBar": false, // true/false
    "positionClass": "toast-bottom-right", // toast-top-right / toast-top-left / toast-bottom-right / toast-bottom-left
    "preventDuplicates": false, //true,false
    "onclick": null,
    "showDuration": "300", // in milliseconds
    "hideDuration": "1000", // in milliseconds
    "timeOut": "5000", // in milliseconds
    "extendedTimeOut": "1000", // in milliseconds
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}


$('.selectpicker').selectpicker(
    //style: 'btn-info',
    //size: 4
);

if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
    $('.selectpicker').selectpicker('mobile');
}

