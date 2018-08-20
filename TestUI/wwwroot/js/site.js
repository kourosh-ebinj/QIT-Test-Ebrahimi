/// <reference path="../lib/jquery/dist/jquery.js" />
/// <reference path="../js/toastr.min.js" />
/// <reference path="../js/jquery.blockUI.js" />

var app = app || {};

app.dataService = (function () {
    var ajaxAsPost, ajaxAsGet;

    $.ajaxSetup({
        error: function (XMLHttpRequest, textStatus, errorThrown) {

            if (errorThrown != "abort")
                alert('oops, something bad happened. Please try again.');
        }
    });

    //jQuery(document).ajaxStart(jQuery.blockUI({ message: '<h1><img src="busy.gif" /> Just a moment...</h1>' }));
    jQuery(document).ajaxStop(jQuery.unblockUI);

    ajaxAsPost = function (url, data, successCallback, failureCallback) {
        jQuery.blockUI({ message: null });

        return jQuery.ajax(url, {
            data: data,
            type: "post",
            contentType: "application/json; charset=utf-8",
            success: successCallback,
            error: failureCallback
        });
    }

    ajaxAsPut = function (url, data, successCallback, failureCallback) {
        jQuery.blockUI({ message: null });

        return jQuery.ajax(url, {
            data: data,
            type: "put",
            contentType: "application/json; charset=utf-8",
            success: successCallback,
            error: failureCallback
        });
    }

    ajaxAsGet = function (url, data, successCallback, failureCallback) {
        jQuery.blockUI({ message: null });

        return jQuery.ajax(url, {
            data: data,
            type: "get",
            contentType: "application/json; charset=utf-8",
            success: successCallback,
            error: failureCallback
        });
    }
    ajaxAsDelete = function (url, data, successCallback, failureCallback) {
        jQuery.blockUI({ message: null });

        return jQuery.ajax(url, {
            data: data,
            type: "delete",
            contentType: "application/json; charset=utf-8",
            success: successCallback,
            error: failureCallback
        });
    }
    return {
        ajaxAsPost: ajaxAsPost,
        ajaxAsGet: ajaxAsGet,
        ajaxAsDelete: ajaxAsDelete
    }
})();

app.toastr = (function () {
    if (toastr) {
        toastr.options.positionClass = "toast-top-right";

        return toastr;
    }
})();// Write your JavaScript code.
