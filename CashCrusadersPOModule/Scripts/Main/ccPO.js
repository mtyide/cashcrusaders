$(document).ready(function () {
    init();
    $("#save-supplier-btn").on("click", function () {
        createSupplier();
    });
    $("#save-product-btn").on("click", function () {
        createProduct();
    });
    $("#complete-order-btn").on("click", function () {
        createOrder();
    });
    $("#close-product-btn").on("click", function () {
        $("#products-home").attr('style', 'display: none');
        $("#supplierid").val(null);
    });
});

function init() {
    getSuppliers();
}

function getPurchaseOrders(orderId) {
    $("#div-purchaseorders-items").fadeIn();
    $("#get-purchaseorders").on("click", function () {
        getPurchaseOrders(orderId);
    });
    $("#purchaseorders-body").html('Fetching purchase orders, one moment...');
    var data = { id: orderId };
    $.get("api/po/getpurchaseorder", data, function (res) {
        var products = JSON && JSON.parse(res) || $.parseJSON(res);
        var content = '';
        var count = products.length;
        $.each(products, function (key, item) {
            var code = item.Code;
            var description = item.Description;
            var price = item.Price;
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
            content += '</tr>';
        });
        if (count === 0) {
            content = 'No purchase order items in the database. Create one';
            $("#purchaseorders-body").html(content);
        } else {
            $("#purchaseorders-body").html(content);
            $("#purchaseorders-items").DataTable({ 'paging': true, 'lengthChange': false, 'searching': false, 'ordering': true, 'info': false, 'autoWidth': true, 'destroy': true, 'retrieve': true });
        }
    });
}

var purchaseOrderItems = [];
var purchaseOrderItemsTotal = [];
function createOrder() {
    var supplierId = $("#supplierid").val();
    var data = { Total: total, VAT: 0.14, SupplierID: supplierId, Items: purchaseOrderItems };
    if (purchaseOrderItems.length !== 0) {
        $.post("api/po/createorder", data, function (res) {
            $("#modal-task").modal();
            getOrders(supplierid);
        });
    }
}

function updateTotal(price, input, id) {
    var value = input.val();
    var total = 0;
    $.each(purchaseOrderItemsTotal, function (i) {
        var price = purchaseOrderItemsTotal[i];
        var check = $("#check-" + i);
        var input = $("#qty-" + i);
        var qty = input.val();
        if (check.prop('checked')) {
            total += qty * price;
        }
    });
    $("#purchase-total").html(total);
}

function controlPurchaseItemSelection(input) {
    var checked = input.prop('checked');
    var id = input.val();
    if (checked) {
        purchaseOrderItems.push(id);
    } else {
        $.each(purchaseOrderItems, function (i) {
            if (purchaseOrderItems[i] === id) {
                purchaseOrderItems.splice(i, 1);
                return false;
            }
        });
    }
}

function reset() {
    $("#products-home").fadeOut();
    $("#orders-home").fadeOut();
    $("#id").val(null);
    total = 0;
    purchaseOrderItems = [];
    init();
}

function getOrders(supplierid) {
    console.log(supplierid);
    $("#div-purchaseorders-items").fadeOut();
    $("#orders-home").fadeIn();
    $("#orders-body").html('Fetching orders, one moment...');
    var data = { id: supplierid };
    $.get("api/po/getorders", data, function (res) {
        var orders = JSON && JSON.parse(res) || $.parseJSON(res);
        var content = '';
        var count = orders.length;
        $.each(orders, function (key, item) {
            var created = item.Created;
            var total = item.Total;
            var vat = item.VAT;
            var id = item.Id;
            content += '<tr>';
            content += '  <td valign="top">';
            content += '      <span title="' + created + '">' + created + '</span>';
            content += '  </td>';
            content += '  <td valign="top">';
            content += '      <span title="' + total + '">' + total + '</span>';
            content += '  </td>';
            content += '  <td valign="top">';
            content += '      <span title="' + vat + '">' + vat + '</span>';
            content += '  </td>';
            content += '  <td valign="top">';
            content += '      <span class="pull-right"><a onclick="getPurchaseOrders(' + id + ');" style="cursor: pointer;font-weight: bold;">Get Purchase Orders</a></span>';
            content += '  </td>';
            content += '</tr>';
        });
        if (count === 0) {
            content = 'No orders in the database. Create one';
            $("#orders-body").html(content);
        } else {
            $("#orders-body").html(content);
            $("#orders-items").DataTable({ 'paging': true, 'lengthChange': false, 'searching': false, 'ordering': true, 'info': false, 'autoWidth': true, 'destroy': true, 'retrieve': true });
        }
    });
}