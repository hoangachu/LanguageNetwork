const ACCEPT_LOGIN = 1;
(function () {

  
})();
const signUpButton = document.getElementById('signUp');
const signInButton = document.getElementById('signIn');
const container = document.getElementById('container');

document.getElementById('signUp').addEventListener('click', () => {
    container.classList.add("right-panel-active");
});

document.getElementById('signIn').addEventListener('click', () => {
    container.classList.remove("right-panel-active");
});
document.getElementById('googlelogin').addEventListener('click', () => {
    window.location.href = '/account/google-login';
});
document.getElementById('googleLogin').addEventListener('click', () => {
    window.location.href = '/account/google-login';
});
document.getElementById('fblogin').addEventListener('click', () => {
    window.location.href = '/account/facebook-login';
});
document.getElementById('fbLogin').addEventListener('click', () => {
    window.location.href = '/account/facebook-login';
});
document.getElementById('btnlogin').addEventListener('click', () => {
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
