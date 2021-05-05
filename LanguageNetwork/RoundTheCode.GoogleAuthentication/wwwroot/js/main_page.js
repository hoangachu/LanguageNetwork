"use strict";
(function () {

    //$('input[name=switch-two]').on("click",function () {

    //});
    $('.btnAsk').click(function () {
        let html = $('#AskBody').html();

        let dialogify = new Dialogify(html)
            .title('Ask a public question')
            .buttons([
                {
                    type: Dialogify.BUTTON_PRIMARY,
                    text: 'Publish question',
                    click: function (e) {
                        console.log('danger-style button click');
                      /*  this.close();*/
                    }
                },
                {
                    type: Dialogify.BUTTON_DANGER,
                    text: 'Cancel',
                    click: function (e) {
                        console.log('danger-style button click');
                        /*  this.close();*/
                    }
                }
            ], { position: Dialogify.BUTTON_CENTER });

        dialogify.showModal();
        $('.sidebarleft').css('display', 'none');
        $('.wmd-preview').css('width', '500px');
        $('.wmd-input ').css('width', '500px'); 
        $('.dialogify__body').css('padding-right', '80px');
       /* window.location.href = "/question/ask";*/
    });


})();

function Setactive(id) {
    debugger
    $('.switch-field').find('label').removeClass('labelactive');
    $('.switch-field').find('label.lbl' + id).addClass('labelactive');
}
