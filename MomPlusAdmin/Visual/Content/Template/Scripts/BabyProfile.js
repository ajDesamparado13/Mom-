$(document).ready(function () {
    $("#editBtn").click(function () {
        $('#firstname').attr("readonly", false)
        $('#lastname').attr("readonly", false)
        $('#weight').attr("readonly", false)
        $('#height').attr("readonly", false)
        $('#updateBtn').show();
        $('#cancelBtn').show();
        $('#editBtn').hide();
    });
});

$(document).ready(function () {
    $("#cancelBtn").click(function () {
        $('#firstname').attr("readonly", true)
        $('#lastname').attr("readonly", true)
        $('#weight').attr("readonly", true)
        $('#height').attr("readonly", true)
        $('#updateBtn').hide();
        $('#cancelBtn').hide();
        $('#editBtn').show();

    });
});
