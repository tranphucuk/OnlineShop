var cart = {
    init: function () {
        $("#yourCart").off('click').on('click', cart.loadData());
        cart.registerEvents();
    },

    registerEvents: function () {
        $('#frmClientDetail').validate({
            rules: {
                name: 'required',
                address: 'required',
                mobile: {
                    required: true,
                    minlength: 9,
                    number: true,
                },
                email: {
                    required: true,
                    email: true
                },
            },
            message: {
                name: 'Please provide your name.',
                address: 'Please provide your address.',
                mobile: {
                    required: 'Please provide your phone number.',
                    minlength: 'Please provide a correct format phone number.',
                    number: 'invalid phone number, ex: 0988123456.',
                },
                email: {
                    required: 'Please provide your phone number.',
                    email: 'Invalid email address. Please check.',
                },
            },
        });
        $('.btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.addItem(productId);
        });
        $('.btnDeleteItem').off('click').on('click', function () {
            var cartId = parseInt($(this).data('id'));
            cart.deleteItem(cartId);
        });
        $('.txtQuantity').off('change').on('change', function () {
            var price = $(this).data('price');
            var quantity = $(this).val();
            var cartId = $(this).data('id');

            var total = price * quantity;
            $('#total_' + cartId).text(cart.formatNumber(total));
            $('#totalBill').text(cart.formatNumber(cart.getTotalBill()));
            cart.updateBill();
        });

        $('#btnCntinue').off('click').on('click', function () {
            window.location.href = '/';
        });

        $('#btnClearCart').off('click').on('click', function () {
            cart.deleteAll();
        });

        $('#btnCheckout').off('click').on('click', function () {
            $('.checkout').toggle();
        });

        $('#checkUserLogin').off('click').on('click', function () {
            if ($(this).is(':checked'))
                cart.getLoginUser();
            else {
                $('#txtName').val('');
                $('#txtAddress').val('');
                $('#txtPhone').val('');
                $('#txtEmail').val('');
            }
        });

        $('#btnCreateOrder').off('click').on('click', function () {
            var isValid = $('#frmClientDetail').valid();
            if (isValid) {
                cart.Createorder();
            }
        });
    },

    Createorder: function () {
        var orderVm = {
            CustomerName: $('#txtName').val(),
            CustomerAddress: $('#txtAddress').val(),
            CustomerMobile: $('#txtPhone').val(),
            CustomerEmail: $('#txtEmail').val(),
            CustomerMessage: $('#txtMessage').val(),
            PaymentMethod: 'Cash',
            PaymentStatus: 'Pending',
            CreatedBy: $('#txtName').val(),
            Status: false,
        };
        $.ajax({
            url: 'ShoppingCart/CreateOrder',
            type: 'POST',
            dataType: 'json',
            data: {
                orderString: JSON.stringify(orderVm),
            },
            success: function (res) {
                if (res.status) {
                    cart.deleteAll();
                    $('.checkout').hide();
                    setTimeout(function () {
                        $('#cartContent').html('Thank you for purchasing at our shop. Your order is processing and will be delivering to you in next working days');
                    }, 1000)
                }
            }
        });
    },

    getLoginUser: function () {
        $.ajax({
            url: 'ShoppingCart/GetUserDetails',
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    $('#txtName').val(res.data.Fullname);
                    $('#txtAddress').val(res.data.Address);
                    $('#txtPhone').val(res.data.PhoneNumber);
                    $('#txtEmail').val(res.data.Email);
                };
            },
        });
    },

    getTotalBill: function () {
        var listQuantity = $('.txtQuantity');
        var total = 0;
        $.each(listQuantity, function (i, item) {
            total += $(item).data('price') * $(item).val();
        });
        return total;
    },

    formatNumber: function (num) {
        var string = numeral(num).format('$ 0,0[.]00');
        return string;
    },

    addItem: function (productId) {
        $.ajax({
            url: 'ShoppingCart/Add',
            data: {
                productId: productId,
            },
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    $.notify(res.data + " added to cart successfully.", "success");
                }
            }
        });
    },

    updateBill: function () {
        var listItems = [];
        $.each($('.txtQuantity'), function (i, item) {
            listItems.push({
                CartID: $(item).data('id'),
                Quantity: $(item).val(),
            });
        });
        $.ajax({
            url: 'ShoppingCart/Update',
            data: {
                cartData: JSON.stringify(listItems)
            },
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    cart.loadData();
                }
            },
        });
    },

    deleteItem: function (cartId) {
        $.ajax({
            url: 'ShoppingCart/DeleteItem',
            data: {
                cartId: cartId,
            },
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    cart.loadData();
                }
            }
        });
    },

    deleteAll: function () {
        $.ajax({
            url: 'ShoppingCart/DeleteAll',
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    cart.loadData();
                };
            }
        });
    },

    loadData: function () {
        $.ajax({
            url: "ShoppingCart/GetAll",
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    var template = $('#tplCart').html();
                    var html = '';
                    var data = res.data;
                    if (data.length > 0 && template != null) {
                        $.each(data, function (i, item) {
                            html += Mustache.render(template, {
                                CartId: item.CartID,
                                ProductName: item.Product.Name,
                                Quantity: item.Quantity,
                                Image: item.Product.Image,
                                Price: item.Product.Price,
                                PriceF: cart.formatNumber(item.Product.Price),
                                Total: cart.formatNumber(item.Product.Price * item.Quantity) || 0,
                            })
                        });
                        $('#cartBody').html(html);
                        $('#totalBill').text(cart.formatNumber(cart.getTotalBill()));
                    }
                    else {
                        $('#cartContent').html('Your cart is Empty.');
                        $('.checkout').hide();
                    }
                } else {
                    $('#cartContent').html('Your cart is Empty.');
                    $('.checkout').hide();
                }
                cart.registerEvents();
            }
        });
    }
}
cart.init();