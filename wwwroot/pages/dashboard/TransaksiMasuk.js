var rfid = '';

$(document).bind('keydown', function (e) {
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
            berat: $('#berat').text()
        },
        success: function (result) {
            if (result.success) {
                alert('Sukses input data');
            } else {
                alert('Gagal input data');
            }
        }
    })
}