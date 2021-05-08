"use strict";


(function () {
    $('#btnpublish').click(function () {
        debugger
        var title = $('#title').val();
        var body = $('#wmd-preview').html();
        var labelinfo = document.getElementsByClassName('label-info');
        var listtag = '';
        for (var index = 0; index < labelinfo.length; index++) {
            if (index == 0) {
                listtag += labelinfo[index].innerText
            } else { listtag += ("," + labelinfo[index].innerText) }
        }
        if (title == "" || title == null) { DangerMessage("Tiêu đề chưa được nhập"); return }
        if (body == "" || body == null) { DangerMessage("Nội dung chưa được nhập"); return }
        $.ajax({
            url: 'RegistQuestion',
            data: { title: title, body: body, tag: listtag },
            success: function () {
                $('#btncancel').click();
                SuccessMessage("Đăng câu hỏi thành công");
                setTimeout(function () {

                    window.location.href = window.location.href;
                }, 2000);
            }

        });
    });
    //$('input[name=switch-two]').on("click",function () {

    //});
    //$('.btnAsk').click(function () {
    //    let html = $('#AskBody').html();

    //    let dialogify = new Dialogify(html)
    //        .title('Ask a public question')
    //        .buttons([
    //            {
    //                type: Dialogify.BUTTON_PRIMARY,
    //                text: 'Publish question',
    //                click: function (e) {
    //                    console.log('danger-style button click');
    //                  /*  this.close();*/
    //                }
    //            },
    //            {
    //                type: Dialogify.BUTTON_DANGER,
    //                text: 'Cancel',
    //                click: function (e) {
    //                    console.log('danger-style button click');
    //                    /*  this.close();*/
    //                }
    //            }
    //        ], { position: Dialogify.BUTTON_CENTER });

    //    dialogify.showModal();

    //    $('.sidebarleft').css('display', 'none');
    //    $('.wmd-preview').css('width', '500px');
    //    $('.wmd-input ').css('width', '500px'); 
    //    $('.dialogify__body').css('padding-right', '80px');
    /* window.location.href = "/question/ask";*/
    /*});*/
})();

function Setactive(id) {
    $('.switch-field').find('label').removeClass('labelactive');
    $('.switch-field').find('label.lbl' + id).addClass('labelactive');
}
window.onload = function () {

    //ấn button insert image trên thanh editor
    document.getElementById("wmd-image-button").addEventListener("click", function () {
        $('.wmd-prompt-background').css('display', 'none'); 
        $('.wmd-prompt-dialog').css('display', 'none');
        let html = ' <input type="file" onchange="preview()">' +
           
            '<img  id = "frame" src = "" width = "100px" height = "100px" class="mt-5"/>';

        let dialogify = new Dialogify(html)
            .title('Tải ảnh của bạn lên từ thiết bị')
            .buttons([
                '<a class="btn btn-primary" href="javascript:;">Tải lên</a>'
                    ,
                {
                    type: Dialogify.BUTTON_DANGER,
                    click: function (e) {
                        console.log('danger-style button click');
                        this.close();
                        $('.wmd-prompt-dialog').css('display', 'none');
                    }
                }
            ], { position: Dialogify.BUTTON_CENTER });

        dialogify.showModal();
        $('.wmd-prompt-dialog').css('display', 'none');
    });

 
};

function preview() {
    frame.src = URL.createObjectURL(event.target.files[0]);
}


