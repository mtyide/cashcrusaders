function getSuppliers() {
    $("#save-supplier-btn").attr('disabled', 'disabled');
    $("#supplier-body").html('Fetching suppliers, one moment...');
    $.get("api/supplier/getsuppliers", function (res) {
        var suppliers = JSON && JSON.parse(res) || $.parseJSON(res);
        var content = '';
        var count = suppliers.length;
        $.each(suppliers, function (key, item) {
            var name = item.Name;
            var id = item.Id;
            content += '<tr>';
            content += '  <td valign="top">';
            content += '      <span title="' + name + '">' + name + '</span><span class="pull-right"><a onclick="displayProductsBlock(' + id + ');" style="cursor: pointer;font-weight: bold;">Add Products</a> | <a onclick="getProducts(' + id + ');" style="cursor: pointer;font-weight: bold;">Get Products</a> | <a onclick="getOrders(' + id + ');" style="cursor: pointer;font-weight: bold;">Get Orders</a></span>';
            content += '  </td>';
            content += '</tr>';
        });
        if (count === 0) {
            content = 'No suppliers in the database. Create one';
            $("#supplier-body").html(content);
        } else {
            $("#supplier-body").html(content);
            $("#supplier-items").DataTable({ 'paging': true, 'lengthChange': false, 'searching': false, 'ordering': true, 'info': false, 'autoWidth': true, 'destroy': true, 'retrieve': true });
        }

        $("#save-supplier-btn").removeAttr('disabled');
    });
}

function createSupplier() {
    var name = $("#name").val();
    if (name) {
        $("#save-supplier-btn").html('Busy, one moment...');
        $("#save-supplier-btn").attr('disabled', 'disabled');
        var supplierData = $("#fSuppliers");
        var data = supplierData.serialize();
        console.log(data);
        $.post("api/supplier/createsupplier", data, function (res) {
            if (res > 0) {
                getSuppliers();
                $("#name").val(null);
                $("#name").focus();
            }
            $("#save-supplier-btn").html('Save');
            $("#save-supplier-btn").removeAttr('disabled');
        });
    }
}