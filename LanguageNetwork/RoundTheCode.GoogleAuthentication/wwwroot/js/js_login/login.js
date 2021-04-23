const ACCEPT_LOGIN = 1;
document.getElementById('btnlogin').addEventListener('click', () => {
    debugger;
    var user = $("#email").val();
    var password = $("#password").val();
    $.ajax({
        url: 'Account/LoginUser',
        type : "POST",
        data: { user: user, password: password },
        success: function (response) {
            if (response.data == ACCEPT_LOGIN) {
                window.open(response.url, '_self');
            }
            else {
                toastr.error('Username or Password not correct.Try against!');
            }
        }
    });

});
