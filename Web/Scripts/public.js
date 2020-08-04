/*
Show or Hide field 
*/
function showHidden(id) {
    if ($("#" + id).css("display") == "none")
        $("#" + id).show();
    else
        $("#" + id).hide();
}

/*
Check form Submit for Client
*/
function checkSubmit(label, num, err) {

    for (var i = 1; i <= num; i++) {
        /*alert('input.' + label + i + ' : ' + $('input.' + label + i).val());*/
        if ($('input.' + label + i).val() == '') {
            $('label.' + err + i).show();
            $('input.' + label + i).focus();
            return false;
        }
        else
            $('label.' + err + i).hide();
    }
    return true;
}