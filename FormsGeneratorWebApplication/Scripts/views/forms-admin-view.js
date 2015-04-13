;
(function ($, form) {
    'use strict';
    //We will be using strict java script!
    $.extend(form, {
        
        settings: {
            AccordionHeader: '.ui-accordion-header',
            AccordionHeightStyle: 'content',  
        },

        arrayCount: 0,

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
            form.arrayCount--;
            selectedObject.parents('.ui-accordion-content').prev().slideUp('slow', function () {
                var $selectedHeader = $(".ui-accordion-header-active");
                var selectedIndex = $selectedHeader.attr('id').replace("ui-id-", "");
                $selectedHeader.remove();
                var headers = $(".ui-accordion-header");
                for(var i = Math.floor(selectedIndex/2);i<headers.length-1;++i)
                {
                    var curIndex = $(headers[i]).attr('id').replace("ui-id-", "");
                    $(headers[i]).attr("id", "ui-id-" + (curIndex - 2));
                    $(headers[i]).attr("aria-controls", "ui-id-" + (curIndex - 1));
                }
            });

            selectedObject.parents('.ui-accordion-content').slideUp('slow', function (){
                var $selectedContent = $(".ui-accordion-content-active");
                var selectedIndex = $selectedContent.attr('id').replace("ui-id-", "");//for ids
                var selectedIListItem = $(".ui-accordion-content-active .form-control").first(); //for input names
                var $listIndex = $(selectedIListItem).attr("name").replace("FormItemIList[","").replace("].question","");
                $selectedContent.remove();
                var $contents = $(".ui-accordion-content");
                for (var i = (selectedIndex / 2)-1 ; i < $contents.length; ++i) {
                    var curIndex = $($contents[i]).attr('id').replace("ui-id-", "");
                    $($contents[i]).attr("id", "ui-id-" + (curIndex - 2));
                    $($contents[i]).attr("aria-labelledby", "ui-id-" + (curIndex - 3));
                }
                var $inputs = $("#item-zone .form-control");
                var name = "";
                console.log($listIndex);
                for(i = 0;i<$inputs.length;++i)
                {

                    name = $($inputs[i]).attr("name");
                    var before = name.substr(0, name.indexOf("[")+1);
                    var after = name.substr(name.indexOf("]"), name.length - 1);
                    var number = name.replace(before, "").replace(after, "");
                    if(number > $listIndex)
                    {
                        --number;
                        $($inputs[i]).attr("name", before+number+after)
                    }
                }


            });
        },

        updatePostion: function (selectedObject) {
            var active = selectedObject.accordion("option", "active");
        },
        addFormItem: function (selectedObject, url) {

           // var count = form.arrayCount;//selectedObject.children(form.settings.AccordionHeader).length;
            // var count = parseInt(form.settings.CurrentIndex);
            ///form.settings.CurrentIndex = (++count) + "";
            var addCount = parseInt($('.form-selection-count').val());
            for (var i = 0; i < addCount; ++i) {
                var count = form.arrayCount;
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
                form.arrayCount++;
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
