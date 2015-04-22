;
(function ($, form) {
    'use strict';
    //We will be using strict java script!
    $.extend(form, {

        settings: {

        },

        init: function () {
            var $form = $('#new-form');
            var $sample = $('#sample');
          
            
            // Dynamic java script command
            $sample.on('mouseover', '.classname', function () {
                samplefunc($(this)); 
            });

            $form.on('click', '#form-submit', function () {
                //var url = "http://localhost:49477/Forms/Sucess";
                //$(location).attr('href', url);
              //  $(this).Submit();
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
