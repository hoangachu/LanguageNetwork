"use strict";
(function () {
   
    //$('input[name=switch-two]').on("click",function () {
       
    //});
    $('.btnAsk').click(function(){
        window.location.href = "/question/ask";
    });
})();
function Setactive(id) {
    debugger
    $('.switch-field').find('label').removeClass('labelactive');
    $('.switch-field').find('label.lbl' + id).addClass('labelactive');
}
