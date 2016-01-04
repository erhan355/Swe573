
function toAscii(str) {
    //str = str.replace(/\s/g, '').toUpperCase();
    str = str.toUpperCase();

    //var nonAscii = "ÇĞİÖŞÜ";
    //var ascii = "CGIOSU";
    var nonAscii = "İ";
    var ascii = "I";

    for (var i = 0; i < nonAscii.length; i++) {
        var re = new RegExp(nonAscii[i], "g");
        str = str.replace(re, ascii[i]);
    }
    return str;
}
//



function IsNullOrEmpty(value) {
    return (value == undefined || value == null || value == '');
}
function replaceCharacter(value, tobeReplaced) {
    if (tobeReplaced == '.') {
        return value.replace(/\./g, '');
    }
    if (tobeReplaced == ',') {
        return value.replace(/,/g, '.');
    }
}
function replaceComaToPoint(value) {
    return replaceCharacter(value, ',', '.');
}
function removePoints(value) {
    return replaceCharacter(value, '.', '');
}

function setLoadingTemplateVisibility(show, msg, control) {
    if (show) {
        if (msg == null || msg == '')
            msg = "İşlem Yapılıyor. Lütfen Bekleyiniz...";
        var img = ensureAppPath('Content/loading_indicator.gif');
        msg = '<table style="align:center; width:100%"><tr><td><img src="' + img + '"/></td><td><div style="font-size:12px;margin-left:20px">' + msg + '</div></td></tr></table>';

        if (control != null) {
            $(control).block({
                message: $(msg),
                css: {
                    "padding": "10px 20px 10px 30px",
                    "align": "center",
                    "width": "auto",
                    "border-style": "solid",
                    "border-width": "medium",
                    "border-color": "#c7dcf1",
                    "border-radius": "10px"
                }
            });
        }
        else {
            $.blockUI({
                message: $(msg),
                css: {
                    "padding": "10px 20px 10px 30px",
                    "align": "center",
                    "width": "auto",
                    "border-style": "solid",
                    "border-width": "medium",
                    "border-color": "#c7dcf1",
                    "border-radius": "10px"
                }
            });
        }
    }
    else {
        if (control != null) {
            $(control).unblock();
        }
        else {
            $.unblockUI();
        }
    }
}
function ShowLoading(msg) {
    setLoadingTemplateVisibility(true, msg);
}

function HideLoading() {
    setLoadingTemplateVisibility(false);
}
