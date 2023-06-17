var rfid = '';

$(document).ready(function () {
    $('#berat').text(curValue);
    loadKeluar();
});

$(document).bind('keypress', function (e) {
    var nilai = e.which;

    GetValue(nilai);

    if (e.which == 13) {
        if (rfid.length > 10) {
            rfid = rfid.substr(rfid.length - 10);
        }

        if (rfid.length == 10) {
            SaveTimbanganMasuk();
            // alert(rfid);
        } else {
            alert('RFID tidak sesuai');
        }

        rfid = '';
    }
});


function GetValue(value) {
    switch (value) {
        case 48:
            rfid += 0;
            break;
        case 49:
            rfid += 1;
            break;
        case 50:
            rfid += 2;
            break;
        case 51:
            rfid += 3;
            break;
        case 52:
            rfid += 4;
            break;
        case 53:
            rfid += 5;
            break;
        case 54:
            rfid += 6;
            break;
        case 55:
            rfid += 7;
            break;
        case 56:
            rfid += 8;
            break;
        case 57:
            rfid += 9;
            break;
    }
}

function SaveTimbanganMasuk() {
    $.ajax({
        url: '/transaction/keluar/store',
        type: 'POST',
        data: {
            noRFID: rfid,
            berat: curValue
        },
        success: function (result) {
            if (result.success == 'yes') {
                const bb = document.getElementById('WeightBefore');
                const db = document.getElementById('DoorBefore');
                const ib = document.getElementById('TruckIDBefore');

                loadKeluar();

                bb.textContent = result.WeightBefore;
                db.textContent = result.DoorBefore;
                ib.textContent = result.TruckBefore;
            } else {
                alert('RFID Tidak Terdaftar');
            }
        }
    });
}

function loadKeluar() {
    $('#tableTransaksi').DataTable().destroy();
    $('#tableTransaksi').DataTable({
        serverSide: true,
        processing: true,
        responsive: true,
        stateSave: true,
        lengthMenu: [10, 20, 50],
        ajax: {
            url: '/api/transaksi/keluar',
            method: 'POST'
        },
        columns: [
            { data: 'transactionID', name: 'transactionID', autowidth: true },
            { data: 'beratMasuk', name: 'beratMasuk', autowidth: true },
            { data: 'beratKeluar', name: 'beratKeluar', autowidth: true },
            { data: 'tglMasuk', name: 'tglMasuk', autowidth: true },
            { data: 'tglKeluar', name: 'tglKeluar', autowidth: true },
            { data: 'noPintu', name: 'noPintu', autowidth: true },
            { data: 'noPolisi', name: 'noPolisi', autowidth: true },
            { data: 'nett', name: 'nett', autowidth: true, searchable: false, orderable: false },
            {
                data: 'transactionID',
                render: function (data, type, row) {
                    return `<button class='btn btn-sm btn-success mr-2 btnPrint' style='width:100%;' data-href='/transaction/print-ulang/?id=`
                        + row.transactionID + `'><i class='fa fa-edit'></i> Print</button>`;
                }
            }
        ],
        order: [[1, "desc"]]
    });
}

$(document).on('click', '.btnPrint', function () {
    var thisUrl = $(this).attr('data-href');    
    thisUrl += '&pos=' + pos;

    $.ajax({
        url: thisUrl,
        method: 'GET'
    });

});