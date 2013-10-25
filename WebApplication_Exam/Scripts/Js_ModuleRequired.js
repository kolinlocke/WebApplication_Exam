/// <reference path="jquery-1.7.1.js" />

var IsCheckSave = false
var IsSave = true
var IsTempReleased = false

//Requires a Save Check
IsCheckSave = true;

function RequireSave() {
    IsSave = false;
}

function ReleaseSave() {
    IsSave = true;
}

function TempReleaseSave() {
    IsTempReleased = true;
    IsSave = true;
}

function CheckModuleSave() {
    if (IsCheckSave) {
        if (!IsSave) {
            return "Please save your work first before leaving this page.";
        }
    }

    if (IsTempReleased) {
        IsTempReleased = false;
        IsSave = false;
    }
}

function RequireSave_RadEditor(RadEditorID) {
    $telerik.$(document).ready(function () {
        var Control = $find(RadEditorID);
        Control.attachEventHandler('onkeydown', function () { RequireSave_RadEditor_DetectChanges(); });
    });
}

function RequireSave_RadEditor_DetectChanges() {
    IsSave = false;
}

function CheckDelete() {
    if (!window.confirm("Are you sure you want to delete this record?")) {
        return false;
    }
}


window.onbeforeunload = CheckModuleSave;