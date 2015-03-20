;
(function ($, form) {
    'use strict';
    //We will be using strict java script!
    $.extend(form, {

        settings: {

        },

        init: function () {
            var $form = $('#form');
            var $sample = $('#sample');
          
            
            // Dynamic java script command
            $sample.on('mouseover', '.classname', function () {
                samplefunc($(this)); 
            });

            $form.on('click', '.btn', function () { 
               
            });
        },
        //Example of a function
        samplefunc: function(selectedObject){
            
        },
        showModle: function(selectedObject){
            $("#myModle").modal('show');
        }

    });//extend
})(window.jQuery, window.form || (window.form = {}));

jQuery(function () {
    form.init();
});

//THIS IS BAD CODE DONT DO THIS!!!! IF I SEE THIS I WILL DELETE THIS USE THE ABOVE PATTERN
//$(document).ready(function () {
//    $(".btn").click(function () {
//        $("#myModal").modal('show');
//    });
//});
