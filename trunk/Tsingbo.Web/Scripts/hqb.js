$(function () {
    //#region Search
    //Watermark
    $('.watermark input').focus(function () {
        $(this).siblings('label').hide();
    }).blur(function () {
        if ($(this).val() == '') {
            $(this).siblings('label').show();
        }
    });
    //Watermark init
    $('.watermark input').each(function () {
        if ($(this).val() != '') {
            $(this).siblings('label').hide();
        }
    });
    //#endregion

    // Datepicker
    $(":input[data-datepicker=true]").datepicker({
        numberOfMonths: 1,
        //dateFormat: 'dd-mm-yy',
        constrainInput: true,
        showButtonPanel: true,
        stepMonths: 1,
        //minDate: "0D",
        maxDate: "+331D",
        defaultDate: new Date(),
        buttonImageOnly: false,
        inline: true
    });
    //Button
    $('button').button();

    $('.radioset').buttonset();

});