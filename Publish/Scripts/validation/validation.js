// THE SCRIPT THAT CHECKS IF THE KEY PRESSED IS A NUMERIC OR DECIMAL VALUE.
function isNumber(evt, val, len) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    
    if (charCode == 8 || charCode == 9 || charCode == 46)
        return true;

    if (charCode != 45 && (charCode != 46 || $(this).val().indexOf('.') != -1) && (charCode < 48 || charCode > 57))
        return false;
    if (val.length >= len)
        return false;
    return true;
}

//isLetterOnly
function isLetters(evt, str, len) {
    //evt = (evt) ? evt : event;
    //var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode :
    //      ((evt.which) ? evt.which : 0));

    var charCode = (evt.which) ? evt.which : event.keyCode
    //alert(charCode);
    var filter = "/^[a-zA-Z\ \'\u00c0-\u1ef9]+$/";
    
    if (charCode == 8 || charCode == 9 || charCode == 39)
        return true;

    //charcode english
//    if (charCode > 31 && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122) && charCode != 32 && charCode != 231) {
//        //alert("Enter letters only.");
//        return false;
//    }

    //check for unicode
    var enter = String.fromCharCode(evt.which);
    var invalidStr = enter.replace(/^[a-zA-Z0-9\ \'\u00c0-\u1ef9]/gi, '');
    if (invalidStr.length > 0 || str.length >= len || enter == "\'")
        return false;

    return true;
}