var productTable;

$(document).ready(function () {
    productTable();
})

productTable = $('#tableData').DataTable({
    ajax: '/Admin/Product/getall',
    columns: [
        { data: 'title' },
        { data: 'isbn' },
        { data: 'price', "render": (data) => '$' + data.toFixed(2) },
        { data: 'author' },
        { data: 'category.name' },
        {
            data: 'id', "render": (data) => {
                return `
                     <div class="d-flex justify-content-end gap-3">
                        <a href="product/upsert?id=${data}" class="btn btn-outline-success">
                            <i class="bi bi-pencil-square"></i> Edit
                        </a>
                        <a onclick="Delete('product/delete/${data}')" class="btn btn-outline-danger">
                            <i class="bi bi-trash-square"></i> Delete
                        </a>
                     </div>
                `;
            }
        }
    ]
});

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    productTable.ajax.reload();
                    Swal.fire({
                        title: "Deleted!",
                        text: "Your file has been deleted.",
                        icon: "success"
                    });
                }
            })
        }
    });
}