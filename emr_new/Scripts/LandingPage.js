
//bind the data for first tab initially on page load

$(document).ready(function () {
    $("#lblticketName").text('Tickets');
    url = "LandingTicket.aspx";
    loader(url);
    BindTicketData('#grdActiveTickets1', '#pagernav1', '1');

});




//Bind the data to different ticket grids on the basis of conditions
function BindTicketData(Grid, Pager, GridFill) {
    $(Grid).jqGrid({

        url: 'LandingTicket.aspx?gridFill=' + GridFill,
        contentType: "application/json",
        datatype: 'json',
        mtype: "GET",

        colNames: ['FollowUp_ID', 'In Progress', 'Name', 'Subject', 'Due', 'Days Old', 'Category', 'Priority', 'Assigned', 'PatientID'],
        colModel: [
             { name: 'FollowUp_ID', index: 'FollowUp_ID', editable: false, title: 'open', sortable: false, align: "center", formatter: createOpenPOPupButton },
             { name: 'InPRogress', index: 'InPRogress', editable: false, sortable: true },
             { name: 'Name', index: 'Name', editable: false, sorttype: "string", sortable: true, classes: 'wrap' },
             { name: 'Subject', index: 'Subject', editable: false, sorttype: "string", sortable: true, classes: 'wrap' },
             { name: 'CreateDate', index: 'CreateDate', editable: false, sortable: true },
             { name: 'DaysOld', index: 'DaysOld', editable: false, sortable: true },
             { name: 'Category', index: 'Category', editable: false, sortable: true, classes: 'wrap' },
             { name: 'Priority', index: 'Priority', editable: false, sortable: true },
             { name: 'Assigned', index: 'Assigned', editable: false, sortable: true, classes: 'wrap' },
             { name: 'PatientID', index: 'PatientID', editable: false, hidden: true },

        ],


        pager: Pager,
        sortable: true,
        height: 500,
        autowidth: true,
        viewrecords: true,
        gridview: true,
        gridComplete: function () {
            $(Grid).jqGrid('setGridWidth', $(window).width() - 50, true);
            var rows = $(Grid).getDataIDs();

            if (rows.length > 0) {
                $(Grid).show();
                $("#DivNoRecord" + GridFill).hide();
            }
            else {
                $(Grid).hide();
                $("#DivNoRecord" + GridFill).show();

            }


            for (var i = 0; i < rows.length; i++) {
                var Priority = $(Grid).getCell(rows[i], "Priority");
                if (Priority == "Normal") {
                    $(Grid).jqGrid('setCell', rows[i], 'Priority', '', { color: 'white', background: 'green' });

                }
                else if (Priority == "Low") {
                    $(Grid).jqGrid('setCell', rows[i], 'Priority', '', { color: 'black', background: 'Yellow' });

                }
                else if (Priority == "High") {
                    $(Grid).jqGrid('setCell', rows[i], 'Priority', '', { color: 'white', background: 'Red' });

                }
            }
        }

    }).navGrid(Pager,
        {
            edit: false, add: false, del: false, search: false, refresh: true
        }

   );
}


//Bind the data to different ticket grids on the basis of conditions
function BindCalendarTicketData(Grid, Pager, GridFill) {
    $(Grid).jqGrid({

        url: 'LandingCalendar.aspx?gridFill=' + GridFill,
        contentType: "application/json",
        datatype: 'json',
        mtype: "GET",

        colNames: ['FollowUp_ID', 'In Progress', 'Name', 'Subject', 'Due', 'Days Old', 'Category', 'Priority', 'Assigned', 'PatientID'],
        colModel: [
             { name: 'FollowUp_ID', index: 'FollowUp_ID', editable: false, title: 'open', sortable: false, align: "center", formatter: createOpenPOPupButton },
             { name: 'InPRogress', index: 'InPRogress', editable: false, sortable: true },
             { name: 'Name', index: 'Name', editable: false, sorttype: "string", sortable: true, classes: 'wrap' },
             { name: 'Subject', index: 'Subject', editable: false, sorttype: "string", sortable: true, classes: 'wrap' },
             { name: 'CreateDate', index: 'CreateDate', editable: false, sortable: true },
             { name: 'DaysOld', index: 'DaysOld', editable: false, sortable: true },
             { name: 'Category', index: 'Category', editable: false, sortable: true, classes: 'wrap' },
             { name: 'Priority', index: 'Priority', editable: false, sortable: true },
             { name: 'Assigned', index: 'Assigned', editable: false, sortable: true, classes: 'wrap' },
             { name: 'PatientID', index: 'PatientID', editable: false, hidden: true },

        ],


        pager: Pager,
        sortable: true,
        height: 500,
        autowidth: true,
        viewrecords: true,
        gridview: true,
        gridComplete: function () {
            $(Grid).jqGrid('setGridWidth', $(window).width() - 50, true);
            var rows = $(Grid).getDataIDs();

            if (rows.length > 0) {
                $(Grid).show();
                $("#DivNoRecord" + GridFill).hide();
            }
            else {
                $(Grid).hide();
                $("#DivNoRecord" + GridFill).show();

            }


            for (var i = 0; i < rows.length; i++) {
                var Priority = $(Grid).getCell(rows[i], "Priority");
                if (Priority == "Normal") {
                    $(Grid).jqGrid('setCell', rows[i], 'Priority', '', { color: 'white', background: 'green' });

                }
                else if (Priority == "Low") {
                    $(Grid).jqGrid('setCell', rows[i], 'Priority', '', { color: 'black', background: 'Yellow' });

                }
                else if (Priority == "High") {
                    $(Grid).jqGrid('setCell', rows[i], 'Priority', '', { color: 'white', background: 'Red' });

                }
            }
        }

    }).navGrid(Pager,
        {
            edit: false, add: false, del: false, search: false, refresh: true
        }

   );
}



function createOpenPOPupButton(cellvalue, options, rowObject) {
    return "<input class='blue-btn' type='button' id='" + rowObject[9] + "/" + rowObject[0] + "' value='Open' onclick='OpenPopupforSearchPages1(this); return false;'>";
}

//function that fires on click of open button from grid
function OpenPopupforSearchPages1(obj) {

    var buttonId = $(obj).attr("id");
    var arr = buttonId.split('/');
    var strPatientId = arr[0];
    var strFollowupId = arr[1];

    if (strFollowupId != 0) {
        var postData = new Object();
        postData.SessionValue = strFollowupId;

        $.ajax({
            type: "POST",
            url: "LandingPage.aspx/SetSession",
            data: JSON.stringify(postData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (response) {
                var listObj = response.d;
                if (listObj > 0) {
                    if (strPatientId != '') {
                        MM_goToURL('parent', 'Manage.aspx?TicketID=' + strFollowupId + '&PatientID=' + strPatientId); return document.MM_returnValue;
                    }
                    else {
                        MM_goToURL('parent', 'TicketNoPatient.aspx?TicketID=' + strFollowupId); return document.MM_returnValue;
                    }
                }

            },
            error: function () {
                alert("Fail to load data.");
            }
        });

    }
}


function ShowDiv(ID) {
    if (ID == 1) {
        $("#lblticketName").text('Tickets');

        url = "LandingTicket.aspx";
        loader(url);

        BindTicketData('#grdActiveTickets1', '#pagernav1', '1');


    }

    else {
        $("#lblticketName").text('Calendar Tickets');
        url = "LandingCalendar.aspx";
        loader(url);
        BindCalendarTicketData('#grdcalActiveTickets1', '#pagernavcal1', '1');
    }
}

//function for loading the url in ifram
function loader(url) {
    var iframe = $("#MainContent_PageContents");

    $("#loading-div-background").show(); // show wait

    iframe.on('load', function () {
        $("#loading-div-background").hide(); // remove wait, done!
    });
    setTimeout(function () {
        iframe.attr('src', url);
    },
            400);
}



