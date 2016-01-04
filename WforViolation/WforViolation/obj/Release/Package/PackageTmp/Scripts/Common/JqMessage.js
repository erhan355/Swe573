function jqGetInputText(message, okFunction, cancelFunction, devParams, isMultiLine, isPassword) {
    var header = "Bilgi Girişi";

    if (IsNullOrEmpty(header)) header = 'Bilgi Girişi';
    if (IsNullOrEmpty(message)) message = '';
    else message = message.replace(/\r\n/gi, '<br>').replace(/\r/gi, '<br>').replace(/\n/gi, '<br>');

    if (IsNullOrEmpty(isMultiLine)) isMultiLine = false;

    var fullMessage = '<table><tr><td>'
                    + message
                    + '</td>'
                    + '</tr>'
                    + '<tr><td>'
                    +
                    (
                        (!isMultiLine || isPassword) ?
                        '<input type="' + (isPassword ? 'password' : 'text') + '" id="InputText" name="InputText" style="width:450px;"/>' :
                        '<textarea id="InputText" name="InputText" style="height:90px;width:450px;"></textarea>'
                    )
                    + '</td>'
                    + '</tr>'
                    + '</table>';


    var buttonArr = new Array();
    buttonArr.push({
        text: "Tamam",
        click: function (ev, dialog) {
            var input = $('#InputText');
            var retVal = true;
            if (typeof (okFunction) === "function") retVal = eval(okFunction)(input.val(), devParams);
            if (retVal === false) input.focus(); else removeDialog(this, dialog);
        }
    });
    buttonArr.push({
        text: "İptal",
        click: function (ev, dialog) {
            var input = $('#InputText');
            var retVal = true;
            if (typeof (cancelFunction) === "function") retVal = eval(cancelFunction)(devParams);
            if (retVal === false) input.focus(); else removeDialog(this, dialog);
        }
    });

    jqShowDialog(header, fullMessage, buttonArr);
    $('#InputText').focus();
}
function jqAlert(header, message, okFunc) {
    var txt = '';
    if (IsNullOrEmpty(header)) header = 'Bilgi';

    if (IsNullOrEmpty(message)) message = '';
    else message = message.replace(/\r\n/gi, '<br>').replace(/\r/gi, '<br>').replace(/\n/gi, '<br>');

    var buttonArr = new Array();
    {
        buttonArr.push({
            text: "Ok",
            click: function (ev, dialog) { removeDialog(this, dialog); if (typeof (okFunc) === "function") eval(okFunc)(); }
        });
    }
    message = '<table><tr><td>' + message + '</td></tr></table>'
    jqShowDialog(header, message, buttonArr);
}
function jqConfirm(header, message, okFunc, cancelFunc) {
    var txt = '';
    if (IsNullOrEmpty(header)) header = 'Onay';

    if (IsNullOrEmpty(message)) message = '';
    else message = message.replace(/\r\n/gi, '<br>').replace(/\r/gi, '<br>').replace(/\n/gi, '<br>');

    var buttonArr = new Array();
    {
        buttonArr.push({
            text: "Tamam",
            click: function (ev, dialog) { removeDialog(this, dialog); if (typeof (okFunc) === "function") eval(okFunc)(); }
        });
        buttonArr.push({
            text: "İptal",
            click: function (ev, dialog) { removeDialog(this, dialog); if (typeof (cancelFunc) === "function") eval(cancelFunc)(); }
        });
    }
    message = '<table><tr><td>' + message + '</td></tr></table>'
    jqShowDialog(header, message, buttonArr);
}
function jqShowMessage(header, shortMessage, detailMessage) {
    var okTxt = "Tamam";

    if (IsNullOrEmpty(detailMessage))
        jqShowMessage_Show(header, shortMessage, okTxt);
    else {
        var detTxt = "Detay Göster";
        var sendTxt = "Mail Gönder";
        var address = "";

        jqShowMessage_Show(header, shortMessage, okTxt, detailMessage, detTxt, sendTxt, address);
    }
}
function jqShowMessage_Show(header, shortMessage, okButtonText, detailMessage, detailButtonText, mailButtonText, mailAddress) {
    var fullMessage = '';

    if (IsNullOrEmpty(header))
        header = 'Hata/Uyarı';

    if (IsNullOrEmpty(shortMessage)) shortMessage = '';
    if (IsNullOrEmpty(okButtonText)) okButtonText = 'Tamam';

    if (IsNullOrEmpty(detailMessage)) detailMessage = '';
    else detailMessage = detailMessage.replace(/<br>/gi, '&#10;').replace(/\r\n/gi, '&#10;').replace(/\r/gi, '&#10;').replace(/\n/gi, '&#10;');

    var detailButtonText, mailButtonText, mailAddress;
    if (detailMessage != '') {
        if (IsNullOrEmpty(detailButtonText))
            detailButtonText = 'Detay Göster';
        if (IsNullOrEmpty(mailButtonText))
            mailButtonText = 'Mail Gönder';
        if (IsNullOrEmpty(mailAddress))
            mailAddress = 'aykut.canturk@softtech.com.tr;';
    }

    var useTextArea = shortMessage.length > 200;

    if (useTextArea) shortMessage = shortMessage.replace(/<br>/gi, '&#10;');
    else shortMessage = shortMessage.replace(/\r\n/gi, '<br>').replace(/\r/gi, '<br>').replace(/\n/gi, '<br>');

    fullMessage = '<table align="left" ' + (IsNullOrEmpty(detailMessage) ? '' : ' width="100%" ') + '><tr><td colspan="2">'
                    +
                    (
                    useTextArea ?
                    '<textarea id="txtShort" readonly="true" wrap="hard" rows="5" style="width:475px;background-color:#f2f5f7;border-width:0px;">' + shortMessage + '</textarea>' :
                     shortMessage
                    )
                    + '</td></tr>'
                    +
                    (
                        IsNullOrEmpty(detailMessage) ? '' :
                        '<tr><td align="right" colspan="2">'
                        + '<br><button name="detailButton" id="detailButton" style="font-weight:normal;" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" onclick="ShowHideDetail(this);">'
                        + detailButtonText + '</button>'
                        + '</td>'
                        + '</tr>'
                        + '<tr id="ErrorBoxDetailRow" style="display: none">'
                        + '<td><br>'
                        + '  <table width="475px" heigth="100%">'
                        + '     <tr>'
                        + '         <td colspan="2">'
                        + '             <textarea id="txtDetail" readonly="true" wrap="hard" rows="5" style="width:470px;background-color:#f2f5f7;">' + detailMessage + '</textarea>'
                        + '         </td>'
                        + '     </tr>'
                        /*+ '     <tr>'
                        + '         <td align="left"><br>'
                        + '             <input type="text" id="txtMailAddress" style="font-family:Arial,Helvetica,sans-serif;overflow:hidden;width:300px" value="' + mailAddress + '"></input>'
                        + '         </td>'
                        + '         <td align="right"><br>'
                        + '             <button name="mailButton" id="mailButton" style="font-weight:normal; width: 100px" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" onclick="SendMail(this);">' + mailButtonText + '</button>'
                        + '         </td>'
                        + '     </tr>'*/
                        + '  </table>'
                        + '</td>'
                        + '</tr>'
                    )
                    + '</table>';

    var buttonArr = new Array();
    buttonArr.push({
        text: okButtonText,
        click: function (ev, dialog) { removeDialog(this, dialog); if (typeof (onInfoButtonClicked) === "function") eval(onInfoButtonClicked)(); }
    });

    jqShowDialog(header, fullMessage, buttonArr);
}


function jqShowDialog(header, message, buttons) {
    var div = document.createElement("DIV");
    div.innerHTML = message;
    var escapeFunc = (buttons.length == 2) ? buttons[1].click : buttons[0].click;

    $(div).dialog({
        buttons: buttons,
        title: header,
        modal: true,
        resizable: false,
        minWidth: 500,
        dialogClass: "ui-dialog-message",
        minHeight: 170,
        maxHeight: 600,
        autoResize: true,
        zIndex: 500,
        open: function (event, ui) {
            $(".ui-dialog-titlebar-close", $(this).parent()).hide(); // sağ üstte X ile kapama butonu çıkmasın
            $(".ui-dialog-message .ui-dialog-content").css("margin", "10px");
            $(".ui-dialog").keydown(function (event) {
                if (event.keyCode == 13) {
                    if (buttons.length == 1) {
                        buttons[0].click(null, this);
                    }
                    else {
                        if ($(".ui-dialog-buttonpane").find("button").last().is(':focus'))
                            buttons[1].click(null, this);
                        else
                            buttons[0].click(null, this);
                    }
                }
            });
        },
        close: function (event, ui) { escapeFunc(); }
    });
}

function jqShowHtmlMessage(html, buttons, showHeader, header, width, height) {
    if (header == null) header = "Detay";

    var div = document.createElement("DIV");
    div.innerHTML = html;

    if (buttons == null) {
        buttons = new Array();
        buttons.push({
            text: "Kapat",
            click: function (ev, dialog) { removeDialog(this, dialog); }
        });
    }

    $(div).dialog({
        buttons: buttons,
        title: header,
        modal: true,
        resizable: true,
        minWidth: 700,
        maxHeight: 600,
        autoResize: true,
        zIndex: 500,
        open: function (event, ui) {
            var dialog = $(this).closest(".ui-dialog");
            if (showHeader === true) {
                dialog.find(".ui-dialog-titlebar-close", $(this).parent()).hide(); // sağ üstte X ile kapama butonu çıkmasın            
                dialog.find(".ui-widget-header span").height(20);
            }
            else {
                dialog.find(".ui-dialog-titlebar").hide();
            }
            //$(".popup .ui-widget-header").hide();
            //$(".popup .ui-dialog-content").css("background-color", "#e9e9e9");
            //$(".popup .ui-dialog-buttonpane").css("background-color", "#e9e9e9");
            //$(".ui-dialog-popup .ui-dialog-buttonpane button").addClass("nar-btn nar-form-size50");
        },
        close: function (event, ui) { }
    });

    if (width != null && width > 0)
        $(div).dialog("option", "width", width);

    if (height != null && height > 0)
        $(div).dialog("option", "height", height);
}


function ShowHideDetail(button) {
    var row = $(button).closest(".ui-dialog").find('#ErrorBoxDetailRow');
    if (row.is(':visible')) row.hide();
    else row.show();
}

function SendMail(button) {
    var mailAddresses = $get('txtMailAddress').value;
    var mailBody = $get('txtDetail').value;
    mailBody = mailBody.replace(/\r\n/gi, '<br>');

    if (IsNullOrEmpty(mailAddresses) || IsNullOrEmpty(mailBody)) return;

    Rhapsody.UI.Presenter.Web.Services.CommonService.SendExceptionAsEMail(mailBody, mailAddresses, onSendEMailSuccess, onSendEMailError, button);
}
function onSendEMailSuccess(result, userContext, methodName) {
    if (result == 'Ok')
        ShowHideDetail(userContext);
    else
        alert('Mail Gönderilemedi\r\n' + result);
}
function onSendEMailError(result, userContext, methodName) {
    alert(result);
}

function IsNullOrEmpty(value) {
    return (value == undefined || value == null || value == '');
}

function removeDialog(button, dialog) {
    if (dialog != null) $(dialog).remove();
    else $(button).closest(".ui-dialog").remove();
}