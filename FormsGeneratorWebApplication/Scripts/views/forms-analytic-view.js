;
(function ($, chart) {
    'use strict';
    //We will be using strict java script!
    $.extend(chart, {
        settings: {

        },
        QuestionsCR: [],
        QuestionsText: [],
        OverallResponses: [],
        Qoptions: [],
        chartSwtichContext: "",
        chartQuestionSwtichContext: [],
        init: function (model) {

            var obj = JSON.parse(model);

            chart.getQuestionList(obj.form.FormItemIList);

            chart.getOverallResponses(obj.selectable, obj.text);

            var overallQuestions = $.merge($.merge([], chart.QuestionsCR), chart.QuestionsText);

            chart.createBigChart(overallQuestions, chart.OverallResponses);

            chart.createQuestionsChart(obj.selectable);

            chart.createTextChart(obj.text);

            $('#graph-list a').click(function (e) {
                e.preventDefault();
                var context = $("#big-chart").get(0).getContext("2d");
                var value = $(this).attr('href');
                if (value === "#Bar") {
                    chart.chartSwtichContext.destroy();
                    chart.chartSwtichContext = new Chart(context).Bar(chart.makeData(overallQuestions, chart.OverallResponses), chart.options());
                }
                else if (value === "#Line") {
                    chart.chartSwtichContext.destroy();
                    chart.chartSwtichContext = new Chart(context).Line(chart.makeData(overallQuestions, chart.OverallResponses), chart.options());
                }
                else if (value === "#Radar") {
                    chart.chartSwtichContext.destroy();
                    chart.chartSwtichContext = new Chart(context).Radar(chart.radarData(overallQuestions, chart.OverallResponses), chart.options());
                }
            });

            $('#q-graph-list a').click(function (e) {
                e.preventDefault();
                for (var i = 0; i < chart.QuestionsCR.length; ++i) {
                    var q = "question-" + i;
                    var context = $('#' + q).get(0).getContext("2d");

                    var opt = chart.Qoptions[i];
                    var sel = obj.selectable[i];

                    var value = $(this).attr('href');
                    if (value === "#Bar") {
                        chart.chartQuestionSwtichContext[i].destroy();
                        chart.chartQuestionSwtichContext[i] = new Chart(context).Bar(chart.makeData(opt, sel), chart.options());
                    }
                    else if (value === "#Line") {
                        chart.chartQuestionSwtichContext[i].destroy();
                        chart.chartQuestionSwtichContext[i] = new Chart(context).Line(chart.makeData(opt, sel), chart.options());
                    }
                    else if (value === "#Radar") {
                        chart.chartQuestionSwtichContext[i].destroy();
                        chart.chartQuestionSwtichContext[i] = new Chart(context).Radar(chart.radarData(opt, sel), chart.options());
                    }
                }
            });

        },

        createTextChart: function(text){
            for (var i = 0; i < chart.QuestionsText.length; ++i) {
                var q = chart.QuestionsText[i];
                var restext = text[i];
                var html = "";
                for (var j = 0; j < restext.length; j++) {
                    html += '<p class="wrap-paragraph">' + restext[j] + '<p>';
                }
                $('.questions-graphs').append('<div class="form-group"><div class="panel panel-default"><div class="panel-heading"> Question-' +
                    i + chart.QuestionsCR.length + ' : ' + q + '</div><div class="panel-body"><div class="container"><div class="row"><div class="col-md-10">' + html +
                    '</div></div></div></div></div></div>');

            }
        },
        createQuestionsChart: function(selectable){

            for (var i = 0; i < chart.QuestionsCR.length; ++i) {
                var q = chart.QuestionsCR[i];
                $('.questions-graphs').append('<div class="form-group"><div class="panel panel-default"><div class="panel-heading"> Question-' +
                    i + ' : ' + q + '</div><div class="panel-body"><div class="form-horizontal">' +
                    '<div class="form-group"><div class="col-lg-12 col-lg-offset-1">' +
                    '<canvas id="question-' + i + '" class="convas-size"></canvas>' +
                    '</div></div></div></div></div></div>');
                var context = $("#question-" + i).get(0).getContext("2d");
                var opt = chart.Qoptions[i];
                var sel = selectable[i];
                chart.chartQuestionSwtichContext[i] = new Chart(context).Bar(chart.makeData(opt, sel), chart.options());
            }
        },

        getOverallResponses: function(selectable, text){
            for (var i = 0; i < selectable.length; ++i) {
                var temp = selectable[i];
                var count = 0;
                for (var j = 0; j < temp.length; j++) {
                    count += temp[j];
                }
                chart.OverallResponses.push(count);
            }
            for (var i = 0; i < text.length; ++i) {
                var temp = text[i].length;
                chart.OverallResponses.push(temp);
            }
        },

        getQuestionList: function(formItems){
            for (var i = 0; i < formItems.length; ++i) {
                if( formItems[i].type >= 2){
                    chart.QuestionsCR.push(formItems[i].question);
                    var opt = [];
                    for (var j = 0; j < formItems[i].options.length; ++j)
                    {
                        opt.push(formItems[i].options[j].option);
                    }
                    chart.Qoptions.push(opt);
                }
                else{
                    chart.QuestionsText.push(formItems[i].question);
                }
            }
        },

        createBigChart: function (questions, overallResponses) {

            var context = $("#big-chart").get(0).getContext("2d");
            chart.chartSwtichContext = new Chart(context).Line(chart.makeData(questions, overallResponses), chart.options());


        },

        makeData: function (questions, overallResponses) {
            var data = {
                    labels: questions,
                    datasets: [
                        {
                            label: "Overall Form Results",
                            labelColor: "rgba(255,255,255,0)",
                            fillColor: "rgba(220,220,220,0.5)",
                            strokeColor: "rgba(220,220,220,0.8)",
                            highlightFill: "rgba(220,220,220,0.75)",
                            highlightStroke: "rgba(220,220,220,1)",
                            data: overallResponses
                        }
                    ]
            }
            return data;
        },

        radarData: function (questions, overallResponses) {
            var data = {
                labels: questions,
                datasets: [
                    {
                        label: "My First dataset",
                        fillColor: "rgba(220,220,220,0.2)",
                        strokeColor: "rgba(220,220,220,1)",
                        pointColor: "rgba(220,220,220,1)",
                        pointStrokeColor: "#fff",
                        pointHighlightFill: "#fff",
                        pointHighlightStroke: "rgba(220,220,220,1)",
                        data: overallResponses
                    }
                ]
            }
            return data;
        },


        options: function () {
            var options = {
                //Boolean - Whether the scale should start at zero, or an order of magnitude down from the lowest value
                scaleBeginAtZero: true,

                //Boolean - Whether grid lines are shown across the chart
                scaleShowGridLines: true,

                //String - Colour of the grid lines
                scaleGridLineColor: "rgba(255,255,255,.05)",

                //Number - Width of the grid lines
                scaleGridLineWidth: 2,

                //Boolean - Whether to show horizontal lines (except X axis)
                scaleShowHorizontalLines: true,

                //Boolean - Whether to show vertical lines (except Y axis)
                scaleShowVerticalLines: true,

                //Boolean - If there is a stroke on each bar
                barShowStroke: true,

                //Number - Pixel width of the bar stroke
                barStrokeWidth: 2,

                //Number - Spacing between each of the X value sets
                barValueSpacing: 5,

                //Number - Spacing between data sets within X values
                barDatasetSpacing: 1,

                //String - A legend template
                legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].fillColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>"

            }
            return options;
        },
    });//extend
})(window.jQuery, window.chart || (window.chart = {}));
