;
(function ($, form) {
    'use strict';
    //We will be using strict java script!
    $.extend(form, {

        settings: {
            AccordionHeader: '.ui-accordion-header',
            AccordionHeightStyle:  'content',
        },        

        init: function () {
            var $formname = $('#new-form');
            var $itemZone = $('#item-zone');

            this.initAccordion($itemZone);
            this.addFormItem($itemZone, form.settings.UrlAddTextBox);
      
            this.refreshAccordion($itemZone);
            $formname.on('click', '.btn-add-form-item', function () {

                form.addFormItem($itemZone, $('.form-selection-type').val());
            });
           
        },
   
        initAccordion: function(selectedObject) {
            
            $(selectedObject).accordion({
                header: form.settings.AccordionHeader,
                heightStyle: form.settings.AccordionHeightStyle,
                collapsible: true,
            });
        },

        refreshAccordion: function(selectedObject){
            $(selectedObject).accordion('refresh');
        },

        updatePostion: function(selectedObject){
            var active = selectedObject.accordion("option", "active");
        },
        addFormItem: function (selectedObject, url) {
            
            var count = selectedObject.children(form.settings.AccordionHeader).length;
            var addCount = parseInt($('form-selection-count').val() );
            for (var i = 0; i < addCount; ++i) {

                $('#test').load(url + '?count=' + count);
                selectedObject.append($(test).html());
                this.refreshAccordion(selectedObject);

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

        } 
    });//extend
})(window.jQuery, window.form || (window.form = {}));
