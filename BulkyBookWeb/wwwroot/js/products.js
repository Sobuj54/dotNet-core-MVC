$('#tableData').DataTable({
    ajax: '/Customer/Product/getall',
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
                        <a href="/customer/product/edit?id=${data}" class="btn btn-outline-success">
                            <i class="bi bi-pencil-square"></i> Edit
                        </a>
                        <a href="/customer/product/edit?id=${data}" class="btn btn-outline-danger">
                            <i class="bi bi-trash-square"></i> Delete
                        </a>
                     </div>
                `;
            }
        }
    ]
});