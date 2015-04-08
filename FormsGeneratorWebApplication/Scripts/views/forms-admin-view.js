;
(function ($, form) {
    'use strict';
    //We will be using strict java script!
    $.extend(form, {
        
        settings: {
            AccordionHeader: '.ui-accordion-header',
            AccordionHeightStyle: 'content',  
        },

        init: function () {
            var $formname = $('#new-form');
            var $itemZone = $('#item-zone');
          
            this.initAccordion($itemZone);
            form.updateSlected(null);
            
            $("input[type='number']").on("click", function () {
                $(this).select();
            });

            $("input[type='text']").on("click", function () {
                $(this).select();
            });
            this.refreshAccordion($itemZone);
            $formname.on('click', '.btn-add-form-item', function () {
                form.addFormItem($itemZone, $('.form-selection-type').val());
            });
            $formname.on('change', '.form-selection-type', function () {
                form.updateSlected($(this));
            });

            $formname.on('focus', '.date', function () {
                form.initDateTimePicker($(this));
            });

            $formname.on('click', '.btn-remove-form-item', function () {
                //TODO: Add code to shift the dom
                form.removeAccordion($(this));
            });
            
        },

        initDateTimePicker: function(selectedObject){
            selectedObject.datetimepicker();
        },

        initAccordion: function (selectedObject) {
            $(selectedObject).accordion({
                header: form.settings.AccordionHeader,
                heightStyle: form.settings.AccordionHeightStyle,
                collapsible: true,
            });
        },

        refreshAccordion: function (selectedObject) {
            $(selectedObject).accordion('refresh');
        },

        removeAccordion: function (selectedObject) {


            selectedObject.parents('.ui-accordion-content').prev().slideUp('slow', function () {
                console.log('remove header');
                selectedObject.remove($(this));
            });

            selectedObject.parents('.ui-accordion-content').slideUp('slow', function (){
                console.log('remove content');
                selectedObject.remove($(this));
            });


        },

        updatePostion: function (selectedObject) {
            var active = selectedObject.accordion("option", "active");
        },
        addFormItem: function (selectedObject, url) {

            var count = selectedObject.children(form.settings.AccordionHeader).length/2;
            // var count = parseInt(form.settings.CurrentIndex);
            ///form.settings.CurrentIndex = (++count) + "";
            var addCount = parseInt($('.form-selection-count').val());
            for (var i = 0; i < addCount; ++i) {

                var subelem = $('.number-Of-sub-elements').val();
                if (subelem < 0) {
                    // $('#test').html("");
                    $('#test').load(url + '?count=' + count, function () {
                        /* When load is done */
                        selectedObject.append($(test).html());
                        form.refreshAccordion(selectedObject);
                        selectedObject.accordion({ active: count });
                    });
                } else {
                    $('#test').load(url + '?count=' + count + "&numberOfSubElements=" + subelem, function () {
                        /* When load is done */
                        selectedObject.append($(test).html());
                        form.refreshAccordion(selectedObject);
                        selectedObject.accordion({ active: count });
                    });
                }

            }

            //$.ajax({
            //    type: 'GET',
            //    url: url,
            //    cache: false,
            //    data: ({ count: count }),
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "html",
            //    sucess: function (data) {
            //        console.log("AddForIteam:");
            //        selectedObject.append(data);
            //       form.refreshAccordion(selectedObject);
            //       // selectedObject.accordion({ active: count });
            //    },
            //    error: function (jqXHR, exception,m) {
            //        if (jqXHR.status === 0) {
            //            alert('Not connect.\n Verify Network.');
            //        } else if (jqXHR.status == 404) {
            //            alert('Requested page not found. [404]');
            //        } else if (jqXHR.status == 500) {
            //            alert('Internal Server Error [500].');
            //        } else if (exception === 'parsererror') {
            //            alert('Requested JSON parse failed.');
            //        } else if (exception === 'timeout') {
            //            alert('Time out error.');
            //        } else if (exception === 'abort') {
            //            alert('Ajax request aborted.');
            //        } else {
            //            alert('Uncaught Error.\n' + jqXHR.responseText);
            //        }
            //        console.log(m.toString());
            //    }

            //});

        },

        updateSlected: function (selectedObject) {           

            if (selectedObject !=null && ( selectedObject.val().indexOf('AddRadioButton') > 0 || selectedObject.val().indexOf('AddCheckBoxes') > 0))
            {
                $('.sub-elements').show();
                $('.number-Of-sub-elements').val(1);
            }
            else
            {
                $('.sub-elements').hide();
                $('.number-Of-sub-elements').val(-1);
            }
        }

    });//extend
})(window.jQuery, window.form || (window.form = {}));
