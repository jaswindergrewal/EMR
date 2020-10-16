<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <title>Wcf in JavaScript</title>
    <link type="text/css" rel="Stylesheet" href="Stylesheet1.css" />

    <script language="javascript" type="text/javascript">
    	var todoCollection;
    	var UIMode = 'New'; //other mode is 'Edit'
    	var EditingTodo;
    	//page load function

    	function ClearForm() {
    		document.getElementById('Text1').value = "";
    	}

    	function GetToDo(id) {
    		if (todoCollection.length > 0) {
    			for (var i = 0; i < todoCollection.length; i++) {
    				var todo = todoCollection[i];
    				if (todo.ID == id)
    					return todo;
    			}
    			return null;
    		}
    	}
    	function pageLoad() {
    		BindAllData();
    	}

    	function BindAllData() {
    		var client = new ServiceClients.EMRClient();
    		client.GetAllToDo(BindAllDataCallBack, BindAllDataErrorCallBack);
    	}

    	function BindAllDataCallBack(result) {
    		var todos = result;
    		todoCollection = todos;
    		if (todos.length > 0) {
    			var string = "<table cellpadding='3' cellspacing='0' class='table'><tbody>";
    			for (var i = 0; i < todos.length; i++) {
    				string = string + "<tr>";

    				string = string + "<td>";
    				string = string + todos[i].Description;
    				string = string + "</td>";

    				string = string + "<td>";
    				string = string + todos[i].Status;
    				string = string + "</td>";

    				string = string + "<td><span class='Command' onClick=\"EditTodo('" + todos[i].ID + "');\">Edit</span>";
    				string = string + "</td>";

    				string = string + "<td><span class='Command' onClick=\"DeleteTodo('" + todos[i].ID + "');\">Remove</span>";
    				string = string + "</td>";

    				string = string + "</tr>";
    			}
    			string = string + "</tbody></table>";

    			document.getElementById('Results').innerHTML = string;
    		}
    		else {
    			document.getElementById('Results').innerHTML = "No to do Exists!";
    		}
    	}
    	function BindAllDataErrorCallBack(result) {
    		window.alert("Error occured during service communication");
    	}

    	function AddAToDo() {
    		var client = new ServiceClients.EMRClient();
    		if (UIMode == "New") {
    			var todo = new ServiceLibrary.ToDo();
    			todo.Description = document.getElementById('Text1').value;
    			todo.Status = "1";
    			client.AddToDo(todo, ToDoCallBack, ToDoErrorCallBack);
    		}
    		else {
    			EditingTodo.Description = document.getElementById('Text1').value;
    			EditingTodo.Status = "1";
    			client.UpdateToDo(EditingTodo, ToDoCallBack, ToDoErrorCallBack);
    			UIMode = "New";
    		}
    		ClearForm();
    	}

    	function EditTodo(id) {
    		var todo = GetToDo(id);
    		EditingTodo = todo;
    		document.getElementById('Text1').value = todo.Description;
    		UIMode = "Edit";
    	}

    	function DeleteTodo(id) {
    		var todo = GetToDo(id);
    		if (todo != null) {
    			var client = new ServiceClients.EMRClient();
    			client.DeleteToDo(todo, ToDoCallBack, ToDoErrorCallBack);
    		}
    	}

    	function ToDoCallBack(result) {
    		BindAllData();
    	}

    	function ToDoErrorCallBack(result) {
    		window.alert("Error on " + result);
    	}

    	if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
        
    </script>

    <style type="text/css">
        #Text1
        {
            width: 280px;
        }
        #btnAdd
        {
            width: 100px;
        }
        #Reset1
        {
            width: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="MainContainer">

        <div class="Input">
            <h2>
                Type Here to Add New task</h2>
            <input id="Text1" type="text" /><br />
            <br />
            <input id="btnAdd" type="button" value="Add" onclick="AddAToDo();" />
            <input id="Reset1" type="reset" value="reset" />
        </div>
        <hr />
        <br />
        <div id="Results">
            <table cellpadding='0' cellspacing='0'></table>
        </div>
    </div>
</asp:Content>

