var customerDataTabble;
$(document).ready(function () {
    LoadDataTable();
});
function LoadDataTable() {
    try {
        customerDataTabble = $('#customerTable').DataTable({
            ajax: { url: '/Admin/Customer/GetAll' },
            responsive: true,
            columns: [
                {
                    data: null,
                    render: function (data, type, row) {

                        return `<div  role="group">
                            <a class="btn btn-primary circle-button mx-2"  data-bs-target="#createModal" onclick="createBtn_click(${row.customerNo});" title="${titleUpdatebtn}"><i class="fas fa-edit"></i></a>
                            <a Onclick ="DeleteCustomer('/Admin/Customer/Delete', '${row.customerNo}')" class="btn btn-danger circle-button mx-2" title="${titleDeletebtn}"><i class="fas fa-trash"></i></a>
                            </div>`
                    }, "width": "8%"
                },
                { data: 'customerNo', "width": "10%", "class": "text-center" },
                { data: 'customerName', "width": "15%" },
                { data: 'customerSurName', "width": "18%" },
                { data: 'address', "width": "19%" },
                { data: 'postCode', "width": "8%", "class": "text-center" },
                { data: 'country', "width": "12%" },
                { data: 'dateOfBirth', "width": "10%", "class": "text-center" }
            ]
        });
    }
    catch (ex) {
        alert(ex.toString());
    }
}


function DeleteCustomer(url, customerNo) {
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
                    customerNo: customerNo
                },
                type: 'DELETE',
                success: function (data) {      
                    if (data) {                        
                        Swal.fire(
                            'Deleted!',
                            'Customer has been deleted.',
                            'successfully'
                        )                    
                        customerDataTabble.ajax.reload();                        
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

function createBtn_click(customerNo) {
    $(".modal-body").load("/Admin/Customer/Create?customerNo=" + customerNo.toString());
    $("#CustomerModal").modal("show");
}
