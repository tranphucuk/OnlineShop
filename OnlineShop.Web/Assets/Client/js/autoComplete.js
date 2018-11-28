var search = {
    init: function () {
        search.registerEvents();
    },

    registerEvents: function () {
        //$('#txtKeyword').autocomplete({
        //    source: function (request, response) {
        //        $.ajax({
        //            url: "/Product/GetListProductByName",
        //            dataType: "json",
        //            data: {
        //                keyword: request.term
        //            },
        //            success: function (res) {
        //                response(res.data);
        //            }
        //        });
        //    },
        //    minLength: 0,
        //    select: function (event, ui) {
        //        log("Selected: " + ui.item.value + " aka " + ui.item.id);
        //    }
        //});


        $('#txtKeyword').autoComplete({
            minChars: 0,
            source: function (request, suggest) {
                $.ajax({
                    url: "/Product/GetListProductByName",
                    dataType: "json",
                    data: {
                        keyword: request
                    },
                    success: function (res) {
                        suggest(res.data);
                        request = request.toLowerCase();
                        var choices = res.data;
                        var matches = [];
                        for (i = 0; i < choices.length; i++)
                            if (~choices[i].toLowerCase().indexOf(request)) {
                                matches.push(choices[i]);
                            } else {
                                matches.push("");
                            }

                        suggest(matches);
                    }
                });
            }
        });
    },
};

search.init();