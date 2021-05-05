(function () {
    $('#taginput').change(function () {

        if ($('.bootstrap-tagsinput').find('span').length >= 2 && $('.bootstrap-tagsinput').find('span').length <= 10) {

        }
        else if ($('.bootstrap-tagsinput').find('span').length > 10) {
            $('.bootstrap-tagsinput').find('span').last().remove();
            $('.bootstrap-tagsinput').find('span.tag.label.label-info').last().remove();
            ErrorMessage('Do not enter more than 5 tags');
        }
        else {

            /* ErrorMessage('error');*/
        }
        $('.bootstrap-tagsinput').find('input').attr('placeholder', '');
    });
    $('#wmd-image-button').click(function () {
        alert(1)
    });
})();
