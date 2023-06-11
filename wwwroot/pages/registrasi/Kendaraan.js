$(document).ready(function () {
    loadTable();
});

$(document).on('select2:open', () => {
    document.querySelector('.select2-search__field').focus();
});

$(document).on('shown.bs.modal', '#myModal', function () {
    PopulatePenugasan();
    PopulateClient();
    PopulateTipe();
    PopulateRoda();
    $('.sArea').select2({
        placeholder: 'Pilih Area Kerja...',
        dropdownParent: $('#myModal')
    });
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
            url: '/api/registrasi/kendaraan',
            method: 'POST'
        },
        columns: [            
            { data: 'noPolisi', name: 'noPolisi', autowidth: true },
            { data: 'noPintu', name: 'noPintu', autowidth: true },
            { data: 'clientName', name: 'clientName', autowidth: true },
            { data: 'createdAt', name: 'createdAt', autowidth: true },
            { data: 'beratKIR', name: 'beratKIR', autowidth: true },
            { data: 'statusName', name: 'statusName', autowidth: true },
            {
                data: 'kendaraanID',
                render: function (data, type, row) {
                    return "<button class='btn btn-sm btn-success mr-2 showMe' style='width:100%;' data-href='/registrasi/kendaraan/edit/?id="
                        + row.kendaraanID + "'><i class='ri-edit-line'></i></button>";
                }
            }
        ],
        columnDefs: [
            { className: 'text-center', targets: [3,4, 5, 6] }            
        ],
        order: [[3, "desc"]]
    });
}

function PopulateClient() {
    $('.sClient').select2({
        placeholder: 'Pilih Ekspenditur...',
        dropdownParent: $('#myModal'),
        allowClear: true,
        ajax: {
            url: "/api/registrasi/clients/search",
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
    }).on('change', function () {
        $('.sArea').val(null).trigger('change');
        var theID = $('.sPenugasan option:selected').val();
        PopulateArea(theID);
        $('.sArea').select2('focus');
    });
}

$(document).on('change', '.sPenugasan', function () {
    var id = $('.sPenugasan option:selected').val();

    alert(id);
});

function PopulateTipe() {
    $('.sTipe').select2({
        placeholder: 'Pilih Tipe...',
        dropdownParent: $('#myModal'),
        allowClear: true,
        ajax: {
            url: "/api/master/tipe-kendaraan/search",
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

function PopulateRoda() {
    $('.sRoda').select2({
        placeholder: 'Pilih Jumlah Roda...',
        dropdownParent: $('#myModal'),
        allowClear: true,
        ajax: {
            url: "/api/master/roda/search",
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

function PopulateArea(tugas) {
    $('.sArea').select2({
        placeholder: 'Pilih Area Kerja...',
        dropdownParent: $('#myModal'),
        allowClear: true,
        ajax: {
            url: "/api/master/area-kerja/search?tugas=" + tugas,
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
