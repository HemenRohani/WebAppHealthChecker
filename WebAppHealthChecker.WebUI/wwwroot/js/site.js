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

$(document).on("click", "[data-submit='ajax']", function (evt) {

    (window.event || evt).preventDefault();

    var frm = $((window.event || evt).target).closest("form");

    var callback = $(this).data('callback') || $(this).parents('[data-submit=ajax]').data('callback');
    $.post(frm.attr("action"), frm.serialize(),
        function (data) {
            if (typeof (callback) != "undefined") {
                window[callback](data);
            }

        })
        .fail(function (x, e, s) {
            alert(x.responseText);
        })
        .always(function () {
            
        });
});