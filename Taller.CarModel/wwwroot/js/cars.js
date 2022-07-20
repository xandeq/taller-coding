var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Car/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "autoWidth": true },
            { "data": "make", "autoWidth": true },
            { "data": "model", "autoWidth": true },
            { "data": "year", "autoWidth": true },
            { "data": "doors", "autoWidth": true },
            { "data": "color", "autoWidth": true },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <input type="text" id="gtp${data}" name="gtp${data}" />
                        <a onclick="GuessThePrice(${data})" class="btn btn-info text-white" style='cursor: pointer; width: 100px'>
                            <i class="far fa-question-mark"></i> Guess The Price
                        </a>
                    </div>`;
                },
                "width": "20%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Car/Upsert/${data}" class="btn btn-success text-white" style='cursor: pointer; width: 100px'>
                            <i class="far fa-edit"></i> Edit
                        </a>
                        &nbsp;
                        <a onclick=Delete("/Car/Delete/${data}") class="btn btn-danger text-white" style='cursor: pointer; width: 100px'>
                            <i class="far fa-trash-alt"></i> Delete
                        </a>
                        &nbsp;
                    </div>`;
                },
                "width": "20%"
            }
        ],
        "language": {
            "emptyTable": "No records found."
        }
    });
}

function GuessThePrice(id) {
    let price = $('#gtp' + id).val();
    let url = "/Car/GuessThePrice/";
    console.log(url);
    $.ajax({
        type: "POST",
        url: url,
        data: { id: id, price: price },
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
            else {
                toastr.error(data.message);
            }
        }
    })
}

function Delete(url) {
    console.log(url);
    swal({
        title: "Are you sure?",
        text: "You can't recover it.",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#FF6B55",
        confurmButtonText: "Yes, Delete.",
        closeOnConfirm: true
    }, function () {
        $.ajax({
            type: "DELETE",
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        })
    })
}

function ShowMessage() {
    toastr.success(msg);
}