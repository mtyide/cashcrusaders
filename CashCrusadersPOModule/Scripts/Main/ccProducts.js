function getProducts(id) {
    displayProductsBlock(id);
    $("#save-product-btn").attr('disabled', 'disabled');
    $("#product-body").html('Fetching products, one moment...');
    var data = { supplierId: id };
    $.get("api/product/getproducts", data, function (res) {
        var products = JSON && JSON.parse(res) || $.parseJSON(res);
        var content = '';
        var count = products.length;
        var i = 0;
        $.each(products, function (key, item) {
            var code = item.Code;
            var description = item.Description;
            var price = item.Price;
            var id = item.Id;
            purchaseOrderItemsTotal[key] = price;
            content += '<tr>';
            content += '  <td valign="top">';
            content += '      <span title="' + code + '">' + code + '</span>';
            content += '  </td>';
            content += '  <td valign="top">';
            content += '      <span title="' + description + '">' + description + '</span>';
            content += '  </td>';
            content += '  <td valign="top">';
            content += '      <span title="' + price + '">' + price + '</span>';
            content += '  </td>';
            content += '  <td valign="top">';
            content += '      <span class="pull-right"><input type="checkbox" onclick="controlPurchaseItemSelection($(this))" value="' + id + '" id="check-' + i + '" /> <input type="number" title="Qty" placeholder="Qty" onchange="updateTotal(' + price + ', $(this), ' + id + ')" id="qty-' + i + '" /></span>';
            content += '  </td>';
            content += '</tr>';
            i++;
        });
        if (count === 0) {
            content = 'No products in the database. Create one';
            $("#product-body").html(content);
        } else {
            $("#product-body").html(content);
            $("#product-items").DataTable({ 'paging': true, 'lengthChange': false, 'searching': false, 'ordering': true, 'info': false, 'autoWidth': true, 'destroy': true, 'retrieve': true });
            $("#complete-order").fadeIn();
            $("#complete-order-btn").removeAttr('disabled');
        }
        $("#save-product-btn").removeAttr('disabled');
    });
}

function displayProductsBlock(id) {
    $("#products-home").fadeIn();
    $("#supplierid").val(id);
    $("#get-products").on("click", function () {
        getProducts(id);
    });
    $("#get-orders").on("click", function () {
        getOrders(id);
    });
}

function createProduct() {
    var code = $("#code").val();
    var description = $("#description").val();
    if (code && description) {
        var price = document.getElementById("price");
        var check = price.checkValidity();
        var id = $("#supplierid").val();
        if (check) {
            $("#save-product-btn").html('Busy, one moment...');
            $("#save-product-btn").attr('disabled', 'disabled');
            var productData = $("#fProducts");
            var data = productData.serialize();
            $.post("api/product/createproduct", data, function (res) {
                if (res > 0) {
                    getProducts(id);
                    $("#code").val(null);
                    $("#description").val(null);
                    $("#price").val(null);
                }
                $("#save-product-btn").html('Save');
                $("#save-product-btn").removeAttr('disabled');
            });
        }
    }
}