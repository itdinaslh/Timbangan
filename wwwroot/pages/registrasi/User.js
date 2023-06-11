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
            url: '/api/registrasi/users',
            method: 'POST'
        },
        columns: [
            { data: 'id', name: 'id', autowidth: true },
            { data: 'userName', name: 'userName', autowidth: true },
            { data: 'fullName', name: 'userName', autowidth: true },
            { data: 'email', name: 'email', autowidth: true },
            {
                data: 'id',
                render: function (data, type, row) {
                    return `<button class='btn btn-sm btn-success mr-2 showMe' style='width:30%;' data-href='/registrasi/users/edit/?id=`
                        + row.id + `'><i class='fa fa-edit'></i> Edit</button>
                        <a href='/registrasi/manage/roles?id=` + row.id + `' class='btn btn-sm btn-primary mr-2' style='width:30%;'>Roles</a>
                        <button class='btn btn-sm btn-warning showMe' style='width:37%;' data-href='/user/password/change/?user=`
                        + row.id + `'>Change Password</button>`;
                }
            }
        ],
        order: [[0, "desc"]]
    });
}