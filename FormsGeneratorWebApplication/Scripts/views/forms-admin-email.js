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
            var $document = $(document);
            //$email.on('click', '#email-add-btnsf', function () {
            //    form.addEmailItem();
            //});
            //$email.on('click', '#email-add-text', function () {
            //    $('#email-error-message').hide();
            //});

            $email.on('click', '#email-submit-btn', function () {
                form.sub();
            });


            //$email.on('click', '#email-remove-btn', function () {
            //    form.removeEmailItem();
            //});


            $document.on('click', '.btn-add', function (e) {
                form.addEmail($(this),e);
               
            });

            $document.on('click', '.btn-remove', function (e) {
                form.removeEmail($(this),e);

            });


            
        },
        
        addEmail: function(selectedIteam,e){
              e.preventDefault();

                $("input[name='fields[]']").each(function () {
                    form.Email.push(selectedIteam.val());
                });
       
                if (!form.emailValidation()) {
                    return;
                }
                else {
                    var controlForm = $('.controls form:first'),
                            currentEntry = selectedIteam.parents('.entry:first'),
                            newEntry = $(currentEntry.clone()).appendTo(controlForm);

                    newEntry.find('input').val('');
                    controlForm.find('.entry:not(:last) .btn-add')
                        .removeClass('btn-add').addClass('btn-remove')
                        .removeClass('btn-success').addClass('btn-danger')
                        .html('<span class="glyphicon glyphicon-minus"></span>');


                    $('#email-error-message').hide();
                }
            
             
        },

        removeEmail: function(selectedIteam,e){      
                 e.preventDefault();

                 selectedIteam.parents('.entry:first').remove();
             
        },
        
        sub: function () {
            $("input[name='fields[]']").each(function () {
                form.Email.push($(this).val());
            });
            var txt = "";
            for (var i = 0; i < form.Email.length; ++i) {
                if (i == 0) {
                    txt += form.Email[i];
                } else {
                    txt += ",\n" + form.Email[i];
                }
            }
            $('#email-recp').val(txt);

            $('#Email-form').submit();
        },
        emailValidation: function () {
            var feild = [];
            $("input[name='fields[]']").each(function () {
                feild.push($(this).val());
            });
            var regex = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
            for (var i = 0; i < feild.length; ++i) {
                if (!regex.test(feild[i])) {
                    $('#email-error-message').show();
                    return false;
                }
            }
            return true;
        },

        //removeEmailItem: function () {
        //    var emailToRemove = $('#email-remove-text').val();
        //    var index = form.Email.indexOf(emailToRemove);
        //    if (index > -1) {
        //        form.Email.splice(index, 1);


                //var txt = "";
                //for (var i = 0; i < form.Email.length; ++i) {
                //    if (i == 0) {
                //        txt += form.Email[i];
                //    } else {
                //        txt += ",\n" + form.Email[i];
                //    }
                //}
                //$('.email-text-area').val(txt);
        //    }
        //}

    });//extend
})(window.jQuery, window.form || (window.form = {}));
