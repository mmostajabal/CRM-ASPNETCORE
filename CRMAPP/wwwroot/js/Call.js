var callDataTabble;
$(document).ready(function () {
    if ($("#CustomerList").val() != "" && $("#CustomerList").val() != null){
        LoadCallDataTable();
    }
});
$(document).ready(function () {
    //$("#createBtn").prop('disabled', ($("#CustomerList").val() == null ? true : false));
    showHideBtn("createBtn", ($("#CustomerList").val() == null ? true : false))
});
function LoadCallDataTable() {
    
    //$("#createBtn").attr("href", $("#createBtn").attr("href") +"?customerNo=" + $("#CustomerList").val());
    
    
    callDataTabble = $('#callTable').DataTable({
        ajax: {
            url: '/Employee/Call/GetAll',
            data: { custno: $("#CustomerList").val(), status : 1}
        },
        destroy: true,
        responsive: true,
        columns: [
            {
                data: null,
                render: function (data, type, row) {

                    return `<div  role="group">
                            <a class="btn btn-primary circle-button mx-2" data-bs-target="#CallsModal" onclick="createBtn_click(${row.id});"  title='${titleUpdatebtn}''><i class="fas fa-edit"></i></a>
                            <a Onclick ="DeleteCall('/Employee/Call/Delete', '${row.id}')" class="btn btn-danger circle-button mx-2"  title="${titleDeletebtn}"><i class="fas fa-trash"></i></a>
                            </div>`
                }, "width": "1%"
            },
            { data: 'subject', "width": "30%", "class": "text-center" },
            { data: 'description', "width": "40%" },
            { data: 'callDate', "width": "10%" },
            { data: 'callTime', "width": "10%" },
        ]
    });

    showHideBtn("createBtn", ($("#CustomerList").val() == null ? true : false))
}
/**
 *
 * @param {any} id
 */
function createBtn_click(id) {
    $(".modal-body").load("/Employee/Call/Create?id=" + id.toString());
    $("#CallsModal").modal("show");
}
/*
*@param {any} objId
@param {any} status
*/
function showHideBtn(objId, status) {
    $("#" + objId).prop('disabled', status);
}

function DeleteCall(url, id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                data: {
                    id: id
                },
                type: 'DELETE',
                success: function (data) {
                    if (data) {
                        Swal.fire(
                            'Deleted!',
                            'Call has been deleted.',
                            'successfully'
                        )
                        callDataTabble.ajax.reload();
                        //tostar.success("Successfully Deleted!!!")
                    } else {
                        Swal.fire(
                            'There is an Issue, It is not Deleted!!!'
                        )
                        //tostar.success("There is an Issue, It is not Deleted!!!")
                    }
                }
            });
        }
    })

}
