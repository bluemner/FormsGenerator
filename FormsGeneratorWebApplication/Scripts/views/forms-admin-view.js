;
(function ($, form) {
    'use strict';
    //We will be using strict java script!
    $.extend(form, {

        settings: {
            AccordionHeader: '.ui-accordion-header',
            AccordionStyle:  'content',
        },

        init: function () {
            var $formname = $('#new-form');
            var $sample = $('#');
            
            // Dynamic java script command
            $sample.on('mouseover', '.classname', function () {
                samplefunc($(this));
            });
            
            //$formname.on('click', '#Accordion', function () {
            //    updatePostion($(this));
            //});

            $formname.on('click', '.btn-add-form-item', function () {
                addFormItem($(this));
            });
           
        },
        //Example of a function
        samplefunc: function (selectedObject) {

        },

        initAccordion: function (selectedObject) {
            $(selectedObject).accordion({
                header: form.settings.AccordionHeader,
                heightStyle: form.settings.AccordionStyle,
                collapsible: true,
            });
        },

        refreshAccordion: function(selectedObject){
            $(selectedObject).accordion('refresh');
        },

        addTextBox: function (selectedObject, url) {
            var count = selectedObject.children(form.settings.AccordionHeader).length;
            
            $.ajax({
                type: 'GET',
                url: url,
                cache: false,
                data: ({ type: 0, count: count }),
                sucess: function (data) {
                    selectedObject.append(data);
                    form.refreshAccordion(selectedObject);
                    selectedObject.accordion({ active: count });
                }
                
            });

        },

        updatePostion: function(selectedObject){
            var active = selectedObject.accordion("option", "active");
        },
        addFormItem: function (selecteObject){
            
        } 
    });//extend
})(window.jQuery, window.form || (window.form = {}));

jQuery(function () {
    form.init();
});