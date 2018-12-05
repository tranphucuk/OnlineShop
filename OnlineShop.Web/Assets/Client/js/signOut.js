var signOut = {
    init: function () {
        signOut.registerEvents();
    },

    registerEvents: function () {
        $("#btnLogout").off('click').on('click', function (e) {
            e.preventDefault();
            $('#frmLogout').submit();
        });
    }
}

signOut.init();