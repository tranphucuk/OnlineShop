var email = {
    init: function () {
        this.registerEvents();
    },

    registerEvents: function () {
        $('#btnSubscribe').off('click').on('click', function () {
            $.ajax({
                url: 'Home/NewsLester',
                data: {
                    emailAddress: $('#emailal').val()
                },
                type: 'POST',
                dataType: 'JSON',
                success: function (res) {
                    if (res.status) {
                        Ply.dialog(
                            "alert",
                            { effect: "3d-flip" },
                            "You are now successfully subscribed our monthly newsletter. Thank you!!!"
                        );
                    } else {
                        Ply.dialog(
                            "alert",
                            { effect: "3d-flip" },
                            res.data
                        );
                    }
                }
            });
        });
    },
};
email.init();