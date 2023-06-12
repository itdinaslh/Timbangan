$(document).ready(function () {
    loadTable();
});

function loadContent() {
    loadTable();
}

function loadTable() {
    $('#tblData').DataTable().destroy();
    $('#tblData').DataTable({
        serverSide: true,
        processing: true,
        responsive: true,
        stateSave: true,
        lengthMenu: [10, 20, 30],
        ajax: {
            url: '/api/master/penugasan',
            method: 'POST'
        },
        columns: [
            { data: 'clientID', name: 'clientID', autowidth: true },
            { data: 'clientName', name: 'clientName', autowidth: true },
            {
                data: 'clientID',
                render: function (data, type, row) {
                    return "<button class='btn btn-sm btn-success mr-2 showMe' style='width:100%;' data-href='/master/penugasan/edit/?id="
                        + row.clientID + "'><i class='fa fa-edit'></i> Edit</button>";
                }
            }
        ],
        order: [[0, "desc"]]
    });
}