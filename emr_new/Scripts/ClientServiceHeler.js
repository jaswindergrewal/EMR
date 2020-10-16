Type.registerNamespace("ServiceClients");

ServiceClients.EMRClient = function() {
}

ServiceClients.EMRClient.prototype = {

	CloseScrip: function (data, callback, errorCallBack) {
		ServiceLibrary.IEMRService.CloseScrip(data, callback, errorCallBack);
    },

	DeleteScrip: function (data, callback, errorCallBack) {
		ServiceLibrary.IEMRService.DeleteScrip(data, callback, errorCallBack);
    },

    AddRefill: function(data, callback, errorCallBack) {
    	ServiceLibrary.IEMRService.AddRefill(data, callback, errorCallBack);
    },

    CloseSupp: function (data, callback, errorCallBack) {
    	ServiceLibrary.IEMRService.CloseSupp(data, callback, errorCallBack);
    },

    DeleteSupp: function (data, callback, errorCallBack) {
    	ServiceLibrary.IEMRService.DeleteSupp(data, callback, errorCallBack);
    },

    NewSuppScrip: function (data, callback, errorCallBack) {
    	ServiceLibrary.IEMRService.NewSuppScrip(data, callback, errorCallBack);
    },

    dispose: function () {
        //disposed
    }
}
ServiceClients.EMRClient.registerClass('ServiceClients.EMRClient', null, Sys.IDisposable)

// Notify ScriptManager that this is the end of the script.
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();