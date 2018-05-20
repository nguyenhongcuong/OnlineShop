var cart = {
    init: function () {
        cart.loadData();
        cart.registerEvent();
    },

    registerEvent: function () {
        var a = $(".txt-quantity");
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

        $(".btn-delete-item").off("click").on("click",
            function () {
                debugger;
                var productId = $(this).data("id");
                var cf = confirm("Bạn có muốn xóa sản phẩm này ?");
                if (cf) {
                    cart.deleteItem(productId);
                    cart.loadData();
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
    }



}


cart.init();