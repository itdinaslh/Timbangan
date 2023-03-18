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
        lengthMenu: [5, 10, 20],
        ajax: {
            url: '/api/master/tipe-kendaraan',
            method: 'POST'
        },
        columns: [
            { data: 'tipeKendaraanID', name: 'tipeKendaraanID', autowidth: true },
            { data: 'namaTipe', name: 'namaTipe', autowidth: true },
            { data: 'kode', name: 'kode', autowidth: true },
            {
                data: 'tipeKendaraanID',
                render: function (data, type, row) {
                    return "<button class='btn btn-sm btn-success mr-2 showMe' style='width:100%;' data-href='/master/tipe-kendaraan/edit/?tipe="
                        + row.tipeKendaraanID + "'><i class='fa fa-edit'></i> Edit</button>";
                }
            }
        ],
        order: [[0, "desc"]]
    });
}