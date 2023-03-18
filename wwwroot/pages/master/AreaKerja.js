$(document).ready(function () {
    loadTable();
});

$(document).on('select2:open', () => {
    document.querySelector('.select2-search__field').focus();
});

$(document).on('shown.bs.modal', '#myModal', function () {
    PopulatePenugasan();
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
            url: '/api/master/area-kerja',
            method: 'POST'
        },
        columns: [
            { data: 'areaKerjaID', name: 'areaKerjaID', autowidth: true },
            { data: 'namaArea', name: 'namaArea', autowidth: true },
            { data: 'namaPenugasan', name: 'namaPenugasan', autowidth: true },
            {
                data: 'areaKerjaID',
                render: function (data, type, row) {
                    return "<button class='btn btn-sm btn-success mr-2 showMe' style='width:100%;' data-href='/master/area-kerja/edit/?id="
                        + row.areaKerjaID + "'><i class='fa fa-edit'></i> Edit</button>";
                }
            }
        ],
        order: [[0, "desc"]]
    });
}

function PopulatePenugasan() {
    $('.sPenugasan').select2({
        placeholder: 'Pilih Penugasan...',
        dropdownParent: $('#myModal'),
        allowClear: true,
        ajax: {
            url: "/api/master/penugasan/search",
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