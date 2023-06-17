var rfid = '';

$(document).ready(function () {
    $('#berat').text(curValue);
    loadMasuk();
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
        url: '/transaction/masuk/store',
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

                loadMasuk();

                bb.textContent = result.WeightBefore;
                db.textContent = result.DoorBefore;
                ib.textContent = result.TruckBefore;
            } else if (result.doubleTap) {

            } else if (result.blocked) {
                blockedMessage();
            } else if (result.retribusi) {
                retriMessage();
            } else {
                rfidMessage();
            }
        }
    });
}

function loadMasuk() {
    $('#tableTransaksi').DataTable().destroy();
    $('#tableTransaksi').DataTable({
        serverSide: true,
        responsive: true,
        stateSave: true,        
        lengthMenu: [10, 20, 50],
        ajax: {
            url: '/api/transaksi/masuk',
            method: 'POST'
        },
        columns: [
            { data: 'transactionID', name: 'transactionID', autowidth: true },
            { data: 'tglMasuk', name: 'tglMasuk', autowidth: true, searchable: false, orderable: false },
            { data: 'noPintu', name: 'noPintu', autowidth: true, searchable: false, orderable: false },
            { data: 'noPolisi', name: 'noPolisi', autowidth: true, searchable: false, orderable: false },            
            { data: 'beratMasuk', name: 'beratMasuk', autowidth: true, searchable: false, orderable: false }            
        ],
        order: [[0, "desc"]]
    });
}

function blockedMessage() {
    Swal.fire(
    {
        type: 'warning',
        title: 'Truk diblokir!',
        showConfirmButton: true
    });
}

function retriMessage() {
    Swal.fire(
    {
        type: 'warning',
        title: 'Belum bayar retribusi!',
        showConfirmButton: true
    });
}
function rfidMessage() {
    Swal.fire(
        {
            type: 'warning',
            title: 'RFID tidak terdaftar!',
            showConfirmButton: true
        });
}