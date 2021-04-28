function SuccessMessage(message) {
    $.jnoty(message, {

        theme: 'jnoty-success',
        icon: 'fa fa-check-circle'

    });
}
function ErrorMessage(message) {
    $.jnoty(message, {

        theme: 'jnoty-warning',
        icon: 'fa fa-check-circle'

    });
}
function AlertMessage(message) {
    $.jnoty(message, {

        theme: 'jnoty-alert',
        icon: 'fa fa-check-circle'

    });
}
function InfoMessage(message) {
    $.jnoty(message, {

        theme: 'jnoty-info',
        icon: 'fa fa-check-circle'

    });
}
function DangerMessage(message) {
    $.jnoty(message, {

        theme: 'jnoty-danger',
        icon: 'fa fa-check-circle'

    });
}