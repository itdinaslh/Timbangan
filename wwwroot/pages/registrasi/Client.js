$(document).ready(function () {
    loadTable();
});

$(document).on('select2:open', () => {
    document.querySelector('.select2-search__field').focus();
});

$(document).on('shown.bs.modal', '#myModal', function () {
    PopulateStatus();
    PopulateTypes();
    
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
            url: '/api/registrasi/clients',
            method: 'POST'
        },
        columns: [
            { data: 'clientID', name: 'clientID', autowidth: true },
            { data: 'clientName', name: 'clientName', autowidth: true },
            { data: 'clientType', name: 'clientType', autowidth: true },
            { data: 'statusName', name: 'statusName', autowidth: true },
            { data: 'pkmID', name: 'pkmID', autowidth: true },
            {
                data: 'clientID',
                render: function (data, type, row) {
                    return "<button class='btn btn-sm btn-success mr-2 showMe' style='width:100%;' data-href='/registrasi/ekspenditur/edit/?id="
                        + row.clientID + "'><i class='fa fa-edit'></i> Edit</button>";
                }
            }
        ],
        columnDefs: [
            { className: 'text-center', targets: [2, 3, 5] }
        ],
        order: [[0, "desc"]]
    });
}

function PopulateStatus() {
    $('.sStatus').select2({
        placeholder: 'Pilih Status...',
        dropdownParent: $('#myModal'),
        allowClear: true,
        ajax: {
            url: "/api/master/status/search",
            contentType: "application/json; charset=utf-8",
            data: function (params) {
                var query = {
                    term: params.term,
                };
                return query;
            },
            processResults: function (result) {
                return {
                    results: $.map(result, function (item) {
                        return {
                            text: item.data,
                            id: item.id
                        }
                    })
                }
            },
            cache: true
        }
    });
}

function PopulateTypes() {
    $('.sClientType').select2({
        placeholder: 'Pilih Jenis...',
        dropdownParent: $('#myModal'),
        allowClear: true,
        ajax: {
            url: "/api/master/clients/type/search",
            contentType: "application/json; charset=utf-8",
            data: function (params) {
                var query = {
                    term: params.term,
                };
                return query;
            },
            processResults: function (result) {
                return {
                    results: $.map(result, function (item) {
                        return {
                            text: item.data,
                            id: item.id
                        }
                    })
                }
            },
            cache: true
        }
    });
}