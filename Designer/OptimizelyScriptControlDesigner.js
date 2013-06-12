Type.registerNamespace("OptimizelyScriptControl.Designer");

OptimizelyScriptControl.Designer.OptimizelyScriptControlDesigner = function (element) {

    OptimizelyScriptControl.Designer.OptimizelyScriptControlDesigner.initializeBase(this, [element]);
    this._resizeControlDesignerDelegate = null;
}

OptimizelyScriptControl.Designer.OptimizelyScriptControlDesigner.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */

    initialize: function () {
        OptimizelyScriptControl.Designer.OptimizelyScriptControlDesigner.callBaseMethod(this, 'initialize');

        this._textBoxValueChangedDelegate = Function.createDelegate(this, this._RadTextBoxValueChangedHandler);
        this._urlTextBox.add_valueChanged(this._textBoxValueChangedDelegate)

        this._beforeSaveChangesDelegate = Function.createDelegate(this, this._beforeSaveChanges);
        this.get_propertyEditor().add_beforeSaveChanges(this._beforeSaveChangesDelegate);
    },

    dispose: function () {
        OptimizelyScriptControl.Designer.OptimizelyScriptControlDesigner.callBaseMethod(this, 'dispose');

        if (this._urlTextBox) {
            this._urlTextBox.remove_valueChanged(this._textBoxValueChangedDelegate);
        }
        delete this._textBoxValueChangedDelegate;

        if (this._beforeSaveChangesDelegate) {
            this.get_propertyEditor().remove_beforeSaveChanges(this._beforeSaveChangesDelegate);
            delete this._beforeSaveChangesDelegate;
        }
    },

    /* --------------------------------- public methods --------------------------------- */
    // implementation of IControlDesigner: Forces the control to refersh from the control Data
    refreshUI: function () {
        var data = this.get_controlData();
        if (data.OptimizelyCodeUrl != null) {
            this.get_urlTextBox().set_value(data.OptimizelyCodeUrl);
        }
    },

    // implementation of IControlDesigner: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {
        // save selected page
        var controlData = this._propertyEditor.get_control();
        // start to save the data
        controlData.OptimizelyCodeUrl = this.get_urlTextBox().get_value();
    },

    /* ------------------------------------- events ------------------------------------------- */
    // add resize events
    resizeEvents: function () {
        dialogBase.resizeToContent();
    },

    _pageLoadHandler: function () {
        // handle the checkbox for the notifications
    },

    /* --------------------------------- event handlers --------------------------------- */

    _RadTextBoxValueChangedHandler: function (sender, args) {
        var urlString = sender.get_valueAsString().trim();
        var validSciptTag = /<script[\d\D]*?>[\d\D]*?<\/script>/g;
        if (validSciptTag.test(urlString) == true && jQuery(urlString).attr("src")) {
            sender.set_value(jQuery(urlString).attr("src"));
        }
        this.resizeEvents();
    },

    // validator logic here:
    _beforeSaveChanges: function (sender, args) {
        var errorMessage = "";
        var toCancel = false;

        //check if the text value is valid
        if (this.get_urlTextBox() && this.get_urlTextBox().get_value() != null && this.get_urlTextBox().get_value() != "") {
            var urlString = this.get_urlTextBox().get_value().trim();
            var validSrcAttr = /([A-Za-z0-9_\-\.\/])+\.(js$)/g;
            if (validSrcAttr.test(urlString) == false) {
                toCancel = true;
                errorMessage += "Please paste a valid Optimizely url, i.e. \"//cdn.optimizely.com/js/123456789.js\" \n";
            } else {
                toCancel = false;
            }
        }
        args.set_cancel(toCancel);
        var errorLabelObj = jQuery(this.get_errorLabel());

        if (errorMessage != "") {
            errorLabelObj.removeClass("hidden").text(errorMessage);
            this.resizeEvents();
        } else {
            errorLabelObj.addClass("hidden");
            this.resizeEvents();
        }
    },
    /* --------------------------------- private methods --------------------------------- */


    /* --------------------------------- properties --------------------------------- */

    // gets the reference to the propertyEditor control
    get_propertyEditor: function () {
        return this._propertyEditor;
    },
    // sets the reference fo the propertyEditor control
    set_propertyEditor: function (value) {
        this._propertyEditor = value;
    },

    get_urlTextBox: function () {
        return this._urlTextBox;
    },

    set_urlTextBox: function (value) {
        this._urlTextBox = value;
    },

    get_errorLabel: function () {
        return this._errorLabel;
    },

    set_errorLabel: function (value) {
        this._errorLabel = value;
    }
}


OptimizelyScriptControl.Designer.OptimizelyScriptControlDesigner.registerClass('OptimizelyScriptControl.Designer.OptimizelyScriptControlDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();