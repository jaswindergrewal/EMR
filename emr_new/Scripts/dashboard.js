var pie1;
var plot1;
var lineMultiple;

//Bind the graph initially with the first campaignid in a drop down
$(document).ready(function () {

    GetDashBoardData();
    
    GetDashBoardEventData();

});



//Purpose: To Get Dashboard Data
function GetDashBoardData() {
  

    var campaignID = $('#ddlCampaign option:selected').val();
    try {
        var URL = "../CRM/CRM_Dashboard.aspx/PlotCampaignGraph";
        $.ajax({
            cache: false,
            type: "POST",
            url: URL,
            data: "{campaignID:'" + campaignID + "'}",
            dataType: "json",
            contentType: "application/json",
            beforeSend:function ()
            {
                $("#loading-div-background").show(); // show wait
            },
            success: function (result) {
               
                var listObj = result.d;
                if (plot1) {
                    plot1.destroy();
                }
               
                if (listObj != null) {
                    if (listObj.length > 0) {
                        var data = new Array();
                        var dataConverted = new Array();
                        var datam = new Array();
                        for (var i = 0; i < listObj.length; i++) {
                            data.push(listObj[i].Total);
                            dataConverted.push(listObj[i].PatientConverted);
                            datam.push(listObj[i].MarketSourceName);
                        }

                        // Can specify a custom tick Array.
                        // Ticks should match up one for each y value (category) in the series.
                        var ticks = datam;
                      
                       
                        plot1 = $.jqplot('barDiv1', [data, dataConverted], {
                            animate: true,
                            // The "seriesDefaults" option is an options object that will
                            // be applied to all series in the chart.
                            seriesDefaults: {
                                renderer: $.jqplot.BarRenderer,
                                rendererOptions: { fillToZero: true, barDirection: 'horizontal' }
                            },
                            // Custom labels for the series are specified with the "label"
                            // option on the series option.  Here a series option object
                            // is specified for each series.
                            series: [
                                { label: 'Total' },
                                { label: 'Converted' },

                            ],
                            // Show the legend and put it outside the grid, but inside the
                            // plot container, shrinking the grid to accomodate the legend.
                            // A value of "outside" would not shrink the grid and allow
                            // the legend to overflow the container.
                            legend: {
                                show: true,
                                placement: 'inside',
                                rendererOptions: {
                                    numberRows: 1,
                                    numberColumns: 2
                                },

                            },
                            axes: {
                                // Use a category axis on the x axis and use our custom ticks.
                                yaxis: {
                                    renderer: $.jqplot.CategoryAxisRenderer,
                                    ticks: datam,
                                  
                                    
                                },
                                // Pad the y axis just a little so bars can get close to, but
                                // not touch, the grid boundaries.  1.2 is the default padding.
                                xaxis: {
                                    //padMax:1.3,
                                    ticks: { formatString: '%d' }

                                }
                            }
                        });

                        

                    }


                    $("#loading-div-background").hide(); // remove wait, done!
                }
                $("#loading-div-background").hide(); // remove wait, done!
            },
            
            error: function (xhr, ajaxOptions, thrownError) {
                alert("error");
                $("#loading-div-background").hide(); // remove wait, done!
            }
        });
      
       
    }
    catch (ex) {
        alert("Error on GetDashBoardData Function", ex);
    }
}


//function to show the Record for grid lines

function GetDashBoardEventData() {
  
    var eventID = $('#ddlEvent option:selected').val();
    
    try {
        var URL = "../CRM/CRM_Dashboard.aspx/PlotEventGraph";
        $.ajax({
            cache: false,
            type: "POST",
            url: URL,
            data: "{eventID:'" + eventID + "'}",
            dataType: "json",
            contentType: "application/json",
            beforeSend: function () {
                $("#loading-div-background").show(); // show wait
            },
            success: function (result) {

                var listObj = result.d;
                if (lineMultiple) {
                    lineMultiple.destroy();
                }
                $('#lblConverted').text(0);
                $('#lblMedstart').text(0);
                $('#lbltotal').text(0);
                $('#lblAttendent').text(0);
                $('#lblNotConverted').text(0);
                if (listObj != null) {

                    if (listObj.length > 0) {

                        var data = new Array();
                        var dataConverted = new Array();
                        var datam = new Array();
                        var dataMedStartPatient = new Array();
                        var dataProspectAttended = new Array();
                        var totalPatientConverted = 0;
                        var totalMedStartPatient = 0;
                        var totalProspectAttended = 0;
                        var totalTotal = 0;


                        for (var i = 0; i < listObj.length; i++) {
                            data.push(listObj[i].Total);
                            dataConverted.push(listObj[i].PatientConverted);
                            dataMedStartPatient.push(listObj[i].MedStartPatient);
                            dataProspectAttended.push(listObj[i].ProspectAttended);
                            datam.push(listObj[i].MarketSourceName);
                            totalPatientConverted = totalPatientConverted + parseInt(listObj[i].PatientConverted);
                            totalMedStartPatient = totalMedStartPatient + parseInt(listObj[i].MedStartPatient);
                            totalProspectAttended = totalProspectAttended + parseInt(listObj[i].ProspectAttended);
                            totalTotal = totalTotal + parseInt(listObj[i].Total);

                        }
                        $('#lblConverted').text(totalPatientConverted);
                        $('#lblMedstart').text(totalMedStartPatient);
                        $('#lbltotal').text(totalTotal);
                        $('#lblAttendent').text(totalProspectAttended);
                        var NotConverted = totalTotal - totalPatientConverted;
                        $('#lblNotConverted').text(NotConverted);


                        // Can specify a custom tick Array.
                        // Ticks should match up one for each y value (category) in the series.
                        var ticks = datam;
                        
                        lineMultiple = $.jqplot('lineMultiple', [data, dataConverted, dataMedStartPatient, dataProspectAttended], {
                            animate: true,
                            // The "seriesDefaults" option is an options object that will
                            // be applied to all series in the chart.
                            seriesDefaults: {
                                renderer: $.jqplot.BarRenderer,
                                rendererOptions: { fillToZero: true, }
                            },
                            // Custom labels for the series are specified with the "label"
                            // option on the series option.  Here a series option object
                            // is specified for each series.
                            series: [
                                { label: 'Total' },
                                { label: 'Converted' },
                                { label: 'Med Start' },
                                { label: 'Attendent' },

                            ],
                            // Show the legend and put it outside the grid, but inside the
                            // plot container, shrinking the grid to accomodate the legend.
                            // A value of "outside" would not shrink the grid and allow
                            // the legend to overflow the container.
                            legend: {
                                show: true,
                                placement: 'inside',
                                rendererOptions: {
                                    numberRows: 1,
                                    numberColumns: 2
                                },

                            },
                            axes: {
                                // Use a category axis on the x axis and use our custom ticks.
                                xaxis: {
                                    renderer: $.jqplot.CategoryAxisRenderer,
                                    ticks: datam
                                },
                                // Pad the y axis just a little so bars can get close to, but
                                // not touch, the grid boundaries.  1.2 is the default padding.
                                yaxis: {
                                    //padMax:1.3,
                                    ticks: { formatString: '%d' }

                                }
                            }
                        });

                    }

                    $("#loading-div-background").hide(); // remove wait, done!

                }
                $("#loading-div-background").hide(); // remove wait, done!
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert("error");
                $("#loading-div-background").hide(); // remove wait, done!
            }
        });
      
    }
    catch (ex) {
        alert("Error on GetDashBoardEventData Function", ex);
    }
}






