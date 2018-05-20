var cart = {
    init: function () {
        cart.loadData();
        cart.registerEvent();
    },

    registerEvent: function () {
        $("#frmPayment").validate({
            rules: {
                name: "required",
                address: "required",
                email: {
                    required: true,
                    email: true
                },
                phone: {
                    required: true,
                    number: true
                }
            },
            messages: {
                name: "Yêu cầu nhập tên",
                address: "Yêu cầu nhập địa chỉ",
                email: {
                    required: "Bạn cần nhập email",
                    email: "Định dạng email chưa đúng"
                },
                phone: {
                    required: "Số điện thoại được yêu cầu",
                    number: "Số điện thoại phải là số."
                }
            }
        });

        $("#btn-add-to-cart, .btn-add-to-cart").off("click").on("click", function (e) {
            e.preventDefault();
            var productId = $(this).data("id");
            cart.addItem(productId);
        });

        $(".txt-quantity").on("change keyup paste", function () {
            debugger;
            var productId = $(this).data("id");
            var quantity = $(this).val();
            if (parseInt(quantity) < 1) {
                alert("Số lượng không hợp lệ !");
                $(this).val(1);
                return false;
            }
            cart.updateItem(productId, quantity);
        });

        $(".btn-delete-item").off("click").on("click", function () {
            debugger;
            var productId = $(this).data("id");
            var cf = confirm("Bạn có muốn xóa sản phẩm này ?");
            if (cf) {
                cart.deleteItem(productId);
                cart.loadData();
            }
        });

        $("#btn-continue").off("click").on("click", function (e) {
            e.preventDefault();
            location.href = "/";
        });

        $("#btn-delete-all").off("click").on("click", function (e) {
            e.preventDefault();
            cart.deleteAllItem();
        });

        $("#btn-checkout").off("click").on("click", function (e) {
            e.preventDefault();
            $("#divCheckout").show();
        });

        $('#chkUserLoginInfo').off('click').on('click', function () {
            if ($(this).prop('checked'))
                cart.getLoginUser();
            else {
                $('#txtName').val('');
                $('#txtAddress').val('');
                $('#txtEmail').val('');
                $('#txtPhone').val('');
            }
        });

        $('#btnCreateOrder').off('click').on('click', function (e) {
            e.preventDefault();
            var isValid = $('#frmPayment').valid();
            if (isValid) {
                cart.createOrder();
            }

        });

    },

    loadData: function () {
        $.ajax({
            url: "/ShoppingCart/GetAll",
            type: "GET",
            dataType: "json",
            success: function (response) {
                if (response.status) {
                    var template = $("#tplCart").html();
                    var html = "";
                    var data = response.data;
                    if (data.length > 0) {
                        $("#tbl-cart").show();
                    }
                    $.each(data, function (i, item) {
                        html += Mustache.render(template,
                            {
                                ProductId: item.ProductId,
                                ProductName: item.ProductViewModel.Name,
                                Image: item.ProductViewModel.Image,
                                Price: item.ProductViewModel.Price,
                                Quantity: item.Quantity,
                                Amount: item.Quantity * item.ProductViewModel.Price
                            });
                    });

                    if (html === "") {
                        $("#cartContent").html("Không có sản phẩm nào trong giỏ hàng.");
                    }
                    $("#cartBody").html(html);
                    $("#total-amount").text(response.amount);
                    cart.registerEvent();
                }
            }
        });
    },

    addItem: function (productId) {
        $.ajax({
            url: "/ShoppingCart/Add",
            type: "POST",
            data: {
                productId: productId
            },
            dataType: "json",
            success: function (response) {
                if (response.status) {
                    alert("Thêm sản phẩm thành công");
                }
            }
        });
    },

    updateItem: function (productId, quantity) {
        debugger;
        $.ajax({
            url: "/ShoppingCart/Update",
            type: "POST",
            dataType: "json",
            data: {
                productId: productId,
                quantity: quantity
            },
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                }
            }
        });
    },

    deleteItem: function (productId) {
        $.ajax({
            url: "/ShoppingCart/Delete",
            type: "POST",
            data: {
                productId: productId
            },
            dataType: "json",
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                }
            }
        });
    },

    deleteAllItem: function () {
        $.ajax({
            url: "/ShoppingCart/DeleteAll",
            type: "POST",
            dataType: "json",
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                }
            }
        });
    },

    getLoginUser: function () {
        $.ajax({
            url: '/ShoppingCart/GetUser',
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var user = response.data;
                    $('#txtName').val(user.FullName);
                    $('#txtAddress').val(user.Address);
                    $('#txtEmail').val(user.Email);
                    $('#txtPhone').val(user.PhoneNumber);
                }
            }
        });
    },

    createOrder: function () {
        var order = {
            CustomerName: $('#txtName').val(),
            CustomerAddress: $('#txtAddress').val(),
            CustomerEmail: $('#txtEmail').val(),
            CustomerMobile: $('#txtPhone').val(),
            CustomerMessage: $('#txtMessage').val(),
            PaymentMethod: "Thanh toán tiền mặt",
            Status: false
        }
        $.ajax({
            url: '/ShoppingCart/CreateOrder',
            type: 'POST',
            dataType: 'json',
            data: {
                orderViewModel: JSON.stringify(order)
            },
            success: function (response) {
                if (response.status) {
                    console.log('create order ok');
                    $('#divCheckout').hide();
                    cart.deleteAllItem();
                    setTimeout(function () {
                        $('#cartContent').html('Cảm ơn bạn đã đặt hàng thành công. Chúng tôi sẽ liên hệ sớm nhất.');
                    }, 2000);

                }
            }
        });
    },


}


cart.init();