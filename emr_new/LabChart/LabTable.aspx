<%@ Page Language="c#" CodeFile="LabTable.aspx.cs" AutoEventWireup="true" Inherits="Quest.LabTable" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>LabTable</title>
    <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
   
    
   <%-- <style type="text/css">
        .floatingHeader {
            position: fixed;
            top: 0;
            visibility: hidden;
            background-color: antiquewhite;
            z-index: 1000;
        }
    </style>
    <script type="text/javascript">
       
        


        function UpdateTableHeaders() {
            $(".persist-area").each(function () {

                var el = $(this),
                    offset = el.offset(),
                    scrollTop = $(window).scrollTop(),
                    floatingHeader = $(".floatingHeader", this)

                if ((scrollTop > offset.top) && (scrollTop < offset.top + el.height())) {
                    floatingHeader.css({
                        "visibility": "visible"
                    });
                } else {
                    floatingHeader.css({
                        "visibility": "hidden"
                    });
                };
            });
        }

        // DOM Ready
        $(function () {

            $(window)
                .scroll(UpdateTableHeaders)
                .trigger("scroll");

        });
        window.onload = function () {
            $("table.tableWithFloatingHeader").each(function () {
                $(this).wrap("<div class='divTableWithFloatingHeader' style='position:absolute'></div>");

                $("tr:first", this).before($("tr:first", this).clone());
                clonedHeaderRow = $("tr:first", this);
                clonedHeaderRow.addClass("floatingHeader");


                var row_ths = $("tr:nth-child(2)", this).children("th");
                var crow_ths = $("tr:nth-child(1)", this).children("th");;

                for (var i = 0; i < row_ths.size(); ++i) {
                    crow_ths.eq(i).width(row_ths.eq(i).width());
                }
            });
        }
        window.onresize = function () {
            $("table.tableWithFloatingHeader").each(function () {

                clonedHeaderRow = $("tr:first", this);

                var row_ths = $("tr:nth-child(2)", this).children("th");
                var crow_ths = $("tr:nth-child(1)", this).children("th");;

                for (var i = 0; i < row_ths.size(); ++i) {
                    crow_ths.eq(i).width(row_ths.eq(i).width());
                }
            });

        }
    </script>--%>

    
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <p>
            <b>Patient Name:<asp:Label ID="lblPatnames" Text="" runat="server"></asp:Label></b>
        </p>
        <obout:Grid id="DataGridLabResults" runat="server" AutoGenerateColumns="true" OnColumnsCreated="DataGridLabResults_ColumnsCreated" AllowPaging="false" AllowColumnResizing="true" AllowAddingRecords="false" AllowPageSizeSelection="false" PageSizeOptions="-1" PageSize="500" >
            <%--<Columns>
        <obout:Column DataField="TestName" Wrap="true" runat="server" />        
    </Columns>--%>
<ScrollingSettings ScrollHeight="600" />
            
        </obout:Grid>
        <%--<asp:GridView ID="DataGridLabResults" runat="server" AutoGenerateColumns="true" Width="100%" Font-Names="Arial" Font-Size="9pt" AllowPaging="false" CssClass=" persist-area tableWithFloatingHeader">
            <HeaderStyle Font-Bold="True" ></HeaderStyle>
            <AlternatingRowStyle BackColor="#EFEFEF" />
            <Columns>
            </Columns>
        </asp:GridView>--%>

    </form>
</body>
</html>
