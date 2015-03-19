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
            var $sample = $('#');

            // Dynamic java script command
            $sample.on('mouseover', '.classname', function () {
                samplefunc($(this));
            });
        },
        //Example of a function
        samplefunc: function (selectedObject) {

        },

        refreshAccordion: function(selectedObject){
            $(selectedObject).accordion('refresh');
        },

        addTextBox: function (selectedObject) {
            var count = selectedObject.children(form.settings.AccordionHeader).length;
            
            $.ajax({
                type: 'GET',
                url: url,
                cache: false,
                data: ({ type: 0, count: 1 }),
                sucess: function (data) {
                    selectedObject.append(data);
                    form.refreshAccordion(selectedObject);
                    selectedObject.accordion({ active: count });
                }
                
            });

        },

        initAccordion: function (selectedObject) {
            $(selectedObject).accordion({
                header: form.settings.AccordionHeader,
                heightStyle: form.settings.AccordionStyle,
                collapsible: true,
         });
        }

    });//extend
})(window.jQuery, window.form || (window.form = {}));

jQuery(function () {
    forms.init();
});