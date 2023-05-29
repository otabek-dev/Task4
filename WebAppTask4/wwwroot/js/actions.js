$(document).ready(function () {
    let selectedRowData = [];

    $('#selectAll').change(function () {
        $('.userCheckbox').prop('checked', $(this).prop('checked'));
    });

    $('.userCheckbox').change(function () {
        if ($("input.userCheckbox:checked").length == $("input.userCheckbox").length) {
            $('#selectAll').attr('checked', 'checked');
        }
        else {
            $('#selectAll').removeAttr('checked');
        }
    });

    $('#deleteButton').click(function () {

        $('.row-select input:checked').each(function () {
            let id;
            id = $(this).closest('tr').find('.Id').html();
            selectedRowData.push(id);
        });

        $.ajax({
            url: '/api/users',
            type: 'DELETE',
            data: JSON.stringify(selectedRowData),
            contentType: 'application/json',
            success: function (response) {
                location.reload();
                console.log(response);
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            }
        });

        console.log(JSON.stringify(selectedRowData));
        selectedRowData = [];
    });
});