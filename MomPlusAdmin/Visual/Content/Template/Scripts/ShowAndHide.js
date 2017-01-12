$(document).ready(function () {
    $("#editBtn").click(function () {
        $('#r_fname').attr("readonly", false)
        $('#r_lname').attr("readonly", false)
        $('#r_address').attr("readonly", false)
        $('#r_cnum').attr("readonly", false)
        $('#updateBtn').show();
        $('#cancelBtn').show();
        $('#editBtn').hide();
        $('#addTTR').show();
    });
});

$(document).ready(function () {
    $("#cancelBtn").click(function () {
        $('#r_fname').attr("readonly", true)
        $('#r_lname').attr("readonly", true)
        $('#r_address').attr("readonly", true)
        $('#r_cnum').attr("readonly", true)
        $('#updateBtn').hide();
        $('#cancelBtn').hide();
        $('#editBtn').show();

    });
});


$(document).ready(function () {
    $("#prenatalBtn").click(function () {
        $('#prenatalDiv').show();
        $('#postpartumDiv').hide();
        $('#motherInfo').hide();
        $('#babyList').hide();
    });
});

$(document).ready(function () {
    $("#postpartumBtn").click(function () {
        $('#postpartumDiv').show();
        $('#prenatalDiv').hide();
    });
});

$(document).ready(function () {
    $("#LMPid").click(function () {
        $('#en_lmp').attr("readonly", false)
        $("#checkID").show();
        $("#cancelID").show();
        $("#LMPid").hide();

        $('input[type="image"][name="submit"][alt]').replaceWith('<input type="submit" name="submit">');
    });


});

$(document).ready(function () {
    $("#cancelID").click(function () {
        $('#en_lmp').attr("readonly", true)
        $("#checkID").hide();
        $("#cancelID").hide();
        $("#LMPid").show();
    });
});
