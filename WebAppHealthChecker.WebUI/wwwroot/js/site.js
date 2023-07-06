$(document).on('click', '[data-modal-url]', function () {
    var url = $(this).data('modal-url');
    var title = $(this).data('modal-title');

    // AJAX request
    $.ajax({
        url: url,
        type: 'get',
        success: function (response) {
            $('#tempModal .modal-title').html(title);
            $('#tempModal .modal-body').html(response);

            // Display Modal
            $('#tempModal').modal('show');
        }
    });
});