﻿;
(function ($, form) {
    'use strict';
    //We will be using strict java script!
    $.extend(form, {

        settings: {
            
        },

        Email: [],

        init: function () {
  
            var $email = $('#Email-form');
            $email.on('click', '#email-add-btn', function () {
                form.addEmailItem();
            });
            $email.on('click', '#email-add-text', function () {
                $('#email-error-message').hide();
            });
            
        },

        addEmailItem: function () {
            var feild = $('#email-add-text').val();
            var regex = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
    
            if (!regex.test(feild)) {
                $('#email-error-message').show();
                return;
            }
            form.Email.push(feild);
            $('.email-text-area').val(" ");
            var txt = "";
            for (var i=0; i < form.Email.length; ++i) {
                if (i == 0) {
                    txt += form.Email[i];
                } else {
                   txt+=",\n"+ form.Email[i];
                }
            }
            $('.email-text-area').val(txt);

         
        },

    });//extend
})(window.jQuery, window.form || (window.form = {}));
